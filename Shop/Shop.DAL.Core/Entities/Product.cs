using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Description { get; set; }
        public float Cost { get; set; }
    }
}
