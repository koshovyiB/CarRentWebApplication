using System;
using System.Collections.Generic;

namespace CarRentWebApplication
{
    public partial class Brand
    {
        public Brand()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
