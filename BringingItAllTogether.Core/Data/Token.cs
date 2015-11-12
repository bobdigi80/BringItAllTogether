using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BringingItAllTogether.Core.Data
{
    public class Token : BaseEntity
    {
        public int UserId { get; set; }
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        public virtual User User { get; set; }
    }
}
