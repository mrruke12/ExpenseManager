<template>
    <container-component Dashboard>
        <div class="flex flex-col h-full">
            <!-- Main Content -->
            <div v-if="modalView < 0" class="flex flex-col flex-grow">
                <!-- Header Section -->
                <div class="bg-white sticky top-0 z-20 shadow-sm border-b border-gray-100">
                    <!-- Account Picker -->
                    <div class="px-4 py-3 flex items-center">
                        <div class="relative w-full">
                            <select @change="handleAccountChange" v-model="currentAccount"
                                class="w-full pl-4 pr-10 py-3 rounded-xl border border-gray-200 bg-gray-50 focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition appearance-none">
                                <option class="italic text-gray-500" value="-2">{{ t('dashboard.default-account-option')
                                }}</option>
                                <option class="font-medium" v-for="(account, index) in accounts" :value="index">
                                    {{ account.name }}
                                </option>
                                <option class="italic text-amber-600" value="-1">{{ t('dashboard.new-account-option') }}
                                </option>
                            </select>
                            <div class="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none">
                                <svg class="h-5 w-5 text-gray-400" fill="none" viewBox="0 0 24 24"
                                    stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                        d="M19 9l-7 7-7-7" />
                                </svg>
                            </div>
                        </div>
                        <div v-if="currentAccount >= 0"
                            class="px-2 space-x-2 flex transition duration-100 hover:rotate-z-10"
                            @click="handleAccountSettings">
                            <svg width="30px" height="30px" viewBox="0 0 24 24" fill="none"
                                xmlns="http://www.w3.org/2000/svg" class="cursor-pointer">
                                <circle cx="12" cy="12" r="3" class="stroke-current" stroke-width="1.5" />
                                <path
                                    d="M3.66122 10.6392C4.13377 10.9361 4.43782 11.4419 4.43782 11.9999C4.43781 12.558 4.13376 13.0638 3.66122 13.3607C3.33966 13.5627 3.13248 13.7242 2.98508 13.9163C2.66217 14.3372 2.51966 14.869 2.5889 15.3949C2.64082 15.7893 2.87379 16.1928 3.33973 16.9999C3.80568 17.8069 4.03865 18.2104 4.35426 18.4526C4.77508 18.7755 5.30694 18.918 5.83284 18.8488C6.07287 18.8172 6.31628 18.7185 6.65196 18.5411C7.14544 18.2803 7.73558 18.2699 8.21895 18.549C8.70227 18.8281 8.98827 19.3443 9.00912 19.902C9.02332 20.2815 9.05958 20.5417 9.15224 20.7654C9.35523 21.2554 9.74458 21.6448 10.2346 21.8478C10.6022 22 11.0681 22 12 22C12.9319 22 13.3978 22 13.7654 21.8478C14.2554 21.6448 14.6448 21.2554 14.8478 20.7654C14.9404 20.5417 14.9767 20.2815 14.9909 19.9021C15.0117 19.3443 15.2977 18.8281 15.7811 18.549C16.2644 18.27 16.8545 18.2804 17.3479 18.5412C17.6837 18.7186 17.9271 18.8173 18.1671 18.8489C18.693 18.9182 19.2249 18.7756 19.6457 18.4527C19.9613 18.2106 20.1943 17.807 20.6603 17C20.8677 16.6407 21.029 16.3614 21.1486 16.1272M20.3387 13.3608C19.8662 13.0639 19.5622 12.5581 19.5621 12.0001C19.5621 11.442 19.8662 10.9361 20.3387 10.6392C20.6603 10.4372 20.8674 10.2757 21.0148 10.0836C21.3377 9.66278 21.4802 9.13092 21.411 8.60502C21.3591 8.2106 21.1261 7.80708 20.6601 7.00005C20.1942 6.19301 19.9612 5.7895 19.6456 5.54732C19.2248 5.22441 18.6929 5.0819 18.167 5.15113C17.927 5.18274 17.6836 5.2814 17.3479 5.45883C16.8544 5.71964 16.2643 5.73004 15.781 5.45096C15.2977 5.1719 15.0117 4.6557 14.9909 4.09803C14.9767 3.71852 14.9404 3.45835 14.8478 3.23463C14.6448 2.74458 14.2554 2.35523 13.7654 2.15224C13.3978 2 12.9319 2 12 2C11.0681 2 10.6022 2 10.2346 2.15224C9.74458 2.35523 9.35523 2.74458 9.15224 3.23463C9.05958 3.45833 9.02332 3.71848 9.00912 4.09794C8.98826 4.65566 8.70225 5.17191 8.21891 5.45096C7.73557 5.73002 7.14548 5.71959 6.65205 5.4588C6.31633 5.28136 6.0729 5.18269 5.83285 5.15108C5.30695 5.08185 4.77509 5.22436 4.35427 5.54727C4.03866 5.78945 3.80569 6.19297 3.33974 7C3.13231 7.35929 2.97105 7.63859 2.85138 7.87273"
                                    class="stroke-current" stroke-width="1.5" stroke-linecap="round" />
                            </svg>
                        </div>
                    </div>

                    <!-- Navigation Menu -->
                    <div class="flex px-4 relative">
                        <div class="absolute bottom-0 h-1 bg-amber-400 rounded-full transition-all duration-300 ease-in-out"
                            :style="`width: ${100 / 3}%; left: ${(100 / 3) * page}%`"></div>

                        <button v-for="(tab, index) in tabs" @click="changePage(index)"
                            class="flex-1 py-3 flex flex-col items-center justify-center hover:bg-gray-50 transition group"
                            :class="{ 'text-amber-600': page === index }">
                            <component :is="tab.icon" class="w-6 h-6 mb-1 group-hover:text-amber-500 transition" />
                            <span class="text-xs font-medium">{{ tab.label }}</span>
                        </button>
                    </div>

                    <!-- Secondary Navigation -->
                    <div v-if="currentAccount >= 0" class="border-t border-gray-100 bg-gray-50 px-4">
                        <!-- Transfer Type Picker -->
                        <div v-if="page > 0" class="flex rounded-lg bg-gray-100 p-1 relative my-3">
                            <div class="absolute top-1 bottom-1 w-1/2 rounded-md transition-all duration-300 ease-in-out"
                                :class="transferType ? 'left-1/2 bg-white shadow-sm' : 'left-1 bg-white shadow-sm'">
                            </div>

                            <button @click="changeTransferType(0)"
                                class="flex-1 py-2 text-sm font-medium rounded-md transition z-10"
                                :class="{ 'text-green-600': !transferType, 'text-gray-500': transferType }">
                                {{ t('dashboard.income-transfers-selector') }}
                            </button>

                            <button @click="changeTransferType(1)"
                                class="flex-1 py-2 text-sm font-medium rounded-md transition z-10"
                                :class="{ 'text-red-600': transferType, 'text-gray-500': !transferType }">
                                {{ t('dashboard.outcome-transfers-selector') }}
                            </button>
                        </div>

                        <!-- Category Picker -->
                        <div v-if="page === 2" class="mb-3 flex items-center">
                            <div class="relative w-full">
                                <select @change="handleCategoryChange" v-model="currentCategory"
                                    class="w-full pl-4 pr-10 py-3 rounded-xl border border-gray-200 bg-white focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition appearance-none">
                                    <option class="italic text-gray-500" value="-2">{{
                                        t('dashboard.default-category-option') }}</option>
                                    <option class="font-medium" v-for="(category, index) in categories"
                                        :value="category.id">
                                        {{ category.name }}
                                    </option>
                                    <option class="italic text-amber-600" value="-1">{{
                                        t('dashboard.new-category-option') }}</option>
                                </select>
                                <div class="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none">
                                    <svg class="h-5 w-5 text-gray-400" fill="none" viewBox="0 0 24 24"
                                        stroke="currentColor">
                                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                            d="M19 9l-7 7-7-7" />
                                    </svg>
                                </div>
                            </div>
                            <div v-if="currentCategory >= 0"
                                class="px-2 space-x-2 flex transition duration-100 hover:rotate-z-10"
                                @click="handleCategorySettings">
                                <svg width="30px" height="30px" viewBox="0 0 24 24" fill="none"
                                    xmlns="http://www.w3.org/2000/svg" class="cursor-pointer">
                                    <circle cx="12" cy="12" r="3" class="stroke-current" stroke-width="1.5" />
                                    <path
                                        d="M3.66122 10.6392C4.13377 10.9361 4.43782 11.4419 4.43782 11.9999C4.43781 12.558 4.13376 13.0638 3.66122 13.3607C3.33966 13.5627 3.13248 13.7242 2.98508 13.9163C2.66217 14.3372 2.51966 14.869 2.5889 15.3949C2.64082 15.7893 2.87379 16.1928 3.33973 16.9999C3.80568 17.8069 4.03865 18.2104 4.35426 18.4526C4.77508 18.7755 5.30694 18.918 5.83284 18.8488C6.07287 18.8172 6.31628 18.7185 6.65196 18.5411C7.14544 18.2803 7.73558 18.2699 8.21895 18.549C8.70227 18.8281 8.98827 19.3443 9.00912 19.902C9.02332 20.2815 9.05958 20.5417 9.15224 20.7654C9.35523 21.2554 9.74458 21.6448 10.2346 21.8478C10.6022 22 11.0681 22 12 22C12.9319 22 13.3978 22 13.7654 21.8478C14.2554 21.6448 14.6448 21.2554 14.8478 20.7654C14.9404 20.5417 14.9767 20.2815 14.9909 19.9021C15.0117 19.3443 15.2977 18.8281 15.7811 18.549C16.2644 18.27 16.8545 18.2804 17.3479 18.5412C17.6837 18.7186 17.9271 18.8173 18.1671 18.8489C18.693 18.9182 19.2249 18.7756 19.6457 18.4527C19.9613 18.2106 20.1943 17.807 20.6603 17C20.8677 16.6407 21.029 16.3614 21.1486 16.1272M20.3387 13.3608C19.8662 13.0639 19.5622 12.5581 19.5621 12.0001C19.5621 11.442 19.8662 10.9361 20.3387 10.6392C20.6603 10.4372 20.8674 10.2757 21.0148 10.0836C21.3377 9.66278 21.4802 9.13092 21.411 8.60502C21.3591 8.2106 21.1261 7.80708 20.6601 7.00005C20.1942 6.19301 19.9612 5.7895 19.6456 5.54732C19.2248 5.22441 18.6929 5.0819 18.167 5.15113C17.927 5.18274 17.6836 5.2814 17.3479 5.45883C16.8544 5.71964 16.2643 5.73004 15.781 5.45096C15.2977 5.1719 15.0117 4.6557 14.9909 4.09803C14.9767 3.71852 14.9404 3.45835 14.8478 3.23463C14.6448 2.74458 14.2554 2.35523 13.7654 2.15224C13.3978 2 12.9319 2 12 2C11.0681 2 10.6022 2 10.2346 2.15224C9.74458 2.35523 9.35523 2.74458 9.15224 3.23463C9.05958 3.45833 9.02332 3.71848 9.00912 4.09794C8.98826 4.65566 8.70225 5.17191 8.21891 5.45096C7.73557 5.73002 7.14548 5.71959 6.65205 5.4588C6.31633 5.28136 6.0729 5.18269 5.83285 5.15108C5.30695 5.08185 4.77509 5.22436 4.35427 5.54727C4.03866 5.78945 3.80569 6.19297 3.33974 7C3.13231 7.35929 2.97105 7.63859 2.85138 7.87273"
                                        class="stroke-current" stroke-width="1.5" stroke-linecap="round" />
                                </svg>
                            </div>
                        </div>

                        <!-- Period Picker -->
                        <div class="pb-3">
                            <div class="flex items-center md:space-x-2 flex-col md:flex-row">
                                <div class="relative flex-1 w-full">
                                    <select v-model="period" @change="handlePeriodSelection"
                                        class="w-full pl-4 pr-10 py-3 rounded-xl border border-gray-200 bg-white focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition appearance-none">
                                        <option value="all">{{ t('dashboard.period-all-option') }}</option>
                                        <option value="week">{{ t('dashboard.period-week-option') }}</option>
                                        <option value="month">{{ t('dashboard.period-month-option') }}</option>
                                        <option value="year">{{ t('dashboard.period-year-option') }}</option>
                                        <option value="custom">{{ t('dashboard.period-custom-option') }}</option>
                                    </select>
                                    <div class="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none">
                                        <svg class="h-5 w-5 text-gray-400" fill="none" viewBox="0 0 24 24"
                                            stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                d="M19 9l-7 7-7-7" />
                                        </svg>
                                    </div>
                                </div>

                                <div v-if="period !== 'all' && period !== 'custom'"
                                    class="flex items-center justify-between bg-white rounded-xl border border-gray-200 md:w-[300px] w-full">
                                    <button @click="changeMilestone(-1)"
                                        class="p-2 text-gray-500 hover:text-amber-500 transition">
                                        <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20"
                                            fill="currentColor">
                                            <path fill-rule="evenodd"
                                                d="M12.707 5.293a1 1 0 010 1.414L9.414 10l3.293 3.293a1 1 0 01-1.414 1.414l-4-4a1 1 0 010-1.414l4-4a1 1 0 011.414 0z"
                                                clip-rule="evenodd" />
                                        </svg>
                                    </button>
                                    <span class="px-2 text-sm font-medium whitespace-nowrap">
                                        {{ formatDateRange(currentPeriod.from, currentPeriod.to, locale) }}
                                    </span>
                                    <button @click="changeMilestone(1)"
                                        class="p-2 text-gray-500 hover:text-amber-500 transition">
                                        <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20"
                                            fill="currentColor">
                                            <path fill-rule="evenodd"
                                                d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z"
                                                clip-rule="evenodd" />
                                        </svg>
                                    </button>
                                </div>
                            </div>

                            <div v-if="period === 'custom'" class="mt-3 grid grid-cols-2 gap-3">
                                <div class="space-y-1">
                                    <label class="block text-sm font-medium text-gray-700">{{
                                        t('dashboard.date-picker-from') }}</label>
                                    <div class="relative">
                                        <input @change="handleDatePicker" type="date" id="date-picker-from"
                                            class="w-full pl-4 pr-10 py-3 rounded-xl border border-gray-200 bg-white focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition">
                                        <div
                                            class="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none">
                                            <svg class="h-5 w-5 text-gray-400" fill="none" viewBox="0 0 24 24"
                                                stroke="currentColor">
                                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                    d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                                            </svg>
                                        </div>
                                    </div>
                                </div>
                                <div class="space-y-1">
                                    <label class="block text-sm font-medium text-gray-700">{{
                                        t('dashboard.date-picker-to') }}</label>
                                    <div class="relative">
                                        <input @change="handleDatePicker" type="date" id="date-picker-to"
                                            class="w-full pl-4 pr-10 py-3 rounded-xl border border-gray-200 bg-white focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition">
                                        <div
                                            class="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none">
                                            <svg class="h-5 w-5 text-gray-400" fill="none" viewBox="0 0 24 24"
                                                stroke="currentColor">
                                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                    d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                                            </svg>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Loading State -->
                <div v-if="isLoading" class="flex-1 p-6 space-y-4">
                    <div class="animate-pulse space-y-6">
                        <div class="h-8 bg-gray-100 rounded-xl w-3/4"></div>
                        <div class="h-6 bg-gray-100 rounded-xl w-1/2"></div>
                        <div class="h-64 bg-gray-100 rounded-2xl"></div>
                        <div class="grid grid-cols-2 gap-4">
                            <div class="h-24 bg-gray-100 rounded-2xl"></div>
                            <div class="h-24 bg-gray-100 rounded-2xl"></div>
                        </div>
                    </div>
                </div>

                <!-- Content -->
                <div v-else class="flex-1 overflow-auto">
                    <!-- Dashboard Page -->
                    <div v-if="currentAccount >= 0 && page === 0" class="p-6 space-y-6">

                        <div class="bg-white rounded-2xl shadow-sm p-5 max-h-[600px]">
                            <canvas ref="chartCanvas" class=" w-full h-full"></canvas>
                        </div>

                        <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                            <div class="bg-white rounded-2xl shadow-sm p-5">
                                <div class="flex items-center space-x-3">
                                    <div class="p-2 rounded-lg bg-green-50 text-green-600">
                                        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none"
                                            viewBox="0 0 24 24" stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                                        </svg>
                                    </div>
                                    <div>
                                        <h3 class="text-sm font-medium text-gray-500">{{ t('dashboard.total-income') }}
                                        </h3>
                                        <p class="text-2xl font-bold text-green-600">
                                            {{ accounts[currentAccount].income.toFixed(2) }}
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="bg-white rounded-2xl shadow-sm p-5">
                                <div class="flex items-center space-x-3">
                                    <div class="p-2 rounded-lg bg-red-50 text-red-600">
                                        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none"
                                            viewBox="0 0 24 24" stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                                        </svg>
                                    </div>
                                    <div>
                                        <h3 class="text-sm font-medium text-gray-500">{{ t('dashboard.total-expenses')
                                        }}</h3>
                                        <p class="text-2xl font-bold text-red-600">
                                            {{ Math.abs(accounts[currentAccount].outcome).toFixed(2) }}
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Accounts Page -->
                    <div v-else-if="currentAccount >= 0 && page === 1" class="pb-20 px-4">
                        <div class="space-y-3">
                            <div v-for="(category, index) in accounts[currentAccount].categories"
                                v-show="!transferType ? category.income > 0 : category.outcome < 0"
                                @click="goToCategory(index)"
                                class="bg-white rounded-xl shadow-sm p-4 hover:shadow-md transition cursor-pointer">
                                <div class="flex items-center justify-between">
                                    <div class="flex-1 min-w-0 space-y-2">
                                        <p class="text-sm font-medium text-gray-900">
                                            {{ category.name }}
                                        </p>
                                        <div class="w-full bg-gray-100 rounded-full h-2">
                                            <div class="h-2 rounded-full"
                                                :class="!transferType ? 'bg-green-400' : 'bg-red-400'"
                                                :style="`width: ${!transferType ? category.percentage.income : category.percentage.outcome}%`">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ml-4 flex flex-col items-end">
                                        <span class="text-sm font-medium"
                                            :class="!transferType ? 'text-green-600' : 'text-red-600'">
                                            {{ !transferType ? category.income.toFixed(2) :
                                                Math.abs(category.outcome).toFixed(2) }}
                                        </span>
                                        <span class="text-xs text-gray-500">
                                            {{ !transferType ? category.percentage.income : category.percentage.outcome
                                            }}%
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="absolute bottom-2 right-2 flex space-x-2">
                            <button @click="modalView = 6"
                                class="w-14 h-14 bg-[#F14635] hover:bg-red-600 text-white rounded-full shadow-lg flex items-center justify-center transition hover:shadow-xl hover:scale-105">
                                <svg fill="white" width="33" height="33" viewBox="0 0 33 33"
                                    xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd" clip-rule="evenodd"
                                        d="M16.7475 17.6513C19.6113 18.0934 19.9984 19.7879 20.2299 21.365L20.2685 21.6339L20.2948 21.8169L20.4342 22.745C20.707 24.5171 21.2762 28.1955 21.2762 30.115C21.2762 30.5947 21.234 30.9629 21.1569 31.1652C21.0284 31.477 20.654 31.7762 20.137 32.0256C18.8861 32.3335 17.5786 32.5 16.2318 32.5C16.0892 32.5 15.9489 32.4931 15.8081 32.4889C15.2638 32.3343 14.8547 32.0762 14.6381 31.7235C13.9841 30.6589 13.9447 28.287 13.9316 25.5152L13.9301 25.1937L13.9248 24.3629C13.9035 21.4286 13.8852 18.8986 14.9513 17.9907C15.3642 17.6415 15.9509 17.5259 16.7475 17.6513ZM10.5037 24.0398C11.1243 24.0049 11.5234 27.0574 11.6036 29.3721C11.6598 30.9836 11.4906 31.6465 11.2204 31.8845C10.9145 31.7824 10.6139 31.6719 10.3177 31.5519C10.1201 31.1928 9.96907 30.628 9.87312 29.8882C9.58018 27.5633 9.83465 24.0808 10.5037 24.0398ZM26.0622 28.6014C26.0235 28.8134 25.9689 28.9772 25.9016 29.1154C25.371 29.5412 24.8133 29.9321 24.2297 30.2838C24.0438 30.3151 23.8787 30.2933 23.798 30.1569C23.0245 28.7978 22.7342 24.5342 23.4827 24.1631C24.4586 23.6917 26.2069 27.8643 26.0622 28.6014ZM16.0008 0.5C24.7145 0.5 31.8006 7.40062 31.996 15.9873L32 16.2841V16.4213C31.984 20.3 30.5566 23.8486 28.205 26.5952C28.1197 26.5524 27.9641 26.4123 27.6822 25.9906C27.3905 25.5667 24.8749 21.6953 24.8749 17.2581C24.8749 16.3873 26.1301 15.027 27.2408 13.833C28.0711 12.9345 28.8576 12.087 29.1472 11.4209C29.5161 10.5614 29.258 9.96125 28.8498 9.75782C28.4803 9.57703 27.9256 9.7089 27.5363 10.4003C26.8975 11.5193 26.6928 11.7284 25.761 12.4771C24.8441 13.2254 23.3864 13.9787 23.3864 13.0036C23.3864 12.4771 24.1983 11.2847 24.5994 10.4489C25.009 9.60377 24.5675 8.99196 23.7217 8.99196C22.0608 8.99196 20.9583 11.1077 20.9583 11.8407C20.9583 12.5731 21.3136 12.6774 21.3136 13.5303C21.3136 14.3896 19.4913 15.5042 17.7747 15.5042C16.122 15.5042 15.1636 15.1697 14.7665 14.2273L14.7158 14.095L14.601 13.7554C14.1938 12.5653 13.9017 11.7022 13.3932 10.8002C13.1232 10.3232 12.7066 9.99252 12.3463 9.70156C11.8754 9.33579 11.6311 8.99901 11.5801 8.74784C11.5331 8.49865 11.5078 8.02904 12.3128 6.9465C13.1161 5.87014 13.2287 5.057 12.8267 4.63743C12.681 4.48817 12.4326 4.39254 12.1136 4.39254C11.5494 4.39254 10.7654 4.69149 9.95745 5.51265C8.7015 6.7996 9.41451 8.02149 9.41451 8.55788C9.41451 9.09392 9.18442 9.39713 8.43071 10.1264C7.67148 10.8589 7.40945 11.4869 7.32262 14.0109C7.28935 15.3114 7.06031 16.06 6.85471 16.7238C6.67705 17.3056 6.51366 17.8526 6.50483 18.6429C6.49076 19.5187 6.63654 20.0831 6.80666 20.7343C6.97369 21.3362 7.15109 22.0259 7.26475 23.1796C7.44553 24.9655 7.38268 26.4741 7.054 28.016L6.96628 28.4023L6.94578 28.5133C6.87847 28.7843 6.79846 29.1134 6.69031 29.2367C2.64076 26.3604 0 21.663 0 16.3531C0 7.59843 7.1635 0.5 16.0008 0.5Z" />
                                </svg>
                            </button>

                            <button @click="modalView = 2"
                                class="w-14 h-14 bg-amber-500 hover:bg-amber-600 text-white rounded-full shadow-lg flex items-center justify-center transition hover:shadow-xl hover:scale-105">
                                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24"
                                    stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                        d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
                                </svg>
                            </button>
                        </div>

                    </div>

                    <!-- Categories Page -->
                    <div v-else-if="currentCategory >= 0 && currentAccount >= 0 && page === 2" class="py-5 px-4">
                        <div v-if="getCategoryId(currentCategory) >= 0" class="space-y-3">
                            <div v-for="(transfer, index) in (!transferType ?
                                accounts[currentAccount].categories[getCategoryId(currentCategory)].transfers.income :
                                accounts[currentAccount].categories[getCategoryId(currentCategory)].transfers.outcome)"
                                class="bg-white rounded-xl shadow-sm overflow-hidden">
                                <div @click="toggleTransferDetails(index)"
                                    class="p-4 hover:bg-gray-50 transition cursor-pointer flex items-center justify-between">
                                    <div class="flex items-center space-x-3">
                                        <div :class="!transferType ? 'bg-green-100 text-green-600' : 'bg-red-100 text-red-600'"
                                            class="p-2 rounded-lg">
                                            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none"
                                                viewBox="0 0 24 24" stroke="currentColor">
                                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                    d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                                            </svg>
                                        </div>
                                        <div>
                                            <p class="text-sm font-medium">
                                                {{ dateToString(transfer.moment, locale) }}
                                            </p>
                                            <p class="text-xs text-gray-500">
                                                {{ transfer.description || t('dashboard.no-description') }}
                                            </p>
                                        </div>
                                    </div>
                                    <span class="text-sm font-medium"
                                        :class="!transferType ? 'text-green-600' : 'text-red-600'">
                                        {{ transfer.bill >= 0 ? `+${transfer.bill.toFixed(2)}` :
                                            transfer.bill.toFixed(2) }}
                                    </span>
                                </div>

                                <div v-if="expandedTransfers.includes(index)"
                                    class="px-4 py-3 bg-gray-50 border-t border-gray-100">
                                    <div class="flex justify-between text-xs text-gray-500">
                                        <span>{{ t('dashboard.transfer-id') }}: {{ transfer.id }}</span>
                                        <div class="flex space-x-3">
                                            <button @click="handleTransferSettings(transfer)"
                                                class="text-amber-600 hover:text-amber-700 flex items-center">
                                                <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 mr-1" fill="none"
                                                    viewBox="0 0 24 24" stroke="currentColor">
                                                    <path stroke-linecap="round" stroke-linejoin="round"
                                                        stroke-width="2"
                                                        d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z" />
                                                </svg>
                                                {{ t('dashboard.edit') }}
                                            </button>
                                            <button @click="deleteTransfer(transfer.id); toggleTransferDetails(index)"
                                                class="text-red-600 hover:text-red-700 flex items-center">
                                                <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 mr-1" fill="none"
                                                    viewBox="0 0 24 24" stroke="currentColor">
                                                    <path stroke-linecap="round" stroke-linejoin="round"
                                                        stroke-width="2"
                                                        d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                                                </svg>
                                                {{ t('dashboard.delete') }}
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Empty States -->
                    <div v-else class="h-full flex items-center justify-center p-6">
                        <div class="text-center max-w-md">
                            <div class="mx-auto h-24 w-24 text-gray-300">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                    stroke="currentColor">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1"
                                        d="M9 17v-2m3 2v-4m3 4v-6m2 10H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                                </svg>
                            </div>
                            <h3 class="mt-4 text-lg font-medium text-gray-900">
                                {{ currentAccount < 0 ? t('dashboard.select-account-warning') :
                                    t('dashboard.select-category-warning') }} </h3>
                                    <p class="mt-2 text-sm text-gray-500">
                                        {{ t('dashboard.select-to-continue') }}
                                    </p>
                                    <div v-if="currentAccount < 0" class="mt-6">
                                        <button @click="modalView = 0"
                                            class="px-4 py-2 bg-amber-500 text-white rounded-lg hover:bg-amber-600 transition">
                                            {{ t('dashboard.create-account-button') }}
                                        </button>
                                    </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modal Content -->
            <div v-else class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-30">
                <div class="w-full max-w-md bg-white rounded-2xl shadow-xl overflow-hidden transform transition-all">
                    <div class="bg-gradient-to-r from-amber-400 to-amber-500 p-6 text-white">
                        <h2 class="text-xl font-bold">
                            {{
                                modalView === 0 ? t('dashboard.modal.create-account-title') :
                                    modalView === 1 ? t('dashboard.modal.create-category-title') :
                                        modalView === 2 ? t('dashboard.modal.create-transfer-title') :
                                            modalView === 3 ? t('dashboard.modal.update-transfer-title') :
                                                modalView === 4 ? t('dashboard.modal.update-account-title') :
                                                    modalview === 5 ? t('dashboard.modal.update-category-title') :
                                                        t('dashboard.modal.upload-kaspi-title')
                            }}
                        </h2>
                        <p v-if="validationErrors.length === 0" class="mt-1 text-amber-100">
                            {{ t('dashboard.modal.form-instructions') }}
                        </p>
                        <p v-else class="mt-1 text-red-100">
                            {{ validationErrors[0] }}
                        </p>
                    </div>

                    <form @submit.prevent="submitForm" class="p-6 space-y-4">
                        <!-- Account Name -->
                        <div v-if="modalView === 0 || modalView === 4" class="space-y-2">
                            <label class="block text-sm font-medium text-gray-700">
                                {{ t('dashboard.modal.account-name-placeholder') }}
                            </label>
                            <input v-model="form.accountName" type="text"
                                class="w-full px-4 py-3 rounded-xl border border-gray-200 focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition"
                                :placeholder="t('dashboard.modal.account-name-placeholder')">
                        </div>

                        <!-- Category Name -->
                        <div v-else-if="modalView === 1 || modalView === 5" class="space-y-2">
                            <label class="block text-sm font-medium text-gray-700">
                                {{ t('dashboard.modal.category-name-placeholder') }}
                            </label>
                            <input v-model="form.categoryName" type="text"
                                class="w-full px-4 py-3 rounded-xl border border-gray-200 focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition"
                                :placeholder="t('dashboard.modal.category-name-placeholder')">
                        </div>

                        <!-- Transfer Form -->
                        <div v-else-if="modalView === 2 || modalView === 3" class="space-y-4">
                            <div class="space-y-2">
                                <label class="block text-sm font-medium text-gray-700">
                                    {{ t('dashboard.modal.transfer-amount-label') }}
                                </label>
                                <div class="relative">
                                    <span class="absolute left-3 top-3 text-gray-500">$</span>
                                    <input v-model="form.amount" type="number" step="0.01"
                                        class="w-full pl-8 pr-4 py-3 rounded-xl border border-gray-200 focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition"
                                        :placeholder="t('dashboard.modal.transfer-amount-placeholder')">
                                </div>
                            </div>

                            <div class="space-y-2">
                                <label class="block text-sm font-medium text-gray-700">
                                    {{ t('dashboard.modal.category-picker-label') }}
                                </label>
                                <div class="relative">
                                    <select v-model="form.categoryId"
                                        class="w-full pl-4 pr-10 py-3 rounded-xl border border-gray-200 focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition appearance-none">
                                        <option v-for="category in categories" :value="category.id">
                                            {{ category.name }}
                                        </option>
                                    </select>
                                    <div class="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none">
                                        <svg class="h-5 w-5 text-gray-400" fill="none" viewBox="0 0 24 24"
                                            stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                d="M19 9l-7 7-7-7" />
                                        </svg>
                                    </div>
                                </div>
                            </div>

                            <div class="space-y-2">
                                <label class="block text-sm font-medium text-gray-700">
                                    {{ t('dashboard.modal.transfer-description-label') }} ({{ t('dashboard.optional')
                                    }})
                                </label>
                                <textarea v-model="form.description" rows="3"
                                    class="w-full px-4 py-3 rounded-xl border border-gray-200 focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition"
                                    :placeholder="t('dashboard.modal.transfer-description-placeholder')"></textarea>
                            </div>

                            <div class="space-y-2">
                                <label class="block text-sm font-medium text-gray-700">
                                    {{ t('dashboard.modal.date') }}
                                </label>
                                <div class="relative">
                                    <input v-model="form.date" type="date"
                                        class="w-full pl-4 pr-10 py-3 rounded-xl border border-gray-200 focus:ring-2 focus:ring-amber-400 focus:border-amber-400 transition">
                                    <div class="absolute inset-y-0 right-0 flex items-center pr-3 pointer-events-none">
                                        <svg class="h-5 w-5 text-gray-400" fill="none" viewBox="0 0 24 24"
                                            stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                                d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                                        </svg>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div v-else-if="modalView === 6">
                            <label class="flex flex-col items-center justify-center w-full h-32 px-4 py-6 
                                            border-2 border-dashed border-gray-300 rounded-lg 
                                            cursor-pointer bg-gray-50 hover:bg-gray-100
                                            transition-colors duration-200">
                                <div class="flex flex-col items-center justify-center">
                                    <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8 text-gray-500" fill="none"
                                        viewBox="0 0 24 24" stroke="currentColor">
                                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                            d="M7 16a4 4 0 01-.88-7.903A5 5 0 1115.9 6L16 6a5 5 0 011 9.9M15 13l-3-3m0 0l-3 3m3-3v12" />
                                    </svg>
                                    <p class="mt-2 text-sm text-gray-600">
                                        {{ t("dashboard.modal.upload-pdf-instruction-1") }}
                                    </p>
                                    <p class="text-xs text-gray-500">{{ t("dashboard.modal.upload-pdf-instruction-2") }}
                                    </p>
                                </div>
                                <input type="file" @change="(e) => form.files = e.target.files" class="hidden"
                                    accept=".pdf" />
                            </label>
                        </div>

                        <div class="m-auto w-full">
                            <button @disabled="isSubmitting" v-if="modalView == 4" @click="deleteAccount" type="button"
                                class="disabled:bg-gray-100 w-full flex-1 py-3 px-4 border border-gray-200 rounded-xl text-sm font-medium text-gray-700 bg-red-300 hover:bg-red-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-amber-400 transition">
                                {{ t('dashboard.delete') }}
                            </button>

                            <button @disabled="isSubmitting" v-if="modalView == 5" @click="deleteCategory" type="button"
                                class="disabled:bg-gray-100 w-full flex-1 py-3 px-4 border border-gray-200 rounded-xl text-sm font-medium text-gray-700 bg-red-300 hover:bg-red-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-amber-400 transition">
                                {{ t('dashboard.delete') }}
                            </button>
                        </div>

                        <div class="pt-4 flex space-x-3">
                            <button type="button" @click="modalView = -1; resetForm()"
                                class="flex-1 py-3 px-4 border border-gray-200 rounded-xl text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-amber-400 transition">
                                {{ t('dashboard.modal.cancel-button') }}
                            </button>
                            <button type="submit" :disabled="isSubmitting"
                                class="flex-1 py-3 px-4 border border-transparent rounded-xl shadow-sm text-sm font-medium text-white bg-amber-500 hover:bg-amber-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-amber-400 transition disabled:opacity-50">
                                <span v-if="!isSubmitting">
                                    {{
                                        modalView === 0 ? t('dashboard.modal.create-account-button') :
                                            modalView === 1 ? t('dashboard.modal.create-category-button') :
                                                modalView === 2 ? t('dashboard.modal.create-transfer-button') :
                                                    modalView === 3 ? t('dashboard.modal.update-transfer-button') :
                                                        modalView === 4 ? t('dashboard.modal.update-account-button') :
                                                            modalview === 5 ? t('dashboard.modal.update-category-button') :
                                                                t('dashboard.modal.upload-kaspi-button')
                                    }}
                                </span>
                                <span v-else class="flex items-center justify-center">
                                    <svg class="animate-spin -ml-1 mr-2 h-4 w-4 text-white"
                                        xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                                        <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor"
                                            stroke-width="4"></circle>
                                        <path class="opacity-75" fill="currentColor"
                                            d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z">
                                        </path>
                                    </svg>
                                    {{ t('dashboard.processing') }}
                                </span>
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </container-component>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import api from '../api/api'
import { formatDateRange, getPeriodDates, dateToString } from '../utils/datetime'
import { useModalFormValidator } from '../utils/validators'
import ContainerComponent from '../components/ContainerComponent.vue'
import ChartsIcon from '../components/ChartsIcon.vue'
import AccountsIcon from '../components/AccountsIcon.vue'
import CategoriesIcon from '../components/CategoriesIcon.vue'
import { Chart } from 'chart.js/auto'

