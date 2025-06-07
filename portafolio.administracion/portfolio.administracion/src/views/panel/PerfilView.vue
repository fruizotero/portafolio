<template>
  <div class="relative">
    <!-- Alert flotante -->
    <AlertComponent
      v-model:visible="showAlert"
      :success="isSuccess"
      :message="alertMessage"
      :duration="5000"
      class="sticky top-4 right-4 z-50 ml-auto"
    />

    <form @submit.prevent="submitPerfil" class="space-y-6">

      <!-- Foto + Nombre/Apellidos/Saludo -->
      <div class="flex flex-col md:flex-row md:space-x-6 items-center md:items-start">
        <!-- Foto -->
        <div class="w-full md:w-1/4 flex flex-col items-center space-y-4">
          <div class="avatar">
            <div class="w-32 h-32 rounded-full ring ring-primary ring-offset-base-100 ring-offset-2 overflow-hidden bg-base-200">
              <img
                v-if="previewUrl"
                :src="previewUrl"
                alt="Preview"
                class="object-cover w-full h-full"
              />
              <span
                v-else
                class="flex items-center justify-center w-full h-full text-sm text-base-content"
              >
                Sin foto
              </span>
            </div>
          </div>
          <input
            type="file"
            @change="onFileChange"
            accept="image/*"
            class="file-input file-input-bordered w-full"
          />
        </div>

        <!-- Campos de texto -->
        <div class="w-full md:w-3/4 space-y-4">
          <div class="form-control">
            <label class="label"><span class="label-text">Nombre</span></label>
            <input
              v-model="nombre"
              type="text"
              placeholder="Tu nombre"
              class="input input-bordered w-full"
              required
            />
          </div>

          <div class="form-control">
            <label class="label"><span class="label-text">Apellidos</span></label>
            <input
              v-model="apellidos"
              type="text"
              placeholder="Tus apellidos"
              class="input input-bordered w-full"
              required
            />
          </div>

          <div class="form-control">
            <label class="label"><span class="label-text">Saludo</span></label>
            <input
              v-model="saludo"
              type="text"
              placeholder="Un saludo breve"
              class="input input-bordered w-full"
            />
          </div>
        </div>
      </div>

      <!-- Descripción -->
      <div class="form-control">
        <label class="label"><span class="label-text">Descripción</span></label>
        <TipTapComponent v-model="descripcion" height="5rem" />
      </div>

      <!-- Acerca de mí -->
      <div class="form-control">
        <label class="label"><span class="label-text">Acerca de mí</span></label>
        <TipTapComponent v-model="acercaDeMi" height="8rem" />
      </div>

      <!-- Botón de envío -->
      <div class="form-control mt-6">
        <button
          type="submit"
          class="btn btn-primary w-full"
          :disabled="isLoading"
        >
          <span v-if="isLoading" class="loading loading-spinner"></span>
          <span v-else>Guardar Perfil</span>
        </button>
      </div>
    </form>
  </div>
</template>


<script setup>
import { onMounted, ref } from 'vue'
import AlertComponent from '@/components/AlertComponent.vue'
import { usePost } from '@/comporsables/usePost'
import TipTapComponent from '@/components/TipTapComponent.vue'
import { useGet } from '@/comporsables/useGet'


// 1️⃣ Composable para POST
const { isLoading, isSuccess, post } = usePost()
const { isLoading: isLoadingGet, isSuccess: isSuccessGet, data: perfilData, get } = useGet()

const usuarioAdministradorId = localStorage.getItem('usuarioId')

// 3️⃣ Campos del formulario
const nombre      = ref('')
const apellidos   = ref('')
const saludo      = ref('')
const descripcion = ref('')
const acercaDeMi  = ref('')
const fotoFile    = ref(null)
const showAlert = ref(false)
const alertMessage = ref('')

// preview URL
const previewUrl = ref(null)
let objectUrl = null



onMounted( async () => {
  // Cargar datos del perfil al montar el componente
  if (usuarioAdministradorId) {
    await cargarPerfil()
  } else {
    alertMessage.value = 'Usuario no encontrado'
    showAlert.value = true
  }
})

// 4️⃣ Captura el file
function onFileChange(e) {
  const file = e.target.files[0] || null
  fotoFile.value = file

  // libera la URL anterior
  if (objectUrl) {
    URL.revokeObjectURL(objectUrl)
    objectUrl = null
  }

  if (file) {
    objectUrl = URL.createObjectURL(file)
    previewUrl.value = objectUrl
  } else {
    previewUrl.value = null
  }
}

// 5️⃣ Envío del formulario usando `post`
async function submitPerfil() {
  // Preparamos FormData
  const formData = new FormData()
  formData.append('Nombre',      nombre.value)
  formData.append('Apellidos',   apellidos.value)
  formData.append('Saludo',      saludo.value)
  formData.append('Descripcion', descripcion.value)
  formData.append('AcercaDeMi',  acercaDeMi.value)
  if (fotoFile.value) {
    formData.append('Foto', fotoFile.value)
  }

  try {
    // El composable maneja isLoading, error e isSuccess
    await post(
      `/Perfil/${usuarioAdministradorId}`,
      formData,
      { headers: { 'Content-Type': 'multipart/form-data' } }
    )
    // Limpieza automática tras éxito
    alertMessage.value = 'Perfil actualizado con éxito.'
    showAlert.value = true
    // setTimeout(() => { isSuccess.value = false }, 3000)
  } catch {
    // El mensaje de `error.value` ya está puesto por el composable
    alertMessage.value = 'Error al actualizar el perfil.'
    showAlert.value = true
    // setTimeout(() => { error.value = null }, 3000)
  }
}

// 6️⃣ función para cargar el perfil
async function cargarPerfil() {
  try {
    const response = await get(`/Perfil/${usuarioAdministradorId}`)
    if (response && response.data) {
      const {datos, mensaje} = response.data
      nombre.value      = datos.nombre || ''
      apellidos.value   = datos.apellidos || ''
      saludo.value      = datos.saludo || ''
      descripcion.value = datos.descripcion || ''
      acercaDeMi.value  = datos.acercaDeMi || ''
      previewUrl.value = datos.fotoURL || null

      alertMessage.value = mensaje || 'Perfil cargado correctamente.'
    } else {
      alertMessage.value = 'No se encontraron datos del perfil.'
      showAlert.value = true
    }
  } catch (error) {
    alertMessage.value = 'Perfil no encontrado'
    showAlert.value = true
  }
}
</script>
