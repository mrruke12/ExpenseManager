<template>
    <header :class="`${Sticky ? 'sticky top-0' : ''} z-10 p-4 bg-linear-to-b from-emerald-600 to-emerald-400 flex sm:items-center justify-between`">
        <router-link to="/">
            <img src="../assets/KermitTheFrogHappy.png" alt="logo" class="w-10 mr-2">
        </router-link>

        <div class="flex flex-col w-auto sm:w-full sm:flex-row sm:justify-between">
            <ul @click="ExpandMenu" id="menu-btn">
                <li>
                    <svg width="25px" height="25px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path class="stroke-gray-800" d="M4 6H20M4 12H20M4 18H20" stroke="#000000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg> 
                </li>
            </ul>
            
            <div id="menu" class="w-full hidden sm:flex sm:justify-between items-center">
                <ul>
                    <router-link :class="{ 'underline' : Home }" to="/">
                        <li>{{ t('header.home-route') }}</li>
                    </router-link>

                    
                    <router-link :class="{ 'underline' : Dashboard }" to="/dashboard">
                        <li>{{ t('header.dashboard-route') }}</li>
                    </router-link>
                </ul>

                <ul>
                    <li v-show="Authorized" class="flex items-center" @click="Logout(router)">
                        <span>{{ t('header.logout-btn') }}</span>
                        <svg width="25px" height="25px" viewBox="0 -0.5 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path class="stroke-current" d="M11.75 9.874C11.75 10.2882 12.0858 10.624 12.5 10.624C12.9142 10.624 13.25 10.2882 13.25 9.874H11.75ZM13.25 4C13.25 3.58579 12.9142 3.25 12.5 3.25C12.0858 3.25 11.75 3.58579 11.75 4H13.25ZM9.81082 6.66156C10.1878 6.48991 10.3542 6.04515 10.1826 5.66818C10.0109 5.29121 9.56615 5.12478 9.18918 5.29644L9.81082 6.66156ZM5.5 12.16L4.7499 12.1561L4.75005 12.1687L5.5 12.16ZM12.5 19L12.5086 18.25C12.5029 18.25 12.4971 18.25 12.4914 18.25L12.5 19ZM19.5 12.16L20.2501 12.1687L20.25 12.1561L19.5 12.16ZM15.8108 5.29644C15.4338 5.12478 14.9891 5.29121 14.8174 5.66818C14.6458 6.04515 14.8122 6.48991 15.1892 6.66156L15.8108 5.29644ZM13.25 9.874V4H11.75V9.874H13.25ZM9.18918 5.29644C6.49843 6.52171 4.7655 9.19951 4.75001 12.1561L6.24999 12.1639C6.26242 9.79237 7.65246 7.6444 9.81082 6.66156L9.18918 5.29644ZM4.75005 12.1687C4.79935 16.4046 8.27278 19.7986 12.5086 19.75L12.4914 18.25C9.08384 18.2892 6.28961 15.5588 6.24995 12.1513L4.75005 12.1687ZM12.4914 19.75C16.7272 19.7986 20.2007 16.4046 20.2499 12.1687L18.7501 12.1513C18.7104 15.5588 15.9162 18.2892 12.5086 18.25L12.4914 19.75ZM20.25 12.1561C20.2345 9.19951 18.5016 6.52171 15.8108 5.29644L15.1892 6.66156C17.3475 7.6444 18.7376 9.79237 18.75 12.1639L20.25 12.1561Z" fill="#000000"/>
                        </svg>
                    </li>
                    
                    <li v-show="!Authorized" class="flex items-center" @click="() => router.push('/auth')">
                        <span>{{ t('header.login-btn') }}</span>
                        <svg width="25px" height="25px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd" d="M3.5 9.56757V14.4324C3.5 16.7258 3.5 17.8724 4.22162 18.5849C4.87718 19.2321 5.89572 19.2913 7.81827 19.2968C7.81303 19.262 7.80803 19.2271 7.80324 19.192C7.68837 18.3484 7.68839 17.2759 7.68841 15.9453L7.68841 15.8919C7.68841 15.4889 8.01933 15.1622 8.42754 15.1622C8.83575 15.1622 9.16667 15.4889 9.16667 15.8919C9.16667 17.2885 9.16824 18.2626 9.26832 18.9975C9.36554 19.7114 9.54337 20.0895 9.81613 20.3588C10.0889 20.6281 10.4718 20.8037 11.195 20.8996C11.9394 20.9985 12.926 21 14.3406 21H15.3261C16.7407 21 17.7273 20.9985 18.4717 20.8996C19.1948 20.8037 19.5778 20.6281 19.8505 20.3588C20.1233 20.0895 20.3011 19.7114 20.3983 18.9975C20.4984 18.2626 20.5 17.2885 20.5 15.8919V8.10811C20.5 6.71149 20.4984 5.73743 20.3983 5.0025C20.3011 4.28855 20.1233 3.91048 19.8505 3.6412C19.5778 3.37192 19.1948 3.19635 18.4717 3.10036C17.7273 3.00155 16.7407 3 15.3261 3H14.3406C12.926 3 11.9394 3.00155 11.195 3.10036C10.4718 3.19635 10.0889 3.37192 9.81613 3.6412C9.54337 3.91048 9.36554 4.28855 9.26832 5.0025C9.16824 5.73743 9.16667 6.71149 9.16667 8.10811C9.16667 8.51113 8.83575 8.83784 8.42754 8.83784C8.01933 8.83784 7.68841 8.51113 7.68841 8.10811L7.68841 8.05472C7.68839 6.72409 7.68837 5.65156 7.80324 4.80803C7.80803 4.77288 7.81303 4.73795 7.81827 4.70325C5.89572 4.70867 4.87718 4.76792 4.22162 5.41515C3.5 6.12759 3.5 7.27425 3.5 9.56757ZM13.385 14.9484L15.8487 12.516C16.1374 12.231 16.1374 11.769 15.8487 11.484L13.385 9.05157C13.0963 8.76659 12.6283 8.76659 12.3397 9.05157C12.051 9.33655 12.051 9.79859 12.3397 10.0836L13.5417 11.2703H6.45652C6.04831 11.2703 5.71739 11.597 5.71739 12C5.71739 12.403 6.04831 12.7297 6.45652 12.7297H13.5417L12.3397 13.9164C12.051 14.2014 12.051 14.6635 12.3397 14.9484C12.6283 15.2334 13.0963 15.2334 13.385 14.9484Z" fill="#1C274C"/>
                        </svg>
                    </li>

                    <li class="flex items-center">
                        <div @mouseover="LangForecast" @mouseout="LangBase" @click="ChangeLang" class="mr-1">
                            <p  class="block">{{ t('header.curr-lang-label') }}</p>
                            <p  class="absolute -translate-y-6 opacity-0 transition">{{ t('header.next-lang-label') }}</p>
                        </div>
                        
                        
                        <svg width="20px" height="20px" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg">
                            <path class="fill-gray-800" clip-rule="evenodd" d="M4 0H6V2H10V4H8.86807C8.57073 5.66996 7.78574 7.17117 6.6656 8.35112C7.46567 8.73941 8.35737 8.96842 9.29948 8.99697L10.2735 6H12.7265L15.9765 16H13.8735L13.2235 14H9.77647L9.12647 16H7.0235L8.66176 10.9592C7.32639 10.8285 6.08165 10.3888 4.99999 9.71246C3.69496 10.5284 2.15255 11 0.5 11H0V9H0.5C1.5161 9 2.47775 8.76685 3.33437 8.35112C2.68381 7.66582 2.14629 6.87215 1.75171 6H4.02179C4.30023 6.43491 4.62904 6.83446 4.99999 7.19044C5.88743 6.33881 6.53369 5.23777 6.82607 4H0V2H4V0ZM12.5735 12L11.5 8.69688L10.4265 12H12.5735Z" fill="#000000"/>
                        </svg>
                    </li>
                </ul>    
            </div>
        </div>
        
    </header>
