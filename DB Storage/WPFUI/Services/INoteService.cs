using System.Net.Http;

namespace WPFUI.Services
{
    public interface INoteService
    {
        Task<HttpResponseMessage> CreateNoteAsync(string text);
        Task<HttpResponseMessage> GetNoteAsync(int id);
        Task<HttpResponseMessage> UpdateNoteAsync(int id, string newText);
        Task<HttpResponseMessage> DeleteNoteAsync(int id);
    }

   
}