using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DominicoBus.Db;
using DominicoBus.DataTransfer;

namespace DominicoBus.Services
{
    public class StopService
    {
        private readonly AppDbContext _context;
        public StopService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(StopDTO stop)
        {
            await _context.Stops.AddAsync(new Models.Stop()
            {
                Id = Guid.NewGuid().ToString(),
                Code = stop.Code,
                Latitude = stop.Latitude.ToString(),
                Longitude = stop.Longitude.ToString(),
                Name = stop.Name
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StopDTO stop, string? stopId)
        {
            if (stopId is null) return;

            var stopInDb = await _context.Stops.FindAsync(stopId);

            if (stopInDb is null) return;

            stopInDb.Code = stop.Code;
            stopInDb.Latitude = stop.Latitude.ToString();
            stopInDb.Longitude = stop.Longitude.ToString();
            stopInDb.Name = stop.Name;

            _context.Stops.Update(stopInDb);

            await _context.SaveChangesAsync();
        }

        public async Task<StopDTO?> GetAsync(string stopId)
        {
            var stopInDb = await _context.Stops.FindAsync(stopId);

            if (stopInDb is null) return null;

            return new StopDTO()
            {
                Code = stopInDb.Code,
                Latitude = double.Parse(stopInDb?.Latitude ?? "0"),
                Longitude = double.Parse(stopInDb?.Longitude ?? "0"),
                Name = stopInDb?.Name
            };
        }

        public async Task DeleteAsync(string? stopId)
        {
            if (stopId is null) return;

            var stopInDb = await _context.Stops.FindAsync(stopId);

            if (stopInDb is not null)
            {
                _context.Stops.Remove(stopInDb);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<StopDTO> Search(string search)
        {
            return _context.Stops
                .Where(stop => stop.Code.ToUpper().Contains(search.ToUpper()))
                .Select(stop => new StopDTO()
                {
                    Id = stop.Id,
                    Code = stop.Code,
                    Latitude = double.Parse(stop.Latitude),
                    Longitude = double.Parse(stop.Longitude),
                    Name = stop.Name
                })
                .ToList();
        }

        public IEnumerable<RouteStop> GetAll()
        {
            return _context.Stops.Select(x => new RouteStop(x.Name, x.Id)).ToList();
        }
    }
}