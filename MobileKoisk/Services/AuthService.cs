
using System.Net.Http.Json;

using MobileKoisk.Models;
namespace MobileKoisk.Services
{
    public class AuthService
    {

        private readonly HttpClient _httpClient;
        private static string BaseUrl => "http://10.0.2.2:5171/api/Auth/";

        public AuthService()
        {

            // Add handler for development to bypass SSL validation
#if DEBUG
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler);
#else
        _httpClient = new HttpClient();
#endif

            _httpClient.BaseAddress = new Uri(BaseUrl);

            // Add timeout and headers
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<AuthResponse> Register(UserData userData)
        {
            try
            {
                // Log the request URL and data
                System.Diagnostics.Debug.WriteLine($"Sending request to: {BaseUrl}register");

                var registerData = new
                {
                    Name = userData.Name,
                    Surname = userData.Surname,
                    Email = userData.Email,
                    Password = userData.Password,
                    DateOfBirth = userData.DateOfBirth,
                    PhoneNumber = userData.PhoneNumber,

                };
                // Test API connection first
                try
                {
                    using (var ping = new HttpClient())
                    {
                        ping.Timeout = TimeSpan.FromSeconds(5);
                        var responze = await ping.GetAsync($"http://10.0.2.2:5171/swagger");
                        System.Diagnostics.Debug.WriteLine($"API ping result: {responze.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"API ping failed: {ex.Message}");
                }

                // log reguest data
                System.Diagnostics.Debug.WriteLine($"Request data: {System.Text.Json.JsonSerializer.Serialize(registerData)}");



                var response = await _httpClient.PostAsJsonAsync("register", registerData);
                var responseContent = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine($"Response Status Code: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"Response Content: {responseContent}");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to deserialize response: {ex.Message}");
                        throw new Exception($"Failed to process server response: {ex.Message}");
                    }
                }


                throw new Exception($"Server returned {response.StatusCode}: {responseContent}");
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Request failed: {ex.Message}");
                throw new Exception($"Failed to connect to server: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.Message}");
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
