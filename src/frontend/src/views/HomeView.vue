<template>
    <container-component Home>

        <!-- Баннер с Кермитом -->
        <div
            class="flex flex-col items-center md:flex-row pt-5 px-10 bg-linear-to-b from-amber-100 from-10% via-amber-200 via-30% to-blue-100 to-90%">
            <div class="border-l-[30px] border-emerald-500 min-h-full pl-10 relative w-full mb-5 md:m-0">
                <svg width="20%" height="20%" viewBox="0 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg">
                    <path class="fill-emerald-900"
                        d="M9.563 8.469l-0.813-1.25c-5.625 3.781-8.75 8.375-8.75 12.156 0 3.656 2.688 5.375 4.969 5.375 2.875 0 4.906-2.438 4.906-5 0-2.156-1.375-4-3.219-4.688-0.531-0.188-1.031-0.344-1.031-1.25 0-1.156 0.844-2.875 3.938-5.344zM21.969 8.469l-0.813-1.25c-5.563 3.781-8.75 8.375-8.75 12.156 0 3.656 2.75 5.375 5.031 5.375 2.906 0 4.969-2.438 4.969-5 0-2.156-1.406-4-3.313-4.688-0.531-0.188-1-0.344-1-1.25 0-1.156 0.875-2.875 3.875-5.344z">
                    </path>
                </svg>

                <h1 class="text-emerald-700 text-[3vw] font-bold text-shadow-lg/10">{{ t('home.main-quote') }}</h1>
                <h2 class="text-emerald-900 text-[2vw] font-bold flex justify-end">- Kermit the Frog</h2>
            </div>

            <img class="w-1/2 md:w-4/11" src="../assets/KermitTheFrogBusiness.png" alt="">
        </div>

        <div>
            <slider-component sliderId="slider1">
                <template #slide-0>
                    <div class="w-full min-h-[500px] md:min-h-[600px] flex items-center justify-center p-4 sm:p-6 md:p-10 bg-gradient-to-br from-lime-50 to-green-50 relative overflow-hidden">
                        <div class="absolute top-10 left-10 w-16 h-16 md:w-20 md:h-20 bg-green-300 rounded-full opacity-20"></div>
                        <div class="absolute bottom-10 right-10 w-24 h-24 md:w-32 md:h-32 bg-green-200 rounded-full opacity-30"></div>

                        <div class="relative z-10 w-full max-w-4xl">
                            <div class="flex flex-col md:flex-row items-center md:items-start gap-6 md:gap-8">
                                <div class="flex-shrink-0">
                                    <div class="bg-green-100 p-4 md:p-6 rounded-2xl shadow-lg border-4 border-green-200">
                                        <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 md:h-16 md:w-16 text-green-700" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                                        </svg> 
                                    </div>
                                </div>

                                <div class="text-center md:text-left">
                                    <h2 class="text-3xl md:text-4xl font-bold text-green-800 mb-4 md:mb-6 font-serif">{{ t('home.financial-title') }}</h2>
                                    <div class="grid grid-cols-1 sm:grid-cols-2 gap-4 md:gap-6 mb-6 md:mb-8">
                                        <div class="bg-white/80 p-4 md:p-5 rounded-xl backdrop-blur-sm shadow-md">
                                            <h3 class="text-lg md:text-xl font-bold text-green-700 mb-2 md:mb-3 flex flex-col md:flex-row items-center md:items-start">
                                                <span class="inline-block w-8 h-8 bg-green-100 rounded-full flex items-center text-center mb-2 md:mb-0 md:mr-3">🐸</span> {{ t("home.financial-benefit-1") }}
                                            </h3>
                                            <p class="text-sm md:text-base text-green-600">{{ t('home.financial-benefit-2') }}</p>
                                        </div>
                                        <div class="bg-white/80 p-4 md:p-5 rounded-xl backdrop-blur-sm shadow-md">
                                            <h3 class="text-lg md:text-xl font-bold text-green-700 mb-2 md:mb-3 flex flex-col md:flex-row items-center md:items-start">
                                                <span class="inline-block w-8 h-8 bg-green-100 rounded-full flex items-center text-center mb-2 md:mb-0 md:mr-3">🏦</span> {{ t('home.financial-benefit-3') }}
                                            </h3>
                                            <p class="text-sm md:text-base text-green-600">{{ t('home.financial-benefit-4') }}</p>
                                        </div>
                                    </div>
                                    <div class="bg-green-100/60 p-3 md:p-4 rounded-lg inline-block">
                                        <p class="text-sm md:text-base text-green-800 italic">{{ t('home.financial-quote') }}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </template>

                <template #slide-1>
                    <div
                        class="w-full min-h-[500px] md:min-h-[600px] flex items-center justify-center p-4 sm:p-6 md:p-10 bg-gradient-to-br from-teal-50 to-blue-50 relative overflow-hidden">
                        <div
                            class="absolute top-0 right-0 w-24 h-24 md:w-40 md:h-40 bg-teal-200 rounded-full opacity-20">
                        </div>
                        <div
                            class="absolute bottom-10 left-1/4 w-16 h-16 md:w-24 md:h-24 bg-blue-200 rounded-full opacity-30">
                        </div>

                        <div class="relative z-10 w-full max-w-4xl">
                            <div class="flex flex-col md:flex-row items-center md:items-start gap-6 md:gap-8">
                                <div class="flex-shrink-0">
                                    <div class="bg-blue-100 p-4 md:p-6 rounded-2xl shadow-lg border-4 border-blue-200">
                                        <svg xmlns="http://www.w3.org/2000/svg"
                                            class="h-12 w-12 md:h-16 md:w-16 text-blue-700" fill="none"
                                            viewBox="0 0 24 24" stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                d="M4 5a1 1 0 011-1h14a1 1 0 011 1v2a1 1 0 01-1 1H5a1 1 0 01-1-1V5zM4 13a1 1 0 011-1h6a1 1 0 011 1v6a1 1 0 01-1 1H5a1 1 0 01-1-1v-6zM16 13a1 1 0 011-1h2a1 1 0 011 1v6a1 1 0 01-1 1h-2a1 1 0 01-1-1v-6z" />
                                        </svg>
                                    </div>
                                </div>

                                <div class="text-center md:text-left">
                                    <h2 class="text-3xl md:text-4xl font-bold text-blue-800 mb-4 md:mb-6 font-serif">{{ t('home.transfer-title') }}</h2>
                                    <div class="flex flex-col md:flex-row gap-4 md:gap-6 mb-6 md:mb-8">
                                        <div class="bg-white/80 p-4 md:p-5 rounded-xl backdrop-blur-sm shadow-md">
                                            <h3 class="text-lg md:text-xl font-bold text-blue-700 mb-2 md:mb-3">{{ t("home.transfer-benefit-1") }}</h3>
                                            <p class="text-sm md:text-base text-blue-600 mb-3 md:mb-4">{{ t('home.transfer-benefit-2') }}</p>
                                            <div class="flex flex-wrap justify-center md:justify-start gap-2">
                                                <span
                                                    class="px-2 py-1 bg-blue-100 text-blue-800 rounded-full text-xs md:text-sm">{{ t('home.transfer-benefit-3') }}</span>
                                                <span
                                                    class="px-2 py-1 bg-blue-100 text-blue-800 rounded-full text-xs md:text-sm">{{ t('home.transfer-benefit-4') }}</span>
                                            </div>
                                        </div>
                                        <div class="bg-white/80 p-4 md:p-5 rounded-xl backdrop-blur-sm shadow-md">
                                            <h3 class="text-lg md:text-xl font-bold text-blue-700 mb-2 md:mb-3">{{ t('home.transfer-benefit-5') }}</h3>
                                            <div class="space-y-2 md:space-y-3">
                                                <div class="flex items-center">
                                                    <div class="w-2 h-2 md:w-3 md:h-3 bg-blue-500 rounded-full mr-2">
                                                    </div>
                                                    <span
                                                        class="text-sm md:text-base text-blue-700 flex-1">{{ t('home.transfer-benefit-6') }}</span>
                                                    <span class="font-bold text-blue-800">25%</span>
                                                </div>
                                                <div class="flex items-center">
                                                    <div class="w-2 h-2 md:w-3 md:h-3 bg-blue-400 rounded-full mr-2">
                                                    </div>
                                                    <span
                                                        class="text-sm md:text-base text-blue-700 flex-1">{{ t('home.transfer-benefit-7') }}</span>
                                                    <span class="font-bold text-blue-800">18%</span>
                                                </div>
                                                <div class="flex items-center">
                                                    <div class="w-2 h-2 md:w-3 md:h-3 bg-blue-300 rounded-full mr-2">
                                                    </div>
                                                    <span
                                                        class="text-sm md:text-base text-blue-700 flex-1">{{ t('home.transfer-benefit-8') }}</span>
                                                    <span class="font-bold text-blue-800">15%</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bg-blue-100/60 p-3 md:p-4 rounded-lg inline-block">
                                        <p class="text-sm md:text-base text-blue-800 italic">{{ t("home.transfer-quote") }}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </template>

                <template #slide-2>
                    <div
                        class="w-full min-h-[500px] md:min-h-[600px] flex items-center justify-center p-4 sm:p-6 md:p-10 bg-gradient-to-br from-purple-50 to-indigo-50 relative overflow-hidden">
                        <div
                            class="absolute bottom-0 right-0 w-32 h-32 md:w-48 md:h-48 bg-purple-200 rounded-full opacity-20">
                        </div>
                        <div
                            class="absolute top-1/3 left-4 md:left-10 w-12 h-12 md:w-16 md:h-16 bg-indigo-200 rounded-full opacity-30">
                        </div>

                        <div class="relative z-10 w-full max-w-4xl">
                            <div class="flex flex-col md:flex-row items-center md:items-start gap-6 md:gap-8">
                                <div class="flex-shrink-0">
                                    <div
                                        class="bg-purple-100 p-4 md:p-6 rounded-2xl shadow-lg border-4 border-purple-200">
                                        <svg xmlns="http://www.w3.org/2000/svg"
                                            class="h-12 w-12 md:h-16 md:w-16 text-purple-700" fill="none"
                                            viewBox="0 0 24 24" stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z" />
                                        </svg>
                                    </div>
                                </div>

                                <div class="text-center md:text-left">
                                    <h2 class="text-3xl md:text-4xl font-bold text-purple-800 mb-4 md:mb-6 font-serif">{{ t('home.analytics-title') }}</h2>
                                    <div
                                        class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 md:gap-6 mb-6 md:mb-8">
                                        <div class="bg-white/80 p-4 md:p-5 rounded-xl backdrop-blur-sm shadow-md">
                                            <h3 class="text-lg md:text-xl font-bold text-purple-700 mb-2 md:mb-3">{{ t('home.analytics-benefit-1') }}</h3>
                                            <div class="h-24 md:h-32 flex items-end space-x-1 md:space-x-2 justify-center">
                                                <div class="w-4 md:w-6 bg-purple-300 rounded-t" style="height: 40%">
                                                </div>
                                                <div class="w-4 md:w-6 bg-purple-400 rounded-t" style="height: 60%">
                                                </div>
                                                <div class="w-4 md:w-6 bg-purple-500 rounded-t" style="height: 80%">
                                                </div>
                                                <div class="w-4 md:w-6 bg-purple-600 rounded-t" style="height: 50%">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bg-white/80 p-4 md:p-5 rounded-xl backdrop-blur-sm shadow-md">
                                            <h3 class="text-lg md:text-xl font-bold text-purple-700 mb-2 md:mb-3">{{ t('home.analytics-benefit-2') }}</h3>
                                            <div class="w-20 h-20 md:w-32 md:h-32 mx-auto relative">
                                                <div class="absolute inset-0 rounded-full border-4 md:border-8 border-purple-300">
                                                </div>
                                                <div class="absolute inset-0 rounded-full border-4 md:border-8 border-purple-500" style="clip-path: polygon(0 0, 100% 0, 100% 50%, 0 50%)"></div>
                                                <div class="absolute inset-0 rounded-full border-4 md:border-8 border-purple-700" style="clip-path: polygon(0 0, 50% 0, 50% 100%, 0 100%)"></div>
                                            </div>
                                        </div>
                                        <div class="bg-white/80 p-4 md:p-5 rounded-xl backdrop-blur-sm shadow-md">
                                            <h3 class="text-lg md:text-xl font-bold text-purple-700 mb-2 md:mb-3">{{ t('home.analytics-benefit-3') }}</h3>
                                            <ul class="space-y-1 md:space-y-2 text-purple-600">
                                                <li class="flex items-center text-sm md:text-base"><span class="mr-1 md:mr-2">📅</span> {{ t('home.analytics-benefit-4') }}</li>
                                                <li class="flex items-center text-sm md:text-base"><span class="mr-1 md:mr-2">🔄</span> {{ t('home.analytics-benefit-5') }}</li>
                                                <li class="flex items-center text-sm md:text-base"><span class="mr-1 md:mr-2">💾</span> {{ t('home.analytics-benefit-6') }}</li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="bg-purple-100/60 p-3 md:p-4 rounded-lg inline-block">
                                        <p class="text-sm md:text-base text-purple-800 italic">{{ t('home.analytics-quote') }}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </template>
            </slider-component>
        </div>



    </container-component>
</template>

<script setup>
import SliderComponent from '../components/SliderComponent.vue'
import ContainerComponent from '../components/ContainerComponent.vue'

import { useI18n } from 'vue-i18n'

const { t, locale } = useI18n()


</script>

<style scoped>
@reference 'tailwindcss';

#slides div {
    @apply w-1/5 h-auto flex items-center;

}

#slider button {
    @apply cursor-pointer;
}
</style>