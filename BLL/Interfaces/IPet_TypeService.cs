using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPet_TypeService
    {
        Task<List<Pet_TypeDTO>> GetAllAsync();
    }
}
