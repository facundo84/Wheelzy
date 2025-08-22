//Ejercicio 4
public async Task<List<OrderDTO>> GetOrders(DateTime? dateFrom, DateTime? dateTo, List<int> customerIds, List<int> statusIds, bool? isActive)
{
// your implementation
}

//Implementacion

public async Task<List<OrderDTO>> GetOrders(DateTime? dateFrom, DateTime? dateTo, List<int> customerIds, List<int> statusIds, bool? isActive)
{
    //Se obtienen los datos de la base.
    var query = _dbContext.Orders.AsQueryable();

    // Si se establece el filtro por fechas, se ejecuta el filtro
    if (dateFrom.HasValue)
    {
        query = query.Where(o => o.OrderDate >= dateFrom.Value);
    }
    if (dateTo.HasValue)
    {
        query = query.Where(o => o.OrderDate <= dateTo.Value);
    }

    // Si se establece el filtro por cliente, se ejecuta el filtro
    if (customerIds != null && customerIds.Any())
    {
        query = query.Where(o => customerIds.Contains(o.CustomerId));
    }

    // Si se establece el filtro por status, se ejecuta el filtro
    if (statusIds != null && statusIds.Any())
    {
        query = query.Where(o => statusIds.Contains(o.StatusId));
    }

    // Filtro por estado de actividad si se proporciona
    if (isActive.HasValue)
    {
        query = query.Where(o => o.IsActive == isActive.Value);
    }

    // Proyección a OrderDTO y ejecución de la consulta de forma asíncrona
    var orders = await query.Select(o => new OrderDTO
    {
        OrderId = o.OrderId,
        OrderDate = o.OrderDate,
        TotalAmount = o.TotalAmount,
        CustomerName = o.Customer.Name, // Se asume que la entidad Order tiene tiene una propiedad a Customer
        StatusDescription = o.Status.Description, // Se asume que la entidad Order tiene una propiedad a Status
        IsActive = o.IsActive
    }).ToListAsync();

    return orders;
}
