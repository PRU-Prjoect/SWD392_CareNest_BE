using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Service_CartRepository : GenericRepository<Service_Cart>, IService_CartRepository
    {
        private ApplicationDbContext _context;
        public Service_CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
