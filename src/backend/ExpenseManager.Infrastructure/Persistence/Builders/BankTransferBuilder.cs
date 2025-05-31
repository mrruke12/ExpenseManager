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
                    CategoryId = prototype.CategoryId,
                    Bill = prototype.Bill,
                    Description = prototype.Description,
                    //UserId = prototype.UserId,
                    AccountId = prototype.AccountId
                };
            }

            return this;
        }

        public IBankTransferBuilder Reference(BankTransfer prototype) {
            Transfer = prototype;
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

        public IBankTransferBuilder SetCategoryId(int categoryId) {
            Transfer.CategoryId = categoryId;
            return this;
        }

        public IBankTransferBuilder SetMoment(DateOnly date) {
            Transfer.Moment = date;
            return this;
        }

        public IBankTransferBuilder SetMoment(DateTime dateTime) {
            Transfer.Moment = DateOnly.FromDateTime(dateTime);
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
