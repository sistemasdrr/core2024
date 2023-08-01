﻿using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.MYSQLContext;

public partial class RPerVsBc
{
    public string PeCodigo { get; set; } = null!;

    public int BcOrden { get; set; }

    public string? BcNombre { get; set; }

    public string? BcSector { get; set; }

    public string? BcTelefo { get; set; }

    public string? BcNroCta { get; set; }

    public string? BcMonNac { get; set; }

    public string? BcMonExt { get; set; }
}
