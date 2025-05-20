import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import { checkAuth } from '../utils/auth'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            name: 'home',
            component: HomeView
        },
    ]
})

router.beforeEach(async (to, from, next) => {
    const isAuthenticated = await checkAuth()

    if (to.meta.requiresAuth && !isAuthenticated) {
        next('/auth')
    } else {
        next()
    }
})

export default router