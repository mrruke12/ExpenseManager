<template>
  <button
    @click="logout(router)"
    class="logout-button flex items-center gap-2 px-4 py-2 rounded-lg transition-colors"
    :title="t('logout_tooltip')"
  >
    <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
    </svg>
    <span>{{ t('logout_button') }}</span>
  </button>
</template>


<style scoped>
.logout-button {
  background-color: rgba(239, 68, 68, 0.1);
  color: #ef4444;
  border: 1px solid rgba(239, 68, 68, 0.3);
}

.logout-button:hover {
  background-color: rgba(239, 68, 68, 0.2);
}

.logout-button:active {
  transform: scale(0.98);
}
</style>

<script setup>
    import { logout } from '../utils/auth';
    import { useRouter } from 'vue-router';
    import { useI18n } from 'vue-i18n'
    
    const { t } = useI18n()
    const router = useRouter()
</script>