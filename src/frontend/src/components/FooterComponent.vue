<template>
    <footer class="bg-linear-to-b from-emerald-900 to-green-800 text-white flex flex-col items-center p-5">
        <img class="w-10 animate-bounce" src="../assets/KermitTheFrogHappy.png" alt="Kermit the Frog">
        <h2 class="animate-pulse">{{ t('footer.all-rights-reserved') }}.</h2>
    </footer>
</template>

<script setup>
    import { useI18n } from 'vue-i18n'

    const { t, locale } = useI18n()
</script>