// Icons for tabs

const { t, locale } = useI18n()
const { ValidateAccountName, ValidateCategoryName, ValidateTransferAmount, ValidateDate, ValidateCategory } = useModalFormValidator()

// Tabs configuration
const tabs = [
    { name: 'dashboard', icon: ChartsIcon },
    { name: 'accounts', icon: AccountsIcon },
    { name: 'categories', icon: CategoriesIcon }
]

// State
const page = ref(0)
const transferType = ref(0)
const period = ref('week')
const currentPeriod = ref(getPeriodDates('week', new Date()))
const currentAccount = ref(-2)
const currentCategory = ref(-2)
const currentTransfer = ref(-1)
const accounts = ref([])
const categories = ref([])
const isLoading = ref(true)
const modalView = ref(-1)
const expandedTransfers = ref([])
const user = ref(null)
const categoriesMap = ref({})

// Form state
const isSubmitting = ref(false)
const validationErrors = ref([])
const form = ref({
    accountName: '',
    categoryName: '',
    categoryId: -1,
    amount: '',
    description: '',
    date: new Date().toISOString().split('T')[0],
    files: []
})

// Chart ref
const chartCanvas = ref(null)
const chart = ref(null)

const handleAccountChange = () => {
    if (currentAccount.value == -1) {
        currentAccount.value = -2
        modalView.value = 0
    } else {
        prepareData()
    }
}

