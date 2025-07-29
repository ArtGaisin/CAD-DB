using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi
{
    [ApiController]
    [Route("Note")]
    public class NoteController(INoteService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string text)
        {
            await service.CreateAsync(text);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetNoteAsync([FromRoute] int id)
        {
            var result = await service.GetByIDAsync(id);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNoteAsync([FromRoute] int id, [FromBody] string newText)
        {
            await service.UpdateAsync(id, newText);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNoteAsync([FromRoute] int id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}
