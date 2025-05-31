<template>
    <container-component Dashboard>
        <div v-if="modalView < 0" class="shadow-xl flex flex-col w-full flex-grow bg-amber-50">
            <!-- settings and navigation -->
            <div class="bg-neutral-100 sticky top-0 z-12">
                <!-- account picker -->
                <div>
                    <div class="flex flex-col items-center border-y-2">
                        <select @change="(() => {if (currentAccount == -1) {currentAccount = -2; modalView = 0}})()" v-model="currentAccount" name="account" class="w-[120px] p-2 focus:outline-none overflow-hidden">
                            <option class="bg-amber-100/20 italic" value="-2" selected>{{ t('dashboard.default-account-option') }}</option>
                            <option class="font-bold" v-for="(account, index) in accounts" :value="index">{{ account.name }}</option>
                            <option class="bg-amber-100/20 italic" value="-1">{{ t('dashboard.new-account-option') }}</option>
                        </select> 
                    </div>
                </div>

                <!-- menu -->
                <div id="sidebar" class="border-b-2 divide-x-3 divide-solid w-full h-[50px] flex justify-around relative">
                    <div id="sidebar-highlighter" class="absolute top-0 left-0 h-full border-none z-0 bg-amber-100 shadow-xs transition-translate duration-200 ease-in-out">
                        
                    </div>

                    <div @click="ChangePage(0)" class="z-1">
                        <svg width="40px" height="40px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M22 12.2039V13.725C22 17.6258 22 19.5763 20.8284 20.7881C19.6569 22 17.7712 22 14 22H10C6.22876 22 4.34315 22 3.17157 20.7881C2 19.5763 2 17.6258 2 13.725V12.2039C2 9.91549 2 8.77128 2.5192 7.82274C3.0384 6.87421 3.98695 6.28551 5.88403 5.10813L7.88403 3.86687C9.88939 2.62229 10.8921 2 12 2C13.1079 2 14.1106 2.62229 16.116 3.86687L18.116 5.10812C20.0131 6.28551 20.9616 6.87421 21.4808 7.82274"
                                class="stroke-current stroke-2" stroke-width="1.5" stroke-linecap="round" />
                            <path d="M15 18H9" class="stroke-current stroke-2" stroke-width="1.5" stroke-linecap="round" />
                        </svg>
                    </div>

                    <div @click="ChangePage(1)" class="z-1">
                        <svg width="40px" height="40px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M3 19H21M12 12V19M18 12V19M6 12V19M12.4472 3.22361L20.59 7.29502C21.4395 7.71974 21.1372 9 20.1875 9H3.81246C2.86276 9 2.56053 7.71974 3.40997 7.29502L11.5528 3.22361C11.8343 3.08284 12.1657 3.08284 12.4472 3.22361Z"
                                class="stroke-current stroke-2" stroke-linecap="round" stroke-linejoin="round" />
                        </svg>
                    </div>

                    <div @click="ChangePage(2)" class="z-1">
                        <svg width="40px" height="40px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path
                                d="M3 14.6C3 14.0399 3 13.7599 3.10899 13.546C3.20487 13.3578 3.35785 13.2049 3.54601 13.109C3.75992 13 4.03995 13 4.6 13H5.4C5.96005 13 6.24008 13 6.45399 13.109C6.64215 13.2049 6.79513 13.3578 6.89101 13.546C7 13.7599 7 14.0399 7 14.6V19.4C7 19.9601 7 20.2401 6.89101 20.454C6.79513 20.6422 6.64215 20.7951 6.45399 20.891C6.24008 21 5.96005 21 5.4 21H4.6C4.03995 21 3.75992 21 3.54601 20.891C3.35785 20.7951 3.20487 20.6422 3.10899 20.454C3 20.2401 3 19.9601 3 19.4V14.6Z"
                                class="stroke-current stroke-2" stroke-linecap="round" stroke-linejoin="round" />
                            <path
                                d="M10 4.6C10 4.03995 10 3.75992 10.109 3.54601C10.2049 3.35785 10.3578 3.20487 10.546 3.10899C10.7599 3 11.0399 3 11.6 3H12.4C12.9601 3 13.2401 3 13.454 3.10899C13.6422 3.20487 13.7951 3.35785 13.891 3.54601C14 3.75992 14 4.03995 14 4.6V19.4C14 19.9601 14 20.2401 13.891 20.454C13.7951 20.6422 13.6422 20.7951 13.454 20.891C13.2401 21 12.9601 21 12.4 21H11.6C11.0399 21 10.7599 21 10.546 20.891C10.3578 20.7951 10.2049 20.6422 10.109 20.454C10 20.2401 10 19.9601 10 19.4V4.6Z"
                                class="stroke-current stroke-2" stroke-linecap="round" stroke-linejoin="round" />
                            <path
                                d="M17 10.6C17 10.0399 17 9.75992 17.109 9.54601C17.2049 9.35785 17.3578 9.20487 17.546 9.10899C17.7599 9 18.0399 9 18.6 9H19.4C19.9601 9 20.2401 9 20.454 9.10899C20.6422 9.20487 20.7951 9.35785 20.891 9.54601C21 9.75992 21 10.0399 21 10.6V19.4C21 19.9601 21 20.2401 20.891 20.454C20.7951 20.6422 20.6422 20.7951 20.454 20.891C20.2401 21 19.9601 21 19.4 21H18.6C18.0399 21 17.7599 21 17.546 20.891C17.3578 20.7951 17.2049 20.6422 17.109 20.454C17 20.2401 17 19.9601 17 19.4V10.6Z"
                                class="stroke-current stroke-2" stroke-linecap="round" stroke-linejoin="round" />
                        </svg>
                    </div>
                </div>

                <!-- transfer type picker -->
                <div v-show="page > 0 && currentAccount >= 0" id="transfer-type-picker" class="border-b-2 divide-x-2 divide-solid w-full h-[30px] flex justify-around relative">
                    <div id="transfer-type-highlighter" :class="`absolute top-0 left-0 h-full border-none z-0 shadow-xs transition-translate duration-200 ease-in-out ${transferType ? 'bg-red-200' : 'bg-green-200'}`">
                        
                    </div>

                    <div @click="ChangeTransferType(0)" class="z-1">
                        {{ t('dashboard.income-transfers-selector') }}
                    </div>


                    <div @click="ChangeTransferType(1)" class="z-1">
                        {{ t('dashboard.outcome-transfers-selector') }}
                    </div>
                </div>

                <!-- category picker -->
                <div v-show="page==2">
                    <div class="flex flex-col items-center border-b-2">
                        <select @change="(() => {if (currentCategory == -1) {currentCategory = -2; modalView = 1}})()" v-model="currentCategory" name="account" class="w-[150px] p-2 focus:outline-none overflow-hidden">
                            <option class="bg-amber-100/20 italic" value="-2">{{ t('dashboard.default-category-option') }}</option>
                            <option class="font-bold" v-for="(category, index) in categories" :value="index">{{ category.name }}</option>
                            <option class="bg-amber-100/20 italic" value="-1">{{ t('dashboard.new-category-option') }}</option>
                        </select> 
                    </div>
                </div>

                <!-- period picker -->
                <div v-show="currentAccount >= 0" class="border-b-2 py-2 shadow-lg">
                    <div class="flex justify-center items-center"> 
                        <select v-model="period" @change="HandlePeriodSelection" name="period" class="w-[80px] focus:outline-none overflow-hidden">
                            <option value="all">{{ t('dashboard.period-all-option') }}</option>
                            <option value="week">{{ t('dashboard.period-week-option') }}</option>
                            <option value="month">{{ t('dashboard.period-month-option') }}</option>
                            <option value="year">{{ t('dashboard.period-year-option') }}</option>
                            <option value="custom">{{ t('dashboard.period-custom-option') }}</option>
                        </select> 
                    </div>

                    <div v-if="period == 'custom'" class="flex space-x-5 justify-center">
                        <div class="flex space-x-2 justify-center">
                            <p>{{ t('dashboard.date-picker-from') }}</p>
                            <input @change="HandleDatePicker" type="date" id="date-picker-from">
                        </div>

                        <hr class="h-5 border-1">
                        
                        <div class="flex space-x-2 justify-center">
                            <p>{{ t('dashboard.date-picker-to') }}</p>
                            <input @change="HandleDatePicker" type="date" id="date-picker-to">
                        </div>
                        
                    </div>
                    
                    <div v-else-if="period != 'all'" class="flex justify-around relative">
                        <svg @click="ChangeMilestone(-1)" class="hover:scale-150 transition stroke-current stroke-2 rotate-180 absolute bottom-2 left-1/7" width="20px" height="20px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M4 12H20M20 12L16 8M20 12L16 16" stroke-linecap="round" stroke-linejoin="round"/></svg>
                        <p id="period-preview">{{ formatDateRange(currentPeriod.from, currentPeriod.to, locale) }}</p>
                        <svg @click="ChangeMilestone(1)" class="hover:scale-150 transition stroke-current stroke-2 absolute bottom-2 right-1/7" width="20px" height="20px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M4 12H20M20 12L16 8M20 12L16 16" stroke-linecap="round" stroke-linejoin="round"/></svg>
                    </div>
                </div>
            </div>
        
            <!-- show while data is loading  -->
            <div v-if="isLoading" class="flex flex-col m-auto items-center space-y-3 w-full">
                <div class="rounded-full w-full h-[20px] bg-gray-300 animate-pulse"></div>
                <div class="w-full">
                    <div class="rounded-full w-2/10 h-[20px] bg-gray-300 animate-pulse"></div>
                </div>
                <div class="rounded-lg w-full h-[200px] bg-gray-300 animate-pulse"></div>
            </div>

            <!-- show when data is loaded -->
            <div v-else class="flex flex-grow">
                <!-- pages, which depend on account selection -->
                <div v-if="page < 2" class="flex flex-grow">
                    <!-- dashboard page -->
                    <div v-if="currentAccount >= 0 && page == 0">
                        <canvas ref="chartCanvas"></canvas>
                    </div>

                    <!-- accounts page -->
                    <div id="account-page" v-else-if="currentAccount >= 0 && page == 1" class="flex flex-col justify-between items-center text-lg font-bold w-full">
                        <div class="w-full p-5 space-y-5">
                            <div v-for="(category, index) in accounts[currentAccount].categories" class="w-full" @click="GoToCategory(index)">
                                <div v-show="!transferType ? category.income > 0 : category.outcome < 0" class="w-full flex bg-teal px-5 py-4 bg-blue-400/50 hover:bg-blue-400/60 rounded-xl hover:scale-102  transition duration-100">
                                    <div class="w-3/5">
                                        <p>{{ category.name }}</p>
                                    </div>

                                    <div class="w-1/5 text-center">
                                        <p>{{ !transferType ? category.income : category.outcome }}</p>
                                    </div>

                                    <div class="w-1/5 text-center text-3">
                                        <p>{{ !transferType ? category.percentage.income : category.percentage.outcome }}%</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="w-full bg-amber-200 p-3 text-center sticky bottom-0 text-3xl hover:bg-amber-300/70 border-t-2 transition duration-100" style="box-shadow: 0px -6px 8px 0px rgba(0,0,0,0.1);">
                            +
                        </div>
                    </div>

                    <div v-else class="flex flex-grow items-center justify-center">
                        <p class="text-xl font-bold text-center">{{ t('dashboard.select-account-warning') }}</p>
                    </div>
                </div>
               

                <!-- categories page -->
                <div v-else-if="currentCategory >= 0 && currentAccount >= 0 && page == 2" class="w-full flex flex-grow">
                    <div class="flex w-full flex-col justify-between">
                        <div class="p-5 font-bold text-xl space-y-5" v-if="GetCategoryId(categories[currentCategory]) >= 0 && GetCategoryId(categories[currentCategory]) < accounts[currentAccount].categories.length">
                            <div @click="(e) => e.target.parentNode.parentNode.parentNode.querySelector(`#transfer-description-${index}`).classList.toggle('hidden')" class="hover:scale-102 hover:-translate-y-1 transition duration-100 bg-teal-100 border-1 rounded-xl overflow-hidden" v-for="(transfer, index) in (!transferType ? accounts[currentAccount].categories[GetCategoryId(categories[currentCategory])].transfers.income : accounts[currentAccount].categories[GetCategoryId(categories[currentCategory])].transfers.outcome)">
                                <div class="w-full select-none flex bg-teal px-5 py-4 bg-blue-400/50 hover:bg-blue-400/60  transition duration-100">
                                    <div class="w-2/5">
                                        <p>{{ dateToString(transfer.moment, locale) }}</p>
                                    </div>

                                    <div class="w-3/5 text-end text-3">
                                        <p>{{ transfer.bill }}</p>
                                    </div>
                                </div>
                                <div :id="`transfer-description-${index}`" class="select-none w-full p-5 flex justify-between items-begin hidden">
                                    <p class="w-4/5">{{ transfer.description }}</p>
                                    <svg class="hover:rotate-z-10 transiton duration-100" width="30px" height="30px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <circle class="stroke-current" cx="12" cy="12" r="3" stroke-width="2.5"/>
                                        <path class="stroke-current" d="M13.7654 2.15224C13.3978 2 12.9319 2 12 2C11.0681 2 10.6022 2 10.2346 2.15224C9.74457 2.35523 9.35522 2.74458 9.15223 3.23463C9.05957 3.45834 9.0233 3.7185 9.00911 4.09799C8.98826 4.65568 8.70226 5.17189 8.21894 5.45093C7.73564 5.72996 7.14559 5.71954 6.65219 5.45876C6.31645 5.2813 6.07301 5.18262 5.83294 5.15102C5.30704 5.08178 4.77518 5.22429 4.35436 5.5472C4.03874 5.78938 3.80577 6.1929 3.33983 6.99993C2.87389 7.80697 2.64092 8.21048 2.58899 8.60491C2.51976 9.1308 2.66227 9.66266 2.98518 10.0835C3.13256 10.2756 3.3397 10.437 3.66119 10.639C4.1338 10.936 4.43789 11.4419 4.43786 12C4.43783 12.5581 4.13375 13.0639 3.66118 13.3608C3.33965 13.5629 3.13248 13.7244 2.98508 13.9165C2.66217 14.3373 2.51966 14.8691 2.5889 15.395C2.64082 15.7894 2.87379 16.193 3.33973 17C3.80568 17.807 4.03865 18.2106 4.35426 18.4527C4.77508 18.7756 5.30694 18.9181 5.83284 18.8489C6.07289 18.8173 6.31632 18.7186 6.65204 18.5412C7.14547 18.2804 7.73556 18.27 8.2189 18.549C8.70224 18.8281 8.98826 19.3443 9.00911 19.9021C9.02331 20.2815 9.05957 20.5417 9.15223 20.7654C9.35522 21.2554 9.74457 21.6448 10.2346 21.8478C10.6022 22 11.0681 22 12 22C12.9319 22 13.3978 22 13.7654 21.8478C14.2554 21.6448 14.6448 21.2554 14.8477 20.7654C14.9404 20.5417 14.9767 20.2815 14.9909 19.902C15.0117 19.3443 15.2977 18.8281 15.781 18.549C16.2643 18.2699 16.8544 18.2804 17.3479 18.5412C17.6836 18.7186 17.927 18.8172 18.167 18.8488C18.6929 18.9181 19.2248 18.7756 19.6456 18.4527C19.9612 18.2105 20.1942 17.807 20.6601 16.9999C21.1261 16.1929 21.3591 15.7894 21.411 15.395C21.4802 14.8691 21.3377 14.3372 21.0148 13.9164C20.8674 13.7243 20.6602 13.5628 20.3387 13.3608C19.8662 13.0639 19.5621 12.558 19.5621 11.9999C19.5621 11.4418 19.8662 10.9361 20.3387 10.6392C20.6603 10.4371 20.8675 10.2757 21.0149 10.0835C21.3378 9.66273 21.4803 9.13087 21.4111 8.60497C21.3592 8.21055 21.1262 7.80703 20.6602 7C20.1943 6.19297 19.9613 5.78945 19.6457 5.54727C19.2249 5.22436 18.693 5.08185 18.1671 5.15109C17.9271 5.18269 17.6837 5.28136 17.3479 5.4588C16.8545 5.71959 16.2644 5.73002 15.7811 5.45096C15.2977 5.17191 15.0117 4.65566 14.9909 4.09794C14.9767 3.71848 14.9404 3.45833 14.8477 3.23463C14.6448 2.74458 14.2554 2.35523 13.7654 2.15224Z" stroke-width="2.5"/>
                                    </svg>
                                </div>
                            </div>
                        </div>

                        <div v-else>

                        </div>

                        <div class="w-full bg-amber-200 p-3 text-center sticky bottom-0 text-3xl hover:bg-amber-300 border-t-2 transition duration-100" style="box-shadow: 0px -6px 8px 0px rgba(0,0,0,0.1);">
                            +
                        </div>
                    </div>
                    
                </div>
                
                <div v-else class="flex flex-grow items-center justify-center">
                    <div class="flex flex-col space-y-5">
                        <p class="text-xl font-bold text-center">{{ currentAccount < 0 ? t('dashboard.select-account-warning') : '' }}</p>
                        <p class="text-xl font-bold text-center">{{ currentCategory < 0 ? t('dashboard.select-category-warning') : '' }}</p>
                    </div>      
                </div>

            </div>

            
        </div>

        <!-- modals -->
        <div v-else class="flex flex-grow">
            <div class="w-full flex flex-col items-center justify-center">
                <form @submit.prevent="SubmitForm" class="flex flex-col shadow-xl border-1 px-5 pb-5 space-y-5 w-[350px]">
                    <div class="flex w-full flex-col px-10 justify-around items-center relative mb-5 overflow-hidden">
                    <img class="w-1/5 relative -bottom-16 z-0" src="../assets/KermitTheFrogHello.png" alt="Kermit the Frog">
                    <p :class="`transition duration-500 py-2 border-y-1 border-black/30 w-full text-xl text-center z-1 ${validationErrors.length > 0 ? 'bg-red-300' : 'bg-emerald-300'}`">
                        {{ 
                            validationErrors.length > 0 ? 
                                validationErrors[0] :
                                t('auth.login-quote') 
                        }}
                    </p>
                </div>
                    
                    <input v-show="modalView == 0" v-model="form.accountName" name="account-name" type="text" :placeholder="t('dashboard.modal.account-name-placeholder')">
                    <input v-show="modalView == 1" v-model="form.categoryName" name="category-name" :placeholder="t('dashboard.modal.category-name-placeholder')">
                    <div v-show="modalView > 1" class="flex flex-col">
                        <label class="font-bold" for="transfer-category">{{ t('dashboard.modal.category-picker-label') }}</label>
                        <select v-model="form.categoryId" name="transfer-category" class="w-full">
                            <option v-for="category in categories" :value="category.id">{{ category.name }}</option>
                        </select>
                    </div>
                    <div class="flex flex-col space-y-2">
                        <input class="disabled:bg-gray-100 hover:bg-amber-100 transition duration-100" type="submit" :disabled="isSubmitting" :value="t(modalView == 0 ? 'dashboard.modal.create-account-button' : (modalView == 1 ? 'dashboard.modal.create-category-button' : 'dashboard.modal.create-transfer-button'))">
                        <input @click="modalView = -1" class="disabled:bg-gray-100 hover:bg-amber-100 transition duration-100" type="submit" :disabled="isSubmitting" :value="t('dashboard.modal.cancel-button')">
                    </div>
                </form>
            </div>
        </div>
    </container-component>
