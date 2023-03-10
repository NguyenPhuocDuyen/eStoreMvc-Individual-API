using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Cart
    {
        [Key] public int CartId { get; set; }
        [Required] public string MemberId { get; set; }
        [Required] public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public Product Product { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; }
    }
}
