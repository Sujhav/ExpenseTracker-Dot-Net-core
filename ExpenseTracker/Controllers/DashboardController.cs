using ExpenseTracker.Repository;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using System.Globalization;

namespace ExpenseTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardRepository _dashboardRepository;
        private readonly DoughnutRepository _doughnutRepository;
        private readonly SplineChartRepository _splineChartRepository;
        private readonly TransactionRepository _transactionRepository;
        public DashboardController(DashboardRepository dashboardRepository,
            DoughnutRepository doughnutRepository,
            SplineChartRepository splineChartRepository,
            TransactionRepository transactionRepository)
        {
            _dashboardRepository = dashboardRepository;
            _doughnutRepository = doughnutRepository;
            _splineChartRepository = splineChartRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<IActionResult> Index()
        {
           
            var data = await _dashboardRepository.Amount();
            ViewBag.TotalIncome = data.Income.ToString("C0");
            ViewBag.TotalExpense=data.Expense.ToString("C0");
            CultureInfo cultureInfo=CultureInfo.CreateSpecificCulture("en-US");
            cultureInfo.NumberFormat.CurrencyNegativePattern = 1;

            ViewBag.Balance = string.Format(cultureInfo, "{0:C0}", data.Balance);
            var chart=await _doughnutRepository.Chart();
            ViewBag.Chart = chart;

            List<SplineChartModel> splinecharts=await _splineChartRepository.SplineChart();

            ViewBag.spline = splinecharts;

            var recent=await _transactionRepository.RecentTransactions();
            ViewBag.RecentTransactions = recent;



            return View();
            
        }
    }
}
