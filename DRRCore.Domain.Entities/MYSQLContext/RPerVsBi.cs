﻿using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.MYSQLContext;

public partial class RPerVsBi
{
    public string PeCodigo { get; set; } = null!;

    public int BiOrden { get; set; }

    public string? BiTipo { get; set; }

    public string? BiTipoIng { get; set; }

    public string? BiNombre { get; set; }

    public string? BiNombreIng { get; set; }

    public double? BiValor { get; set; }
}
