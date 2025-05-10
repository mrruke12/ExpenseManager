import { createI18n } from 'vue-i18n'

// Локальные переводы
import en from './locales/en.json'
import ru from './locales/ru.json'

const i18n = createI18n({
  legacy: false, // для Composition API
  locale: localStorage.getItem('locale') || 'en', // язык по умолчанию
  fallbackLocale: 'en', // резервный язык
  messages: { en, ru }
})

export default i18n