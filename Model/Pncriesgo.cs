﻿using System;
using System.Collections.Generic;

namespace Calidad.Model;

public partial class Pncriesgo
{
    public int IdRiesgo { get; set; }

    public string Rnombre { get; set; } = null!;

    public string? Rdescri { get; set; }

    public bool Restado { get; set; }

    public virtual ICollection<ProNoCon> ProNoCons { get; set; } = new List<ProNoCon>();
}