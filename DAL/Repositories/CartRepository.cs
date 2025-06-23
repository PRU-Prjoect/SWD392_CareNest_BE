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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cart?> GetByCustomerIdAsync(Guid id)
        {
            return await _context.Cart
                .Include(a => a.service_Carts).ThenInclude(a=> a.service)
                .FirstOrDefaultAsync(a => a.customer_id == id);
        }
    }
}
