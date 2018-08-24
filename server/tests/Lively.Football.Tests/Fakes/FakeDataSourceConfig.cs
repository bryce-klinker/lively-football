using Lively.Football.Application.Common;

namespace Lively.Football.Tests.Fakes
{
    public class FakeDataSourceConfig : IDataSourceConfig
    {
        public const string DefaultApiKey = "f0bd2551475a981323ba7a83eeea738e96820b9206e5a6f51503af4d5b375c63";
        public const string DefaultBaseUrl = "https://apifootball.com/api/";

        public string ApiKey { get; set; } = DefaultApiKey;
        public string BaseUrl { get; set; } = DefaultBaseUrl;
    }
}
