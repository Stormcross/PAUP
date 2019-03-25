using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStudenti2.Models
{
    public class StudentiDb
    {
        private List<Student> lista = new List<Student>();

        public StudentiDb()
        {

            //dodamo prvog studenta
            lista.Add(new Student
            {
                Id = 1,
                Ime = "Jurica",
                Prezime = "Juric",
                Spol = 'M',
                Oib = "12345678901",
                DatumRodenja = new DateTime(1989, 6, 16),
                GodinaStudija = GodinaStudija.Druga,
                RedovniStudent = true
            });

            //dodamo drugog studenta
            lista.Add(new Student
            {
                Id = 2,
                Ime = "Marko",
                Prezime = "Maric",
                Spol = 'M',
                Oib = "12345678902",
                DatumRodenja = new DateTime(1993, 1, 12),
                GodinaStudija = GodinaStudija.Prva,
                RedovniStudent = false
            });

            //dodamo treceg studenta
            lista.Add(new Student
            {
                Id = 3,
                Ime = "Nia",
                Prezime = "Anic",
                Spol = 'F',
                Oib = "12345678903",
                DatumRodenja = new DateTime(1995, 2, 20),
                GodinaStudija = GodinaStudija.Treca,
                RedovniStudent = true
            });

        }
        //vracamo listu studenata
        public List<Student> VratiListu()
        {
            return lista;
        }

        public void AzurirajStudenta(Student s)
        {
            int index = lista.FindIndex(x => x.Id == s.Id);
            lista[index] = s;
        }
    }
}