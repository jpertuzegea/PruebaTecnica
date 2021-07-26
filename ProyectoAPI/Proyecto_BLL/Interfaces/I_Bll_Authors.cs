using Proyecto_DAO.Entitys;
using Proyecto_DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_BLL.Interfaces
{
    public interface I_Bll_Authors
    {
        Task<ResultModel> AuthorAdd(Authors AuthorsModel);

        Task<ResultModel> AuthorList();

        Task<ResultModel> GetAuthorByAuthorId(int Id);

        Task<ResultModel> AuthorUpdate(Authors AuthorsModel);
    }
}