const handleCategoryChange = () => {
    if (currentCategory.value == -1) {
        currentCategory.value = -2
        modalView.value = 1
    }
}

const changePage = (p) => {
    page.value = p
    if (p == 0) setTimeout(updateCharts, 100)
}

const changeTransferType = (p) => {
    transferType.value = p
    sortData()
}

const handleDatePicker = (e) => {
    if (e.target.id === "date-picker-from") {
        currentPeriod.value.from = new Date(e.target.value)
    } else {
        currentPeriod.value.to = new Date(e.target.value)
    }
    prepareData()
}

const handlePeriodSelection = () => {
    currentPeriod.value.milestone = new Date()

    if (period.value === 'custom') {
        const now = new Date()
        const from = new Date(now.setDate(now.getDate() - 1))
        const to = new Date(now.setDate(now.getDate() + 2))
        document.querySelector('#date-picker-from').value = from.toISOString().split('T')[0]
        document.querySelector('#date-picker-to').value = to.toISOString().split('T')[0]
        currentPeriod.value.from = from
        currentPeriod.value.to = to
    }

    updatePeriodPreview()
}

const changeMilestone = (direction) => {
    const temp = new Date(currentPeriod.value.milestone)

    switch (period.value) {
        case 'week':
            temp.setDate(temp.getDate() + 7 * direction)
            break
        case 'month':
            temp.setMonth(temp.getMonth() + 1 * direction)
            break
        case 'year':
            temp.setFullYear(temp.getFullYear() + 1 * direction)
            break
        default:
            break
    }

    currentPeriod.value.milestone = temp
    updatePeriodPreview()
}

