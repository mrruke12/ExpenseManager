using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Builders {
    public class BankTransferBuilder : IBankTransferBuilder {
        private BankTransfer Transfer;
        public BankTransfer Build() {
            return Transfer;
        }

        public IBankTransferBuilder Reset(BankTransfer? prototype = null) {
            if (prototype == null) Transfer = new BankTransfer();
            else {
                Transfer = new BankTransfer {
                    Id = prototype.Id,
                    Moment = prototype.Moment,
                    Category = prototype.Category,
                    Bill = prototype.Bill,
                    Description = prototype.Description,
                    //UserId = prototype.UserId,
                    AccountId = prototype.AccountId
                };
            }

            return this;
        }

        public IBankTransferBuilder SetAccountId(int id) {
            Transfer.AccountId = id;
            return this;
        }

        public IBankTransferBuilder SetBill(int bill) {
            Transfer.Bill = bill;
            return this;
        }

        public IBankTransferBuilder SetCategory(BankTransferCategory category) {
            Transfer.Category = category;
            return this;
        }

        public IBankTransferBuilder SetMoment(DateTime dateTime) {
            Transfer.Moment = dateTime;
            return this;
        }

        public IBankTransferBuilder SetDescription(string description) {
            Transfer.Description = description;
            return this;
        }

        //public IBankTransferBuilder SetUserId(string id) {
        //    Transfer.UserId = id;
        //    return this;
        //}
    }
}
