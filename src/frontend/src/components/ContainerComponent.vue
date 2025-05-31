<template>
    <div class="flex min-h-screen flex-col">
        <HeaderComponent :Home="Home" :Dashboard="Dashboard" :Sticky="Sticky"/>

        <main :class="`flex-grow relative flex flex-col ${Center ? 'items-center justify-center' : ''}`">
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
        Dashboard: Boolean,
        Center: Boolean,
        Sticky: Boolean
    })
</script>