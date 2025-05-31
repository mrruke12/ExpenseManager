<template>
    <container-component Sticky>
        <div class="absolute top-0 bottom-0 left-0 right-0 bg-white flex flex-col items-center justify-center space-y-10">
            <img class="h-1/2" src="../assets/KermitTheFrogPanic.png" alt="">
            <h1 class="text-3xl font-bold text-center">{{ t('404.not-found') }}</h1>
        </div>
    </container-component>
</template>

<script setup>
    import ContainerComponent from '../components/ContainerComponent.vue'
    import { useI18n } from 'vue-i18n'

    const { t, locale } = useI18n()
</script>