<template>
    <container-component Center Sticky>
        <!-- <div class="bg-emerald-400/50 absolute left-0 right-0 top-0 bottom-0 flex items-center justify-center"> -->
        <div class="my-5">    
            <div class="m-auto shadow-xl bg-linear-to-r from-emerald-50 to-green-50 w-9/10 sm:w-[350px] flex flex-col items-center rounded-lg overflow-hidden pb-10">

                <div class="hidden w-full bg-linear-to-r from-emerald-50 to-green-50 mb-5 text-xl font-bold border-b-1 border-black/20">
                    <button @click="isLogin = true;" :class="`transition py-2 w-1/2 text-center ${isLogin ? 'bg-emerald-500' : ''}`">
                        {{ t('auth.login-label') }}
                    </button>
                    <button @click="isLogin = false;" :class="`transition py-2 w-1/2 text-center ${!isLogin ? 'bg-emerald-500' : ''}`">
                        {{ t('auth.register-label') }}
                    </button>
                </div>
                
                <div class="flex w-full flex-col px-10 justify-around items-center relative mb-5 overflow-hidden">
                    <img class="w-1/5 relative -bottom-16 z-0" src="../assets/KermitTheFrogHello.png" alt="Kermit the Frog">
                    <p :class="`transition duration-500 py-2 border-y-1 border-black/30 w-full text-xl text-center z-1 ${validationErrors.length > 0 ? 'bg-red-300' : 'bg-emerald-300'}`">
                        {{ 
                            validationErrors.length > 0 ?
                                validationErrors[0] :
                                t(isLogin ? 'auth.login-quote' : 'auth.register-quote') 
                        }}
                    </p>
                </div>
                

                <form @submit.prevent="SubmitForm" id="form" class="flex flex-col items-center w-5/6 text-xl space-y-4">
                    <div>
                        <label for="email">{{ t('auth.email-input-label') }}</label>
                        <input v-model="form.email" type="text" name="email" :placeholder="t('auth.email-input-placeholder')">
                    </div>
                    
                    <div class="">
                        <label for="password">{{ t('auth.password-input-label') }}</label>
                        <input v-model="form.password" type="text" name="password" :placeholder="isLogin ? t('auth.password-input-login-placeholder') : t('auth.password-input-register-placeholder')">
                    </div>
                    
                    <div v-if="!isLogin">
                        <label for="password-verification">{{ t('auth.password-verification-input-label') }}</label>
                        <input v-model="form.passwordVerification" type="text" name="password-verification" :placeholder="t('auth.password-verification-input-placeholder')">
                    </div>

                    <input :disabled="isSubmiting" type="submit" class="transition text-xl bg-emerald-500 disabled:bg-emerald-500/20 border-none font-bold hover:bg-green-300" :value="isLogin ? t('auth.submit-login') : t('auth.submit-register')">
                
                    <h3>{{ isLogin ? t('auth.no-account-yet') : t('auth.already-have-account') }} <span @click="isLogin = !isLogin" class="cursor-pointer text-blue-500 underline">{{ isLogin ? t('auth.register-label') : t('auth.login-label') }}</span></h3>
                </form>

            </div>
        </div>
    </container-component>
</template>

<script setup>
    import ContainerComponent from '../components/ContainerComponent.vue'
    import { useI18n } from 'vue-i18n'
    import { ref } from 'vue'
    import { useAuthFormValidator } from '../utils/validators'
    import api from '../api/api'
    import { useRouter } from 'vue-router'

    const { t, locale } = useI18n()
    const router = useRouter()
    const { ValidateEmail, ValidatePassword, ValidatePasswordVerification } = useAuthFormValidator()
    const isLogin = ref(true)
    const validationErrors = ref([])

    const form = ref({
        email: '',
        password: '',
        passwordVerification: ''
    })

    const isSubmiting = ref(false)

    const Validate = () => {
        validationErrors.value = []
        const emailError = ValidateEmail(form.value.email)
        if (emailError !== false) validationErrors.value.push(emailError)

        const passwordError = ValidatePassword(form.value.password)
        if (passwordError !== false) validationErrors.value.push(passwordError)

        const passwordVerificationError = ValidatePasswordVerification(form.value.password, form.value.passwordVerification)
        if (!isLogin.value && passwordVerificationError !== false) validationErrors.value.push(passwordVerificationError)

        return validationErrors.value.length > 0 ? false : true
    }

    const SubmitForm = async () => {
        if (!Validate()) return

        isSubmiting.value = true
        const response = ref({})

        try {
            response.value = await api.post(isLogin.value ? '/auth/login' : "/auth/register", {
                email: form.value.email,
                password: form.value.password
            })

            if (isLogin.value) {
                localStorage.setItem('token', response.value.data.token)
                api.defaults.headers['Authorization'] = `Bearer ${response.value.data.token}`
                const userLocale = (await api.get('/users/current-locale')).data
                locale.value = userLocale
                localStorage.setItem('locale', userLocale)
                router.push('/')
            }else {
                router.go('/auth')
            }
        } catch (ex) {
            if (ex.status == 409) validationErrors.value.push(t('auth.registration-conflict'))
            else validationErrors.value.push(t(isLogin.value ? 'auth.login-fail' : 'auth.registration-fail'))
        } finally {
            isSubmiting.value = false
        }
    }
</script>

<style>
    @reference 'tailwindcss';
    
    #form {
        div {
            @apply flex flex-col items-start w-full;
        }

        input {
            @apply py-4 px-2 w-full border-1 border-black/50 rounded-sm;
        }
    }

    body {
        @apply bg-amber-50;
    }
</style>