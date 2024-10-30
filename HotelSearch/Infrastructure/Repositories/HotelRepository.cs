using System.Collections.Generic;
using System.Threading.Tasks;
using HotelSearch.Core.Entities;
using HotelSearch.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using HotelSearch.Infrastructure.Data;

namespace HotelSearch.Infrastructure.Repositories
{
    public class HotelRepository : IRepository<Hotel>
    {
        private readonly HotelDbContext _context;

        public HotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> AddAsync(Hotel entity)
        {
            _context.Hotels.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Hotel> GetByIdAsync(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> UpdateAsync(Hotel entity)
        {
            _context.Hotels.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return false;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
