//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HireIN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Applicant
    {
        public int ApplicantId { get; set; }
        public Nullable<int> CandidateId { get; set; }
        public Nullable<int> VacancyId { get; set; }
        public string Status { get; set; }
    }
}