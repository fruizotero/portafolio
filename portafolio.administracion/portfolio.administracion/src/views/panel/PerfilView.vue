<template>
  <div class="">

    <h1>Perfil</h1>

    <AlertComponent v-model:visible="showAlert" :success="isSuccess" :message="alertMessage" :duration="5000" />


    <form @submit.prevent="submitPerfil" class="space-y-4">
      <div class="form-control">
        <label class="label"><span class="label-text">Nombre</span></label>
        <input v-model="nombre" type="text" placeholder="Tu nombre" class="input input-bordered w-full" required />
      </div>

      <div class="form-control">
        <label class="label"><span class="label-text">Apellidos</span></label>
        <input v-model="apellidos" type="text" placeholder="Tus apellidos" class="input input-bordered w-full"
          required />
      </div>

      <div class="form-control">
        <label class="label"><span class="label-text">Saludo</span></label>
        <input v-model="saludo" type="text" placeholder="Un saludo breve" class="input input-bordered w-full" />
      </div>

      <div class="form-control">
        <label class="label"><span class="label-text">Descripción</span></label>
        <input v-model="descripcion" type="text" placeholder="Descripción corta" class="input input-bordered w-full" />
      </div>

      <div class="form-control">
        <label class="label"><span class="label-text">Acerca de mí</span></label>
        <textarea v-model="acercaDeMi" placeholder="Escribe algo sobre ti..." class="textarea textarea-bordered w-full"
          rows="4"></textarea>
      </div>

      <div class="form-control">
        <label class="label"><span class="label-text">Foto</span></label>
        <input type="file" @change="onFileChange" class="file-input file-input-bordered w-full" accept="image/*" />
      </div>

      <div class="form-control mt-6">
        <button type="submit" class="btn btn-primary " :disabled="isLoading">
          <span v-if="isLoading" class="loading loading-spinner"></span>
          <span v-else>Guardar Perfil</span>
        </button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import AlertComponent from '@/components/AlertComponent.vue'
import { usePost } from '@/comporsables/usePost'


// 1️⃣ Composable para POST
const { isLoading, error, isSuccess, post } = usePost()


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

// 4️⃣ Captura el file
function onFileChange(e) {
  fotoFile.value = e.target.files[0] || null
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
</script>
