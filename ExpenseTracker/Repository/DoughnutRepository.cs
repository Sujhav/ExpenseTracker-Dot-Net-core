using ExpenseTracker.Database;
using ExpenseTracker.Models;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Repository
    
{
    public class DoughnutRepository
    { private readonly DashboardRepository _dashboardRepository;
        public DoughnutRepository(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }   
        public async Task<List<DoughnutDataModel>> Chart()
        {
            List<Transaction> SelectedTransaction = await _dashboardRepository.Datetime();

            List<DoughnutDataModel> chartdata = SelectedTransaction
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new DoughnutDataModel
                {
                    xValue = k.Sum(j => j.amount), //amount
                    yValue = k.Sum(j => j.amount).ToString("C0"),//formatted amount
                    text = k.First().Category.Icon + "" + k.First().Category.Title //title
                })
                .OrderByDescending(p=>p.xValue)
                .ToList();

            return chartdata;
        }
    }
}
