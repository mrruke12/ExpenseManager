namespace ExpenseManager.Core.Entities {
    public class BankTransferFilter {
        public DateOnly FromDate { get; set; } = DateOnly.MinValue;
        public DateOnly ToDate { get; set; } = DateOnly.MaxValue;
        public int MinBill { get; set; } = int.MinValue;
        public int MaxBill { get; set; } = int.MaxValue;
        public int CategoryId { get; set; } = -1;
    }
}