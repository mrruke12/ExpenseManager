using ExpenseManager.Core.Interfaces;

namespace ExpenseManager.Core.LocalizationResources {
    public class RuLocalization : ILocalizationResource {
        public string StartMessage => "Привет! Рад видеть тебя снова!";

        public string NewUserStartMessage => "Похоже, что ты еще не вошел в систему. Войди через специальную ссылку, ты можешь получить в своем личном кабинете!";

        public string DefaultMessage => "Упс! Кажется возникла ошибка или ты ввел неправильную команду.";

        public string ToRussianMessage => "Язык переключен на русский";

        public string ToEnglishMessage => "Язык переключен на английский";
    }
}
