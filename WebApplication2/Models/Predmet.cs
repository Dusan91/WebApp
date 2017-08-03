using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public enum TipPredmeta
    {
        Izborni,
        Redovni
    }

    public class Predmet
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Opis { get; set; }

        public float Ocena { get; set; }

        public TipPredmeta Tip { get; set; }

        public virtual Nastavnik Nastavnik { get; set; }

        public virtual ICollection<Student> Studenti { get; set; }
    }
}