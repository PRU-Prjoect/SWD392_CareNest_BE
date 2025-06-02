using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Sub_AddressRepository : GenericRepository<Sub_Address>, ISub_AddressRepository
    {
        private readonly ApplicationDbContext _context;
        public Sub_AddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
