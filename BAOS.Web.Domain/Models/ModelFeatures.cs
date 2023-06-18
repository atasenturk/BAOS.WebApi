using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAOS.Web.Domain.Models
{
    public class ModelFeatures
    {
        public int userId { get; set; }
        public string answers { get; set; }
        public List<int> features { get; set; }
    }
}
