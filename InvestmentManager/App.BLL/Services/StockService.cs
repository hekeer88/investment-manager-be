using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;


public class StockService: BaseEntityService<App.Public.DTO.v1.Stock, 
        App.BLL.DTO.Stock, App.DAL.DTO.Stock, IStockRepository>,
    IStockService
{
    public StockService(IStockRepository repository, IMapper<Stock, DAL.DTO.Stock> bllMapper,
        IMapper<App.Public.DTO.v1.Stock, Stock> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }

    public async Task<IEnumerable<Stock>> GetAllAsync(Guid portfolioId, bool noTracking = true)
    {
        var res =
            (await Repository.GetAllAsync(portfolioId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();

        foreach (var stock in res)
        {
            CalculateStockBalance(stock);
            if (stock.LatestPrice != null)
            {
                stock.XIRR = XIRR(stock.Transactions?.ToList() ?? 
                                  new List<Transaction>(), (decimal) stock.LatestPrice, stock.Quantity); 
            }
            
            stock.Ticker = stock.Ticker.ToUpper();
        }

        return res;
    }
    
    public async Task<IEnumerable<Public.DTO.v1.Stock>> PublicGetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId, noTracking)).Select(x => PublicMapper.Map(x)!).ToList();
        
        return res;
    }
    
    public async Task<Public.DTO.v1.Stock?> PublicFirstOrDefaultAsync(Guid stockId, bool noTracking = true)
    {
        var res = BLLMapper.Map(await Repository.FirstOrDefaultAsync(stockId, noTracking));
        return PublicMapper.Map(res);
    }
    
    private void CalculateStockBalance(Stock stock)
    {
        var quantity = stock.Transactions?.Sum(t => t.Quantity) ?? 0;
        var lastPrice = stock.Prices?.OrderByDescending(p => p.PriceTime).FirstOrDefault()?.CurrentPrice ?? 0;
        if (lastPrice == 0)
        {
            lastPrice =
                stock.Transactions?.OrderByDescending(t => t.TransactionDate).FirstOrDefault()?.TransactionPrice ?? 0;
        }
        stock.Balance = quantity * lastPrice;
    }

    private double? XIRR(List<Transaction> transactions, decimal latestPrice, int totalQuantity, int decimals = 4, double maxRate = 1000000)
    {
        var transactionList = transactions;
        transactionList.Add(new Transaction
        {
            Quantity = totalQuantity * -1,
            TransactionPrice = latestPrice,
            TransactionDate = DateTime.Now,
        });
        
        if (transactionList.Count() == 1)
        {
            return null;
        }
        if (transactionList.Where(x=> x.Amount > 0).Count() == 0)
        {
            throw new Exception("Contains only negative cash flows");
            
        }
        if (transactionList.Where(x => x.Amount < 0).Count() == 0)
        {
            throw new Exception("Contains only positive cash flows");
            
        }
        
        var precision = Math.Pow(10, - decimals);
        var minRate = -(1 - precision);
        
        YearsFromFirstTransaction(transactionList);
        var lowResult = CalcEquation(transactionList, minRate);
        var highResult = CalcEquation(transactionList, maxRate);
        
        return XIRRCalculator(minRate, maxRate, lowResult, highResult, transactionList, precision, decimals) * 100;
    }

    
    
    private double XIRRCalculator(double lowRate, double highRate, double lowResult, double highResult,
        List<Transaction> transactions, double precision, int decimals)
    {
        
        if (Math.Sign(lowResult) == Math.Sign(highResult))
        {
            throw new Exception("Value cannot be calculated");
        }
        
        var middleRate = (lowRate + highRate) / 2;
        var middleResult = CalcEquation(transactions, middleRate);
        if (Math.Sign(middleResult) == Math.Sign(lowResult))
        {
            lowRate = middleRate;
            lowResult = middleResult;
        }
        else
        {
            highRate = middleRate;
            highResult = middleResult;
        }
        if (Math.Abs(middleResult) > precision)
        {
            return XIRRCalculator(lowRate, highRate, lowResult, highResult, transactions, precision, decimals);
        }
        else
        {
            return Math.Round((highRate + lowRate) / 2, decimals);
        }

    }
    
    
    private static void YearsFromFirstTransaction(List<Transaction> transactions)
    {
        var firstDate = transactions!.Min(x => x.TransactionDate);

        foreach (var transaction in transactions!)
        {
            var x = ((double)transaction.TransactionDate.Subtract(firstDate).Days) / 365;
            transaction.YearsFromFirstTransaction = x;
        }
    }
    
    private double CalcEquation(List<Transaction> transactions, double interestRate)
    {
        return transactions.Select(x => 
            (decimal.ToDouble(x.Amount) / (Math.Pow((1 + interestRate), x.YearsFromFirstTransaction ?? 0)))).Sum(x => x);
    }
    
    
    
}