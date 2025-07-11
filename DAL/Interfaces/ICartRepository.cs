﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Task<Cart?> GetByCustomerIdAsync(Guid id);
    }
}
