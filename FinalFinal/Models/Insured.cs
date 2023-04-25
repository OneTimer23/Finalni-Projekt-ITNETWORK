using System;
using System.Collections.Generic;

namespace FinalFinal.Models;

public partial class Insured
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Adress { get; set; } = null!;
}
