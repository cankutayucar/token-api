using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedLibrary.CustomHttpClient
{
    public class TryProductService
    {
        private readonly HttpClient _httpClient;

        public TryProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        #region httpclient get

        //private static void GetProducts()
        //{
        //    using var httpClient = new HttpClient();

        //    httpClient.BaseAddress = new Uri("http://localhost:5000/");

        //    var result = httpClient.GetAsync("api/Products").Result;

        //    var jsonString = result.Content.ReadAsStringAsync().Result;

        //    var products = JsonSerializer.Deserialize<List<Product>>(jsonString);

        //    foreach (var product in products)
        //        Console.WriteLine($"Id: {product.Id} - Name: {product.Name}");
        //}

        #endregion

        #region httpclient post
        //private static void AddProduct()
        //{
        //    using var httpClient = new HttpClient();

        //    httpClient.BaseAddress = new Uri("http://localhost:5000/");

        //    ProductDto product = new ProductDto()
        //    {
        //        Name = "Client Test Ürün",
        //        Description = "Client Test Ürün Açıklama",
        //        Price = 1500,
        //        CategoryId = 1
        //    };

        //    var serializeProduct = JsonSerializer.Serialize(product);

        //    StringContent stringContent = new StringContent(serializeProduct, Encoding.UTF8, "application/json");

        //    var result = httpClient.PostAsync("api/Products", stringContent).Result;

        //    if (result.IsSuccessStatusCode)
        //        Console.WriteLine("Ürün ekleme başarılı.");
        //    else
        //        Console.WriteLine($"Ürün eklenemedi. Hata Kodu: {result.StatusCode}");
        //} 
        #endregion

        #region httpclient put
        //private static void UpdateProduct()
        //{
        //    using var httpClient = new HttpClient();

        //    httpClient.BaseAddress = new Uri("http://localhost:5000/");

        //    UpdateProductDto product = new UpdateProductDto()
        //    {
        //        Id = 1,
        //        Name = "Client Test Ürün Güncelleme",
        //        Price = 1500
        //    };

        //    var serializeProduct = JsonSerializer.Serialize(product);

        //    StringContent stringContent = new StringContent(serializeProduct, Encoding.UTF8, "application/json");

        //    var result = httpClient.PutAsync("api/Products/1", stringContent).Result;

        //    if (result.IsSuccessStatusCode)
        //        Console.WriteLine("Ürün güncelleme başarılı.");
        //    else
        //        Console.WriteLine($"Ürün güncelleme başarısız. Hata Kodu: {result.StatusCode}");
        //} 
        #endregion

        #region httpclient delete
        //private static void DeleteProduct()
        //{
        //    using var httpClient = new HttpClient();

        //    httpClient.BaseAddress = new Uri("http://localhost:5000/");

        //    int productId = 1;

        //    var result = httpClient.DeleteAsync($"api/Products/{productId}").Result;

        //    if (result.IsSuccessStatusCode)
        //        Console.WriteLine("Ürün silme başarılı.");
        //    else
        //        Console.WriteLine($"Ürün silme başarısız. Hata Kodu: {result.StatusCode}");
        //} 
        #endregion



    }
}
