using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace piHome.WebHost.Infrastructure
{
    public class MonoPatchingDelegatingHandler : DelegatingHandler
    {
        //http://stackoverflow.com/questions/31590869/web-api-2-post-request-not-working-on-mono
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Host = request.Headers.GetValues("Host").FirstOrDefault();
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
