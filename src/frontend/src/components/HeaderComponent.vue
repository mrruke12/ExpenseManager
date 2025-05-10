<template>
  <header class="bg-gray-900 pt-4 pb-4 px-6 sm:px-12">
    <div class="container mx-auto flex items-center justify-between">
      <!-- Логотип и название -->
      <router-link to="/" class="flex items-center group flex-shrink-0">
        <img 
          src="../assets/KermitTheFrogHappy.png" 
          alt="Kermit Finance"
          class="h-12 w-12 object-contain transition-transform group-hover:scale-110"
        >
        <h1 class="ml-3 text-xl font-bold text-white group-hover:text-emerald-600 transition-colors">
          Kermit<span class="text-emerald-500">Finance</span>
        </h1>
      </router-link>

      <!-- Навигация (скрывается на мобильных) -->
      <nav class="hidden md:flex space-x-4 mx-4">
        <router-link 
          to="/" 
          class="px-4 py-2 text-gray-300 hover:text-white hover:bg-gray-800 rounded-lg transition-colors"
          active-class="text-white bg-gray-800"
        >
          {{ t('home') }}
        </router-link>
        <router-link 
          to="/dashboard" 
          class="px-4 py-2 text-gray-300 hover:text-white hover:bg-gray-800 rounded-lg transition-colors"
          active-class="text-white bg-gray-800"
        >
          {{ t('dashboard') }}
        </router-link>
      </nav>

      <!-- Управление (кнопки справа) -->
      <div class="flex items-center space-x-3">
        <ChangeLang class=" sm:flex" />
        <LogoutComponent v-if="isAuthenticated" />
      </div>
    </div>

    <!-- Мобильная навигация (показывается только на мобильных) -->
    <div class="container mx-auto mt-3 flex md:hidden justify-center space-x-4">
      <router-link 
        to="/" 
        class="px-4 py-2 text-sm text-gray-300 hover:text-white hover:bg-gray-800 rounded-lg transition-colors"
        active-class="text-white bg-gray-800"
      >
        {{ t('home') }}
      </router-link>
      <router-link 
        to="/dashboard" 
        class="px-4 py-2 text-sm text-gray-300 hover:text-white hover:bg-gray-800 rounded-lg transition-colors"
        active-class="text-white bg-gray-800"
      >
        {{ t('dashboard') }}
      </router-link>
    </div>
  </header>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { checkAuth } from '../utils/auth' // Ваша функция проверки аутентификации
import ChangeLang from './ChangeLang.vue'
import LogoutComponent from './LogoutComponent.vue'

const { t } = useI18n()
const isAuthenticated =  ref(false) // Реактивная проверка авторизации

onMounted(async () => {
  isAuthenticated.value = await checkAuth()
})


</script>

<style scoped>
.router-link-active {
  font-weight: 500;
}
</style>