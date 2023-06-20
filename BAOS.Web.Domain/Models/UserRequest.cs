using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAOS.Web.Domain.Models
{
    public class UserRequest
    {
        public int RequestId { get; set; }
        public int Protocol { get; set; }
        public string Answers { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
