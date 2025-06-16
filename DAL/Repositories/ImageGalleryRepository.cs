using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ImageGalleryRepository : GenericRepository<ImageGallery>, IImageGalleryRepository
    {
        private ApplicationDbContext _context;
        public ImageGalleryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
