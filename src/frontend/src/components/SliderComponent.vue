<template>
    <div class="slider transition" :id="sliderId">
        <div v-for="(slide, index) in slides" :key="index" class="slide">
            <slot :name="`slide-${index}`"></slot>
        </div>
    </div>
</template>

<script setup>
import { useSlots, ref, onMounted, nextTick, defineProps } from 'vue'

const props = defineProps({
    sliderId: {
        type: String,
        required: true,
    },
});

const slots = useSlots()
const slides = []

Object.keys(slots).forEach((slotName) => {
    if (slotName.startsWith('slide')) {
        slides.push(slots[slotName]())
    }
});

const slideRefs = ref([])

let progress = 0
let direction = 1
let max = 0
const xshift = ref(0)

const slide = () => {
    if (progress == max-1 && direction == 1 || progress == 0 && direction == -1) direction *= -1
    progress += direction

    slideRefs.value.forEach((s, index) => {
        xshift.value = 100*progress
        console.log(xshift.value)
        s.style.translate = `-${xshift.value}%`
    })
}


onMounted(() => {
    const slider = document.querySelector(`#${props.sliderId}`)
    slideRefs.value = slider.querySelectorAll(".slide")
    max = slider.childNodes.length - 2
    setInterval(slide, 6000)
});


</script>

<style scoped>
@reference 'tailwindcss';

.slider {
    @apply overflow-hidden flex;
}

.slide {
    @apply flex-none w-full min-h-full transition duration-1000 ease-in-out;
}
</style>