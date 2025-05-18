using Microsoft.EntityFrameworkCore;
using patioAPI.Models;

namespace patioAPI.Services
{
    public class VeiculoPatioService
    {
        private readonly PatioContext _context;
        public VeiculoPatioService(PatioContext context)
        {
            _context = context;
        }

        public async Task<List<VeiculoPatio>> GetAllAsync() => await _context.VeiculoPatios.ToListAsync();
        public async Task<VeiculoPatio?> GetByIdsAsync(int vehicleId, int courtId, int branchId) =>
            await _context.VeiculoPatios.FirstOrDefaultAsync(vp => vp.VehicleId == vehicleId && vp.CourtId == courtId && vp.BranchId == branchId);
        public async Task<VeiculoPatio> CreateAsync(VeiculoPatio veiculoPatio)
        {
            _context.VeiculoPatios.Add(veiculoPatio);
            await _context.SaveChangesAsync();
            return veiculoPatio;
        }
        public async Task<bool> UpdateAsync(int vehicleId, int courtId, int branchId, VeiculoPatio veiculoPatio)
        {
            if (vehicleId != veiculoPatio.VehicleId || courtId != veiculoPatio.CourtId || branchId != veiculoPatio.BranchId) return false;
            _context.Entry(veiculoPatio).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.VeiculoPatios.AnyAsync(vp => vp.VehicleId == vehicleId && vp.CourtId == courtId && vp.BranchId == branchId)) return false;
                else throw;
            }
        }
        public async Task<bool> DeleteAsync(int vehicleId, int courtId, int branchId)
        {
            var veiculoPatio = await _context.VeiculoPatios.FirstOrDefaultAsync(vp => vp.VehicleId == vehicleId && vp.CourtId == courtId && vp.BranchId == branchId);
            if (veiculoPatio == null) return false;
            _context.VeiculoPatios.Remove(veiculoPatio);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PatchAsync(int vehicleId, int courtId, int branchId, Dictionary<string, object> updates)
        {
            var veiculoPatio = await _context.VeiculoPatios.FirstOrDefaultAsync(vp => vp.VehicleId == vehicleId && vp.CourtId == courtId && vp.BranchId == branchId);
            if (veiculoPatio == null) return false;
            var type = typeof(VeiculoPatio);
            foreach (var item in updates)
            {
                var prop = type.GetProperty(item.Key);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(veiculoPatio, Convert.ChangeType(item.Value, prop.PropertyType));
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<object>> GetProximidadeAsync(int courtId, int x, int y, int distancia)
        {
            // Busca a moto consultada (se existir)
            var central = await _context.VeiculoPatios.FirstOrDefaultAsync(vp => vp.CourtId == courtId && vp.X == x && vp.Y == y);
            if (central == null) return new List<object>();

            // Busca motos próximas (excluindo a central)
            var proximos = await _context.VeiculoPatios
                .Where(vp => vp.CourtId == courtId && !(vp.X == x && vp.Y == y) &&
                    (Math.Abs(vp.X - x) <= distancia && Math.Abs(vp.Y - y) <= distancia))
                .Join(_context.Motos, vp => vp.VehicleId, m => m.VehicleId, (vp, m) => new {
                    vp.VehicleId,
                    m.Plate,
                    m.Model,
                    vp.X,
                    vp.Y,
                    vp.Position
                })
                .ToListAsync();

            return new List<object> {
                new {
                    central.VehicleId,
                    central.CourtId,
                    central.BranchId,
                    central.Position,
                    central.X,
                    central.Y,
                    distancia,
                    veiculosProximos = proximos
                }
            };
        }
    }
}
