<template>
  <div class="">
    <button
      @click="toggleLanguage"
      class="language-switcher group flex items-center gap-2 px-3 py-2 rounded-full bg-white/90 backdrop-blur-sm border border-emerald-300/30 hover:border-emerald-400/50 shadow-lg hover:shadow-emerald-400/20 transition-all duration-300"
      :title="currentLanguage === 'en' ? 'Switch to Russian' : 'Переключить на английский'"
    >
      <!-- Иконка глобуса с анимацией -->
      <div class="relative w-5 h-5">
        <svg 
          class="w-5 h-5 text-emerald-600 group-hover:text-emerald-800 transition-colors duration-300"
          viewBox="0 0 24 24" 
          fill="none"
        >
          <path 
            d="M12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22Z" 
            stroke="currentColor" 
            stroke-width="1.5"
          />
          <path 
            d="M8 3H9C7.05005 8.08427 7.05005 15.9157 9 21H8M15 3C16.9499 8.08427 16.9499 15.9157 15 21" 
            stroke="currentColor" 
            stroke-width="1.5"
          />
          <path 
            d="M3 16V15C8.08427 16.9499 15.9157 16.9499 21 15V16M3 9C8.08427 7.05005 15.9157 7.05005 21 9" 
            stroke="currentColor" 
            stroke-width="1.5"
          />
        </svg>
        <div 
          class="absolute -inset-1 bg-emerald-400/10 rounded-full scale-0 group-hover:scale-100 transition-transform duration-300"
        ></div>
      </div>

      <!-- Текущий язык с анимацией -->
      <span class="text-sm font-medium text-gray-800 tracking-wider">
        {{ currentLanguage.toUpperCase() }}
      </span>

      <!-- Индикатор смены языка -->
      <span class="text-xs text-emerald-600 font-bold opacity-0 group-hover:opacity-100 transition-opacity duration-300">
        {{ currentLanguage === 'en' ? 'RU' : 'EN' }}
      </span>
    </button>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'

const { locale } = useI18n()
const currentLanguage = ref('en')

// Инициализация из localStorage
onMounted(() => {
  const savedLang = localStorage.getItem('kermit-lang')
  if (savedLang) {
    currentLanguage.value = savedLang
    locale.value = savedLang
  } else {
    // Определение языка браузера
    const userLang = navigator.language.startsWith('ru') ? 'ru' : 'en'
    currentLanguage.value = userLang
    locale.value = userLang
    localStorage.setItem('kermit-lang', userLang)
  }
})

const toggleLanguage = () => {
  const newLang = currentLanguage.value === 'en' ? 'ru' : 'en'
  currentLanguage.value = newLang
  locale.value = newLang
  localStorage.setItem('kermit-lang', newLang)
  
  // Анимация "щелчка"
  const btn = document.querySelector('.language-switcher')
  btn.classList.add('animate-ping-once')
  setTimeout(() => {
    btn.classList.remove('animate-ping-once')
  }, 300)
}
</script>

<style scoped>
@keyframes ping-once {
  0% {
    transform: scale(1);
    opacity: 1;
  }
  50% {
    transform: scale(1.1);
    opacity: 0.7;
  }
  100% {
    transform: scale(1);
    opacity: 1;
  }
}

.animate-ping-once {
  animation: ping-once 0.3s ease-out;
}

.language-switcher {
  box-shadow: 0 4px 15px rgba(16, 185, 129, 0.15);
}

.language-switcher:hover {
  box-shadow: 0 6px 20px rgba(16, 185, 129, 0.25);
  transform: translateY(-1px);
}
</style>