"""Builds every Chapter 0 diagram into one .excalidraw file.

Style follows the existing repo assets: black rough strokes, pastel fills,
violet (#cc5de8) for arrows and annotations, hand-drawn font.
"""
import json, os, random, time

random.seed(23)
NOW = int(time.time() * 1000)

BLACK = "#1e1e1e"
VIOLET = "#cc5de8"
GRAY = "#868e96"
TEAL = "#12b886"
ORANGE_S = "#f08c00"
GREEN = "#b2f2bb"
BLUE = "#a5d8ff"
ORANGE = "#ffd8a8"
PURPLE = "#d0bfff"
YELLOW = "#ffec99"
RED = "#ffc9c9"
TEALF = "#c3fae8"
HAND = 1  # hand-drawn font, valid in every Excalidraw version

elements = []


def base(el_id, el_type, x, y, w, h):
    return {
        "id": el_id, "type": el_type,
        "x": round(x, 2), "y": round(y, 2),
        "width": round(w, 2), "height": round(h, 2),
        "angle": 0,
        "strokeColor": BLACK, "backgroundColor": "transparent",
        "fillStyle": "solid", "strokeWidth": 2, "strokeStyle": "solid",
        "roughness": 1, "opacity": 100,
        "groupIds": [], "frameId": None, "roundness": None,
        "seed": random.randint(1, 2 ** 31),
        "version": 2, "versionNonce": random.randint(1, 2 ** 31),
        "isDeleted": False, "boundElements": None, "updated": NOW,
        "link": None, "locked": False,
    }


_n = [0]


def uid(prefix):
    _n[0] += 1
    return "%s%d" % (prefix, _n[0])


def text(cx, cy, content, size=18, color=BLACK, align="center"):
    lines = content.split("\n")
    w = max(len(l) for l in lines) * size * 0.6
    h = len(lines) * size * 1.25
    x = cx - w / 2 if align == "center" else cx
    y = cy - h / 2 if align == "center" else cy
    el = base(uid("t"), "text", x, y, w, h)
    el.update({
        "strokeColor": color, "fontSize": size, "fontFamily": HAND,
        "text": content, "originalText": content,
        "textAlign": "center", "verticalAlign": "top",
        "containerId": None, "lineHeight": 1.25,
    })
    elements.append(el)


def rect(x, y, w, h, fill="transparent", label=None, size=18,
         dashed=False, stroke=BLACK, opacity=100, label_color=BLACK):
    el = base(uid("r"), "rectangle", x, y, w, h)
    el["backgroundColor"] = fill
    el["strokeColor"] = stroke
    el["roundness"] = {"type": 3}
    el["opacity"] = opacity
    if dashed:
        el["strokeStyle"] = "dashed"
    elements.append(el)
    if label:
        text(x + w / 2, y + h / 2, label, size, label_color)


def ellipse(x, y, w, h, fill="transparent", label=None, size=18, stroke=BLACK):
    el = base(uid("e"), "ellipse", x, y, w, h)
    el["backgroundColor"] = fill
    el["strokeColor"] = stroke
    elements.append(el)
    if label:
        text(x + w / 2, y + h / 2, label, size)


def poly(x, y, points, fill="transparent", stroke=BLACK, dashed=False,
         opacity=100):
    xs = [p[0] for p in points]
    ys = [p[1] for p in points]
    el = base(uid("l"), "line", x, y, max(xs) - min(xs), max(ys) - min(ys))
    el.update({
        "strokeColor": stroke, "backgroundColor": fill,
        "points": [[round(a, 2), round(b, 2)] for a, b in points],
        "lastCommittedPoint": None, "startBinding": None, "endBinding": None,
        "startArrowhead": None, "endArrowhead": None,
    })
    if dashed:
        el["strokeStyle"] = "dashed"
    el["opacity"] = opacity
    elements.append(el)


def arrow(x, y, points, color=VIOLET, start_head=None, end_head="arrow"):
    xs = [p[0] for p in points]
    ys = [p[1] for p in points]
    el = base(uid("a"), "arrow", x, y, max(xs) - min(xs), max(ys) - min(ys))
    el.update({
        "strokeColor": color,
        "points": [[round(a, 2), round(b, 2)] for a, b in points],
        "lastCommittedPoint": None, "startBinding": None, "endBinding": None,
        "startArrowhead": start_head, "endArrowhead": end_head,
    })
    elements.append(el)


