using System.Net.Http.Json;

namespace SalesSaas.Frontend.Services {
    public class UserService {
        private readonly HttpClient _http;

        public UserService(HttpClient http) {
            _http = http;
        }

        public async Task<List<UserDto>> GetUsersAsync() {
            return await _http.GetFromJsonAsync<List<UserDto>>("https://localhost:44357/api/users");
        }

    }

    public class UserDto {
        public string Name { get; set; }
    }
}
