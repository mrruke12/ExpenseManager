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
        {
          path: '/report',
          name: 'report',
          component: () => import('../views/ReportView.vue'),
          meta: {requiresAuth: true}
        },
        {
            path: '/auth',
            name: "auth",
            component: () => import("../views/AuthView.vue")
        },
        {
            path: '/dashboard',
            name: "dashboard",
            meta: {requiresAuth: true},
            component: () => import("../views/DashboardView.vue"),
        }
    ]
})

router.beforeEach(async (to, from, next) => {
    const token = localStorage.getItem('token')
    const isAuthenticated = await checkAuth()

    if (to.meta.requiresAuth && (!token || !isAuthenticated)) {
        next('/auth') // Перенаправляем на страницу входа
    } else {
        next() // Пропускаем
    }
})

export default router