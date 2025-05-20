<template>
    <div class="flex min-h-screen flex-col">
        <HeaderComponent :Home="Home" :Dashboard="Dashboard"/>

        <main class="flex-grow">
            <slot></slot>
        </main>

        <FooterComponent />
    </div>
</template>

<script setup>
    import HeaderComponent from "../components/HeaderComponent.vue"
    import FooterComponent from "../components/FooterComponent.vue";

    const props = defineProps({
        Home: Boolean,
        Dashboard: Boolean
    })
</script>