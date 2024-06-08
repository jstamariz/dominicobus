using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DominicoBus.Db;
using DominicoBus.DataTransfer;

namespace DominicoBus.Services
{
    public class BusService
    {
        private readonly AppDbContext _context;
        public BusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(BusDTO bus)
        {
            await _context.Busses.AddAsync(new Models.Bus()
            {
                Id = Guid.NewGuid().ToString(),
                Capacity = bus.Capacity,
                Code = bus.Code
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BusDTO bus, string? busId)
        {
            if (busId is null) return;

            var busOnDb = await _context.Busses.FindAsync(busId);

            if (busOnDb is null) return;

            busOnDb.Capacity = bus.Capacity;
            busOnDb.Code = bus.Code;

            _context.Busses.Update(busOnDb);

            await _context.SaveChangesAsync();
        }

        public async Task<BusDTO?> GetAsync(string busId)
        {
            var busOnDb = await _context.Busses.FindAsync(busId);

            if (busOnDb is null) return null;

            return new BusDTO()
            {
                Capacity = busOnDb.Capacity,
                Code = busOnDb.Code
            };
        }

        public async Task DeleteAsync(string? busId)
        {
            if (busId is null) return;

            var busOnDb = await _context.Busses.FindAsync(busId);

            if (busOnDb is not null)
            {
                _context.Busses.Remove(busOnDb);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<BusResult> Search(string search)
        {
            return _context.Busses
                .Where(bus => bus.Code.ToUpper().Contains(search.ToUpper()))
                .Select(bus => new BusResult(bus.Code, bus.Capacity, bus.Id))
                .ToList();
        }
    }
}