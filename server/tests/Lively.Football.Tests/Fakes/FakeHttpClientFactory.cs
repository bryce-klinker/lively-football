using System.Net.Http;

namespace Lively.Football.Tests.Fakes
{
    public class FakeHttpClientFactory : IHttpClientFactory
    {
        private readonly FakeHttpMessageHandler _handler;

        public FakeHttpClientFactory()
        {
            _handler = new FakeHttpMessageHandler();
        }

        public HttpClient CreateClient(string name)
        {
            return new HttpClient(_handler);
        }

        public void SetupGet<T>(string url, T content)
        {
            _handler.SetupGet(url, content);
        }
    }
}
