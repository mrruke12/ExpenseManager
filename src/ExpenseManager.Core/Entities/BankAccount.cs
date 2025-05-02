using ExpenseManager.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Core.Entities {
    /// <summary>
    /// Represents a bank account
    /// </summary>
    public class BankAccount {
        /// <summary>
        /// Stores the account ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Stores the account owner's ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Stores the account owner
        /// </summary>
        public AppUser User { get; set; }

        /// <summary>
        /// Stores the account expenses 
        /// </summary>
        public List<Expense> Expenses { get; set; }

        /// <summary>
        /// Stores the account incomes 
        /// </summary>
        public List<Income> Incomes { get; set; }
    }
}
