using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjetoCoreWebAPI.DTO
{
    public class PagedListDTO<T>
    {
        public int TotalRegistros { get; set; }

        public int TotalPaginas { get; set; }

        public int PaginaAtual { get; set; }

        public PagedList<T> Results { get; set; }
    }
}
