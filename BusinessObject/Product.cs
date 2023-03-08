using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Product
    {
        [Key] public int ProductId { get; set; }
        [Required] public int CategoryId { get; set; }
        [Required] public string ProductName { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 1")]
        [Required] public double Weight { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 1")]
        [Required] public double UnitPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 1")]
        [Required] public int UnitInStock { get; set; }


        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
