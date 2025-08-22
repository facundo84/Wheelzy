public class Invoice
{
    public int Id { get; set; }
    public int? CustomerId { get; set; } // Usamos int? para permitir que sea nulo si la factura no tiene cliente
    public decimal Total { get; set; }

    public Customer? Customer { get; set; }
}