using Microsoft.EntityFrameworkCore;
using patioAPI.Models;

namespace patioAPI.Services
{
    public class MotoService
    {
        private readonly PatioContext _context;
        public MotoService(PatioContext context)
        {
            _context = context;
        }

        public async Task<List<Moto>> GetAllAsync()
            => await _context.Motos.ToListAsync();

        public async Task<Moto?> GetByIdAsync(int vehicleId)
            => await _context.Motos.FirstOrDefaultAsync(m => m.VehicleId == vehicleId);

        public async Task<List<Moto>> GetByBranchAsync(string branch)
            => await _context.Motos.Where(m => m.Branch == branch).ToListAsync();

        public async Task<Moto> CreateAsync(Moto moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task<bool> UpdateAsync(int vehicleId, Moto moto)
        {
            if (vehicleId != moto.VehicleId) return false;
            _context.Entry(moto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Motos.Any(e => e.VehicleId == vehicleId)) return false;
                else throw;
            }
        }

        public async Task<bool> DeleteAsync(int vehicleId)
        {
            var moto = await _context.Motos.FirstOrDefaultAsync(m => m.VehicleId == vehicleId);
            if (moto == null) return false;
            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchAsync(int vehicleId, Dictionary<string, object> updates)
        {
            var moto = await _context.Motos.FirstOrDefaultAsync(m => m.VehicleId == vehicleId);
            if (moto == null) return false;
            var motoType = typeof(Moto);
            foreach (var item in updates)
            {
                var prop = motoType.GetProperty(item.Key);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(moto, Convert.ChangeType(item.Value, prop.PropertyType));
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
