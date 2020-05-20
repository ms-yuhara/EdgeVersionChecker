using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EdgeVersionChecker
{
    public class EdgeUpdateService
    {
        private const string _baseUrl = "https://msedge.api.cdp.microsoft.com/api/v1.1/contents/Browser/namespaces/Default/names/msedge-{0}-win-x64/versions/latest?action=select";
        private const string _baseApp = "{0}-arch_x64";
        private static HttpClient _client = new HttpClient();

        public EdgeUpdateResponse GetLatestVersions(string channel)
        {
            return AsyncHelper.RunSync<EdgeUpdateResponse>(() => GetLatestVersionsAsync(channel));
        }

        public async Task<EdgeUpdateResponse> GetLatestVersionsAsync(string channel)
        {
            EdgeUpdateResponse results = null;

            try {
                EdgeUpdateRequest request = new EdgeUpdateRequest();

                request.targetingAttributes = new TargetingAttributes();
                request.targetingAttributes.AppAp = string.Format(_baseApp, channel);

                HttpResponseMessage responseMessage;
                string response;

                using (var ms = new MemoryStream())
                using (var sr = new StreamReader(ms)) {
                    var serializer = new DataContractJsonSerializer(typeof(EdgeUpdateRequest));

                    serializer.WriteObject(ms, request);
                    ms.Position = 0;

                    responseMessage = await _client.PostAsync(string.Format(_baseUrl, channel), new StringContent(sr.ReadToEnd(), Encoding.UTF8, "application/json"));
                    response = await responseMessage.Content.ReadAsStringAsync();
                }

                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(response))) {
                    var serializer = new DataContractJsonSerializer(typeof(EdgeUpdateResponse));

                    results = (EdgeUpdateResponse)serializer.ReadObject(ms);
                }
            } catch (Exception ex) {
                // TBD
                Console.WriteLine(ex.ToString());
            }

            return results;
        }
    }

    internal static class AsyncHelper
    {
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return AsyncHelper._myTaskFactory.StartNew<Task<TResult>>(func).Unwrap<TResult>().GetAwaiter().GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            AsyncHelper._myTaskFactory.StartNew<Task>(func).Unwrap().GetAwaiter().GetResult();
        }
    }
}
