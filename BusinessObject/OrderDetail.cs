using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderDetail
    {
        [Required] public int OrderId { get; set; }
        [Required] public int ProductId { get; set; }
        [Required] public double UnitPrice { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public double Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
