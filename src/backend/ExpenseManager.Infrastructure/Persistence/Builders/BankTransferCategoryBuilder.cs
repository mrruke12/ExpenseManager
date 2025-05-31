using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Builders {
    public class BankTransferCategoryBuilder : IBankTransferCategoryBuilder {
        private BankTransferCategory Category;

        public BankTransferCategory Build() {
            return Category;
        }

        public IBankTransferCategoryBuilder Reset(BankTransferCategory? prototype = null) {
            if (prototype == null) Category = new BankTransferCategory();
            else {
                Category = new BankTransferCategory {
                    Name = prototype.Name,
                    UserId = prototype.UserId
                };
            }

            return this;
        }

        public IBankTransferCategoryBuilder Reference(BankTransferCategory prototype) {
            Category = prototype; 
            return this;
        }

        public IBankTransferCategoryBuilder SetName(string name) {
            Category.Name = name.ToLower().Trim();

            return this;
        }

        public IBankTransferCategoryBuilder SetUserId(string id) {
            Category.UserId = id;

            return this;
        }
    }
}
