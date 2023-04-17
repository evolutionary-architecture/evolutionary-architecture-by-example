using Newtonsoft.Json;

namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Common;

internal static class ResponseMessageDeserializer
{
    internal static async Task<string> Deserialize(HttpResponseMessage responseMessage)
    {
        var responseContent = await responseMessage.Content.ReadAsStringAsync();
        var deserializedResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
        
        return deserializedResponse != null ? (string)deserializedResponse.Message : string.Empty;
    }
}