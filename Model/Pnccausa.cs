﻿using System;
using System.Collections.Generic;

namespace Calidad.Model;

public partial class Pnccausa
{
    public int IdCausa { get; set; }

    public string Cnombre { get; set; } = null!;

    public string? Cdescri { get; set; }

    public bool Cestado { get; set; }

    public virtual ICollection<ProNoCon> ProNoCons { get; set; } = new List<ProNoCon>();
}
