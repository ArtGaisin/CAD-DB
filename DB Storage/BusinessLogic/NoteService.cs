using DataAccess;

namespace BusinessLogic
{
    internal class NoteService(INoteRepository noteRepository) : INoteService
    {
        public async Task CreateAsync(string text, CancellationToken cancellationToken = default)
        {
            var note = new Note()
            {
                Text = text
            };
            await noteRepository.CreateAsync(note, cancellationToken);
        }

        public async Task<string> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            Note note = await GetByID(id, cancellationToken);
            return note.Text;
        }

        private async Task<Note> GetByID(int id, CancellationToken cancellationToken)
        {
            var note = await noteRepository.GetByIDAsync(id, cancellationToken);
            if (note is null) throw new Exception("Note not found");
            return note;
        }

        public async Task UpdateAsync(int id, string newText, CancellationToken cancellationToken = default)
        {
            Note note = await GetByID(id, cancellationToken);
            note.Text = newText;
            await noteRepository.UpdateAsync(note, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            Note note = await GetByID(id, cancellationToken);
            await noteRepository.DeleteAsync(note, cancellationToken);
        }
    }
}
