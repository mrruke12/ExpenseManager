import { useI18n } from 'vue-i18n'

export const useFormValidator = () => {
  const { t } = useI18n()
  
  const validateEmail = (email) => {
    if (!email) return t('validation.required')
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      return t('validation.email')
    }
    return ''
  }

  const validatePassword = (password, minLength = 8) => {
    if (!password) return t('validation.required')
    if (password.length < minLength) {
      return t('validation.password_length', { min: minLength })
    }
    return ''
  }

  const validatePasswordsMatch = (password, confirmPassword) => {
    if (password !== confirmPassword) {
      return t('validation.passwords_match')
    }
    return ''
  }

  return {
    validateEmail,
    validatePassword,
    validatePasswordsMatch
  }
}