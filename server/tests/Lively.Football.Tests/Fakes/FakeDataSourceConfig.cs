using Lively.Football.Application.Common;

namespace Lively.Football.Tests.Fakes
{
    public class FakeDataSourceConfig : IDataSourceConfig
    {
        public const string DefaultApiKey = "this.is.a.token";
        public const string DefaultBaseUrl = "https://somwhere.com";

        public string ApiKey { get; set; } = DefaultApiKey;
        public string BaseUrl { get; set; } = DefaultBaseUrl;
    }
}
