using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces.Builders {
    public interface IBankTransferCategoryBuilder {
        IBankTransferCategoryBuilder Reset(BankTransferCategory? prototype = null);
        IBankTransferCategoryBuilder SetName(string name);
        IBankTransferCategoryBuilder SetUserId(string id);
        BankTransferCategory Build();
    }
}
