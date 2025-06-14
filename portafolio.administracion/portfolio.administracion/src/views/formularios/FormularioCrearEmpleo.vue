<template>
  <ModalBaseComponent v-model="show" class="w-full max-w-lg">
    <!-- Header -->
    <template #header>
      <h3 class="text-lg font-bold">Crear Empleo</h3>
    </template>

    <!-- Body: Formulario -->
    <template #default>
      <form @submit.prevent="submit" ref="formRef" class="space-y-4 pt-6 pb-6">
        <!-- Empresa -->
        <div class="form-control">
          <label class="label"><span class="label-text">Empresa</span></label>
          <input
            v-model="form.empresa"
            type="text"
            class="input input-bordered w-full"
            placeholder="Nombre de la empresa"
            required
          />
        </div>

        <!-- Cargo -->
        <div class="form-control">
          <label class="label"><span class="label-text">Cargo</span></label>
          <input
            v-model="form.cargo"
            type="text"
            class="input input-bordered w-full"
            placeholder="Título del cargo"
            required
          />
        </div>

        <!-- Fecha Inicio -->
        <div class="form-control">
          <label class="label"><span class="label-text">Fecha Inicio</span></label>
          <input
            v-model="form.fechaInicio"
            type="date"
            class="input input-bordered w-full"
            required
          />
        </div>

        <!-- Fecha Fin -->
        <div class="form-control">
          <label class="label"><span class="label-text">Fecha Fin (opcional)</span></label>
          <input
            v-model="form.fechaFin"
            type="date"
            class="input input-bordered w-full"
          />
        </div>

        <!-- Descripción -->
        <div class="form-control">
          <label class="label"><span class="label-text">Descripción (opcional)</span></label>
          <textarea
            v-model="form.descripcion"
            class="textarea textarea-bordered w-full"
            rows="3"
            placeholder="Descripción de las responsabilidades"
          ></textarea>
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
import ModalBaseComponent from '@/components/ModalBaseComponent.vue';
import { usePost } from '@/comporsables/usePost';
import { ref } from 'vue'


// Recibe el id del usuario administrador
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
  empresa: '',
  cargo: '',
  fechaInicio: '',
  fechaFin: '',
  descripcion: ''
})
const form = ref(initialState())
function resetForm() { form.value = initialState() }

// usePost para JSON
const { post, isLoading } = usePost()

async function submit() {
  const payload = {
    Empresa: form.value.empresa,
    Cargo: form.value.cargo,
    FechaInicio: form.value.fechaInicio,
    FechaFin: form.value.fechaFin || null,
    Descripcion: form.value.descripcion || null
  }
  try {
    const res = await post(
      `/Empleo/${props.usuarioAdministradorId}`,
      payload
    )
    emit('created', res.data)
    close()
  } catch (err) {
    console.error('Error al crear empleo:', err)
  }
}

function onCancel() {
  resetForm()
  close()
}
</script>

<style scoped>
::v-deep .modal-box {
  padding-top: 1.5rem;
  padding-bottom: 1.5rem;
}
</style>
