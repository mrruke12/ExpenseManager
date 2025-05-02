using ExpenseManager.Core.Interfaces;

namespace ExpenseManager.Core.Entities {
    public class Income : IBankTransfer {
        public DateTime DateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IBankTransferCategory Category { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AppUser User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AccountId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public BankAccount Account { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
