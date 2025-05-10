import api from '../api/api'
import { useRouter } from 'vue-router'

export const checkAuth = async () => {
  const token = localStorage.getItem('token')
  if (!token) return false
  
  try {
    const response = await api.get('/users/current', {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
    return true
  } catch (error) {
    if (error.response?.status === 401) {
      localStorage.removeItem('token')
    }
    return false
  }
}

export const logout = (router) => {
  localStorage.removeItem('token')
  router.push("/auth")
}