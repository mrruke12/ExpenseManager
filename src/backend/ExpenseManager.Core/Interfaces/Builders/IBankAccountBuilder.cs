using ExpenseManager.Core.Entities;

namespace ExpenseManager.Core.Interfaces.Builders {
    public interface IBankAccountBuilder {
        IBankAccountBuilder Reset(BankAccount? prototype = null);
        IBankAccountBuilder Reference(BankAccount prototype);
        IBankAccountBuilder SetName(string name);
        IBankAccountBuilder SetUserId(string id);
        BankAccount Build();
    }
}
