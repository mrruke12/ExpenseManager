using ExpenseManager.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Core.Interfaces {
    public class IBankTransferCategory {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
