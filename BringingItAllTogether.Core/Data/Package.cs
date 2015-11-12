using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BringingItAllTogether.Core.Data
{
    public class Package : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
