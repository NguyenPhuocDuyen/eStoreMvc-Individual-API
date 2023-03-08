using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Order
    {
        [Key] public int OrderId { get; set; }
        [Required] public string MemberId { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        private DateTime _requiredDate;
        [Required]
        [Display(Name = "Required Date")]
        public DateTime RequiredDate
        {
            get => _requiredDate;
            set
            {
                if (value < OrderDate)
                {
                    throw new ValidationException("Required date must be greater than or equal to order date.");
                }
                _requiredDate = value;
            }
        }

        private DateTime _shippedDate;
        [Required]
        [Display(Name = "Shipped Date")]
        public DateTime ShippedDate
        {
            get => _shippedDate;
            set
            {
                if (value < OrderDate)
                {
                    throw new ValidationException("Shipped date must be greater than or equal to order date.");
                }
                _shippedDate = value;
            }
        }

        [ForeignKey(nameof(MemberId))]
        public virtual Member Member { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
