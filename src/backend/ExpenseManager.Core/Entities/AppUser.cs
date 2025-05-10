using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseManager.Core.Entities {
    /// <summary>
    /// Represents an user of the app
    /// </summary>
    public class AppUser {
        /// <summary>
        /// Stores the user's ID
        /// </summary>
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Stores the user's Telegram ID
        /// </summary>
        [JsonIgnore]
        public long TelegramId { get; set; } = -1;

        /// <summary>
        /// Stores the user's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Stores the user's encrypted Password 
        /// </summary>
        [JsonIgnore]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Stores the user's preferred language
        /// </summary>
        public string LanguageCode { get; set; } = "en";

        /// <summary>
        /// Stores the user's bank Accounts
        /// </summary>
        public List<BankAccount> Accounts { get; set; } = new List<BankAccount>();

        /// <summary>
        /// Stores the user's personal categories
        /// </summary>
        public List<BankTransferCategory> Categories { get; set; } = new List<BankTransferCategory>();
    }
}
