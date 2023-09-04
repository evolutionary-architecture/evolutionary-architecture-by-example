namespace EvolutionaryArchitecture.Fitnet.Offers.Data;

internal sealed class Offer
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTimeOffset PreparedAt { get; init; }
    public decimal Discount { get; init; }
    public DateTimeOffset OfferedFromDate { get; init; }
    public DateTimeOffset OfferedFromTo { get; init; }

    private Offer(Guid id, Guid customerId, DateTimeOffset preparedAt, decimal discount, DateTimeOffset offeredFromDate, DateTimeOffset offeredFromTo)
    {
        Id = id;
        CustomerId = customerId;
        PreparedAt = preparedAt;
        Discount = discount;
        OfferedFromDate = offeredFromDate;
        OfferedFromTo = offeredFromTo;
    }

    internal static Offer PrepareStandardPassExtension(Guid customerId, DateTimeOffset nowDate)
    {
        const decimal standardDiscount = 0.1m;
        var offer = new Offer(
            Guid.NewGuid(),
            customerId,
            nowDate,
            standardDiscount,
            nowDate,
            nowDate.AddYears(1));

        return offer;
    }
}
