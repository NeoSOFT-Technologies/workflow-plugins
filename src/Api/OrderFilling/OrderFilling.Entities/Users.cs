using System;
using System.ComponentModel.DataAnnotations;

namespace OrderFilling.Entities
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Credit { get; set; }
    }
}
