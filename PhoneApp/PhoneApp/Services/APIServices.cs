using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneApp.Models;
namespace PhoneApp.Services
{
    public class APIServices
    {
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
          
            var client = new HttpClient();
            client.DefaultRequestHeaders
      .Accept
      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
    
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json,Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://10.0.0.12:80/api/Account/Register", content);
       
            return response.IsSuccessStatusCode;
            

        }
    }
}
