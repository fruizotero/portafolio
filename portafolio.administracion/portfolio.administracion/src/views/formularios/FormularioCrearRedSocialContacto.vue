<template>
  <ModalBaseComponent v-model="show" class="w-full max-w-md">
    <!-- Header -->
    <template #header>
      <h3 class="text-lg font-bold">Crear Red Social de Contacto</h3>
    </template>

    <!-- Body: Formulario -->
    <template #default>
      <form @submit.prevent="submit" ref="formRef" class="space-y-4 pt-6 pb-6">
        <!-- Plataforma -->
        <div class="form-control">
          <label class="label"><span class="label-text">Plataforma</span></label>
          <input
            v-model="form.plataforma"
            type="text"
            class="input input-bordered w-full"
            placeholder="Nombre de la plataforma"
            required
          />
        </div>

        <!-- URL -->
        <div class="form-control">
          <label class="label"><span class="label-text">URL</span></label>
          <input
            v-model="form.url"
            type="url"
            class="input input-bordered w-full"
            placeholder="Enlace completo (https://...)"
            required
          />
        </div>

        <!-- Icono -->
        <div class="form-control">
          <label class="label"><span class="label-text">Icono (opcional)</span></label>
          <input
            type="file"
            @change="onFileChange"
            accept="image/*"
            class="file-input file-input-bordered w-full"
          />
        </div>
      </form>
    </template>

    <!-- Footer: botones fuera del form -->
    <template #footer>
      <button type="button" class="btn btn-secondary mr-2" @click="onCancel">Cancelar</button>
      <button type="button" class="btn btn-primary" :disabled="isLoading" @click="submit">
        <span v-if="isLoading" class="loading loading-spinner"></span>
        <span v-else>Guardar</span>
      </button>
    </template>
  </ModalBaseComponent>
</template>

<script setup>
import ModalBaseComponent from '@/components/ModalBaseComponent.vue';
import { usePost } from '@/comporsables/usePost';
import { ref } from 'vue'


// Props: usuarioAdministradorId
const props = defineProps({
  usuarioAdministradorId: { type: Number, required: true }
})
const emit = defineEmits(['created'])

// Modal control
defineExpose({ open, close })
const show = ref(false)
function open() {
  resetForm()
  show.value = true
}
function close() {
  show.value = false
}

// Form state & reset logic
function initialState() {
  return {
    plataforma: '',
    url: '',
    icono: null
  }
}
const form = ref(initialState())
function resetForm() {
  form.value = initialState()
}

// File handler
function onFileChange(e) {
  form.value.icono = e.target.files[0] || null
}

// usePost for multipart/form-data
const { post, isLoading } = usePost()

async function submit() {
  const data = new FormData()
  data.append('Plataforma', form.value.plataforma)
  data.append('Url', form.value.url)
  if (form.value.icono) {
    data.append('Icono', form.value.icono)
  }

  try {
    const res = await post(
      `/RedSocialContacto/${props.usuarioAdministradorId}`,
      data,
      { headers: { 'Content-Type': 'multipart/form-data' } }
    )
    emit('created', res.data)
    close()
  } catch (err) {
    emit('created', err.response?.data || err)
  }
}

function onCancel() {
  resetForm()
  close()
}
</script>

<style scoped>
/* Espaciado interior del modal */
::v-deep .modal-box {
  padding-top: 1.5rem;
  padding-bottom: 1.5rem;
}
</style>