def actor(cx, y, h=70, color=BLACK):
    """Stick figure: head, body, arms, legs."""
    r = h * 0.22
    ellipse(cx - r, y, 2 * r, 2 * r, "#ffffff", stroke=color)
    poly(cx, y + 2 * r, [[0, 0], [0, h * 0.42]], stroke=color)
    poly(cx - h * 0.26, y + h * 0.52, [[0, 0], [h * 0.52, 0]], stroke=color)
    poly(cx, y + h * 0.86, [[0, 0], [-h * 0.22, h * 0.28]], stroke=color)
    poly(cx, y + h * 0.86, [[0, 0], [h * 0.22, h * 0.28]], stroke=color)


def title(y, label):
    text(60, y, label, 28, GRAY, align="left")


# =====================================================================
# 1. The building - fundamentals as foundation, chapters as floors
# =====================================================================
def building(b):
    title(b, "1. Fundamentals - the foundation (What this chapter is for)")
    poly(50, b + 180, [[0, 0], [590, -100], [1180, 0]])
    for y, fill, label in [
        (b + 180, TEALF, "Chapter 4 - Applying tactical Domain-Driven Design"),
        (b + 275, PURPLE, "Chapter 3 - Microservice extraction"),
        (b + 370, ORANGE, "Chapter 2 - Modules separation"),
        (b + 465, GREEN, "Chapter 1 - Initial architecture"),
    ]:
        rect(80, y, 1120, 80, fill, label, 22)
    rect(60, b + 555, 1160, 250, dashed=True, stroke=VIOLET)
    text(80, b + 565, "Chapter 0 - the fundamentals", 18, VIOLET, align="left")
    pillars = ["Architect\nRole", "Architecture\nLaws", "Trade-offs",
               "Coupling", "Cohesion", "Risk", "ADRs", "Diagrams\n(C4)",
               "Fitness\nFunctions"]
    for i, label in enumerate(pillars):
        rect(80 + i * 126, b + 595, 110, 200, BLUE, label, 15)
    poly(40, b + 795, [[0, 0], [1200, 0]])
    text(640, b + 840, "Every decision in the chapters above stands on these",
         18, VIOLET)


# =====================================================================
# 2. Cost of change - design versus architecture
# =====================================================================
def cost_of_change(b):
    title(b, "2. Cost of change (What architecture actually is)")
    text(240, b + 150, "Design", 24, VIOLET)
    rect(90, b + 195, 300, 70, GREEN, "Upgrade a library", 18)
    rect(90, b + 285, 300, 90, GREEN, "Switch expression\ninstead of switch", 18)
    text(240, b + 400, "reversible in an afternoon", 18, VIOLET)
    rect(455, b + 260, 290, 100, YELLOW,
         "What would it cost to\nchange your mind later?", 18)
    text(930, b + 150, "Architecture", 24, VIOLET)
    for i, label in enumerate(["Change the database engine",
                               "One schema per module",
                               "1 deployable, or many"]):
        rect(760, b + 180 + i * 90, 340, 70, BLUE, label, 18)
    text(930, b + 470,
         "weeks of work, every team affected,\nreversing costs what deciding did",
         18, VIOLET)
    arrow(60, b + 560, [[0, 0], [1080, 0]])
    text(70, b + 575, "cheap to reverse", 18, VIOLET, align="left")
    text(1020, b + 585, "expensive to reverse", 18, VIOLET)
    text(600, b + 615, "Cost to change your mind later", 18, VIOLET)


# =====================================================================
# 3. Coupling and cohesion
# =====================================================================
def coupling_cohesion(b):
    title(b, "3. Coupling and cohesion (Coupling and cohesion)")
    # --- coupling, high: a direct call from Contracts into Passes
    rect(60, b + 60, 520, 230, dashed=True, stroke=VIOLET)
    text(80, b + 70, "High coupling", 18, VIOLET, align="left")
    rect(90, b + 140, 190, 80, GREEN, "Orders", 18)
    rect(380, b + 140, 190, 80, BLUE, "Invoicing", 18)
    arrow(280, b + 180, [[0, 0], [100, 0]], BLACK)
    text(330, b + 118, "calls CreateInvoice()", 14, VIOLET)
    text(320, b + 250,
         "knows the module, its method,\nand that it runs right now", 16, VIOLET)
    # --- coupling, low: the same flow through an event
    rect(620, b + 60, 520, 230, dashed=True, stroke=VIOLET)
    text(640, b + 70, "Low coupling", 18, VIOLET, align="left")
    rect(645, b + 140, 165, 80, GREEN, "Orders", 18)
    rect(850, b + 145, 130, 70, YELLOW, "Order\nPlaced", 14)
    rect(1015, b + 140, 105, 80, BLUE, "Invoicing", 16)
    arrow(812, b + 180, [[0, 0], [34, 0]])
    arrow(984, b + 180, [[0, 0], [28, 0]])
    text(880, b + 250,
         "knows 1 thing:\nthe shape of the message", 16, VIOLET)
    # --- cohesion, good
    rect(60, b + 330, 520, 250, dashed=True, stroke=VIOLET)
    text(80, b + 340, "High cohesion", 18, VIOLET, align="left")
    rect(100, b + 380, 440, 130, GREEN)
    text(320, b + 400, "Orders", 20)
    for i, label in enumerate(["place order", "cancel order", "order rules"]):
        rect(120 + i * 140, b + 425, 120, 60, "#ffffff", label, 16)
    text(320, b + 535, "one drawer - open it once and everything is there",
         16, VIOLET)
    # --- cohesion, bad
    rect(620, b + 330, 520, 250, dashed=True, stroke=VIOLET)
    text(640, b + 340, "Low cohesion", 18, VIOLET, align="left")
    rect(660, b + 380, 440, 130, RED)
    text(880, b + 400, "Helpers", 20)
    for i, label in enumerate(["DateUtils", "PdfMaker", "MailSender"]):
        rect(680 + i * 140, b + 425, 120, 60, "#ffffff", label, 16)
    text(880, b + 535, "whatever had no other home - things go here to get lost",
         16, VIOLET)


