using ProjetoCoreWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.Interfaces
{
    public interface IRole
    {
        Task<List<RoleDTO>> GetAll();
    }
}
