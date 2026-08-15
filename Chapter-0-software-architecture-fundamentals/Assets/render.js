// Renders the .excalidraw file to one hand-drawn SVG per diagram, using the
// same rough.js engine Excalidraw draws with.
const fs = require('fs');
const rough = require('roughjs');

const SRC = '/Users/kamilbaczek/RiderProjects/evolutionary-architecture-by-example/' +
  'Chapter-0-software-architecture-fundamentals/Assets/fundamentals.excalidraw';
const OUT = process.argv[2];

// name + y band of every diagram in the file
const BANDS = [
  ['fundamentals', 0, 880],
  ['cost_of_change', 900, 1740],
  ['coupling_cohesion', 1750, 2690],
  ['two_traps', 2700, 3540],
  ['architect_elevator', 3550, 4440],
  ['risk_grid', 4450, 5340],
  ['quality_attributes_iceberg', 5350, 6340],
  ['decision_loop', 6350, 7200],
];

const elements = JSON.parse(fs.readFileSync(SRC, 'utf8')).elements;
const gen = rough.generator({}, { width: 4000, height: 9000 });

function esc(s) {
  return s.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}

function opsToPath(drawing) {
  // rough.js gives us drawing sets; turn each into an <path>
  const out = [];
  for (const set of drawing.sets) {
    let d = '';
    for (const op of set.ops) {
      const p = op.data;
      if (op.op === 'move') d += `M${p[0]} ${p[1]} `;
      else if (op.op === 'bcurveTo') d += `C${p[0]} ${p[1]}, ${p[2]} ${p[3]}, ${p[4]} ${p[5]} `;
      else if (op.op === 'lineTo') d += `L${p[0]} ${p[1]} `;
    }
    out.push({ d: d.trim(), set });
  }
  return out;
}

function drawShape(drawing, stroke, strokeWidth, fill, dashed, opacity) {
  let svg = '';
  for (const { d, set } of opsToPath(drawing)) {
    if (!d) continue;
    if (set.type === 'fillPath' || set.type === 'fillSketch') {
      svg += `<path d="${d}" fill="${fill}" stroke="none" opacity="${opacity}"/>`;
    } else {
      const dash = dashed ? ' stroke-dasharray="9 7"' : '';
      svg += `<path d="${d}" fill="none" stroke="${stroke}" stroke-width="${strokeWidth}"` +
        ` stroke-linecap="round" stroke-linejoin="round" opacity="${opacity}"${dash}/>`;
    }
  }
  return svg;
}

function roundedRect(x, y, w, h, r) {
  // Excalidraw's adaptive corner radius
  r = r ?? Math.min(Math.min(w, h) * 0.25, 32);
  return `M${x + r} ${y} L${x + w - r} ${y} Q${x + w} ${y} ${x + w} ${y + r} ` +
    `L${x + w} ${y + h - r} Q${x + w} ${y + h} ${x + w - r} ${y + h} ` +
    `L${x + r} ${y + h} Q${x} ${y + h} ${x} ${y + h - r} ` +
    `L${x} ${y + r} Q${x} ${y} ${x + r} ${y} Z`;
}

function arrowHead(x1, y1, x2, y2, color, w, op) {
  const a = Math.atan2(y2 - y1, x2 - x1);
  const len = 18, spread = 0.42;
  const p1 = [x2 - len * Math.cos(a - spread), y2 - len * Math.sin(a - spread)];
  const p2 = [x2 - len * Math.cos(a + spread), y2 - len * Math.sin(a + spread)];
  return `<path d="M${p1[0]} ${p1[1]} L${x2} ${y2} L${p2[0]} ${p2[1]}" fill="none" ` +
    `stroke="${color}" stroke-width="${w}" stroke-linecap="round" opacity="${op}"/>`;
}

