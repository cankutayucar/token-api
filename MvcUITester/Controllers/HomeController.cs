using System.Net.Http.Headers;
using System.Text;
using AuthServer.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedLibrary.DTOs;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MvcUITester.Controllers
{
    public class HomeController : Controller
    {
        //private readonly HttpClient _httpClient;

        //public HomeController(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        public async Task<IActionResult> Index()
        {

            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7299/");
            var dto = new LoginDto { Email = "cankutayucar@hotmail.com", Password = "Cankutay.01" };
            string jsonDto = JsonSerializer.Serialize(dto);
            var content = new StringContent(jsonDto, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await _httpClient.PostAsync("/api/Auth/CreateToken", content);

            var responseDto =
                Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseDto<TokenDto>>(
                    await result.Content.ReadAsStringAsync());

            var tokenDto = responseDto.Data;



            HttpClient _httpClient2 = new HttpClient();

            _httpClient2.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenDto.AccessToken);
            _httpClient2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient2.BaseAddress = new Uri("https://localhost:7299/");
            var res = await _httpClient2.GetAsync("/api/Product");
            var datataa = await res.Content.ReadAsStringAsync();







            return View();
        }
    }
}
