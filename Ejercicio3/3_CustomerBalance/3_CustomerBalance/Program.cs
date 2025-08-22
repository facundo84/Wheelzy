using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;


var services = new ServiceCollection();

// Base de datos en memoria para pruebas
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TestDatabase"));

// Registro el servicio
services.AddTransient<CustomerService>();

var serviceProvider = services.BuildServiceProvider();

// Obtengo el contexto de la base de datos y el servicio
using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var customerService = scope.ServiceProvider.GetRequiredService<CustomerService>();

    // Creo los datos de prueba
    var customer1 = new Customer { Id = 1, Name = "Juan Pérez", Balance = 1000.00m };
    var customer2 = new Customer { Id = 2, Name = "María García", Balance = 500.00m };
    var customer3 = new Customer { Id = 3, Name = "Pedro López", Balance = 200.00m };

    dbContext.Customers.AddRange(customer1, customer2, customer3);
    await dbContext.SaveChangesAsync();

    var invoicesToUpdate = new List<Invoice>
    {
        new Invoice { Id = 101, CustomerId = 1, Total = 50.00m },
        new Invoice { Id = 102, CustomerId = 2, Total = 75.50m },
        new Invoice { Id = 103, CustomerId = 1, Total = 25.00m },
        new Invoice { Id = 104, CustomerId = 3, Total = 100.00m },
        new Invoice { Id = 105, CustomerId = 4, Total = 150.00m }, // Cliente no existente
        new Invoice { Id = 106, CustomerId = null, Total = 10.00m } // Esta factura no tiene cliente asociado
    };

    // Se ejecuta la prueba
    Console.WriteLine("Ejecutando la actualización de saldos...");
    await customerService.UpdateCustomersBalanceByInvoicesAsync(invoicesToUpdate);
    Console.WriteLine("Actualización completada.");

    // Verifico los resultados
    Console.WriteLine("\n--- Saldos Actualizados ---");
    var updatedCustomers = await dbContext.Customers.ToListAsync();
    foreach (var customer in updatedCustomers.OrderBy(c => c.Id))
    {
        Console.WriteLine($"Cliente: {customer.Name}, Nuevo Saldo: {customer.Balance:C}");
    }
}
