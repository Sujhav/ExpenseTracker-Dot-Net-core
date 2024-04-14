using ExpenseTracker.Database;
using ExpenseTracker.Models;
namespace ExpenseTracker.Repository
{
    public class SplineChartRepository
    {
        private readonly DashboardRepository _dashboardRepository;

        public SplineChartRepository(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<List<SplineChartModel>>   SplineChart()
        {
            List<Transaction> IncomeSummary = await _dashboardRepository.Datetime();
            List<SplineChartModel> Income = IncomeSummary
            .Where(i => i.Category.Type == "Income")
            .GroupBy(j => j.dateTime)
            .Select(k => new SplineChartModel
            {
                day = k.First().dateTime.ToString("dd-MMM"),
                income = k.Sum(l => l.amount)
            }).ToList();

            List<SplineChartModel> Expense=IncomeSummary
                .Where(i=>i.Category.Type=="Expense")
                .GroupBy (j => j.dateTime)
                .Select(k=>new SplineChartModel
                {
                    day = k.First().dateTime.ToString("dd-MMM"),
                    expense=k.Sum(l => l.amount)
                }).ToList();

            DateTime StartDate = DateTime.Today.AddDays(-6);

            string[] Last7Days=Enumerable.Range(0, 7)
                .Select(i=> StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            var SplineChartData = (from day in Last7Days
                                   join income in Income on day equals income.day into dayincomejoined 
                                   from income in dayincomejoined.DefaultIfEmpty()
                                   join expense in Expense on day equals expense.day into ExpenseJoined
                                   from expense in ExpenseJoined.DefaultIfEmpty()
                                   select new SplineChartModel
                                   {
                                       day = day,
                                       income = income == null ? 0 : income.income,
                                       expense = expense == null ? 0 : expense.expense,
                                   }).ToList();

            return SplineChartData;



        }
    }
}
