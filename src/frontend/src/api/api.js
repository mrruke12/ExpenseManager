import axios from 'axios'
import { useRouter } from 'vue-router'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${localStorage.getItem('token')}`
  }
})

const token = localStorage.getItem('token')

if (token) {
  api.defaults.headers['Authorization'] = `Bearer ${token}`
}

api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      const router= useRouter()
      router.push('/login');
    }
    
    return Promise.reject(error);
  }
);

export default api