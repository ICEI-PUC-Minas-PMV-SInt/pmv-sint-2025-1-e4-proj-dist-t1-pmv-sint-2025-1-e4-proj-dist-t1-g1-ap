import axios from 'axios'

const api = axios.create({
  baseURL: 'https://api-adota-pet.azurewebsites.net/api',
})

// Interceptor para adicionar o token automaticamente no header
api.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  error => {
    return Promise.reject(error)
  },
)

export { api }
