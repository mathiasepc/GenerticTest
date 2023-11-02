using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerticTest.Model
{
    public class CompanyModel
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
        public DateTime Created { get; set; }
    }
}
