using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Domain.Models.Common
{
    public class BaseClass
    {
        public virtual Guid? Id { get; set; }
        public DateTime? Register { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? Inactive { get; set; }
    }
}
