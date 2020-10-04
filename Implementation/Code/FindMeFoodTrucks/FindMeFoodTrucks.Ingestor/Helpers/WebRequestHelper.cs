using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace FindMeFoodTrucks.Ingestor.Helpers
{
    /// <summary>
    /// Helps with making a web request
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class WebRequestHelper
    {
        /// <summary>
        /// Http client object for WebAPI requests
        /// </summary>
        private readonly HttpClient client = null;

        /// <summary>
        /// Logging Object
        /// </summary>
        private readonly ILogger logger = null;

        /// <summary>
        /// The only constructor
        /// </summary>
        public WebRequestHelper(ILogger logger, HttpClient httpClient = null)
        {
            this.logger = logger;
            if (httpClient == null)
            {
                client = new HttpClient();
            }
        }
        /// <summary>
        /// Makes a GET call at the URL and returns the response as string
        /// </summary>
        /// <param name="url">GET url</param>
        /// <returns>response string from the service</returns>
        public virtual async Task<string> GetResponse(string url)
        {
            try
            {
                var stringTask = client.GetStringAsync(url);
                //Await for response
                var response = await stringTask;
                return response;
            }
            catch
            {
                logger.LogError("Error retriving data");
                throw;
            }
        }
    }
}
