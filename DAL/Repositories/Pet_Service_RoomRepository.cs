using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Pet_Service_RoomRepository : GenericRepository<Pet_Service_Room>, IPet_Service_RoomRepository
    {
        private readonly ApplicationDbContext _context;
        public Pet_Service_RoomRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }

}
