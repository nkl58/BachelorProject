﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructIT.DAL.Models
{
    public class Projekat
    {
        public int ProjekatID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(128, ErrorMessage = "'Naziv Projekta' ne sme biti duži od 128 karaktera!"), Required(ErrorMessage = "'Naziv Projekta' ne sme biti prazan!")]
        [Display(Name = "Naziv projekta")]
        public String ProjekatNaziv { get; set; }

        [StringLength(1024, ErrorMessage = "'Opis projekta' ne sme biti duži od 1024 karaktera!")]
        [Display(Name = "Opis projekta")]
        [DataType(DataType.MultilineText)]
        public String ProjekatOpis { get; set; }

        [StringLength(128, ErrorMessage = "'Adresa' ne sme biti duža od 128 karaktera!")]
        [Display(Name = "Adresa")]
        public String ProjekatAdresa { get; set; }


        public ICollection<Zadatak> Zadaci { get; set; }

        [InverseProperty("Projekti")]
        public ICollection<Korisnik> Korisnici { get; set; }

        [InverseProperty("KlijentovProjekat")]
        public ICollection<Korisnik> Klijenti { get; set; }
        public ICollection<Galerija> Galerije { get; set; }

        public List<int> OdabraniKorisnici { get; set; }
    }
}
