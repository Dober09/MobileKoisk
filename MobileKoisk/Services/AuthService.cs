
using System.Net.Http.Json;

using MobileKoisk.Models;
namespace MobileKoisk.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        

   
        private static string BaseUrl => "http://10.0.2.2:5171/api/Auth/";

        public AuthService(HttpClient httpClient,IConnectivity connectivity)
        {
            _connectivity = connectivity;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthResponse> Register(UserData userData)
        {
            if (!_connectivity.NetworkAccess.Equals(NetworkAccess.Internet))
            {
                throw new Exception("No internet connection available");
            }

            try
            {
                // Test connection first
                System.Diagnostics.Debug.WriteLine("Testing API connection...");
                using (var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                })
                using (var testClient = new HttpClient(handler))
                {
                    try
                    {
                        var testResponse = await testClient.GetAsync("http://10.0.2.2:5171/swagger");
                        System.Diagnostics.Debug.WriteLine($"Test connection status: {testResponse.StatusCode}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Test connection failed: {ex.Message}");
                    }
                }


                var registerData = new
                {
                    Name = userData.Name,
                    Surname = userData.Surname,
                    Email = userData.Email,
                    Password = userData.Password,
                    DateOfBirth = userData.DateOfBirth,
                    PhoneNumber = userData.PhoneNumber,
                };

                System.Diagnostics.Debug.WriteLine($"Sending registration request to: {BaseUrl}register");
                System.Diagnostics.Debug.WriteLine($"Request data: {System.Text.Json.JsonSerializer.Serialize(registerData)}");

                var response = await _httpClient.PostAsJsonAsync("register", registerData);
                var responseContent = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine($"Response Status Code: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"Response Content: {responseContent}");


                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AuthResponse>();
                }

                throw new Exception($"Server returned {response.StatusCode}: {responseContent}");
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Request failed: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw new Exception($"Failed to connect to server: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Registration failed: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }

        }




        /// <summary>
        /// This  method returns and authrespons
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AuthResponse> Login(string email, string password)
        {
            try
            {

                var loginData = new { Email = email, Password = password };
                var response = await _httpClient.PostAsJsonAsync("login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AuthResponse>();
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
            catch (Exception ex) {

                throw new Exception($"Login failed: {ex.Message}");

            }
        }
    }
}