const updatePeriodPreview = () => {
    if (period.value !== 'custom') {
        currentPeriod.value = getPeriodDates(period.value, currentPeriod.value.milestone)
    }
    prepareData()
}

const toggleTransferDetails = (index) => {
    const idx = expandedTransfers.value.indexOf(index)
    if (idx >= 0) {
        expandedTransfers.value.splice(idx, 1)
    } else {
        expandedTransfers.value.push(index)
    }
}

const deleteTransfer = async (index) => {
    try {
        api.delete(`/transfers/${index}`)
        setTimeout(async () => await fetchUserData(), 200)
    } catch (ex) {
        console.log(`could not delete the transfer with id "${index}"`)
    }
}

const deleteAccount = async () => {
    if (isSubmitting.value) return
    try {
        isSubmitting.value = true
        if (confirm(t('dashboard.modal.are-you-sure')) === true) {
            api.delete(`/accounts/${accounts.value[currentAccount.value].id}`)
            setTimeout(async () => {
                await fetchUserData()
                modalView.value = -1
                currentAccount.value = -2
                currentCategory.value = -2
                resetForm()
            }, 200)
        }
    } catch (ex) {
        console.log(`could not delete the account with id "${accounts.value[currentAccount.value].id}"`)
    } finally {
        isSubmitting.value = false
    }
}


