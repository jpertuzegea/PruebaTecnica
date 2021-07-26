using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_DAO.Entitys
{
    public class Authors
    {
        [Key]
        public int? Id { get; set; }
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 

        public virtual List<Books> Books { get; set; }
    }
}
