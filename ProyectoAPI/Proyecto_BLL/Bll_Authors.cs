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
   public  class Bll_Authors: I_Bll_Authors
    {

        private readonly IUnitOfWork unitofwork;
        private readonly IConfiguration configuration;
        private readonly ILogger<Bll_Authors> logger;


        public Bll_Authors(IUnitOfWork _unitofwork, IConfiguration _configuration, ILogger<Bll_Authors> logger)
        {
            this.unitofwork = _unitofwork;
            this.configuration = _configuration;
            this.logger = logger;
        }


        public async Task<ResultModel> AuthorAdd(Authors AuthorModel)
        {
            ResultModel ResultModel = new ResultModel();

            try
            {
                Authors Author = (await unitofwork.GetRepository<Authors>().Get(x => x.FirstName == AuthorModel.FirstName)).FirstOrDefault();

                if (Author != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "FirstName ya Existe";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                unitofwork.GetRepository<Authors>().Add(AuthorModel);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Author No Creado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "Author Creado Con Exito";
                ResultModel.Data = string.Empty;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Guardar Author: {Error.Message}";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }

        }

        public async Task<ResultModel> AuthorList()
        {

            ResultModel ResultModel = new ResultModel();

            try
            {
                IEnumerable<Authors> List = await unitofwork.GetRepository<Authors>().Get();
                ResultModel.HasError = false;

                ResultModel.Messages = "Author Listados Con Exito";
                ResultModel.Data = List;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Listando Author";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public async Task<ResultModel> GetAuthorByAuthorId(int Id)
        {
            ResultModel ResultModel = new ResultModel();

            try
            {
                ResultModel Result = await AuthorList();

                if (!Result.HasError)
                {
                    Authors[] List = (Authors[])Result.Data;

                    Authors Author = List.Where(x => x.Id == Id).FirstOrDefault();

                    if (Author != null)
                    {

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Author Encontrado";
                        ResultModel.Data = Author;
                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Author No Encontrado";
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

        public async Task<ResultModel> AuthorUpdate(Authors AuthorModel)
        {
            ResultModel ResultModel = new ResultModel();
            try
            {
                Authors AuthorUpdate = (await unitofwork.GetRepository<Authors>().Get(x => x.Id == AuthorModel.Id)).FirstOrDefault();

                if (AuthorUpdate == null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Usuario NO Encontrado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                Authors Author = (await unitofwork.GetRepository<Authors>().Get(x => x.FirstName == AuthorModel.FirstName && x.Id != AuthorModel.Id)).FirstOrDefault();

                if (Author != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "FirstName ya Existe";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                unitofwork.GetRepository<Authors>().Update(AuthorUpdate);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Author No Modificado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "Author Modificado Con Exito";
                ResultModel.Data = string.Empty;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Modificar Author: {Error.Message}";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public Task<ResultModel> AuthorAdd(Bll_Authors AuthorsModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel> AuthorUpdate(Bll_Authors AuthorsModel)
        {
            throw new NotImplementedException();
        } 
    }
}
