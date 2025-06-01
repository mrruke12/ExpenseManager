using ExpenseManager.Core.Entities;
using ExpenseManager.Core.Interfaces.Builders;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ExpenseManager.Core.Services {
    public class PdfService {
        public List<BankTransfer> ImportFromKaspiRu(string path, int accountId, int categoryId, IBankTransferBuilder builder) {
            List<BankTransfer> res = new List<BankTransfer>();

            using (PdfReader reader = new PdfReader(path)) {
                bool write = false;
                for (int page = 1; page <= reader.NumberOfPages; page++) {
                    string text = PdfTextExtractor.GetTextFromPage(reader, page);

                    // Пример для простого текстового PDF
                    string[] lines = text.Split('\n');

                    foreach (string line in lines) {
                        if (line.StartsWith("    - Сумма заблокирована. Банк ожидает подтверждения от платежной системы"))
                            write = false;
                        if (line.StartsWith("АО «Kaspi Bank»") || line.ToLowerInvariant().IndexOf("kaspi депозит") != -1) continue;

                        if (write) {
                            int sign;
                            string[] parts1 = line.Split("₸");
                            string[] parts2 = new string[2];
                            string[] parts3 = new string[2];
                            string date, sum, type, descr;

                            if (parts1[0].IndexOf("+") != -1) {
                                sign = 1;
                                parts2 = parts1[0].Split(" + ");
                            } else {
                                sign = -1;
                                parts2 = parts1[0].Split(" - ");
                            }

                            parts3 = parts1[1].Split("      ");

                            date = parts2[0].Trim();
                            sum = $"{(sign == 1 ? ' ' : '-')}{parts2[1].Trim().Replace(" ", "")}";
                            sum = sum.Substring(0, sum.IndexOf(","));
                            type = parts3[0].Trim();
                            descr = parts3[1].Trim();

                            var transfer = builder
                                            .Reset()
                                            .SetBill(int.Parse(sum))
                                            .SetDescription(descr)
                                            .SetAccountId(accountId)
                                            .SetCategoryId(categoryId)
                                            .SetMoment(DateOnly.Parse(date))
                                            .Build();

                            res.Add(transfer);
                        }

                        if (line.StartsWith("Дата Сумма Операция       Детали"))
                            write = true;
                    }
                }
            }

            return res;
        }
    }
}
