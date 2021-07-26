//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Enero 2021</date>
//-----------------------------------------------------------------------

namespace Proyecto_DAO.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Users
    {
        [Key]
        public int? Id { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; } 

    }
}
