using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseManager.Core.Entities {
    public class BankTransferCategory {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public AppUser User { get; set; }
    }
}
