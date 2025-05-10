using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Core.Entities {
    public class Report {
        public List<int> BalanceDynamics { get; set; }
        public List<int> IncomeDynamics { get; set; }
        public List<int> ExpensesDynamics { get; set; }
        public int TotalIncome { get; set; }
        public int TotalExpenses { get; set; }
        public int TotalBalance { get; set; }
        public Dictionary<string, List<BankTransfer>> CategoryTransfers { get; set; }
        public List<BankAccount> AccountTransfers { get; set; }
    }
}
