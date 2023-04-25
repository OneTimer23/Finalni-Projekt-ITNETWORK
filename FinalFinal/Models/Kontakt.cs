using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FinalFinal.Models;

public partial class Kontakt
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Je nutné vyplnit jméno.")]
    public string Jmeno { get; set; } = null!;
    [Required(ErrorMessage = "Je nutné vyplnit příjmení.")]
    public string Prijmeni { get; set; } = null!;
    [Required(ErrorMessage = "Je nutné vyplnit PSČ.")]
    public string SmerovaciCislo { get; set; } = null!;
    [Required(ErrorMessage = "Je nutné vyplnit Ulici.")]
    public string Ulice { get; set; } = null!;
    [Required(ErrorMessage = "Je nutné vyplnit Město.")]
    public string Mesto { get; set; } = null!;
    [Required(ErrorMessage = "Je nutné vyplnit telefonní číslo.")]
    public string TelCislo { get; set; } = null!;
    [Required(ErrorMessage = "Je nutné vyplnit email.")]
    public string Email { get; set; } = null!;

    public virtual ICollection<Pojisteni> Pojistenis { get; } = new List<Pojisteni>();
    [ForeignKey("Zakladatel")]
    public string ZakladatelId { get; set; }
    public virtual IdentityUser Zakladatel { get; set; } = null!;

    public string PrijmeniPlusJmeno
    {
        get
        {
            return this.Prijmeni + " " + this.Jmeno;
        }
    }
    
}

    



