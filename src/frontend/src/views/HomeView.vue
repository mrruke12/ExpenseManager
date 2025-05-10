<template>
    <container-component>
        <div class="w-full bg-gradient-to-b from-green-50 to-white">
            <!-- Космический финансовый Kермит с затемненным фоном -->
            <section
                class="relative overflow-hidden py-20 px-6 sm:px-12 lg:px-24 rounded-b-3xl border-b-8 border-emerald-400">
                <!-- Затемненный фон с звездами -->
                <div class="absolute inset-0 bg-[url('./assets/stars.png')]">
                    <div class="absolute inset-0 bg-black" style="opacity: 0.5;"></div>
                </div>

                <!-- Анимированные элементы фона -->
                <div class="absolute inset-0 overflow-hidden">
                    <div
                        class="absolute top-1/4 left-1/4 w-32 h-32 bg-emerald-400 rounded-full filter blur-[80px] opacity-20 animate-pulse">
                    </div>
                    <div
                        class="absolute bottom-1/3 right-1/3 w-40 h-40 bg-purple-500 rounded-full filter blur-[100px] opacity-15">
                    </div>
                    <div
                        class="absolute top-10 right-20 w-16 h-16 bg-yellow-300 rounded-full filter blur-[60px] opacity-30 animate-float">
                    </div>
                </div>

                <div class="container mx-auto flex flex-col lg:flex-row items-center gap-12 relative z-10">
                    <!-- Текстовая часть с 3D эффектом -->
                    <div class="lg:w-1/2">
                        <div class="mb-8 transform perspective-1000 group">
                            <h1
                                class="text-5xl md:text-6xl font-extrabold mb-6 leading-tight text-transparent bg-clip-text bg-gradient-to-r from-emerald-300 to-yellow-300 group-hover:rotate-x-6 transition-transform duration-500">
                                <span class="inline-block">Kermit<span class="text-white">.finance</span></span>
                            </h1>
                            <p class="text-xl text-emerald-100 font-medium">{{ t('slogan') }}</p>
                        </div>

                        <!-- Цитата в "голографическом" стиле -->
                        <div class="relative inline-block">
                            <div
                                class="absolute -inset-2 bg-gradient-to-r from-emerald-400 to-purple-500 rounded-xl blur-md opacity-50">
                            </div>
                            <div
                                class="relative bg-gray-900 bg-opacity-70 backdrop-blur-md rounded-xl p-6 border-2 border-emerald-300 border-opacity-50 shadow-lg">
                                <div class="flex items-start">
                                    <div class="text-4xl mr-3 text-yellow-300">"</div>
                                    <div>
                                        <p class="text-lg italic text-white">
                                            <span class="text-emerald-300 font-bold">{{ t('quote') }}</span>
                                        </p>
                                        <div class="mt-3 flex items-center">
                                            <div class="w-8 h-0.5 bg-emerald-300 mr-2"></div>
                                            <span class="text-emerald-200 font-mono text-sm">Kermit the Frog</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Кнопка с эффектом "космического корабля" -->
                        <button @click="goToAuth"
                            class="mx-auto my-12 px-8 py-4 bg-black bg-opacity-60 border-2 border-emerald-300 text-white font-bold rounded-full shadow-lg hover:shadow-emerald-300/30 transition-all group overflow-hidden relative">
                            <span class="relative z-10 flex items-center">
                                <span>{{ t('try_button') }}</span>
                                <svg class="w-5 h-5 ml-2 group-hover:translate-x-1 transition-transform" fill="none"
                                    stroke="currentColor" viewBox="0 0 24 24">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                        d="M14 5l7 7m0 0l-7 7m7-7H3"></path>
                                </svg>
                            </span>
                            <span
                                class="absolute inset-0 bg-gradient-to-r from-emerald-400 to-transparent opacity-0 group-hover:opacity-100 transition-opacity duration-300 -skew-x-12"></span>
                        </button>
                    </div>

                    <!-- Изображение Kермита в "космическом" стиле -->
                    <div class="lg:w-1/2 relative">
                        <div class="relative max-w-md mx-auto">
                            <!-- Декоративные элементы -->
                            <div
                                class="absolute -top-10 -left-10 w-32 h-32 bg-emerald-400 rounded-full filter blur-[70px] opacity-20">
                            </div>
                            <div
                                class="absolute -bottom-5 -right-5 w-40 h-40 bg-purple-500 rounded-full filter blur-[90px] opacity-15">
                            </div>

                            <!-- Основное изображение с "голографическим" эффектом -->
                            <div class="relative z-10">
                                <div class="absolute inset-0 bg-emerald-400 rounded-2xl opacity-20 filter blur-md">
                                </div>
                                <img src="../assets/KermitTheFrogBusiness.png" alt="Kermit the Frog"
                                    class="relative z-20 w-full h-auto rounded-2xl border-4 border-emerald-300 shadow-[0_0_30px_rgba(110,231,183,0.5)] transform rotate-2 hover:rotate-0 transition-transform duration-500">
                            </div>

                            <!-- Плавающие денежные элементы -->
                            <div class="absolute -top-5 -right-5 z-30 animate-float-delay">
                                <div
                                    class="bg-white text-emerald-600 font-bold rounded-full p-3 shadow-lg flex items-center justify-center w-12 h-12">
                                    $$$
                                </div>
                            </div>
                            <div class="absolute -bottom-3 -left-5 z-30 animate-float">
                                <div
                                    class="bg-yellow-300 text-black font-bold rounded-full p-2 shadow-lg flex items-center text-center justify-center w-20 h-20 text-sm">
                                    TO THE MOON
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <!-- Основной контент -->
            <div class="container mx-auto px-6 py-16 max-w-6xl">
                <!-- Как это работает -->
                <section class="mb-0 bg-gradient-to-r from-emerald-50 to-indigo-50 p-12 rounded-3xl">
                    <h2 class="text-3xl font-bold text-center mb-12 text-gray-800">
                        <span class="relative inline-block">
                            <span class="relative z-10">📲 {{ t('how_it_works') }}</span>
                            <span class="absolute -bottom-1 left-0 w-full h-2 bg-yellow-200 z-0 opacity-60"></span>
                        </span>
                    </h2>

                    <div class="grid md:grid-cols-3 gap-8">
                        <div v-for="(step, index) in steps" :key="index" class="text-center">
                            <div
                                class="mx-auto w-20 h-20 bg-white rounded-full flex items-center justify-center shadow-md mb-6 text-2xl font-bold text-emerald-600">
                                {{ index + 1 }}
                            </div>
                            <h3 class="text-xl font-bold mb-3 text-gray-800">{{ step.title }}</h3>
                            <p class="text-gray-600">{{ step.description }}</p>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </container-component>

