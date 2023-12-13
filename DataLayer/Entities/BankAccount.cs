namespace DataLayer.Entities;

public class BankAccount
{
    public int BankAccountId { get; set; }
    public int FileId { get; set; }
    public int AccountNumber { get; set; }
    public int Class { get; set; }
    public decimal IncomingBalanceAsset { get; set; }
    public decimal IncomingBalanceLiability { get; set; }
    public decimal OutgoingBalanceAsset { get; set; }
    public decimal OutgoingBalanceLiability { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public File File { get; set; }
}