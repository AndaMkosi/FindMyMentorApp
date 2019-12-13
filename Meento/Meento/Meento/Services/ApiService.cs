using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Meento.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Meento.Services
{
   public class ApiService
    {
        public async Task<bool>  RegisterUser(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
          var response = await httpClient.PostAsync("https://findmymentor.azurewebsites.net/api/Account/Register", content);
            return response.IsSuccessStatusCode;
        }
        public async Task<TokenResponse> GetToken(string email, string password)
        {
            var httpClient = new HttpClient();
          var content =  new StringContent($"grant_type=password&username={email}&passwor={password}", Encoding.UTF8, "application/x-www-form-urlencoded");
          var response = await httpClient.PostAsync("https://findmymentor.azurewebsites.net/Token", content);
          var jsonResult = await response.Content.ReadAsStringAsync();
          var result = JsonConvert.DeserializeObject<TokenResponse>(jsonResult);
            return result;
        }

        public async Task<bool> PasswordRecovery(string email)
        {
            var httpClient = new HttpClient();
            var recoverPasswordModel = new PasswordRecoveryModel()
            {
                Email = email
            };

            var json = JsonConvert.SerializeObject(recoverPasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://findmymentor.azurewebsites.net/api/Users/PasswordRecovery", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var httpClient = new HttpClient();
            var ChangePasswordModel = new ChangePasswordModel()
            {
                OldPassword = oldPassword, NewPassword = newPassword, ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(ChangePasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.PostAsync("https://findmymentor.azurewebsites.net/api/Account/ChangePassword", content);
           
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> BecomeATutor(Tutors tutor)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(tutor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.PostAsync("https://findmymentor.azurewebsites.net/api/instructors", content);
            return response.StatusCode == HttpStatusCode.Created;
        }

         public async Task<List<Tutors>> GetInstructors()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
          var response = await httpClient.GetStringAsync("https://findmymentor.azurewebsites.net/api/instructors");
           return JsonConvert.DeserializeObject<List<Tutors>>(response);
        }

        public async Task<Tutors> GetTutor(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.GetStringAsync("https://findmymentor.azurewebsites.net/api/instructors/" + id);
            return JsonConvert.DeserializeObject<Tutors>(response);
        }

        public async Task<List<Tutors>> SearchTutor(string subject, string gender, string city)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.GetStringAsync("https://findmymentor.azurewebsites.net/instructors?subject=" + subject + "&gender=" + gender + "&city=" + city);
            return JsonConvert.DeserializeObject<List<Tutors>>(response);
        }


        public async Task<List<City>> GetCities()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://findmymentor.azurewebsites.net/api/cities");
            return JsonConvert.DeserializeObject<List<City>>(response);
        }

        public async Task<List<Courses>> GetCourses()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://findmymentor.azurewebsites.net/api/courses");
            return JsonConvert.DeserializeObject<List<Courses>>(response);
        }
    }
}
