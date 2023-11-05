using Microsoft.EntityFrameworkCore;
using dataApi.Models;
using System;

namespace dataApi.Database
{
    public class Databasecontext:DbContext
    {
        public Databasecontext(DbContextOptions<Databasecontext> options) : base(options)
        {

        }
        public DbSet<User> Users{ get; set; }
    }
}
