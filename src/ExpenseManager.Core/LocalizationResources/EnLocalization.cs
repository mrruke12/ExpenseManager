using ExpenseManager.Core.Interfaces;

namespace ExpenseManager.Core.LocalizationResources {
    public class EnLocalization : ILocalizationResource {
        public string StartMessage => "Hi! Glad to see you again!";

        public string NewUserStartMessage => "It looks like you haven't logged in yet. Log in via a special link, you can get it in your personal account!";

        public string DefaultMessage => "Oops! There seems to be a mistake or you entered the wrong command.";

        public string ToRussianMessage => "Language switched to Russian";

        public string ToEnglishMessage => "Language switched to English";
    }
}