</template>

<script setup>
import ContainerComponent from '../components/ContainerComponent.vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import HeaderComponent from '../components/HeaderComponent.vue'
import FooterComponent from '../components/FooterComponent.vue'

const router = useRouter()
const { t, locale } = useI18n()

const steps = [
    {
        title: t('how_it_works_step1_title'),
        description: t('how_it_works_step1_description')
    },
    {
        title: t('how_it_works_step2_title'),
        description: t('how_it_works_step2_description')
    },
    {
        title: t('how_it_works_step3_title'),
        description: t('how_it_works_step3_description')
    }
]

const goToAuth = () => {
    router.push("/auth")
}
</script>

<style scoped>
@keyframes float {

    0%,
    100% {
        transform: translateY(0);
    }

    50% {
        transform: translateY(-15px);
    }
}

@keyframes float-delay {

    0%,
    100% {
        transform: translateY(-5px);
    }

    50% {
        transform: translateY(-20px);
    }
}

@keyframes twinkle {

    0%,
    100% {
        opacity: 0.3;
    }

    50% {
        opacity: 1;
    }
}

.animate-float {
    animation: float 4s ease-in-out infinite;
}

.animate-float-delay {
    animation: float-delay 4s ease-in-out infinite 1s;
}

.perspective-1000 {
    perspective: 1000px;
}

.rotate-x-6 {
    transform: rotateX(6deg);
}
</style>