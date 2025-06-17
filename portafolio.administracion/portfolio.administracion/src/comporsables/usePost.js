// src/composables/usePost.js
import { ref } from 'vue'
import axios from 'axios'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: { 'Content-Type': 'application/json' },
})

export function usePost() {
  const isLoading = ref(false)
  const error     = ref(null)
  const isSuccess = ref(false)

  /**
   * Ejecuta un POST a la ruta indicada con el payload dado.
   * @param {string} url       Ruta (p.ej. '/Perfil') o URL completa.
   * @param {any}    payload   Datos a enviar en el body.
   * @param {object} config    (opcional) Config axios para este request.
   * @returns {object}         La respuesta de axios.
   */
  const post = async (url, payload, config = {}) => {
    isLoading.value = true
    error.value     = null
    isSuccess.value = false

    try {
      const response = await api.post(url, payload, config)
      isSuccess.value = true
      return response
    } catch (err) {
      // extrae mensaje si viene de la API, o deja genérico
      error.value = err.response?.data?.mensaje
                 || err.response?.data?.message
                 || err.message
      throw err
    } finally {
      isLoading.value = false
    }
  }

  return { isLoading, error, isSuccess, post }
}