const deleteCategory = async () => {
    if (isSubmitting.value) return
    try {
        isSubmitting.value = true
        if (confirm(t('dashboard.modal.are-you-sure')) === true) {
            api.delete(`/categories/${currentCategory.value}`)
            setTimeout(async () => {
                await fetchUserData()
                modalView.value = -1
                currentCategory.value = -2
                resetForm()
            }, 200)
        }
    } catch (ex) {
        console.log(`could not delete the category with id "${currentCategory.value}"`)
    } finally {
        isSubmitting.value = false
    }
}


/*

const form = ref({
    accountName: '',
    categoryName: '',
    categoryId: -1,
    amount: '',
    description: '',
    date: new Date().toISOString().split('T')[0]
})

*/

const handleTransferSettings = (transfer) => {
    form.value.categoryId = transfer.category.id
    form.value.amount = transfer.bill
    form.value.description = transfer.description == null ? '' : transfer.description
    form.value.date = transfer.moment
    currentTransfer.value = transfer.id
    modalView.value = 3
}

const handleAccountSettings = () => {
    form.value.accountName = accounts.value[currentAccount.value].name
    modalView.value = 4
}

const handleCategorySettings = () => {
    form.value.categoryName = categories.value.find(c => c.id == currentCategory.value).name
    modalView.value = 5
}

