namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.ReportGenerator;

using Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

internal sealed class NewPassesPerMonthReportDataPdfReportGenerator : INewPassesPerMonthReportDataPdfReportGenerator
{
    private const string ReportTile = "Registered passes per month 2023 Report";
    
    private const int PdfMargin = 20;
    private const int DefaultColumnWidth = 150;

    public Task<byte[]> GeneratePdfReportAsync(string name, IReadOnlyCollection<NewPassesPerMonthDto> report, CancellationToken cancellationToken)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(PdfMargin);
                page.Content()
                    .Column(column =>
                    {
                        FillTitle(column);
                        DrawTable(report, column);
                    });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }
    
    private static void DrawTable(IReadOnlyCollection<NewPassesPerMonthDto> report, ColumnDescriptor column) =>
        column.Item()
            .Table(tableDescriptor =>
            {
                FillHeaders(tableDescriptor);
                FillTable(report, tableDescriptor);
            });

    private static void FillTitle(ColumnDescriptor columnDescriptor) =>
        columnDescriptor.Item()
            .Element(container => container.PaddingVertical(10))
            .Text(ReportTile)
            .Bold()
            .FontSize(13);
    
    private static void FillHeaders(TableDescriptor table)
    {
        table.ColumnsDefinition(columns =>
        {
            columns.ConstantColumn(DefaultColumnWidth);
            columns.ConstantColumn(DefaultColumnWidth);
        });
        table.Header(header =>
        {
            header.Cell().Element(HeaderStyle).Text("Month").Bold();
            header.Cell().Element(HeaderStyle).Text("Registered Passes").Bold();
        });
        static IContainer HeaderStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
    }
    
    private static void FillTable(IReadOnlyCollection<NewPassesPerMonthDto> report, TableDescriptor table)
    {
        foreach (var passesPerMonthDto in report)
        {
            table.Cell().Element(CellStyle).Text(passesPerMonthDto.Month);
            table.Cell().Element(CellStyle).Text(passesPerMonthDto.RegisteredPasses.ToString());
        }
        
        static IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
    }
    
    private static IContainer DefaultCellStyle(IContainer container, string backgroundColor) =>
        container
            .Border(1)
            .BorderColor(Colors.Grey.Lighten1)
            .Background(backgroundColor)
            .PaddingVertical(5)
            .PaddingHorizontal(10)
            .AlignCenter()
            .AlignMiddle();
}