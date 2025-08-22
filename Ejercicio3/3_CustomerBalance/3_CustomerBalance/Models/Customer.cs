public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
}
