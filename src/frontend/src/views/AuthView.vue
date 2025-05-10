<template>
  <ContainerComponent>
    <!-- Убираем внешний div и оставляем только форму -->
    <div class="w-full max-w-md bg-white rounded-2xl shadow-xl overflow-hidden border border-emerald-100">
      <!-- Переключатель входа/регистрации -->
      <div class="flex bg-gradient-to-r from-emerald-50 to-green-50">
        <button @click="isLogin = true; resetForm()" :class="[
            'flex-1 py-4 font-bold text-center transition-colors',
            isLogin
                ? 'bg-emerald-500 text-white shadow-inner'
                : 'text-emerald-600 hover:bg-emerald-100'
        ]">
          {{ t('login_title') }}
        </button>
        <button @click="isLogin = false; resetForm()" :class="[
            'flex-1 py-4 font-bold text-center transition-colors',
            !isLogin
                ? 'bg-emerald-500 text-white shadow-inner'
                : 'text-emerald-600 hover:bg-emerald-100'
        ]">
          {{ t("register_button") }}
        </button>
      </div>

      <!-- Форма -->
      <div class="p-8">
        <!-- Kермит-амбассадор -->
        <div class="flex justify-center mb-6">
          <img src="../assets/KermitTheFrogHello.png" alt="Kermit Finance" class="w-30 object-contain">
        </div>

        <h2 class="text-2xl font-bold text-center mb-6 text-gray-800">
          {{ isLogin ? t('welcome_back') : t('start_with_kermit') }} 
        </h2>

        <div v-if="formError" class="mb-4 p-3 bg-red-50 text-red-600 rounded-lg text-sm">
          {{ formError }}
        </div>

        <form @submit.prevent="submitForm" class="space-y-4" novalidate>
          <!-- Поле Email -->
          <div>
            <label for="email" class="block text-sm font-medium text-gray-700 mb-1">{{ t("email_title") }}</label>
            <input 
              v-model="form.email" 
              @blur="validateEmail" 
              type="text" 
              id="email" 
              required 
              placeholder="your@email.com"
              :class="[
                  'w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 transition',
                  errors.email ? 'border-red-500' : 'border-gray-300'
              ]">
            <p v-if="errors.email" class="mt-1 text-sm text-red-600">
              {{ errors.email }}
            </p>
          </div>

          <!-- Поле Пароля -->
          <div>
            <label for="password" class="block text-sm font-medium text-gray-700 mb-1">
              {{ t("password_title") }}
            </label>
            <input 
              v-model="form.password" 
              @blur="validatePassword" 
              type="password" 
              id="password" 
              required
              :placeholder="isLogin ? t('password_placeholder') : t('new_password_placeholder')"
              :class="[ 
                  'w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 transition',
                  errors.password ? 'border-red-500' : 'border-gray-300'
              ]">
            <p v-if="errors.password" class="mt-1 text-sm text-red-600">
              {{ errors.password }}
            </p>
          </div>

          <!-- Дополнительные поля для регистрации -->
          <div v-if="!isLogin" class="space-y-4">
            <!-- Подтверждение пароля -->
            <div>
              <label for="confirmPassword" class="block text-sm font-medium text-gray-700 mb-1">
                {{ t("password_confirmation_title") }}
              </label>
              <input 
                v-model="form.confirmPassword" 
                @blur="validatePasswordsMatch" 
                type="password" 
                id="confirmPassword" 
                required
                :placeholder="t('password_confirmation_placeholder')"
                :class="[ 
                    'w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-emerald-500 focus:border-emerald-500 transition',
                    errors.confirmPassword ? 'border-red-500' : 'border-gray-300'
                ]">
              <p v-if="errors.confirmPassword" class="mt-1 text-sm text-red-600">
                {{ errors.confirmPassword }}
              </p>
            </div>
          </div>

          <!-- Кнопка отправки -->
          <button 
            type="submit"
            :disabled="isSubmitting" 
            class="w-full py-3 px-4 bg-gradient-to-r from-emerald-500 to-green-600 text-white font-bold rounded-lg shadow-md hover:shadow-lg transition-all transform hover:-translate-y-0.5 focus:outline-none focus:ring-2 focus:ring-emerald-500 focus:ring-opacity-50 disabled:opacity-70 disabled:cursor-not-allowed">
            {{ isSubmitting ? t('loading') : isLogin ? t('login_button') : t('register_button') }} 
          </button>
        </form>

        <!-- Ссылка на смену режима -->
        <p class="mt-6 text-center text-sm text-gray-500">
          {{ isLogin ? t('no_account_yet') : t('already_have_account') }}
          <button @click="isLogin = !isLogin; resetForm()"
              class="text-emerald-600 font-medium hover:text-emerald-800 focus:outline-none">
            {{ isLogin ? t('register_button') : t('login_button') }}
          </button>
        </p>
      </div>

      <!-- Юмористическая подпись -->
      <div class="bg-emerald-50 px-8 py-4 text-center text-sm text-gray-600 border-t border-emerald-100">
        <p>"{{ isLogin ? t('login_quote') : t('register_quote') }}"</p>
        <p class="mt-1 text-xs">— Kermit Finance Team</p>
      </div>
    </div>
  </ContainerComponent>
</template>

<script setup>
import ContainerComponent from '../components/ContainerComponent.vue'

import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import api from '../api/api'
import { useFormValidator } from '../utils/validators'
import HeaderComponent from '../components/HeaderComponent.vue'
import FooterComponent from '../components/FooterComponent.vue'
import { checkAuth } from '../utils/auth'

const { validateEmail, validatePassword, validatePasswordsMatch } = useFormValidator()
const { t, locale } = useI18n()
const router = useRouter()
const isLogin = ref(true)

const form = ref({
    email: '',
    password: '',
    confirmPassword: ''
})

const errors = ref({
  email: '',
  password: '',
  confirmPassword: ''
})

const formError = ref('')
const isSubmitting = ref(false)


const validateForm = () => {
  errors.value.email = validateEmail(form.value.email)
  errors.value.password = validatePassword(form.value.password)
  if (!isLogin.value) errors.value.confirmPassword = validatePasswordsMatch(form.value.password, form.value.confirmPassword)

  return !errors.value.email && !errors.value.password && !errors.value.confirmPassword
}

const resetForm = () => {
    form.value = {
        email: '',
        password: '',
        confirmPassword: ''
    }
    errors.value = {
        email: '',
        password: '',
        confirmPassword: ''
    }
    formError.value = ''
}


const submitForm = async () => {
    if (!validateForm()) return

    try {
        const response = ref({ token: "" })

        if (isLogin.value) {
            response.value = await api.post("/auth/login", {
                email: form.value.email,
                password: form.value.password
            })

            localStorage.setItem('token', response.value.data.token)
            api.defaults.headers['Authorization'] = `Bearer ${response.value.data.token}`
            
            router.push('/dashboard')
        } else {
            response.value = await api.post("/auth/register", {
                email: form.value.email,
                password: form.value.password,
                verification: form.value.confirmPassword
            })
            
            router.go('/auth')
        }

        
    } catch (error) {
        if (error.status == 409) formError.value = t('validation.email_conflict')
        else formError.value = t("validation.auth_fail")
    } finally {
        isSubmitting.value = false
    }
}

onMounted(async () => {
    const isAuthenticated = await checkAuth()
    if (isAuthenticated) {
        router.push('/dashboard')
    }
})
</script>

<style>
/* Анимация переключения между формами */
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>