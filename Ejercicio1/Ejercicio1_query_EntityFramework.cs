var query = _dbContext.Quotes
    .Where(q => q.IsCurrent == 1)
    .Select(q => new
    {
        NombreComprador = q.Buyer.Name, //Nombre del comprador
        CotizacionActual = q.Amount, //Cantidad
        UltimoHistorial = q.QuoteStatusHistory //Estado de la cotizacion
                             .OrderByDescending(h => h.ModifiedAt) //Ordenado por la fecha de cambio
                             .FirstOrDefault()
    })
    .Where(x => x.UltimoHistorial != null)
    .Select(x => new
    {
        NombreComprador = x.NombreComprador,
        CotizacionActual = x.CotizacionActual,
        EstadoActual = x.UltimoHistorial.QuoteStatus.Name,
        QuoteStatusDate = x.UltimoHistorial.QuoteStatusDate
    })
    .ToList();