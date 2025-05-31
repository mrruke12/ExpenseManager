import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import { checkAuth } from '../utils/auth'
import NotFoundView from '../views/NotFoundView.vue'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/:pathMatch(.*)*',
            name: "notfound",
            component: NotFoundView
        },
        {
            path: '/',
            name: 'home',
            component: HomeView
        },
        {
            path: '/auth',
            name: 'auth',
            component: () => import('../views/AuthView.vue')
        },
        {
            path: '/dashboard',
            name: 'dashboard',
            component: () => import('../views/Dashboard.vue'),
            meta: {requiresAuth: true}
        }
    ]
})

router.beforeEach(async (to, from, next) => {
    const isAuthenticated = await checkAuth()

    if (to.name == 'auth' && isAuthenticated) {
        next('/')
    } else if (to.meta.requiresAuth && !isAuthenticated) {
        next('/auth')
    } else {
        next()
    }
})

export default router