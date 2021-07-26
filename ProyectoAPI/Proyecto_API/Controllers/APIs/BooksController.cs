using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_BLL.Interfaces;
using Proyecto_DAO.Entitys;
using Proyecto_DAO.Models;
using System.Threading.Tasks; 

namespace Proyecto_API.Controllers.APIs
{
    [Route("api/Books")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BooksController : Controller
    {
        private readonly I_Bll_Books I_Bll_Books;

        public BooksController(I_Bll_Books _I_Bll_Books)
        {
            this.I_Bll_Books = _I_Bll_Books;
        }

        [HttpPost("BookAdd")]
        public async Task<ActionResult<ResultModel>> BookAdd([FromBody] Books BookModel)
        {
            return await I_Bll_Books.BookAdd(BookModel);
        }

        [HttpGet("BookList")]
        public async Task<ActionResult<ResultModel>> BookList()
        {
            return await I_Bll_Books.BookList();
        }

        [HttpPost("GetBookByBookId")]
        public async Task<ActionResult<ResultModel>> GetBookByBookId([FromBody] int BookId)
        {
            return await I_Bll_Books.GetBookByBookId(BookId);
        }

        [HttpPut("BookUpdt")]
        public async Task<ActionResult<ResultModel>> BookUpdt([FromBody] Books BookModel)
        {
            return await I_Bll_Books.BookUpdate(BookModel);
        }

        [HttpGet("SelectBooks")]
        public async Task<ActionResult<ResultModel>> SelectBooks()
        { 
            return await I_Bll_Books.SelectBooks(); 
        }


    }
}
