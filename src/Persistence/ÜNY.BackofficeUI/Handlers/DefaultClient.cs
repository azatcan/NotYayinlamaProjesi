using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ÜNY.WebAPI.Model.AccountModel;

namespace ÜNY.BackofficeUI.Handlers
{
    public class DefaultClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public DefaultClient(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            httpClient.BaseAddress = new Uri("https://localhost:7212");
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest requsetData)
        {
            var json = JsonConvert.SerializeObject(requsetData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(responseJson);

            return result;
        }

        //public async Task<dynamic> PostAsync<TRequest>(string endpoint, TRequest requsetData)
        //{
        //    var json = JsonConvert.SerializeObject(requsetData);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync(endpoint, content);
        //    response.EnsureSuccessStatusCode();

        //    var responseJson = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);

        //    return result;
        //}

        public async Task<RegisterResponse> PostAsync<TRequest>(string endpoint, TRequest requestData)
        {

            var formData = new MultipartFormDataContent();
            var model = requestData as RegisterModel;
            if (model != null)
            {
                formData.Add(new StringContent(model.Name), "Name");
                formData.Add(new StringContent(model.SurName), "SurName");
                formData.Add(new StringContent(model.UserName), "UserName");
                formData.Add(new StringContent(model.IdNumber), "IdNumber");
                formData.Add(new StringContent(model.Password), "Password");
                formData.Add(new StringContent(model.RePassword), "RePassword");
                formData.Add(new StringContent(model.DateofBirth.ToString("yyyy-MM-dd")), "DateofBirth");
                formData.Add(new StringContent(model.BirthPlace), "BirthPlace");
                formData.Add(new StringContent(model.MotherName), "MotherName");
                formData.Add(new StringContent(model.FatherName), "FatherName");
                formData.Add(new StringContent(model.GenderId.ToString()), "GenderId");
                formData.Add(new StringContent(model.UnitİnformationId.ToString()), "UnitİnformationId");
                formData.Add(new StringContent(model.ContactİnformationId.ToString()), "ContactİnformationId");

                if (model.ImagePath != null)
                {
                    var imageStream = model.ImagePath.OpenReadStream();
                    var fileName = Path.GetFileName(model.ImagePath.FileName);
                    formData.Add(new StreamContent(imageStream), "ImagePath", fileName);
                }
                var json = JsonConvert.SerializeObject(formData);
                var content = new StringContent(json, Encoding.UTF8, "multipart/form-data");

                var response = await _httpClient.PostAsync(endpoint, formData);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RegisterResponse>(responseJson);
                return result;
            }
            return null;
        }

        //public async Task<AuthorsResponse> PostAuthorsAsync<TRequest>(string endpoint, TRequest requestData)
        //{

        //    var formData = new MultipartFormDataContent();
        //    var model = requestData as AuthorsAddedModel;
        //    if (model != null)
        //    {
        //        formData.Add(new StringContent(model.Name), "Name");
        //        formData.Add(new StringContent(model.SurName), "SurName");
        //        formData.Add(new StringContent(model.About), "About");
        //        formData.Add(new StringContent(model.Pseudonym), "Pseudonym");
        //        formData.Add(new StringContent(model.DateOfBirth.ToString("yyyy-MM-dd")), "DateOfBirth");
        //        formData.Add(new StringContent(model.DateOfDeath.ToString("yyyy-MM-dd")), "DateOfDeath");

        //        if (model.ImagePath != null)
        //        {
        //            var imageStream = model.ImagePath.OpenReadStream();
        //            var fileName = Path.GetFileName(model.ImagePath.FileName);
        //            formData.Add(new StreamContent(imageStream), "ImagePath", fileName);
        //        }
        //        var json = JsonConvert.SerializeObject(formData);
        //        var content = new StringContent(json, Encoding.UTF8, "multipart/form-data");

        //        var response = await _httpClient.PostAsync(endpoint, formData);
        //        response.EnsureSuccessStatusCode();

        //        var responseJson = await response.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<AuthorsResponse>(responseJson);
        //        return result;
        //    }
        //    return null;
        //}
    }
}
