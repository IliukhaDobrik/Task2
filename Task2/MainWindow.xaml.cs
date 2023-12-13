using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Spire.Xls;
using File = DataLayer.Entities.File;
using Window = System.Windows.Window;

namespace Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyDbContext _context;
        public MainWindow(MyDbContext context)
        {
            _context = context;
            InitializeComponent();
        }

        private async void ButtonImport_OnClick(object sender, RoutedEventArgs e)
        {
            var path = TextBox.Text;
            var fileName = Path.GetFileName(path);
            
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Enter path!");
                return;
            }

            if (await _context.Files.AnyAsync(x => x.FileName == fileName))
            {
                MessageBox.Show("File already exist!");
                return;
            }
            
            var fileId = await AddImportedFile(fileName);
            
            Workbook wb = new Workbook();
            
            wb.LoadFromFile(path);

            await ProcessExelData(wb, fileId);
            
            MessageBox.Show("Import completed!");
        }
        
        private async void ButtonViewFiles_OnClick(object sender, RoutedEventArgs e)
        {
            var filesList = await _context.Files.ToListAsync();
            var filesListWindow = new ViewFilesList(_context, filesList);
            filesListWindow.Show();
        }

        private async Task ProcessExelData(Workbook wb, int fileId)
        {
            Worksheet ws = wb.Worksheets[0];
            
            CellRange locatedRange = ws.AllocatedRange;
            var totalRows = locatedRange.Rows.Length;

            var classNumber = string.Empty;
            for (int i = 0; i < totalRows; i++)
            {
                var currentCell = locatedRange[i + 1, 1].Value;
                
                if (currentCell.StartsWith("КЛАСС"))
                {
                    classNumber = GetClassNumber(currentCell);
                }
                
                if (currentCell.Length == 2 || int.TryParse(currentCell, out int accountNumber) == false)
                {
                    continue;
                }
            
                int bankAccountId =  await AddBankAccount(locatedRange, i, accountNumber, fileId, classNumber);
                await AddTransactions(locatedRange, i, bankAccountId, fileId);
            }
        }
        
        private string GetClassNumber(string currentCell)
        {
            var pattern = @"\d+";
            return Regex.Match(currentCell, pattern).Value;
        }

        private async Task<int> AddImportedFile(string fileName)
        {
            var file = new File
            {
                FileName = fileName
            };
            
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();

            return file.FileId;
        }

        private async Task<int> AddBankAccount(CellRange locatedRange, int rowIndex, int accountNumber, int fileId,  string classNumber)
        {
            var bankAccount = new BankAccount
            {
                AccountNumber = accountNumber,
                Class = int.Parse(classNumber),
                FileId = fileId,
                IncomingBalanceAsset = decimal.Parse(locatedRange[rowIndex + 1, 2].Value),
                IncomingBalanceLiability = decimal.Parse(locatedRange[rowIndex + 1, 3].Value),
                OutgoingBalanceAsset = decimal.Parse(locatedRange[rowIndex + 1, 6].Value),
                OutgoingBalanceLiability = decimal.Parse(locatedRange[rowIndex + 1, 7].Value)
            };
            
            await _context.BankAccounts.AddAsync(bankAccount);
            await _context.SaveChangesAsync();

            return bankAccount.BankAccountId;
        }
        
        private async Task AddTransactions(CellRange locatedRange, int rowIndex, int bankAccountId, int fileId)
        {
            await _context.Transactions.AddRangeAsync(
                new Transaction
                {
                    BankAccountId = bankAccountId,
                    FileId = fileId,
                    TransactionType = "Дебет",
                    TransactionAmount = decimal.Parse(locatedRange[rowIndex + 1, 4].Value)
                },
                new Transaction
                {
                    BankAccountId = bankAccountId,
                    FileId = fileId,
                    TransactionType = "Кредит",
                    TransactionAmount = decimal.Parse(locatedRange[rowIndex + 1, 5].Value)
                });
        }
    }
}