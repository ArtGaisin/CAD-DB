using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Note")]
    public class NoteController(INoteService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] string text)
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
            var ans = await service.UpdateAsync(id, newText);
            if (ans) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNoteAsync([FromRoute] int id)
        {
            var ans = await service.DeleteAsync(id);
            if (ans) return NoContent();
            return NotFound();
        }
    }
}
