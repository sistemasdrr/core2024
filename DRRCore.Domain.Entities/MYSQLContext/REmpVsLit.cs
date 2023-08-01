﻿using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.MYSQLContext;

public partial class REmpVsLit
{
    public string EmCodigo { get; set; } = null!;

    public int LiOrden { get; set; }

    public string? LiFecha { get; set; }

    public string? LiFechaIng { get; set; }

    public string? LiAccion { get; set; }

    public string? LiAccionIng { get; set; }

    public string? LiDemand { get; set; }

    public string? LiDemandIng { get; set; }

    public string? LiEstado { get; set; }
}
