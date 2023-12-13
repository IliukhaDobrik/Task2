namespace DataLayer.Entities;

public class Transaction
{
    public int TransactionId { get; set; }
    public int BankAccountId { get; set; }
    public int FileId { get; set; }
    public string TransactionType { get; set; }
    public decimal TransactionAmount { get; set; }
    public BankAccount BankAccount { get; set; }
    public File File { get; set; }
}