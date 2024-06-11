using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DominicoBus.Db;
using DominicoBus.DataTransfer;
using Microsoft.EntityFrameworkCore;

namespace DominicoBus.Services
{
    public class RouteService
    {
        private readonly AppDbContext _context;
        public RouteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RouteDTO route)
        {
            await _context.Routes.AddAsync(new Models.Route()
            {
                Id = Guid.NewGuid().ToString(),
                Name = route.Name,
                Stops = route.Stops
                    .Select(stop => _context.Stops.Find(stop.Id)).ToList()
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RouteDTO route, string? routeId)
        {
            if (routeId is null) return;

            var routeOnDb = await _context.Routes.FindAsync(routeId);

            if (routeOnDb is null) return;

            routeOnDb.Name = route.Name;
            routeOnDb.Stops = route.Stops
                .Select(stop => _context.Stops.Find(stop.Id)).ToList();

            _context.Routes.Update(routeOnDb);

            await _context.SaveChangesAsync();
        }

        public async Task<RouteDTO?> GetAsync(string routeId)
        {
            var routeOnDb = await _context.Routes.FindAsync(routeId);
            if (routeOnDb is null) return null;

            _context.Entry(routeOnDb).Collection(p => p.Stops).Load();

            return new RouteDTO()
            {
                Name = routeOnDb.Name,
                Stops = routeOnDb.Stops.Select(x => new RouteStop(x.Name, x.Id)).ToList()
            };
        }

        public async Task DeleteAsync(string? routeId)
        {
            if (routeId is null) return;

            var routeOnDb = await _context.Routes.FindAsync(routeId);

            if (routeOnDb is not null)
            {
                _context.Routes.Remove(routeOnDb);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<RouteDTO> Search(string search)
        {
            return _context.Routes
                .Where(route => route.Name.ToUpper().Contains(search.ToUpper()))
                .Include(route => route.Stops)
                .Select(route => new RouteDTO()
                    { Id = route.Id, Name = route.Name, Stops = route.Stops
                        .Select(x => new RouteStop(x.Name, x.Id)).ToList() })
                .ToList();
        }
    }
}