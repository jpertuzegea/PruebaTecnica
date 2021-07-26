using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_DAO.Entitys
{
    public class Books
    {
        [Key]
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Excerpt { get; set; }
        public string PublishDate { get; set; }


        public virtual Authors Authors { get; set; }
         
    }
}
