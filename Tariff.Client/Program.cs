using Tariff.Client.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Tariff.Client
{
    class Program
    {
        static readonly string baseUrl = "http://localhost:50123";
        static readonly HttpClient client = new HttpClient();

        static async Task<SecurityToken> Authenticate(Login login)
        {
            var response = await client.PostAsJsonAsync($"/user/authenticate", new { Username = login.UserName, login.Password });
            var token = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SecurityToken>(token);
        }

        static async Task<TariffResult[]> GetProductsAsync(string usage)
        {
            var response = await client.GetAsync($"/tariff/GetProducts/" + usage);
            return await DeserializeResponseContent(response);
        }

        static async Task Main(string[] args)
        {
            await RunAsync();
        }

        static async Task RunAsync()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Tariff Comparison");

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                SecurityToken accessToken = new SecurityToken();
                do
                {
                    var login = ReadLoginDetails();
                    accessToken = await Authenticate(login);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken.Auth_token);
                    if (accessToken.Auth_token == "Username or password is incorrect") Console.WriteLine($"\n {accessToken.Auth_token}");
                } while (accessToken.Auth_token == "Username or password is incorrect");

                Console.WriteLine("\n Login Successfull.");

                DisplayMenu();

                string key;
                while ((key = Console.ReadKey().KeyChar.ToString()) != "2")
                {
                    int.TryParse(key, out int keyValue);

                    switch (keyValue)
                    {
                        case 1:
                            await GetProducts();
                            break;
                    }

                    Console.Write("Enter the option (number): ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("App interrupted.");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("App closed.");
            }

            Console.ReadLine();
        }

        static Login ReadLoginDetails()
        {
            Console.WriteLine();
            Console.Write("Enter the user name: ");
            var username = "anna"; // Console.ReadLine();
            Console.Write("Enter the password: ");
            var password = "anna7654"; // Console.ReadLine();
            return new Login() { UserName = username, Password = password };
        }

        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. GetProducts");
            Console.WriteLine("2. Close app (X)");
            Console.WriteLine();
            Console.Write("Enter the option (number): ");
        }

        static async Task GetProducts()
        {
            Console.WriteLine();
            Console.WriteLine("Please Enter The Usage");
            var usage = Console.ReadLine();

            var products = await GetProductsAsync(usage);
            Console.WriteLine();
            if (products[0].IsSuccessful)
            {
                Console.WriteLine("Tarif Comparison Results:");
                Console.WriteLine();

                Console.WriteLine("The List of products, sorted by annual tariff");
                Console.WriteLine("---------------------------------------------------------");

                foreach (var item in products)
                {
                    Console.WriteLine(item?.Name + " : " + item?.AnnualCost + " Euros");
                }
            }
            else
            {
                Console.WriteLine($"Status: Tariff Comparison failed");
                Console.WriteLine($"Message: {products[0].Message}");
            }

            Console.WriteLine();
        }



        static async Task<TariffResult[]> DeserializeResponseContent(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TariffResult[]>(result);
        }
    }
}
