//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Enero 2021</date>
//-----------------------------------------------------------------------

namespace Proyecto_DAO
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Proyecto_DAO.Entitys;
    using System;

    public class ContextDB : DbContext
    {
        private readonly IConfiguration _configuration;

        public ContextDB(DbContextOptions<ContextDB> options, IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        public ContextDB()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  optionsBuilder.UseSqlServer("Server=localhost;Database=Prueba;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("BDConnetion"),
                opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds)
                );
        }


        public DbSet<Users> Users { get; set; }
        public DbSet<Books> Books { get; set; } 

        //
    }
}
