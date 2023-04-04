namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.ReportGenerator;

using Dtos;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

internal sealed class NewPassesPerMonthReportDataPdfReportGenerator : INewPassesPerMonthReportDataPdfReportGenerator
{
    const int columnCount = 4;

    public async Task<FileStream> GeneratePdfReportAsync(string name, IReadOnlyCollection<NewPassesPerMonthDto> report, CancellationToken cancellationToken)
    {
        var fileName = $"{name}.pdf";
        await using var file = new FileStream(fileName, FileMode.Create);
        await using var writer = new PdfWriter(file);
        var pdf = new PdfDocument(writer);
        using var document = new Document(pdf);
        var table = new Table(columnCount);
        AddTableHeader(table);
        AddTableData(table, report);
        document.Add(table);

        return file;
    }

    private static void AddTableHeader(Table table)
    {
        table.AddHeaderCell(nameof(NewPassesPerMonthDto.NewPasses));
        table.AddHeaderCell(nameof(NewPassesPerMonthDto.Month));
    }

    private static void AddTableData(Table table, IEnumerable<NewPassesPerMonthDto> passes)
    {
        foreach (var pass in passes)
        {
            table.AddCell(pass.NewPasses.ToString());
            table.AddCell(pass.Month);
        }
    }
}