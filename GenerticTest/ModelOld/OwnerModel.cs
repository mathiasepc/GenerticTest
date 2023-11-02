using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerticTest.Model
{
    public class OwnerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public OwnerModel CreateOwner(int id, string name, int age)
        {
            return new OwnerModel
            {
                Id = id,
                Name = name,
                Age = age
            };
        }
    }
}
