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

    public class JWTAuthentication
    {
        public int ExpirationInMinutes { get; set; }
        public string Secret { get; set; }
    }

    public class SentMailConfiguration
    {

        public string EmailSender { get; set; }
        public string PasswordEmailSender { get; set; }
        public string DestinataryEmailAdministrator { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

    }
}
