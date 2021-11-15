using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class UserDetails
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AccountNumber { get; set; }
        public string DocumentName { get; set; }
    }
}
