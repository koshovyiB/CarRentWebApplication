using System;
using System.Collections.Generic;

namespace CarRentWebApplication
{
    public partial class CarImage
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string? ImagePath { get; set; }
        public DateTime Date { get; set; }
        public string? ImageName { get; set; }

        public virtual Car Car { get; set; } = null!;
    }
}
