using DataAccess;

namespace BusinessLogic
{
    internal class NoteService(INoteRepository noteRepository) : INoteService
    {
        public async Task CreateAsync(string text, CancellationToken cancellationToken = default)
        {
            var note = new Note()
            {
                Text = text,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            await noteRepository.CreateAsync(note, cancellationToken);
        }

        public async Task<string> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            Note? note = await GetByID(id, cancellationToken);
            if (note != null) return note.Text;
            return "Заметки с таким ID нет!";
        }

        public async Task<bool> UpdateAsync(int id, string newText, CancellationToken cancellationToken = default)
        {
            Note? note = await GetByID(id, cancellationToken);
            if (note != null)
            {
                note.Text = newText;
                note.Updated = DateTime.UtcNow;
                await noteRepository.UpdateAsync(note, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            Note? note = await GetByID(id, cancellationToken);
            if (note != null)
            {
                await noteRepository.DeleteAsync(note, cancellationToken);
                return true;
            }
            return false;
        }

        private async Task<Note?> GetByID(int id, CancellationToken cancellationToken)
        {
            var note = await noteRepository.GetByIDAsync(id, cancellationToken);
            return note;
        }
    }
}
