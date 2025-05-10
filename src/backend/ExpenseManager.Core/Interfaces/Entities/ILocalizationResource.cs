namespace ExpenseManager.Core.Interfaces.Entities {
    public interface ILocalizationResource {
        string StartMessage { get; }
        string NewUserStartMessage { get; }
        string DefaultMessage { get; }
        string ToRussianMessage { get; }
        string ToEnglishMessage { get; }
    }
}
