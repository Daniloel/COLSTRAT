using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COLSTRAT.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace COLSTRAT.Service
{
    public class ApiService
    {
        public async Task<Response> GetRocks(string controller)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("http://192.168.0.105:3000")
                };
                var response = await client.GetAsync(controller);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "NO hay conexion"
                    };
                }
                var result = await response.Content.ReadAsStringAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Rocks OK",
                    Result = result
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
