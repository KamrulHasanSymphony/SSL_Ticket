using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Models
{
    public class AuditIssueAttachments
    {

        public int Id { get; set; }

        public int? T_TaskId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string CreatedBy { get; set; }

        public bool? IsActive { get; set; }

        private string _displayName;

        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(_displayName)) return _displayName;

                if (FileName is not null)
                    return Path.GetFileNameWithoutExtension(this.FileName).Split("_shp_")[0] + Path.GetExtension(this.FileName);

                return "";
            }


            set
            {
                this._displayName = value;
            }
        }

    }
}
