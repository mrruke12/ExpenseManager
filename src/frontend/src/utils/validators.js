import { useI18n } from "vue-i18n"

export const useAuthFormValidator = () => {
    const { t } = useI18n()

    const ValidateEmail = (email) => {
        if (email == undefined || email.length == 0) return t('validation.email-absence')    

        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
        const res = re.test(email)

        return res ? false : t('validation.email-error')
    }

    const ValidatePassword = (password) => { 
        if (password == undefined || password.length == 0) return t('validation.password-absence')      
        if (password.length < 6) return t('validation.password-insufficient-length')
        if (!/\d/.test(password)) return t('validation.password-numbers-absence')
        if (!/[a-zA-Z]/.test(password)) return t('validation.password-letters-absence')
        
        return false
    }

    const ValidatePasswordVerification = (password, passwordVerification) => {
        if (password !== passwordVerification) return t('validation.password-verification-fail')
        return false
    }

    return {
        ValidateEmail,
        ValidatePassword,
        ValidatePasswordVerification
    }
}

export const useModalFormValidator = () => {
    const { t } = useI18n()

    const ValidateAccountName = (name) => {
        if (name == null || name.length < 3) return t('validation.account-name-length-insufficient')
        if (name.length > 50) return t('validation.account-name-length-exceeded')
        return false;
    }

    const ValidateCategoryName = (name) => {
        if (name == null || name.length < 3) return t('validation.category-name-length-insufficient')
        if (name.length > 20) return t('validation.category-name-length-exceeded')
        return false;
    }

    const ValidateTransferAmount = (amount) => {
        if (Number(amount) == NaN) return t('validation.transfer-amount-type-violation')
        if (Number(amount) == 0) return t('validation.transfer-amount-zero-error')
        if (Number(amount) != Math.round(Number(amount))) return t('validation.transfer-amount-float-error')
        return false
    }

    const ValidateDate = (date) => {
        // if (Object.prototype.toString.call(date) !== "[object Date]") return t('validation.transfer-date-type-violation')
        let now = new Date()
        now.setHours(0, 0, 0, 0)
        let comp = new Date(date)
        comp.setHours(0, 0, 0, 0)
        if (comp.getTime() > now.getTime()) return t('validation.transfer-date-future-error')
        return false
    }

    const ValidateCategory = (categoryId) => {
        if (categoryId === null || categoryId === undefined || categoryId < 0) return t('validation.category-error')
        return false
    }

    return {
        ValidateAccountName,
        ValidateCategoryName,
        ValidateTransferAmount,
        ValidateDate,
        ValidateCategory
    }
}