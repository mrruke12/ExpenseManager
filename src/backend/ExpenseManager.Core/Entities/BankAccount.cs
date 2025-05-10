using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        /// Stores the account name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Stores the account owner's ID
        /// </summary>
        [JsonIgnore]
        public string UserId { get; set; }

        /// <summary>
        /// Stores cached account balance
        /// </summary>
        public int Balance { get; set; } = 0;

        /// <summary>
        /// Stores the account owner
        /// </summary>
        [JsonIgnore]
        public AppUser User { get; set; }

        /// <summary>
        /// Stores the account transfer 
        /// </summary>
        public List<BankTransfer> Transfers { get; set; }
    }
}
