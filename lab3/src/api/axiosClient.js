import axios from 'axios'
 
const BASE_URL = 'http://localhost:5238'
 
const axiosClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
})
 
// Аналог authInterceptor з RetrofitClient.kt
// Автоматично додає JWT токен до кожного запиту
axiosClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('jwt_token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})
 
// Обробка 401 — токен протух, розлогінити
axiosClient.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.clear()
      window.location.href = '/login'
    }
    return Promise.reject(error)
  }
)
 
export default axiosClient