using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class NoteRepository(AppContext context) : INoteRepository
    {
        public async Task CreateAsync(Note note, CancellationToken cancellationToken = default)
        {
            note.Created = DateTime.UtcNow;
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
            note.Updated = DateTime.UtcNow;
            context.Notes.Update(note);
            await context.SaveChangesAsync();
        }
    }
}
