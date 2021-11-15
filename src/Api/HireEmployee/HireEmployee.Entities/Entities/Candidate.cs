using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireEmployee.Entities
{
    public class Candidate
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Skill { get; set; }
        public int Experience { get; set; }
        public string Resume { get; set; }
        public int ExpectedSalary { get; set; }
        public bool Review { get; set; }
        public bool PhoneScreening { get; set; }
        public bool Interview { get; set; }
        public bool OfferAccepted { get; set; }
        public Job Job { get; set; }
    }
}
