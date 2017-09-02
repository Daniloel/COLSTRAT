using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COLSTRAT.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.Connectivity;
using COLSTRAT.Helpers;

namespace COLSTRAT.Service
{
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
