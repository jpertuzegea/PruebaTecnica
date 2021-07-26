using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Proyecto_BLL.Interfaces;
using Proyecto_DAO.Entitys;
using Proyecto_DAO.Interfaces;
using Proyecto_DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Proyecto_BLL
{
    public class Bll_Books : I_Bll_Books
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IConfiguration configuration;
        private readonly ILogger<Bll_Books> logger;


        public Bll_Books(IUnitOfWork _unitofwork, IConfiguration _configuration, ILogger<Bll_Books> logger)
        {
            this.unitofwork = _unitofwork;
            this.configuration = _configuration;
            this.logger = logger;
        }


        public async Task<ResultModel> BookAdd(Books BookModel)
        {
            ResultModel ResultModel = new ResultModel();

            try
            {
                Books Book = (await unitofwork.GetRepository<Books>().Get(x => x.Title == BookModel.Title)).FirstOrDefault();

                if (Book != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Title ya Existe";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                unitofwork.GetRepository<Books>().Add(BookModel);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Book No Creado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "Book Creado Con Exito";
                ResultModel.Data = string.Empty;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Guardar Book: {Error.Message}";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }

        }

        public async Task<ResultModel> BookList()
        {

            ResultModel ResultModel = new ResultModel();

            try
            {
                IEnumerable<Books> List = await unitofwork.GetRepository<Books>().Get();
                ResultModel.HasError = false;

                ResultModel.Messages = "Book Listados Con Exito";
                ResultModel.Data = List;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Listando Book";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public async Task<ResultModel> GetBookByBookId(int Id)
        {
            ResultModel ResultModel = new ResultModel();

            try
            {
                ResultModel Result = await BookList();

                if (!Result.HasError)
                {
                    List<Books> List = (List<Books>)Result.Data;

                    Books Book = List.Where(x => x.Id == Id).FirstOrDefault();

                    if (Book != null)
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Book Encontrado";
                        ResultModel.Data = Book;
                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Book No Encontrado";
                        ResultModel.Data = string.Empty;
                    }
                }

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Buscando Usuario";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public async Task<ResultModel> BookUpdate(Books BookModel)
        {
            ResultModel ResultModel = new ResultModel();
            try
            {
                Books BookUpdate = (await unitofwork.GetRepository<Books>().Get(x => x.Id == BookModel.Id)).FirstOrDefault();

                if (BookUpdate == null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Usuario NO Encontrado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                Books Book = (await unitofwork.GetRepository<Books>().Get(x => x.Title == BookModel.Title && x.Id != BookModel.Id)).FirstOrDefault();

                if (Book != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Nombre ya Existe";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                BookUpdate.Title = BookModel.Title;
                BookUpdate.Description = BookModel.Description;
                BookUpdate.PageCount = BookModel.PageCount;
                BookUpdate.Excerpt = BookModel.Excerpt;
                BookUpdate.PublishDate = BookModel.PublishDate;

                unitofwork.GetRepository<Books>().Update(BookUpdate);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Book No Modificado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "Book Modificado Con Exito";
                ResultModel.Data = string.Empty;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Modificar Book: {Error.Message}";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public async Task<ResultModel> SelectBooks()
        {
            ResultModel ResultModel = new ResultModel();
            IEnumerable<Books> List = await unitofwork.GetRepository<Books>().Get();
             
            var select = List.Select(x => new { Value = x.Id, Text = x.Title }).ToList();
             
            ResultModel.HasError = false;
            ResultModel.Messages = "Listado Books Con Exito";
            ResultModel.Data = new  SelectList(select, "Value", "Text");

            return ResultModel;
        }
    }
}
