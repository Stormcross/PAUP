using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcStudenti2.Models
{
    public class Student
    {
        //Prikazivanje identiteta
        [Display(Name = "ID Studenta")]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public char Spol { get; set; }

        [Display (Name = "OIB")]
        public string Oib { get; set; }
        
        [Display(Name = "Datum rođenja")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodenja { get; set; }

        [Display(Name ="Godina studija")]
        // ako želimo da godina studija nije obavezan podatak moramo tip int primjeniti
        // u nullbilni int(int?). to je integer koji moće primiti null vrijednost
        // kod stringova to nije potreno jer stringovi su već nulabilni samo po sei
        public GodinaStudija? GodinaStudija { get; set; }

        [Display(Name ="Redovni student")]
        public bool RedovniStudent { get; set; }
    }
}

namespace MvcStudenti2.Models
{
    public enum GodinaStudija
    {
        Prva = 1,
        Druga = 2,
        Treca = 3,
        Cetvrta = 4,
        Peta = 5
    }
}
