using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class CRMDocumentVM : RegularVM
    {
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        [Display(Name = "FileName")]
        public IFormFile FileName { get; set; }
        //public HttpPostedFileBase FileName { get; set; }
        public string FileOrginalName { get; set; }
        public int Version { get; set; }
        public string Notes { get; set; }

        public decimal Value { get; set; }
    }
}
