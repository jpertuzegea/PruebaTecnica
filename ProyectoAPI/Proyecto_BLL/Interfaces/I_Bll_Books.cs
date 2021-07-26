using Proyecto_DAO.Entitys;
using Proyecto_DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_BLL.Interfaces
{
    public interface I_Bll_Books
    {
        Task<ResultModel> BookAdd(Books BooksModel);

        Task<ResultModel> BookList();

        Task<ResultModel> GetBookByBookId(int Id);
         
        Task<ResultModel> BookUpdate(Books BooksModel);

        Task<ResultModel> SelectBooks(); 

    }
}
