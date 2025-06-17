import { ref } from 'vue'
import axios from 'axios'

// Axios instance (configura tu baseURL en .env)
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: { 'Content-Type': 'application/json' }
})

/**
 * Composable para ejecutar peticiones DELETE.
 * @returns {{ isLoading: Ref<boolean>, isSuccess: Ref<boolean>, error: Ref<string|null>, remove: (url: string, config?: object) => Promise<import('axios').AxiosResponse> }}
 */
export function useDelete() {
  const isLoading = ref(false)
  const isSuccess = ref(false)
  const error = ref(null)

  /**
   * Ejecuta un DELETE en la ruta indicada.
   * @param {string} url     Ruta relativa (p.ej. '/Proyecto/1')
   * @param {object} config  Opcional: configuración axios para la petición
   * @returns {Promise<AxiosResponse>} Response de Axios
   */
  const remove = async (url, config = {}) => {
    isLoading.value = true
    isSuccess.value = false
    error.value = null

    try {
      const response = await api.delete(url, config)
      isSuccess.value = true
      return response
    } catch (err) {
      error.value =
        err.response?.data?.mensaje ||
        err.response?.data?.message ||
        err.message
      throw err
    } finally {
      isLoading.value = false
    }
  }

  return { isLoading, isSuccess, error, remove }
}
