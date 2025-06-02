using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Service_TypeRepository : GenericRepository<Service_Type>, IService_TypeRepository
    {
        private readonly ApplicationDbContext _context;

        public Service_TypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
