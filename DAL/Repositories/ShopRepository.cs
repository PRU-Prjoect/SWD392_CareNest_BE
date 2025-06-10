using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ShopRepository : GenericRepository<Shop>, IShopRepository
    {
        private readonly ApplicationDbContext _context;
        public ShopRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Shop> GetShopByIdAsync(Guid id)
        {
            return await _context.Shop
                .Include(s => s.sub_address)
                .FirstAsync(s => s.account_id == id);
        }

        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            return await _context.Shop
                .Include(s => s.sub_address)
                .ToListAsync();
        }
    }
}
