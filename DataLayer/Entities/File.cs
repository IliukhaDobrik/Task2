namespace DataLayer.Entities;

public class File
{
    public int FileId { get; set; }
    public string FileName { get; set; }
    public ICollection<BankAccount> BankAccounts { get; set; }
    public ICollection<Transaction> Transaction { get; set; }
}