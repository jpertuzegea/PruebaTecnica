//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Enero 2021</date>
//-----------------------------------------------------------------------

namespace Proyecto_DAO.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LoginModel
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public bool IsLogued { get; set; }
        public string? Token { get; set; }
        //public string? ExpirationToken { get; set; }

    }
}
