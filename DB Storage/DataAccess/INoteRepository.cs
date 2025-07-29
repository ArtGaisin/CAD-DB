using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface INoteRepository
    {
        public Task CreateAsync(Note note, CancellationToken cancellationToken = default);
        public Task<Note?> GetByIDAsync(int id, CancellationToken cancellationToken = default);
        public Task UpdateAsync(Note note, CancellationToken cancellationToken = default);
        public Task DeleteAsync(Note note, CancellationToken cancellationToken = default);
        
    }
}
