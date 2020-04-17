using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Akinator.Api.Net.Utils
{
    public class AkiWebClient : IDisposable
    {
        private readonly HttpClient m_webClient;

        public AkiWebClient()
        {
            m_webClient = new HttpClient(new HttpClientHandler
            {
                UseCookies = false
            });
            m_webClient.DefaultRequestHeaders.Add("Accept", "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01");
            m_webClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,ar;q=0.8");
            m_webClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            m_webClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            m_webClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            m_webClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            m_webClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            m_webClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.92 Safari/537.36");
            m_webClient.DefaultRequestHeaders.Add("Referer", "https://en.akinator.com/game");
        }

        public Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken = default)
        {
            return m_webClient.GetAsync(url, cancellationToken);
        }

        public void Dispose()
        {
            m_webClient?.Dispose();
        }
    }
    
}
