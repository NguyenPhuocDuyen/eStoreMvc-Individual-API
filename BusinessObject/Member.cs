using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BusinessObject
{
    public class Member : IdentityUser
    {
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
