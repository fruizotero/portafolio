// src/composables/useGet.js
import { ref } from 'vue'
import axios from 'axios'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: { 'Content-Type': 'application/json' },
})

/**
 * Composable para ejecutar peticiones GET.
 * @returns {Object} { data, isLoading, isSuccess, error, get }
 */
export function useGet() {
  const data      = ref(null)
  const isLoading = ref(false)
  const isSuccess = ref(false)
  const error     = ref(null)

  /**
   * Ejecuta un GET a la ruta indicada.
   * @param {string} url     Ruta (p.ej. '/Perfil/123') o URL completa.
   * @param {object} config  (opcional) Configuración axios para este request.
   * @returns {object}       Axios response
   */
  const get = async (url, config = {}) => {
    isLoading.value = true
    error.value     = null
    isSuccess.value = false

    try {
      const response = await api.get(url, config)
      data.value      = response.data

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

  return {
    data,
    isLoading,
    isSuccess,
    error,
    get,
  }
}
