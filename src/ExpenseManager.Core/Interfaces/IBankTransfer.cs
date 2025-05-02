using ExpenseManager.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Core.Interfaces {
    public interface IBankTransfer {
        [Key]
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public IBankTransferCategory Category { get; set; }
        public uint Amount { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int AccountId { get; set; }
        public BankAccount Account { get; set; }
    }
}