# =====================================================================
# 4. When decisions get made - the two traps
# =====================================================================
def two_traps(b):
    title(b, "4. The two traps (When those decisions get made)")
    arrow(120, b + 520, [[0, 0], [0, -440]])          # y axis
    arrow(120, b + 520, [[0, 0], [760, 0]])           # x axis
    text(120, b + 55, "Complexity", 18, VIOLET)
    text(880, b + 545, "Time", 18, VIOLET)
    # what the system actually needs
    poly(140, b + 480, [[0, 0], [200, -40], [420, -140], [640, -230]],
         stroke=BLACK)
    text(900, b + 215, "what the system\nactually needs", 16, BLACK)
    # overcomplicated
    poly(140, b + 480, [[0, 0], [80, -260], [260, -330], [640, -370]],
         stroke=ORANGE_S)
    text(560, b + 95, "Overcomplicated trap", 18, ORANGE_S)
    # oversimplified
    poly(140, b + 480, [[0, 0], [300, -20], [500, -35], [640, -90]],
         stroke=TEAL)
    text(560, b + 430, "Oversimplified pitfall", 18, TEAL)
    text(620, b + 600,
         "Both are timing problems, not knowledge problems", 18, VIOLET)


# =====================================================================
# 5. The architect elevator
# =====================================================================
def elevator(b):
    title(b, "5. The architect elevator (The architect role)")
    # working building - a tall one, so the ride itself is the point
    rect(180, b + 60, 440, 100, ORANGE, "Penthouse\n(business stakeholders)", 18)
    for i in range(4):
        rect(180, b + 160 + i * 110, 440, 110, "#f1f3f5")
    rect(180, b + 600, 440, 100, BLUE, "Engineering room", 20)
    actor(245, b + 70, 60)
    text(245, b + 148, "stakeholder", 14, VIOLET)
    actor(245, b + 610, 60)
    text(245, b + 688, "engineer", 14, VIOLET)
    rect(310, b + 160, 170, 440, YELLOW, dashed=True)
    text(395, b + 180, "lift", 16, VIOLET)
    arrow(340, b + 210, [[0, 0], [0, 330]])
    arrow(450, b + 540, [[0, 0], [0, -330]])
    actor(395, b + 335, 80)
    text(395, b + 445, "architect", 14, VIOLET)
    text(400, b + 750,
         "Rides both ways: business intent down,\ntechnical constraints up",
         18, VIOLET)
    # broken building - the lift never comes back down
    rect(780, b + 60, 360, 100, ORANGE,
         "Penthouse\n(business stakeholders)", 18, opacity=40)
    for i in range(4):
        rect(780, b + 160 + i * 110, 360, 110, "#f1f3f5", opacity=40)
    rect(780, b + 600, 360, 100, BLUE, "Engineering room", 20, opacity=40)
    actor(840, b + 70, 60)
    text(840, b + 148, "stakeholder", 14, VIOLET)
    actor(840, b + 610, 60)
    text(840, b + 688, "engineer", 14, VIOLET)
    arrow(1100, b + 580, [[0, 0], [0, -390]], "#e03131")
    text(1015, b + 190, "status only, up", 14, "#e03131")
    text(960, b + 750,
         "One direction only:\nthe architect became a wall", 18, VIOLET)

