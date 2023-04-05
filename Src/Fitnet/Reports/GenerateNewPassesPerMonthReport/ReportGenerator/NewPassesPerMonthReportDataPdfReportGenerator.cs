namespace SuperSimpleArchitecture.Fitnet.Reports.GenerateNewPassesPerMonthReport.ReportGenerator;

using Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

internal sealed class NewPassesPerMonthReportDataPdfReportGenerator : INewPassesPerMonthReportDataPdfReportGenerator
{
    public Task<byte[]> GeneratePdfReportAsync(string name, IReadOnlyCollection<NewPassesPerMonthDto> report, CancellationToken cancellationToken)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);

                page.Content().Column(columnDescriptor =>
                {
                    columnDescriptor.Item()
                        .Element(x => x.PaddingVertical(10))
                        .Text("Registered passes per month 2023 Report")
                        .Bold()
                        .FontSize(13);

                    columnDescriptor.Item().Table(tableDescriptor =>
                    {
                        IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                        {
                            return container
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten1)
                                .Background(backgroundColor)
                                .PaddingVertical(5)
                                .PaddingHorizontal(10)
                                .AlignCenter()
                                .AlignMiddle();
                        }
                    
                        tableDescriptor.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.ConstantColumn(150);
                        });
                        tableDescriptor.Header(y =>
                        {
                            y.Cell().Element(HeaderStyle).Text("Month").Bold();
                            y.Cell().Element(HeaderStyle).Text("Registered Passes").Bold();
                        });

                        foreach (var rNewPassesPerMonthDto in report)
                        {
                            tableDescriptor.Cell().Element(CellStyle).Text(rNewPassesPerMonthDto.Month);
                            tableDescriptor.Cell().Element(CellStyle).Text(rNewPassesPerMonthDto.RegisteredPasses.ToString());
                        }
                    
                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White); 
                        IContainer HeaderStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);                    });
                });
            });
        });

        return Task.FromResult(document.GeneratePdf());
    }
}