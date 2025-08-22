//original

public void UpdateCustomersBalanceByInvoices(List<Invoice> invoices) {
	foreach (var invoice in invoices)
	{
		var customer = dbContext.Customers.SingleOrDefault(invoice.CustomerId.Value);
		customer.Balance -= invoice.Total;
		dbContext.SaveChanges();
	}
}

--Mejoras

Por lo que veo tenes dentro de un foreach, 2 conexiones a la base. 
El get se podría sacar por fuera del foreach. 

- Control de nulos
- Cambio de utilizar List a utilizar IEnumerable
* Usar List<Invoice> como parámetro implica que la función espera una colección de facturas 
que ya ha sido completamente creada y está almacenada en memoria. 
Es útil cuando necesitas manipular la colección, por ejemplo, si quieres agregar o quitar elementos dentro de la función.
Es deficiente cuando la coleccion es muy grande.

* Usar IEnumerable<Invoice> es más flexible y eficiente, especialmente con grandes colecciones. 
Este parámetro representa una colección genérica que puede ser una List, un Array, o cualquier otra estructura de datos 
que implemente la interfaz IEnumerable.
La principal ventaja es que IEnumerable permite la iteración diferida. 
Esto significa que los elementos de la colección no se cargan en memoria hasta que son realmente necesarios, 
lo que reduce el consumo de recursos.

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
    var customersById = await dbContext.Customers
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
    await dbContext.SaveChangesAsync();
}