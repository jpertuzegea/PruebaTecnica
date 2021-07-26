using Microsoft.AspNetCore.Mvc;
using Proyecto_BLL.Interfaces;
using Proyecto_DAO.Entitys;
using Proyecto_DAO.Models;
using System.Threading.Tasks;

namespace Proyecto_API.Controllers.APIs
{
    [Route("api/Authors")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class AuthorsController : Controller
    {
        private readonly I_Bll_Authors I_Bll_Authors;

        public AuthorsController(I_Bll_Authors _I_Bll_Authors)
        {
            this.I_Bll_Authors = _I_Bll_Authors;
        }

        [HttpPost("AuthorAdd")]
        public async Task<ActionResult<ResultModel>> AuthorAdd([FromBody] Authors AuthorModel)
        {
            return await I_Bll_Authors.AuthorAdd(AuthorModel);
        }

        [HttpGet("AuthorList")]

        public async Task<ActionResult<ResultModel>> AuthorList()
        {
            return await I_Bll_Authors.AuthorList();
        }

        [HttpPost("GetAuthorByAuthorId")]
        public async Task<ActionResult<ResultModel>> GetAuthorByAuthorId([FromBody] int AuthorId)
        {
            return await I_Bll_Authors.GetAuthorByAuthorId(AuthorId);
        }

        [HttpPut("AuthorUpdt")]
        public async Task<ActionResult<ResultModel>> AuthorUpdt([FromBody] Authors AuthorModel)
        {
            return await I_Bll_Authors.AuthorUpdate(AuthorModel);
        }
 

    }
}
