using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces.Builders {
    public interface IBankTransferBuilder {
        IBankTransferBuilder Reset(BankTransfer? prototype = null);
        IBankTransferBuilder Reference(BankTransfer prototype);
        IBankTransferBuilder SetMoment(DateTime dateTime);
        IBankTransferBuilder SetMoment(DateOnly dateOnly);
        IBankTransferBuilder SetCategoryId(int categoryId);
        IBankTransferBuilder SetBill(int bill);
        IBankTransferBuilder SetDescription(string description);
        //IBankTransferBuilder SetUserId(string id);
        IBankTransferBuilder SetAccountId(int id);
        BankTransfer Build();
    }
}