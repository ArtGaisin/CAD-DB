using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface INoteService
    {
        public Task CreateAsync(string text, CancellationToken cancellationToken = default);
        public Task<string> GetByIDAsync(int id, CancellationToken cancellationToken = default);
        public Task<bool> UpdateAsync(int id, string newText, CancellationToken cancellationToken = default);
        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
         
    }
}
