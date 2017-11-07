namespace COLSTRAT.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using COLSTRAT.Models;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using COLSTRAT.Helpers;
    using System.Text;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.Internet_Settings
                };
            }

            var response = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!response)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.Internet_Connection
                };
            }
            return new Response
            {
                IsSuccess = true
            };
        }

        public async Task<TokenResponse> GetToken(string urlBase,string username,string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync(
                    "Token",new StringContent(string.Format("grant_type=password&username={0}&password={1}",username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Devuelve una petición por GET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlBase">url base www.ejemplo.com</param>
        /// <param name="controller">controlador de la url /datos</param>
        /// <returns></returns>
        public async Task<Response> GetList<T>(string urlBase, string controller)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
    }
}
