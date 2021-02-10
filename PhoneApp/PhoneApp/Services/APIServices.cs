﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public async Task<Recipes> GetRecipesByIDAsync(int RecipeID)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Globals.Token);


            string queryString = "http://10.0.0.12:80/api/Recipes/ByID?ID=" + RecipeID.ToString(); 
            var response = await client.GetAsync(queryString);

            var content = await response.Content.ReadAsStringAsync();


            var recipe = JsonConvert.DeserializeObject<Recipes>(content);

            return recipe;

        }

        public async Task<List<Recipes>> GetRecipesAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Globals.Token);

            var json = await client.GetStringAsync("http://10.0.0.12:80/api/Recipes");

            var recipes = JsonConvert.DeserializeObject<List<Recipes>>(json);

            return recipes;

        }

        public async Task LoginAsync(string username, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "http://10.0.0.12:80/Token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();


            //Error handle bad log in 
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);


            Globals.Token = values["access_token"];


        }

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
