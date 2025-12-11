using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SSL.Sample.Models
{
    public class CommonDropDown
    {
        public string Name { get; set; }
        public string Value { get; set; }

        #region "Autocomplete"
        public int Id { get; set; }
        public int Count { get; set; }
        #endregion "Autocomplete"
    }
}
