using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lively.Football.Tests.Fakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly List<FakeHttpCall> _fakeCalls;

        public FakeHttpMessageHandler()
        {
            _fakeCalls = new List<FakeHttpCall>();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var call = GetMatchingCall(request);
            if (call == null)
                throw new InvalidOperationException($"No matching request for [{request.Method}] {request.RequestUri}");

            return Task.FromResult(call.Response);
        }

        public void SetupGet<T>(string url, T responseContent)
        {
            _fakeCalls.Add(new FakeHttpCall(HttpMethod.Get, url, responseContent));
        }

        private FakeHttpCall GetMatchingCall(HttpRequestMessage request)
        {
            return _fakeCalls.SingleOrDefault(f => f.IsMatchingRequest(request));
        }
    }

    public class FakeHttpCall
    {
        public HttpRequestMessage Request { get; }
        public HttpResponseMessage Response { get; }

        public FakeHttpCall(HttpMethod method, string url, object responseContent)
            : this(new HttpRequestMessage(method, url), JsonConvert.SerializeObject(responseContent))
        {

        }

        public FakeHttpCall(HttpRequestMessage request, string responseContent, string mediaType = "application/json")
            : this(request, HttpStatusCode.OK, new StringContent(responseContent, Encoding.UTF8, mediaType))
        {

        }

        public FakeHttpCall(HttpRequestMessage request, HttpStatusCode status, HttpContent responseContent)
            : this(request, new HttpResponseMessage(status) { Content = responseContent })
        {

        }

        public FakeHttpCall(HttpRequestMessage request, HttpResponseMessage response)
        {
            Request = request;
            Response = response;
        }

        public bool IsMatchingRequest(HttpRequestMessage request)
        {
            return Request.Method == request.Method
                   && Request.RequestUri == request.RequestUri;
        }
    }
}
