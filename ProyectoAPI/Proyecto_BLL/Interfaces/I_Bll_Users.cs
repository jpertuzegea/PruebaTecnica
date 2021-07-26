//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Enero 2021</date>
//-----------------------------------------------------------------------


using Proyecto_DAO.Entitys;
using Proyecto_DAO.Models;
using System.Threading.Tasks;

namespace Proyecto_BLL.Interfaces
{
    public interface I_Bll_Users
    {

        Task<ResultModel> UserAdd(Users UsersModel);

        Task<ResultModel> UserList();

        Task<ResultModel> GetUserByUserId(int Id);

        Task<string> GetFullNameByUserId(int? Id);

        Task<ResultModel> UserUpdate(Users UsersModel);

        Task<ResultModel> Sincronizate(); 

        Task<ResultModel> LogIn(LoginModel LoginModel);
         
    }
}