function render(name, y0, y1) {
  const els = elements.filter(e => e.y >= y0 - 60 && e.y <= y1);
  if (!els.length) return;
  // skip the gray helper title above each diagram
  const body = els.filter(e => !(e.type === 'text' && e.strokeColor === '#868e96'));

  let minX = 1e9, minY = 1e9, maxX = -1e9, maxY = -1e9;
  for (const e of body) {
    minX = Math.min(minX, e.x); minY = Math.min(minY, e.y);
    maxX = Math.max(maxX, e.x + e.width); maxY = Math.max(maxY, e.y + e.height);
    if (e.points) for (const p of e.points) {
      minX = Math.min(minX, e.x + p[0]); maxX = Math.max(maxX, e.x + p[0]);
      minY = Math.min(minY, e.y + p[1]); maxY = Math.max(maxY, e.y + p[1]);
    }
  }
  const pad = 30;
  const w = maxX - minX + pad * 2, h = maxY - minY + pad * 2;

  let svg = '';
  for (const e of body) {
    const op = (e.opacity ?? 100) / 100;
    const fill = e.backgroundColor && e.backgroundColor !== 'transparent' ? e.backgroundColor : null;
    const opts = {
      seed: (e.seed % 2147483647) || 1,
      roughness: 1.2,
      bowing: 1,
      stroke: e.strokeColor,
      strokeWidth: e.strokeWidth || 2,
      fill: fill || undefined,
      fillStyle: 'solid',
    };
    if (e.type === 'rectangle') {
      const shape = e.roundness
        ? gen.path(roundedRect(e.x, e.y, e.width, e.height), opts)
        : gen.rectangle(e.x, e.y, e.width, e.height, opts);
      svg += drawShape(shape,
        e.strokeColor, e.strokeWidth, fill, e.strokeStyle === 'dashed', op);
    } else if (e.type === 'ellipse') {
      svg += drawShape(gen.ellipse(e.x + e.width / 2, e.y + e.height / 2, e.width, e.height, opts),
        e.strokeColor, e.strokeWidth, fill, e.strokeStyle === 'dashed', op);
    } else if (e.type === 'line' || e.type === 'arrow') {
      const pts = e.points.map(p => [e.x + p[0], e.y + p[1]]);
      const closed = e.type === 'line' && fill &&
        pts.length > 2 && pts[0][0] === pts[pts.length - 1][0] && pts[0][1] === pts[pts.length - 1][1];
      const drawing = closed ? gen.polygon(pts, opts) : gen.linearPath(pts, opts);
      svg += drawShape(drawing, e.strokeColor, e.strokeWidth, fill,
        e.strokeStyle === 'dashed', op);
      if (e.type === 'arrow' && e.endArrowhead) {
        const n = pts.length;
        svg += arrowHead(pts[n - 2][0], pts[n - 2][1], pts[n - 1][0], pts[n - 1][1],
          e.strokeColor, e.strokeWidth, op);
      }
      if (e.type === 'arrow' && e.startArrowhead) {
        svg += arrowHead(pts[1][0], pts[1][1], pts[0][0], pts[0][1],
          e.strokeColor, e.strokeWidth, op);
      }
    } else if (e.type === 'text') {
      const fs_ = e.fontSize, lines = e.text.split('\n');
      const cx = e.x + e.width / 2;
      lines.forEach((line, i) => {
        svg += `<text x="${cx}" y="${e.y + fs_ * (0.95 + 1.25 * i)}" font-size="${fs_}" ` +
          `font-family="Comic Sans MS, Chalkboard, cursive" fill="${e.strokeColor}" ` +
          `text-anchor="middle" opacity="${op}">${esc(line)}</text>`;
      });
    }
  }

  // square canvas so the renderer cannot distort the aspect ratio;
  // the extra white is trimmed off after rasterising
  const side = Math.max(w, h);
  const vx = minX - pad - (side - w) / 2, vy = minY - pad - (side - h) / 2;
  const doc = `<svg xmlns="http://www.w3.org/2000/svg" width="${side}" height="${side}" ` +
    `viewBox="${vx} ${vy} ${side} ${side}">` +
    `<rect x="${vx}" y="${vy}" width="${side}" height="${side}" fill="#ffffff"/>` +
    svg + '</svg>';
  fs.writeFileSync(`${OUT}/${name}.svg`, doc);
  console.log(name, Math.round(w) + 'x' + Math.round(h));
}

for (const [name, a, b] of BANDS) render(name, a, b);
