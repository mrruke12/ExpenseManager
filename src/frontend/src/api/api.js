import axios from 'axios'


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

export default api