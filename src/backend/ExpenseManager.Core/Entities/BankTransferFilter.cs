namespace ExpenseManager.Core.Entities {
    public class BankTransferFilter {
        public DateTime FromDate { get; set; } = DateTime.MinValue;
        public DateTime ToDate { get; set; } = DateTime.MaxValue;
        public int MinBill { get; set; } = 0;
        public int MaxBill { get; set; } = int.MaxValue;
        public int CategoryId { get; set; } = -1;
    }
}