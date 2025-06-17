<template>
  <ModalBaseComponent v-model="show" class="w-full max-w-md">
    <!-- Header -->
    <template #header>
      <h3 class="text-lg font-bold">Crear Conocimiento</h3>
    </template>

    <!-- Body: Formulario -->
    <template #default>
      <form @submit.prevent="submit" ref="formRef" class="space-y-4 pt-6 pb-6">
        <!-- Nombre -->
        <div class="form-control">
          <label class="label"><span class="label-text">Nombre</span></label>
          <input
            v-model="form.nombre"
            type="text"
            class="input input-bordered w-full"
            placeholder="Nombre del conocimiento"
            required
          />
        </div>

        <!-- Descripción (opcional) -->
        <div class="form-control">
          <label class="label"><span class="label-text">Descripción (opcional)</span></label>
          <textarea
            v-model="form.descripcion"
            class="textarea textarea-bordered w-full"
            rows="3"
            placeholder="Detalles adicionales"
          ></textarea>
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
import { ref } from 'vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { usePost } from '@/comporsables/usePost'


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

// Estado del formulario y función de reset
const initialState = () => ({
  nombre: '',
  descripcion: ''
})
const form = ref(initialState())
function resetForm() { form.value = initialState() }

// Cliente POST
const { post, isLoading } = usePost()

async function submit() {
  const payload = {
    Nombre: form.value.nombre,
    Descripcion: form.value.descripcion || null
  }
  try {
    const res = await post(
      `/Conocimiento/${props.usuarioAdministradorId}`,
      payload
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
/* Espaciado interior similar al proyecto */
::v-deep .modal-box { padding-top: 1.5rem; padding-bottom: 1.5rem; }
</style>
