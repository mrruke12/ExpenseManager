<template>
  <div class="min-h-screen flex flex-col max-w-8/10 m-auto">
    <!-- Хедер всегда вверху -->
    <HeaderComponent class="flex-shrink-0"/>
    
    <!-- Основной контент с flex-grow для растягивания -->
    <main class="flex-grow flex flex-col">
      <!-- Контейнер для слота с центрированием содержимого -->
      <div class="flex-grow flex items-center justify-center p-4">
        <slot></slot>
      </div>
    </main>
    
    <!-- Футер всегда внизу -->
    <FooterComponent class="flex-shrink-0"/>
  </div>
</template>

<script setup>
import HeaderComponent from '../components/HeaderComponent.vue'
import FooterComponent from '../components/FooterComponent.vue'
</script>