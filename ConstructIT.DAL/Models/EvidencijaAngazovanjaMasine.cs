﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructIT.DAL.Models
{
    public class EvidencijaAngazovanjaMasine
    {
        public int EvidencijaAngazovanjaMasineID { get; set; }

        [Required]
        [ForeignKey("Zadatak")]
        [Column(Order = 1)]
        public int ProjekatID { get; set; }

        [Required]
        [ForeignKey("Zadatak")]
        [Column(Order = 2)]
        public int ZadatakID { get; set; }

        [Required]
        [ForeignKey("Masina")]
        public int MasinaID { get; set; }

        [Required(ErrorMessage = "'Datum' ne sme biti neodređen!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum")]
        public DateTime EvidAngMasDatum { get; set; }

        [Required(ErrorMessage = "'Vreme od' ne sme biti neodređeno!")]
        [StringLength(5), RegularExpression(@"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Nepravilan unos vremena!")]
        [Display(Name = "Vreme od")]
        public String EvidAngMasVremeOd { get; set; }

        [Required(ErrorMessage = "'Vreme do' ne sme biti neodređeno!")]
        [StringLength(5), RegularExpression(@"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Nepravilan unos vremena!")]
        [Display(Name = "Vreme do")]
        public String EvidAngMasVremeDo { get; set; }


        public virtual Zadatak Zadatak { get; set; }
        public virtual Masina Masina { get; set; }
    }
}
