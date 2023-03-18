namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Passes.Register.Requests;

using Fitnet.Passes.Register;

sealed class RegisterPassRequestFaker: Faker<RegisterPassRequest>
{
    public RegisterPassRequestFaker()
    {
        RuleFor(request => request.CustomerId, faker => faker.Random.Guid());
        RuleFor(request => request.From, faker => faker.Date.Past());
        RuleFor(request => request.To, faker => faker.Date.Future());
    }
}