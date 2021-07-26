using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_BLL.Interfaces;
using Proyecto_DAO.Entitys;
using Proyecto_DAO.Models;
using System.Threading.Tasks;

namespace Proyecto_API.Controllers.APIs
{

    [Route("api/Users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {

        private readonly I_Bll_Users I_Bll_Users;

        public UsersController(I_Bll_Users _I_Bll_Users)
        {
            this.I_Bll_Users = _I_Bll_Users;
        }

        [HttpPost("UserAdd")]
        public async Task<ActionResult<ResultModel>> UserAdd([FromBody] Users UserModel)
        {
            return await I_Bll_Users.UserAdd(UserModel);
        }

        [HttpGet("UserList")]
        public async Task<ActionResult<ResultModel>> UserList()
        {
            return await I_Bll_Users.UserList();
        }

        [HttpPost("GetUserByUserId")]
        public async Task<ActionResult<ResultModel>> GetUserByUserId([FromBody] int UserId)
        {
            return await I_Bll_Users.GetUserByUserId(UserId);
        }

        [HttpPut("UserUpdt")]
        public async Task<ActionResult<ResultModel>> UserUpdt([FromBody] Users UserModel)
        {
            return await I_Bll_Users.UserUpdate(UserModel);
        }

        [HttpGet("Sincronizate")]
        public async Task<ActionResult<ResultModel>> Sincronizate()
        {
            return await I_Bll_Users.Sincronizate();
        }

        [HttpPost("LogIn")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel>> LogIn([FromBody] LoginModel LoginModel)
        {
            return await I_Bll_Users.LogIn(LoginModel);
        }


    }
}
