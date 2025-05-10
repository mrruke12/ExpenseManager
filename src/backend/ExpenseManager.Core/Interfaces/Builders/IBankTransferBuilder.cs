using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces.Builders {
    public interface IBankTransferBuilder {
        IBankTransferBuilder Reset(BankTransfer? prototype = null);
        IBankTransferBuilder SetMoment(DateTime dateTime);
        IBankTransferBuilder SetCategory(BankTransferCategory category);
        IBankTransferBuilder SetBill(int bill);
        IBankTransferBuilder SetDescription(string description);
        //IBankTransferBuilder SetUserId(string id);
        IBankTransferBuilder SetAccountId(int id);
        BankTransfer Build();
    }
}