</template>

<script setup>
    import ContainerComponent from '../components/ContainerComponent.vue'
    import { onMounted, ref, onBeforeMount } from 'vue'
    import { useI18n } from 'vue-i18n'
    import api from '../api/api'
    import { formatDateRange, getPeriodDates, dateToString } from '../utils/datetime'
    import { useModalFormValidator } from '../utils/validators'

    const { t, locale } = useI18n()
    const { validateAccountName, validateCategoryName } = useModalFormValidator()

    const page = ref(0)
    const transferType = ref(0)
    const period = ref('week')
    const currentPeriod = ref({ from: new Date(), to: new Date(), milestone: new Date()})
    const currentAccount = ref(-2)
    const currentCategory = ref(-2)
    const accounts = ref([])
    const categories = ref([])
    const isLoading = ref(true)
    const modalView = ref(-1)

    const isSubmitting = ref(false)
    const validationErrors = ref([])
    const form = ref({
        accountName: '',
        categoryName: '',
        categoryId: -1,
    })

    const user = ref(null)

    const ValidateForm = () => {
        validationErrors.value = []
        let response

        if (modalView.value == 0) {
            response = validateAccountName(form.value.accountName)
        } else if (modalView.value == 1) {
            response = validateCategoryName(form.value.categoryName) 
        } else {
            response = false
        }

        if (response !== false) validationErrors.value.push(response)
        return validationErrors.value.length > 0 ? false : true
    }

    const SubmitForm = async () => {
        if (isSubmitting.value) return
        if (!ValidateForm()) return

        isSubmitting.value = true
    
        try { 
            // 0 - add account 
            // 1 - add category 
            // 2 - add transfer for account 
            // 3 add transfer for category and account
            if (modalView.value == 0) {
                await api.post('/accounts', {
                    name: form.value.accountName
                })
            } else if (modalView.value == 1) {
                await api.post('/categories', {
                    name: form.value.categoryName
                })
            } else if (modalView.value == 2) {

            } else if (modalView.value == 2) {

            } else return

            await FetchUserData()
            modalView.value = -1
        } catch (ex) {
            if (ex.status == 409) validationErrors.value.push(t(modalView == 0 ? 'dashboard.modal.account-conflict-error' : 'dashboard.modal.category-conflict-error'))
            else validationErrors.value.push(t('dashboard.modal.submit-fail'))
        } finally {
            isSubmitting.value = false
        }
    }

    const GetCategoryId = (category) => {
        for (let index = 0; index < accounts.value[currentAccount.value].categories.length; index++) {
            if (accounts.value[currentAccount.value].categories[index].name === category.name) return index
        }
        return -1
    }

    const GoToCategory = (id) => {
        let inputId = -2
        const name = accounts.value[currentAccount.value].categories[id].name

        for (let index = 0; index < categories.value.length; index++) {
            if (categories.value[index].name === name) {
                inputId = index
                break
            }
        }

        currentCategory.value = inputId
        ChangePage(2)
    }

    const ChangePage = (p) => {
        page.value = p
        UpdateHighlighter('#sidebar-highlighter', page.value)
    }

    const ChangeTransferType = (p) => {
        transferType.value = p
        UpdateHighlighter('#transfer-type-highlighter', transferType.value)
    }

    const UpdateHighlighter = (selector, position) => document.querySelector(selector).style.translate = `${100*position}%`

    const HandleDatePicker = (e) => {
        if (e.target.id == "date-picker-from")
            currentPeriod.value.from = new Date([...e.target.value.split('-')])
        else 
            currentPeriod.value.to = new Date([...e.target.value.split('-')])

        PrepareData()
    }

    const HandlePeriodSelection = () => {
        currentPeriod.value.milestone = new Date()

        if (period.value == 'custom') {
            const now = new Date()
            let from = new Date((new Date()).setDate(now.getDate() - 1))
            let to = new Date((new Date()).setDate(now.getDate() + 1))
            document.querySelector('#date-picker-from').value = from.toISOString().split('T')[0]
            document.querySelector('#date-picker-to').value = to.toISOString().split('T')[0]
            currentPeriod.value.from = new Date(from)
            currentPeriod.value.to = new Date(to)
        }

        UpdatePeriodPreview()
    }

    const ChangeMilestone = (direction) => {
        const temp = new Date(currentPeriod.value.milestone)

        switch (period.value) {
            case 'week':
                currentPeriod.value.milestone.setDate(temp.getDate() + 7 * direction)
                break;
            case 'month':
                currentPeriod.value.milestone.setMonth(temp.getMonth() + 1 * direction)
                break;
            case 'year':
                currentPeriod.value.milestone.setFullYear(temp.getFullYear() + 1 * direction)
                break;
            default:
                break;
        }

        UpdatePeriodPreview()
        PrepareData()
    }

    const UpdatePeriodPreview = () => {
        if (period.value != 'custom') {
            currentPeriod.value = getPeriodDates(period.value, currentPeriod.value == null ? new Date() : currentPeriod.value.milestone)
        }

        PrepareData()
    }

    onBeforeMount(async () => {
        await FetchUserData()
    })

    onMounted(() => {
        // ChangePage(1)
        // ChangeTransferType(0)
        // UpdatePeriodPreview()
    })


    const PrepareData = () => {
        if (user.value == null) return

        // обнуляем данные
        accounts.value = []
        categories.value = user.value.categories

        let categoriesKeys = {}

        user.value.accounts.forEach((account, index) => {
            accounts.value.push({
                name: account.name,
                income: 0,
                outcome: 0,
                categories: []
            })

            account.transfers.forEach(transfer => {
                const moment = new Date(transfer.moment)
                if (moment.getTime() >= currentPeriod.value.from.getTime() && moment.getTime() <= currentPeriod.value.to.getTime()) {
                    if (!(transfer.category.name in categoriesKeys)) {
                        accounts.value[index].categories.push({
                            name: transfer.category.name,
                            income: 0,
                            outcome: 0,
                            percentage: {
                                income: 0,
                                outcome: 0
                            },
                            transfers: {
                                income: [],
                                outcome: []
                            }
                        })
          
                        categoriesKeys[transfer.category.name] = accounts.value[index].categories.length - 1
                    } 

                    let category = accounts.value[index].categories[categoriesKeys[transfer.category.name]]
                    let account = accounts.value[index]

                    if (transfer.bill >= 0) {
                        account.income += transfer.bill
                        category.income += transfer.bill
                        category.transfers.income.push(transfer)
                    } else {
                        account.outcome += transfer.bill
                        category.outcome += transfer.bill
                        category.transfers.outcome.push(transfer)
                    }
                }
            })
        })

        accounts.value.forEach((account, index) => {
            account.categories.forEach(category => {
                category.percentage = {
                    income: (category.income / (accounts.value[currentAccount.value].income + 1/10000) * 100).toFixed(1), 
                    outcome: (category.outcome / (accounts.value[currentAccount.value].outcome + 1/10000) * 100).toFixed(1) 
                }
            })
        })

        SortData()
    }

    const SortData = () => {
        accounts.value.forEach(account => {
            if (transferType.value) account.categories = account.categories.sort((a,b) => a.outcome - b.outcome)
            else account.categories = account.categories.sort((a,b) => b.income - a.income)
        })
    }

    const FetchUserData = async () => {
        isLoading.value = true
        
        user.value = (await api.get('/report/user')).data

        PrepareData()

        isLoading.value = false
    }
</script>


<style scoped>
@reference 'tailwindcss';

svg {
    @apply select-none;
}

#sidebar {
    div {
        @apply transition flex items-center justify-center w-1/3 cursor-pointer;    
    }

    div:hover {
        svg {
            @apply -translate-y-1 transition duration-200;
        }
    }
}

#transfer-type-picker {
    div {
        @apply transition flex items-center justify-center w-1/2 cursor-pointer;    
    }

    div:hover {
        @apply shadow-xl -rotate-x-20;      
    }
}

form {
    input {
        @apply p-3 text-lg rounded-sm border-1;
    }
}

</style>