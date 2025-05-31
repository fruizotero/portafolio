import { ref } from 'vue'
import axios from 'axios'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: { 'Content-Type': 'application/json' },
})

export function useAuth() {
  const token = ref(localStorage.getItem('token')) // Cargar token si existe
  const error = ref(null)
  const isLoading = ref(false)

  const login = async (email, password) => {
    isLoading.value = true
    error.value = null
    try {
      const response = await api.post('/Autenticacion/login', { email, password })
      const { datos } = response.data
      const{token:tokenResponse, usuarioId}= datos
      token.value = tokenResponse
      localStorage.setItem('token',token.value)
      localStorage.setItem('usuarioId', usuarioId)
      localStorage.setItem('email', email)
    } catch (err) {
      error.value = err.response?.data?.mensaje || 'Error al iniciar sesión'
    } finally {
      isLoading.value = false
    }
  }

  const logout = () => {
    token.value = null
    localStorage.removeItem('token')
  }

  return { token, error, isLoading, login, logout }
}
