using System;

namespace NewProjectApproval.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectTechnology { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ProjectCost { get; set; }
        public string SubmitterName { get; set; }
        public string SubmitterEmail { get; set; }

    }
}
