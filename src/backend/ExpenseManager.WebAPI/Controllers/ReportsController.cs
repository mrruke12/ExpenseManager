using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.WebAPI.Controllers {
    [Route("api/report")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase {
        private readonly IAppUserRepository _userRepository;

        public ReportsController(IAppUserRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport() {
            DateTime from = DateTime.Now.AddMonths(-1);
            DateTime to = DateTime.Now;

            var email = User.FindFirst("Email").Value;
            var user = await _userRepository.GetByEmailWithAttachmentsAsync(email);

            Report report = new Report();

            int days = (int)(to - from).Days+1;

            report.BalanceDynamics = Enumerable.Repeat(0, days).ToList(); 
            report.IncomeDynamics = Enumerable.Repeat(0, days).ToList();
            report.ExpensesDynamics = Enumerable.Repeat(0, days).ToList();
            report.AccountTransfers = new List<BankAccount>();
            report.CategoryTransfers = new Dictionary<string, List<BankTransfer>>();

            user.Accounts.ForEach(account => {
                report.AccountTransfers.Add(account);

                account.Transfers.ForEach(transfer => {
                    var difference = (int)(transfer.Moment - from).Days;

                    report.TotalBalance += transfer.Bill;

                    if (transfer.Bill >= 0) {
                        report.TotalIncome += transfer.Bill;
                        report.IncomeDynamics[difference] += transfer.Bill;
                    } else {
                        report.TotalExpenses += transfer.Bill;
                        report.ExpensesDynamics[difference] += transfer.Bill;
                    }

                    if (report.CategoryTransfers.ContainsKey(transfer.Category.Name) == false) report.CategoryTransfers[transfer.Category.Name] = new List<BankTransfer>();
                    report.CategoryTransfers[transfer.Category.Name].Add(transfer);
                });
            });

            return Ok(report);
        }
    }
}
