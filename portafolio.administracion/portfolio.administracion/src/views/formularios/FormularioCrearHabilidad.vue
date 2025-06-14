<template>
  <ModalBaseComponent v-model="show" class="w-full max-w-md">
    <!-- Header -->
    <template #header>
      <h3 class="text-lg font-bold">Crear Habilidad</h3>
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
            placeholder="Nombre de la habilidad"
            required
          />
        </div>

        <!-- Es Actual -->
        <div class="form-control flex items-center">
          <input
            id="esActual"
            v-model="form.esActual"
            type="checkbox"
            class="checkbox checkbox-primary mr-2"
          />
          <label for="esActual" class="label-text">Habilidad actual</label>
        </div>

        <!-- Logo -->
        <div class="form-control">
          <label class="label"><span class="label-text">Logo (opcional)</span></label>
          <input
            type="file"
            @change="onFileChange($event)"
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

// Estado del formulario y reset
const initialState = () => ({
  nombre: '',
  esActual: true,
  logo: null
})
const form = ref(initialState())
function resetForm() { form.value = initialState() }

// Manejador de archivo
function onFileChange(e) {
  form.value.logo = e.target.files[0] || null
}

// Cliente POST
const { post, isLoading } = usePost()

async function submit() {
  const data = new FormData()
  data.append('Nombre', form.value.nombre)
  data.append('EsActual', form.value.esActual)
  if (form.value.logo) data.append('Logo', form.value.logo)

  try {
    const res = await post(
      `/Habilidad/${props.usuarioAdministradorId}`,
      data,
      { headers: { 'Content-Type': 'multipart/form-data' } }
    )
    emit('created', res.data)
    close()
  } catch (err) {
    console.error('Error al crear habilidad:', err)
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
