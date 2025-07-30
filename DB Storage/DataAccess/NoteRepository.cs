using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class NoteRepository(AppContext context) : INoteRepository
    {
        public async Task CreateAsync(Note note, CancellationToken cancellationToken = default)
        {            
            await context.Notes.AddAsync(note, cancellationToken);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Note note, CancellationToken cancellationToken = default)
        {
            context.Notes.Remove(note);
            await context.SaveChangesAsync();
        }

        public async Task<Note?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Notes.FirstOrDefaultAsync(x => x.ID == id, cancellationToken);
        }

        public async Task UpdateAsync(Note note, CancellationToken cancellationToken = default)
        {
            context.Notes.Update(note);
            await context.SaveChangesAsync();
        }
    }
}
