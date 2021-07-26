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

    public class ResultModel
    {
        public bool HasError { get; set; }
        public string? Messages { get; set; }
        public object? Data { get; set; }

    }
}
