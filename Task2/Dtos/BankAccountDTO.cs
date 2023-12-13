namespace Task2.Dtos;

public class BankAccountDTO
{
    public int AccountNumber { get; set; }
    public int Class { get; set; }
    public decimal IncomingBalanceAsset { get; set; }
    public decimal IncomingBalanceLiability { get; set; }
    public decimal Debit { get; set; }
    public decimal? Credit { get; set; }
    public decimal OutgoingBalanceAsset { get; set; }
    public decimal OutgoingBalanceLiability { get; set; }
}