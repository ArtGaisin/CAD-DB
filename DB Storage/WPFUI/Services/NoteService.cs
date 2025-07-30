using System.Net.Http;

namespace WPFUI.Services
{
    public class NoteService : INoteService
    {
        private const string ApiBaseUrl = "https://localhost:7197/Note";
        private readonly HttpClient _httpClient;

        public NoteService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> CreateNoteAsync(string text)
        {
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(text);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(ApiBaseUrl, content);
        }

        public async Task<HttpResponseMessage> GetNoteAsync(int id)
        {
            return await _httpClient.GetAsync($"{ApiBaseUrl}/{id}");
        }

        public async Task<HttpResponseMessage> UpdateNoteAsync(int id, string newText)
        {
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(newText);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync($"{ApiBaseUrl}/{id}", content);
        }

        public async Task<HttpResponseMessage> DeleteNoteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{ApiBaseUrl}/{id}");
        }
    }
}