</template>

<script setup>
    import { ref, onMounted } from 'vue'
    import { useRouter } from 'vue-router'
    import { useI18n } from 'vue-i18n'
    import { checkAuth, Logout } from '../utils/auth'
    import api from '../api/api'

    const props = defineProps({
        Home: Boolean,
        Dashboard: Boolean,
        Sticky: Boolean
    })

    const Authorized = ref(false)
    const { t, locale } = useI18n()
    const menu = ref(null)
    const router = useRouter()
    const isChangingLanguage = ref(false)

    onMounted(async () => {
        menu.value = document.querySelector("#menu")
        Authorized.value = await checkAuth()
    })

    const ExpandMenu = e => {
        if (menu.value == null) return
        if (!menu.value.classList.replace("hidden", "block"))
            menu.value.classList.replace("block", "hidden")
    }

    const LangForecast = e => {
        if (e.target.nodeName == "DIV") return;

        const p = e.target.parentNode.querySelectorAll("p")[1]
        p.classList.remove("-translate-y-6")
        p.classList.remove("opacity-0")
        p.classList.add("-translate-y-2")
    }

    const LangBase = e => {
        if (e.target.nodeName == "DIV") return;

        const p = e.target.parentNode.querySelectorAll("p")[1]
        p.classList.add("-translate-y-6")
        p.classList.add("opacity-0")
        p.classList.remove("-translate-y-2")
    }

    const ChangeLang = async () => {
        if (!isChangingLanguage.value) {
            isChangingLanguage.value = true        
            locale.value = locale.value == "ru" ? "en" : "ru"
            if (Authorized.value) await api.put('/users/current-locale', { locale: locale.value })
            localStorage.setItem("locale", locale.value)
            isChangingLanguage.value = false
        }
    }
</script>

<style scoped>
    @reference "tailwindcss";
    
    li {
        @apply mx-2 my-1 flex justify-end transition cursor-pointer hover:text-amber-200 sm:my-0;

        svg path {
            @apply fill-current;
        }
    }

    ul {
        @apply flex flex-col sm:flex-row uppercase font-bold;
    }

    #menu-btn {
        @apply mt-2 block sm:hidden;
    }
</style>