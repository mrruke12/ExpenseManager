import { createI18n } from 'vue-i18n'

// Локальные переводы
import en from './locales/en.json'
import ru from './locales/ru.json'

const i18n = createI18n({
  legacy: false, 
  locale: localStorage.getItem('locale') || 'ru',
  fallbackLocale: 'ru',
  messages: { en, ru }
})

export default i18n