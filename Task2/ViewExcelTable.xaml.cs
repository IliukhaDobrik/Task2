using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Task2.Dtos;

namespace Task2;

public partial class ViewExcelTable : Window
{
    private readonly MyDbContext _context;
    public ViewExcelTable(MyDbContext context, string fileName)
    {
        _context = context;
        InitializeComponent();
        DataContext = this;
        LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        var bankAccountData = new ObservableCollection<BankAccountDTO>(await GetData());
        DataGrid.ItemsSource = bankAccountData;
    }

    private async Task<List<BankAccountDTO>> GetData()
    {
        var bankAccounts = _context.BankAccounts.AsNoTracking();
        var transactions = _context.Transactions.AsNoTracking();
        return await bankAccounts
            .Join(transactions,
                ba => ba.BankAccountId,
                t => t.BankAccountId,
                (ba, t) => new BankAccountDTO()
                {
                    AccountNumber = ba.AccountNumber,
                    Class = ba.Class,
                    IncomingBalanceAsset = ba.IncomingBalanceAsset,
                    IncomingBalanceLiability = ba.IncomingBalanceLiability,
                    Debit = transactions.Where(t => t.BankAccountId == ba.BankAccountId)
                        .Where(t => t.TransactionType == "Дебет")
                        .Select(t => t.TransactionAmount).SingleOrDefault(),
                    Credit = transactions.Where(t => t.BankAccountId == ba.BankAccountId)
                        .Where(t => t.TransactionType == "Кредит")
                        .Select(t => t.TransactionAmount).SingleOrDefault(),
                    OutgoingBalanceAsset = ba.OutgoingBalanceAsset,
                    OutgoingBalanceLiability = ba.OutgoingBalanceLiability
                }).ToListAsync();
    }
}