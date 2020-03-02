using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyProject.Infrastructure.Entities;

namespace MyProject.Infrastructure
{
    public class MyProjectContext : DbContext
    {
        private DbSet<Product> Products { get; set; }

        public MyProjectContext(DbContextOptions options) :
            base(options)
        { }
    }
}
