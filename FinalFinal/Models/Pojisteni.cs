using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalFinal.Models;

public  class Pojisteni
{
    public int Id { get; set; }
    public string TypPojistky { get; set; } = null!;
    [Required(ErrorMessage = "Je nutné vyplnit pojištěný předmět.")]
    public string PojistenyPredmet { get; set; } = null!;
    [Required(ErrorMessage = "Je nutné vyplnit hodnotu.")]
    public double Hodnota { get; set; }
    [Required(ErrorMessage = "Je nutné vyplnit platnost od.")]
    public DateTime PlatnostOd { get; set; }
    [Required(ErrorMessage = "Je nutné vyplnit platnost do.")]
    public DateTime PlatnostDo { get; set; }

    [Display(Name ="Uzivatel")]
    public virtual int UzivatelId { get; set; } 
    [ForeignKey("UzivatelId")]
    public virtual Kontakt Uzivatel { get; set; } = null!;
    [ForeignKey("Zakladatel")]
    public string ZakladatelId { get; set; }
    public virtual IdentityUser Zakladatel { get; set; } = null!;


}
