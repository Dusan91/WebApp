using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Student
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Ime")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Index(IsUnique = true)]
        [Display(Name = "Broj Indeksa")]
        public int IndexNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }

        public Gender Pol { get; set; }

        public virtual Address Adresa { get; set; }

        public virtual ICollection<Predmet> Predmeti { get; set; }
    }
}