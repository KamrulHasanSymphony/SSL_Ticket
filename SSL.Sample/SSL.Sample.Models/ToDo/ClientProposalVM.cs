using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ClientProposalVM : RegularVM
    {
        public string Id{get;set;}
        [Display(Name = "Client Name")]
        public string ClientId{get;set;}
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Product Name")]
        public string ProductId{get;set;}
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPersonId{get;set;}
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }
        [Display(Name = "Is Send Proposal")]
        public bool IsSendProposal{get;set;}
        [Display(Name = "Is Send Price")]
        public bool IsSendPrice { get; set; }
        [Display(Name = "Quotation Amount")]
        public string QuotationAmount{get;set;}
        [Display(Name = "Email Address")]
        public string SendEmailAddress{get;set;}
        public string Attachment { get; set; }
        public string Attachment2 { get; set; }
        public string Attachment3 { get; set; }
        public string Operation { get; set; }
        public string ProposalSend { get; set; }
        [Display(Name = "Proposal Date")]
        public string ProposalDate { get; set; }
        public string ProposalDateFrom { get; set; }
        public string ProposalDateTo { get; set; }

    }
}
