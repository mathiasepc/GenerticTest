using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerticTest.Model
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }

        public CarModel CreateCar(int id, string name, string licensePlate)
        {
            return new CarModel
            {
                Id = id,
                Name = name,
                LicensePlate = licensePlate
            };
        }

    }
}
