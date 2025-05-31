using ExpenseManager.Core.Interfaces.Entities;
using ExpenseManager.Core.LocalizationResources;

namespace ExpenseManager.Core.Services {
    public class LocalizationService {
        public Dictionary<string, ILocalizationResource> LocalizationResources;

        public LocalizationService() {
            LocalizationResources = new Dictionary<string, ILocalizationResource>();
            LocalizationResources["en"] = new EnLocalization();
            LocalizationResources["ru"] = new RuLocalization();
        }

        public ILocalizationResource this[string? languageCode] {
            get {
                if (LocalizationResources.ContainsKey(languageCode == null ? "" : languageCode)) return LocalizationResources[languageCode];
                else return LocalizationResources["en"];
            }
        }
    }
}