const getCategoryId = (id) => {
    if (id in categoriesMap.value) return categoriesMap.value[id]
    return -1
}

const goToCategory = (id) => {
    let categoryIndex = -1

    for (let key in categoriesMap.value) {
        if (categoriesMap.value[key] == id) {
            categoryIndex = key
            break
        }
    }

    if (categoryIndex >= 0) {
        currentCategory.value = categoryIndex
        changePage(2)
    }
}

const validateForm = () => {
    validationErrors.value = []
    let error

    if (modalView.value === 0 || modalView.value == 4) {
        error = ValidateAccountName(form.value.accountName)
        if (error) validationErrors.value.push(error)
    } else if (modalView.value === 1 || modalView.value == 5) {
        error = ValidateCategoryName(form.value.categoryName)
        if (error) validationErrors.value.push(error)
    } else if (modalView.value == 2 || modalView.value == 3) {
        error = ValidateTransferAmount(form.value.amount)
        if (error) validationErrors.value.push(error)

        error = ValidateCategory(form.value.categoryId)
        if (error) validationErrors.value.push(error)

        error = ValidateDate(form.value.date)
        if (error) validationErrors.value.push(error)
    } else {
        error = form.value.files.length == 0 ? t("validation.file-error") : false
        if (error) validationErrors.value.push(error)
    }

    return validationErrors.value.length == 0
}

