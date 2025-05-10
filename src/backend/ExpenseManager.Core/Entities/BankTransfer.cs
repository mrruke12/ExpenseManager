using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseManager.Core.Entities {
    public class BankTransfer {
        [Key]
        public long Id { get; set; }
        public DateTime Moment { get; set; }
        public BankTransferCategory Category { get; set; }
        public int Bill { get; set; }
        public string Description { get; set; }
        //public string UserId { get; set; }
        //public AppUser User { get; set; }
        public int AccountId { get; set; }
        [JsonIgnore]
        public BankAccount Account { get; set; }
    }
}
