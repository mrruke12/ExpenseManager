using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Builders {
    public class BankAccountBuilder : IBankAccountBuilder {
        private BankAccount Account;

        public BankAccount Build() {
            return Account;
        }

        public IBankAccountBuilder Reset(BankAccount? prototype = null) {
            if (prototype == null) Account = new BankAccount();
            else {
                Account = new BankAccount {
                    Id = prototype.Id,
                    UserId = prototype.UserId,
                    Name = prototype.Name,
                    Balance = prototype.Balance
                };
            }

            return this;
        }

        public IBankAccountBuilder Reference(BankAccount prototype) {
            Account = prototype;
            return this;
        }

        public IBankAccountBuilder SetName(string name) {
            Account.Name = name;
            return this;
        }

        public IBankAccountBuilder SetUserId(string id) {
            Account.UserId = id;
            return this;
        }
    }
}
