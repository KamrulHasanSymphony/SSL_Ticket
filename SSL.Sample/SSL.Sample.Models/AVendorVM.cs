using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace SSL.Sample.SSL.Sample.Models
{
    public class AVendorVM
    {
        public AVendorVM()
        {
            AttachmentsList = new List<VendorAttachments>();
        }
        public int VendorId { get; set; }
        [Display(Name = "Code")]
        public string VendorCode { get; set; }
        [Display(Name = "Name")]
        public string VendorName { get; set; }
        [Display(Name = "Address")]
        public string VendorAddress { get; set; }
        [Display(Name = "Email")]
        public string VendorEmail { get; set; }
        [Display(Name = "Image")]
        public string BINCertificate { get; set; }
        public List<VendorAttachments> AttachmentsList { get; set; }
    }
}
