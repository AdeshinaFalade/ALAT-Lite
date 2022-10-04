using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ALAT_Lite.Classes
{
    public class NetworkUtils
    {
        internal static string baseUrl = "https://galacticos-alat-apim.azure-api.net/api/";

        public NetworkUtils()
        {

        }


        internal static async Task<string> PostUserData(string actionName, string rawData)
        {
            string result = string.Empty;
            try
            {
                using (var mClient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
                {
                    mClient.Timeout = TimeSpan.FromMinutes(1);

                    var response = await mClient.PostAsync($"{actionName}", new StringContent(rawData, System.Text.Encoding.UTF8, "application/json")).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var mResult = await response.Content.ReadAsStringAsync();
                        return mResult;
                    }
                    else
                    {
                        result = response.ReasonPhrase;
                    }
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
        internal static async Task<string> GetUserData(string actionName)
        {
            string result = string.Empty;
            try
            {
                using (var mClient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
                {
                    mClient.Timeout = TimeSpan.FromMinutes(1);

                    var response = await mClient.GetAsync($"{actionName}/").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var mResult = await response.Content.ReadAsStringAsync();
                        return mResult;
                    }
                    else
                    {
                        result = response.ReasonPhrase;
                    }
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
    }
}