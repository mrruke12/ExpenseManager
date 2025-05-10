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

// api.interceptors.response.use(
//   response => response,
//   error => {
//     if (error.response.status === 400) {
//       const errors = error.response.data.errors
//       const messages = Object.values(errors).flat()
//       throw new Error(messages.join(', '))
//     }
//     if (error.response.status === 401) {
//       throw new Error("Token is not valid or login data is invalid")
//     }
//     throw error
//   }
// )

export default api