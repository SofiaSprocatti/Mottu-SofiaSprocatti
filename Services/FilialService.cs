using Microsoft.EntityFrameworkCore;
using patioAPI.Models;

namespace patioAPI.Services
{
    public class FilialService
    {
        private readonly PatioContext _context;
        public FilialService(PatioContext context)
        {
            _context = context;
        }

        public async Task<List<Filial>> GetAllAsync() => await _context.Filiais.ToListAsync();
        public async Task<Filial?> GetByIdAsync(int branchId) => await _context.Filiais.FirstOrDefaultAsync(f => f.BranchId == branchId);
        public async Task<Filial> CreateAsync(Filial filial)
        {
            _context.Filiais.Add(filial);
            await _context.SaveChangesAsync();
            return filial;
        }
        public async Task<bool> UpdateAsync(int branchId, Filial filial)
        {
            if (branchId != filial.BranchId) return false;
            _context.Entry(filial).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Filiais.Any(e => e.BranchId == branchId)) return false;
                else throw;
            }
        }
        public async Task<bool> DeleteAsync(int branchId)
        {
            var filial = await _context.Filiais.FirstOrDefaultAsync(f => f.BranchId == branchId);
            if (filial == null) return false;
            _context.Filiais.Remove(filial);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PatchAsync(int branchId, Dictionary<string, object> updates)
        {
            var filial = await _context.Filiais.FirstOrDefaultAsync(f => f.BranchId == branchId);
            if (filial == null) return false;
            var type = typeof(Filial);
            foreach (var item in updates)
            {
                var prop = type.GetProperty(item.Key);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(filial, Convert.ChangeType(item.Value, prop.PropertyType));
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