const submitForm = async () => {
    if (isSubmitting.value) return
    if (!validateForm()) return

    isSubmitting.value = true

    try {
        if (modalView.value === 0) {
            await api.post('/accounts', { name: form.value.accountName })
        } else if (modalView.value === 1) {
            await api.post('/categories', { name: form.value.categoryName })
        } else if (modalView.value === 2) {
            await api.post('/transfers', {
                accountId: accounts.value[currentAccount.value].id,
                categoryId: form.value.categoryId,
                bill: parseInt(form.value.amount),
                description: form.value.description,
                moment: new Date(form.value.date)
            })
        } else if (modalView.value === 3) {
            await api.put(`/transfers/${currentTransfer.value}`, {
                accountId: accounts.value[currentAccount.value].id,
                categoryId: form.value.categoryId,
                bill: parseInt(form.value.amount),
                description: form.value.description,
                moment: new Date(form.value.date)
            })
        } else if (modalView.value === 4) {
            await api.put(`/accounts/${accounts.value[currentAccount.value].id}`, { name: form.value.accountName })
        } else if (modalView.value === 5) {
            await api.put(`/categories/${currentCategory.value}`, { name: form.value.categoryName })
        } else {
            const formData = new FormData();
            formData.append('pdfFile', form.value.files[0]);
            await api.post(`/transfers/pdf/${accounts.value[currentAccount.value].id}`, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
        }

        setTimeout(async () => {
            await fetchUserData()
            modalView.value = -1
            resetForm()
        }, 200)

    } catch (ex) {
        if (ex.status === 409) {
            validationErrors.value.push(t(modalView.value === 0 ?
                'dashboard.modal.account-conflict-error' :
                'dashboard.modal.category-conflict-error'))
        } else {
            validationErrors.value.push(t('dashboard.modal.submit-fail'))
        }
    } finally {
        isSubmitting.value = false
    }
}

const resetForm = () => {
    form.value = {
        accountName: '',
        categoryName: '',
        categoryId: -1,
        amount: '',
        description: '',
        date: new Date().toISOString().split('T')[0],
        files: []
    }
    validationErrors.value = []
}

const prepareData = () => {
    if (!user.value) return

    accounts.value = []
    categories.value = user.value.categories

    user.value.accounts.forEach((account, accountIndex) => {
        categoriesMap.value = {}
        accounts.value.push({
            id: account.id,
            name: account.name,
            income: 0,
            outcome: 0,
            categories: [],
            transfers: {
                income: [],
                outcome: []
            }
        })

        account.transfers.forEach(transfer => {
            const moment = new Date(transfer.moment)
            if (moment >= currentPeriod.value.from && moment <= currentPeriod.value.to) {
                if (!(transfer.category.id in categoriesMap.value)) {
                    accounts.value[accountIndex].categories.push({
                        id: transfer.category.id,
                        name: transfer.category.name,
                        income: 0,
                        outcome: 0,
                        percentage: { income: 0, outcome: 0 },
                        transfers: { income: [], outcome: [] }
                    })
                    categoriesMap.value[transfer.category.id] = accounts.value[accountIndex].categories.length - 1
                }

                const category = accounts.value[accountIndex].categories[categoriesMap.value[transfer.category.id]]
                const accountData = accounts.value[accountIndex]

                if (transfer.bill >= 0) {
                    accountData.income += transfer.bill
                    accountData.transfers.income.push(transfer)
                    category.income += transfer.bill
                    category.transfers.income.push(transfer)
                } else {
                    accountData.outcome += transfer.bill
                    accountData.transfers.outcome.push(transfer)
                    category.outcome += transfer.bill
                    category.transfers.outcome.push(transfer)
                }
            }
        })
    })

    accounts.value.forEach(account => {
        account.categories.forEach(category => {
            category.percentage = {
                income: (category.income / (account.income + 0.0001) * 100).toFixed(1),
                outcome: (Math.abs(category.outcome) / (Math.abs(account.outcome) + 0.0001) * 100).toFixed(1)
            }
        })
    })

    sortData()

    setTimeout(updateCharts, 100);
}

const sortData = () => {
    accounts.value.forEach(account => {
        account.categories.sort((a, b) =>
            transferType.value ?
                Math.abs(b.outcome) - Math.abs(a.outcome) :
                b.income - a.income
        )
    })
}

const fetchUserData = async () => {
    isLoading.value = true
    try {
        const response = await api.get('/report/user')
        user.value = response.data
        prepareData()

        if (categories.value.length > 0) {
            form.value.categoryId = categories.value[0].id
        }
    } catch (error) {
        console.error('Failed to fetch user data:', error)
    } finally {
        isLoading.value = false
    }
}

const generateSteps = (transfers) => {
    transfers = transfers.sort((a, b) => new Date(a.moment) - new Date(b.moment))
    let step = 'day'

    if (period.value == 'year') {
        step = 'month'
    } else if (period.value == 'month' || period.value == 'week') {
        step = 'day'
    } else {

        if (period.value == 'all') {
            currentPeriod.value = {
                from: new Date(transfers[0].moment),
                to: new Date(transfers[transfers.length - 1].moment),
                milestone: new Date((new Date(transfers[0].moment) + new Date(transfers[transfers.length - 1].moment)) / 2)
            }
            currentPeriod.value.from = currentPeriod.value.from.setDate(currentPeriod.value.from.getDate() - 1)
        }

        const diff = Math.floor((currentPeriod.value.to - currentPeriod.value.from) / (1000 * 60 * 60 * 24))

        if (diff >= 365) step = 'month'
        else if (diff >= 31) step = 'month'
        else step = 'day'
    }

    let steps = []

    let left = new Date(currentPeriod.value.from)
    let right = new Date(currentPeriod.value.to)

    left.setHours(0, 0, 0, 0)
    right.setHours(23, 59, 59, 999)

    const loc = `${locale.value}-${locale.value.toUpperCase()}`
    let i = 0

    while (left < right) {
        let border = new Date(left)
        let label = ''

        const options = {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            weekday: 'long',
        };

        if (step == 'year') {
            label = left.getFullYear().toString()
            border.setFullYear(border.getFullYear() + 1)
        } else if (step == 'month') {
            label = left.toLocaleDateString(loc, {
                year: 'numeric',
                month: 'long',
            })
            border.setMonth(border.getMonth() + 1)
        } else {
            label = left.toLocaleDateString(loc, {
                year: 'numeric',
                month: 'long',
                weekday: 'long',
                day: 'numeric',
            })
            border.setDate(border.getDate() + 1)
        }

        steps.push({
            label: label,
            from: new Date(left),
            to: new Date(border)
        })

        left = new Date(border)
    }

    return steps
}

const paginate = (transfers) => {
    const steps = generateSteps([...transfers.income, ...transfers.outcome])

    transfers.income = transfers.income.sort((a, b) => new Date(a.moment) - new Date(b.moment))
    transfers.outcome = transfers.outcome.sort((a, b) => new Date(a.moment) - new Date(b.moment))

    const incomeDataset = {}
    const outcomeDataset = {}
    const balanceDataset = {}

    let i = 0
    let o = 0
    let balance = 0

    steps.forEach(step => {
        incomeDataset[step.label] = 0
        outcomeDataset[step.label] = 0
        balanceDataset[step.label] = 0

        for (i; i < transfers.income.length; i++) {
            const moment = new Date(transfers.income[i].moment)
            if (moment < step.from) continue
            if (moment > step.to) break
            balance += transfers.income[i].bill
            incomeDataset[step.label] += transfers.income[i].bill
        }

        for (o; o < transfers.outcome.length; o++) {
            const moment = new Date(transfers.outcome[o].moment)
            if (moment < step.from) continue
            if (moment > step.to) break
            balance += transfers.outcome[o].bill
            outcomeDataset[step.label] -= transfers.outcome[o].bill
        }

        balanceDataset[step.label] = balance
    })

    return [incomeDataset, outcomeDataset, balanceDataset]
}

const destroyCharts = () => {
    if (chart.value != null) {
        chart.value.destroy()
        chart.value = null
    }
}

const createMainChart = () => {
    if (currentAccount.value < 0 || chartCanvas.value == null) return
    const datasets = paginate({ ...accounts.value[currentAccount.value].transfers })


    chart.value = new Chart(chartCanvas.value, {
        data: {
            datasets: [{
                type: 'line',
                label: t('dashboard.income'),
                data: datasets[0],
                borderColor: '#10B981', 
                backgroundColor: 'rgba(16, 185, 129, 0.1)',
                borderWidth: 1,
                pointBackgroundColor: '#10B981',
                pointBorderColor: '#fff',
                pointRadius: 5,
                pointHoverRadius: 7,
                pointHitRadius: 5,
                tension: 0.5, 
                fill: true 
            }, {
                type: 'line',
                label: t('dashboard.expenses'),
                data: datasets[1],
                borderColor: '#EF4444', 
                backgroundColor: 'rgba(239, 68, 68, 0.1)',
                borderWidth: 1,
                pointBackgroundColor: '#EF4444',
                pointBorderColor: '#fff',
                pointRadius: 5,
                pointHoverRadius: 7,
                pointHitRadius: 5,
                tension: 0.5,
                fill: true
            }, {
                type: 'line',
                label: t('dashboard.balance'),
                data: datasets[2],
                borderColor: '#3260a8',
                backgroundColor: 'rgba(50, 96, 168, 0.1)',
                borderWidth: 1,
                pointBackgroundColor: '#3260a8',
                pointBorderColor: '#fff',
                pointRadius: 5,
                pointHoverRadius: 7,
                pointHitRadius: 5,
                tension: 0,
                fill: true
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                    labels: {
                        color: '#6B7280',
                        font: {
                            size: 14,
                            family: 'Inter, sans-serif'
                        },
                        padding: 20,
                        usePointStyle: true,
                        pointStyle: 'circle'
                    }
                },
                tooltip: {
                    backgroundColor: '#1F2937',
                    titleColor: '#F9FAFB',
                    bodyColor: '#F9FAFB',
                    borderColor: '#374151',
                    borderWidth: 1,
                    padding: 12,
                    usePointStyle: true,
                    callbacks: {
                        label: function (context) {
                            return `${context.dataset.label}: ${context.parsed.y.toFixed(2)}`;
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: {
                        display: false,
                        drawBorder: false
                    },
                    ticks: {
                        color: '#9CA3AF',
                        font: {
                            size: 8
                        }
                    }
                },
                y: {
                    grid: {
                        color: 'rgba(229, 231, 235, 0.5)',
                        drawBorder: false
                    },
                    ticks: {
                        color: '#9CA3AF',
                        font: {
                            size: 8
                        },
                        callback: function (value) {
                            return value.toFixed(2);
                        }
                    },
                }
            },
            animation: {
                duration: 80,
                easing: 'easeOutQuart'
            },
            interaction: {
                intersect: false,
                mode: 'index'
            }
        }
    })
}

const updateCharts = () => {
    try {
        destroyCharts()
        createMainChart()
    } catch (ex) {
        return
    }
}
onMounted(async () => {
    resetForm()
    await fetchUserData()
})
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}

.slide-up-enter-active,
.slide-up-leave-active {
    transition: all 0.3s ease-out;
}

.slide-up-enter-from {
    opacity: 0;
    transform: translateY(20px);
}

.slide-up-leave-to {
    opacity: 0;
    transform: translateY(-20px);
}
</style>
