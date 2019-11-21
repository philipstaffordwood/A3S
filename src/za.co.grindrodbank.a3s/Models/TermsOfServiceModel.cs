using System;
using System.Collections.Generic;

namespace za.co.grindrodbank.a3s.Models
{
    public class TermsOfServiceModel : AuditableModel
    {
        public Guid Id { get; set; }
        public string AgreementName { get; set; }
        public string Version { get; set; }
        public byte[] AgreementFile { get; set; }

        public List<TeamModel> Teams { get; set; }

    }
}
