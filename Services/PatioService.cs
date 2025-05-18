using Microsoft.EntityFrameworkCore;
using patioAPI.Models;

namespace patioAPI.Services
{
    public class PatioService
    {
        private readonly PatioContext _context;
        public PatioService(PatioContext context)
        {
            _context = context;
        }

        public async Task<List<Patio>> GetAllAsync() => await _context.Patios.ToListAsync();
        public async Task<Patio?> GetByIdAsync(int courtId) => await _context.Patios.FirstOrDefaultAsync(p => p.CourtId == courtId);
        public async Task<Patio> CreateAsync(Patio patio)
        {
            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();
            return patio;
        }
        public async Task<bool> UpdateAsync(int courtId, Patio patio)
        {
            if (courtId != patio.CourtId) return false;
            _context.Entry(patio).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Patios.Any(e => e.CourtId == courtId)) return false;
                else throw;
            }
        }
        public async Task<bool> DeleteAsync(int courtId)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.CourtId == courtId);
            if (patio == null) return false;
            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchAsync(int courtId, Dictionary<string, object> updates)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.CourtId == courtId);
            if (patio == null) return false;

            var patioType = typeof(Patio);
            foreach (var item in updates)
            {
                var prop = patioType.GetProperty(item.Key);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(patio, Convert.ChangeType(item.Value, prop.PropertyType));
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<object>> GetOcupacaoGridAsync(int courtId)
        {
            // Busca todas as posições ocupadas no grid do pátio
            var ocupacao = await _context.VeiculoPatios
                .Where(vp => vp.CourtId == courtId)
                .Select(vp => new {
                    vp.VehicleId,
                    vp.X,
                    vp.Y,
                    vp.Position
                })
                .ToListAsync();
            return ocupacao.Cast<object>().ToList();
        }
    }
}
