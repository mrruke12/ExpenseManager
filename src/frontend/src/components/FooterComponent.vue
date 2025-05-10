<template>
    <footer class="bg-gray-900 text-white py-8">
    <div class="container mx-auto px-6 text-center">
        <div class="flex justify-center mb-6">
        <img src="../assets/kermitthefroghappy.png" alt="Kermit" class="h-12 w-12 animate-bounce">
        </div>
        <p class="text-emerald-300 font-bold text-lg mb-2">Don't waste your croak... cash!</p>
        <p class="text-gray-400">© 2023 Kermit Finance. {{ t("all_rights_reserved") }}</p>
    </div>
    </footer>
</template>

<script setup>
    import { useI18n } from 'vue-i18n'
    const { t, locale } = useI18n()
</script>