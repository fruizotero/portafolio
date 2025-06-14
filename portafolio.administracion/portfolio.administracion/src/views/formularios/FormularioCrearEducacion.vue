<template>
  <ModalBaseComponent v-model="show" class="w-full max-w-lg">
    <!-- Header -->
    <template #header>
      <h3 class="text-lg font-bold">Crear Educación</h3>
    </template>

    <!-- Body: formulario -->
    <template #default>
      <form @submit.prevent="submit" class="space-y-4 pt-6 pb-6">
        <!-- Institución -->
        <div class="form-control">
          <label class="label"><span class="label-text">Institución</span></label>
          <input v-model="form.institucion" type="text" class="input input-bordered w-full" required />
        </div>

        <!-- Título -->
        <div class="form-control">
          <label class="label"><span class="label-text">Título</span></label>
          <input v-model="form.titulo" type="text" class="input input-bordered w-full" required />
        </div>

        <!-- Fecha Inicio -->
        <div class="form-control">
          <label class="label"><span class="label-text">Fecha Inicio</span></label>
          <input v-model="form.fechaInicio" type="date" class="input input-bordered w-full" required />
        </div>

        <!-- Fecha Fin -->
        <div class="form-control">
          <label class="label"><span class="label-text">Fecha Fin (opcional)</span></label>
          <input v-model="form.fechaFin" type="date" class="input input-bordered w-full" />
        </div>

        <!-- Descripción -->
        <div class="form-control">
          <label class="label"><span class="label-text">Descripción (opcional)</span></label>
          <textarea v-model="form.descripcion" class="textarea textarea-bordered w-full" rows="3"></textarea>
        </div>
      </form>
    </template>

    <!-- Footer: botones -->
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
import { ref } from 'vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { usePost } from '@/comporsables/usePost'




// Props: usuarioAdministradorId
const props = defineProps({
  usuarioAdministradorId: { type: Number, required: true }
})
const emit = defineEmits(['created'])

// Modal control
defineExpose({ open, close })
const show = ref(false)
function open() { resetForm(); show.value = true }
function close() { show.value = false }

// Form state & reset
const initialState = () => ({
  institucion: '',
  titulo: '',
  fechaInicio: '',
  fechaFin: '',
  descripcion: ''
})
const form = ref(initialState())
function resetForm() { form.value = initialState() }

// usePost for JSON
const { post, isLoading } = usePost()

async function submit() {
  // Prepara objeto DTO
  const payload = {
    Institucion: form.value.institucion,
    Titulo: form.value.titulo,
    FechaInicio: form.value.fechaInicio,
    FechaFin: form.value.fechaFin || null,
    Descripcion: form.value.descripcion || null
  }
  try {
    const res = await post(
      `/Educacion/${props.usuarioAdministradorId}`,
      payload
    )
    emit('created', res.data)
    close()
  } catch (err) {
    console.error(err)
  }
}

function onCancel() {
  resetForm()
  close()
}
</script>

<style scoped>
/* Ajuste de padding en modal */
::v-deep .modal-box {
  padding-top: 1.5rem;
  padding-bottom: 1.5rem;
}
</style>
