using Microsoft.EntityFrameworkCore;

public class CustomerService
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateCustomersBalanceByInvoicesAsync(IEnumerable<Invoice> invoices)
    {
        // 1. Valido la entrada de datos para evitar errores innecesarios.
        if (invoices == null || !invoices.Any())
        {
            return;
        }

        // 2. Obtengo todos los IDs de cliente únicos y no nulos de la lista de facturas, fuera del foreach.
        var customerIds = invoices
            .Where(invoice => invoice.CustomerId.HasValue)
            .Select(inv => inv.CustomerId.Value)
            .Distinct()
            .ToList();

        // 3. Realizo UNA SOLA CONSULTA a la base de datos para obtener todos los clientes necesarios.
        // Uso un diccionario para un acceso más rápido.
        var customersById = await _dbContext.Customers
            .Where(customer => customerIds.Contains(customer.Id))
            .ToDictionaryAsync(c => c.Id);

        // 4. Iterar sobre las facturas y actualizar los saldos de los clientes en memoria.
        foreach (var invoice in invoices)
        {
            // Validamos que la factura tenga un cliente asociado y que lo hayamos encontrado en la BD.
            if (invoice.CustomerId.HasValue && customersById.TryGetValue(invoice.CustomerId.Value, out var customer))
            {
                customer.Balance -= invoice.Total;
            }
            else
            {
                // Registro un log para facturas sin cliente o clientes no encontrados.
                Console.WriteLine($"Advertencia: No se encontró el cliente para la factura ID: {invoice.Id}");
            }
        }

        // 5. Guardar todos los cambios en una sola transaccion.
        // Esto es atómico: o todo se guarda, o nada se guarda.
        await _dbContext.SaveChangesAsync();
    }


}