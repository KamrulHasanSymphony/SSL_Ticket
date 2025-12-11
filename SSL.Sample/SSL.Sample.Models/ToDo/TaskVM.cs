using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class TaskVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Remarks { get; set; }

        public bool IsActive { get; set; }

        public bool IsArchive { get; set; }

        public string Operation { get; set; }

        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFrom { get; set; }
        public string LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateFrom { get; set; }


        public int SequenceNo { get; set; }


    }
}