# =====================================================================
# 6. Risk grid
# =====================================================================
def risk_grid(b):
    title(b, "6. Risk grid (Managing risk)")
    cells = [
        (240, b + 120, "#fff3bf", "watch it"),
        (520, b + 120, RED, "fix before you ship"),
        (240, b + 320, "#d3f9d8", "accept and move on"),
        (520, b + 320, "#fff3bf", "needs a mitigation plan"),
    ]
    for x, y, fill, label in cells:
        rect(x, y, 280, 200, fill)
        text(x + 140, y + 170, label, 16, BLACK)
    arrow(220, b + 540, [[0, 0], [0, -450]])
    arrow(220, b + 540, [[0, 0], [620, 0]])
    text(160, b + 300, "Likelihood", 18, VIOLET)
    text(560, b + 570, "Impact", 18, VIOLET)
    text(240, b + 555, "low", 16, VIOLET)
    text(790, b + 555, "high", 16, VIOLET)
    text(160, b + 110, "high", 16, VIOLET)
    rect(270, b + 150, 220, 90, YELLOW, "lost in-flight event\nduring the MVP", 16)
    rect(550, b + 150, 220, 90, YELLOW, "no load test before\nthe launch", 16)
    rect(550, b + 350, 220, 90, YELLOW, "cloud price change\nin 12 months", 16)
    text(560, b + 620,
         "One risk per sticky note, everyone in the room, on the diagram itself",
         18, VIOLET)


# =====================================================================
# 7. Quality attributes iceberg
# =====================================================================
def iceberg(b):
    title(b, "7. Quality attributes iceberg (Quality attributes)")
    poly(560, b + 100, [[0, 0], [130, 180], [-130, 180], [0, 0]], fill=YELLOW)
    text(560, b + 225, "User stories", 18, BLACK)
    arrow(140, b + 280, [[0, 0], [840, 0]], VIOLET, end_head=None)
    text(215, b + 253, "what everyone talks about", 16, VIOLET)
    text(190, b + 307, "what shapes the architecture", 16, VIOLET)
    poly(560, b + 280, [[0, 0], [300, 60], [250, 300], [-40, 380],
                        [-290, 260], [-260, 40], [0, 0]], fill=BLUE, opacity=40)
    rect(360, b + 340, 200, 60, GREEN, "Availability", 18)
    rect(600, b + 340, 200, 60, RED, "Security", 18)
    ellipse(320, b + 425, 220, 80, PURPLE, "Reliability", 18)
    ellipse(580, b + 425, 220, 80, ORANGE, "Scalability", 18)
    rect(420, b + 525, 280, 65, TEALF, "Maintainability", 18)
    text(560, b + 700,
         "Quality attributes shape the architecture -\n"
         "\"non-functional\" is the wrong name for the part that sinks you",
         18, VIOLET)


# =====================================================================
# 8. The decision loop
# =====================================================================
def decision_loop(b):
    title(b, "8. The decision loop (Putting it together)")
    row1 = ["Requirement", "Quality attribute\n+ metric", "Trade-off", "Cost"]
    row2 = ["Risk", "ADR", "Fitness function", "Trigger to revisit"]
    xs = [80, 360, 640, 920]
    for i, label in enumerate(row1):
        rect(xs[i], b + 120, 240, 90, BLUE, label, 18)
        if i < 3:
            arrow(xs[i] + 240, b + 165, [[0, 0], [40, 0]])
    arrow(1040, b + 210, [[0, 0], [0, 90]])
    for i, label in enumerate(row2):
        x = xs[3 - i]
        rect(x, b + 300, 240, 90, GREEN, label, 18)
        if i < 3:
            arrow(x, b + 345, [[0, 0], [-40, 0]])
    arrow(200, b + 300, [[0, 0], [0, -90]])
    text(600, b + 255, "requirement to metric to trade-off to cost to risk "
                       "to record to test to trigger", 16, VIOLET)
    text(600, b + 450, "the consequences arrive on a trigger you wrote down "
                       "in advance - then the loop starts again", 18, VIOLET)


sections = [
    (0, building),
    (900, cost_of_change),
    (1750, coupling_cohesion),
    (2700, two_traps),
    (3550, elevator),
    (4450, risk_grid),
    (5350, iceberg),
    (6350, decision_loop),
]
for offset, fn in sections:
    fn(offset)

doc = {
    "type": "excalidraw", "version": 2, "source": "https://excalidraw.com",
    "elements": elements,
    "appState": {"gridSize": None, "viewBackgroundColor": "#ffffff"},
    "files": {},
}

out = ("/Users/kamilbaczek/RiderProjects/evolutionary-architecture-by-example/"
       "Chapter-0-software-architecture-fundamentals/Assets/fundamentals.excalidraw")
os.makedirs(os.path.dirname(out), exist_ok=True)
with open(out, "w") as f:
    json.dump(doc, f, indent=2)
print(out)
print(len(elements), "elements,",
      sum(1 for e in elements if e["type"] == "text"), "texts")
