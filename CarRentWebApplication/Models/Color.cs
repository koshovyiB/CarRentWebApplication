using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRentWebApplication
{
    public partial class Color
    {
        public Color()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Поле не повинно бути порожнім")]
        [Display(Name ="Колір")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
