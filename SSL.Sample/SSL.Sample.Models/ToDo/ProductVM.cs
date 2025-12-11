using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ProductVM : RegularVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Attachment { get; set; }

        public string Operation { get; set; }



    }
}
