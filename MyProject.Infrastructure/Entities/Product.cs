using System;
using System.Collections.Generic;
using System.Text;
using MyProject.Infrastructure.Interfaces;

namespace MyProject.Infrastructure.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
