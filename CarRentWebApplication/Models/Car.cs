using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRentWebApplication
{
    public partial class Car
    {
        public Car()
        {
            CarImages = new HashSet<CarImage>();
            Rentals = new HashSet<Rental>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Поле не повинно бути порожнім")]
        [Display(Name ="Модель")]
        public string Model { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Рік")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ціна за добу")]
        public int DailyPrice { get; set; }
        [Display(Name = "Колір")]

        public int ColorId { get; set; }
        [Display(Name = "Бренд")]

        public int BrandId { get; set; }
        [Display(Name = "Бренд")]
        public virtual Brand Brand { get; set; } = null!;
        [Display(Name = "Колір")]
        public virtual Color Color { get; set; } = null!;
        public virtual ICollection<CarImage> CarImages { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
