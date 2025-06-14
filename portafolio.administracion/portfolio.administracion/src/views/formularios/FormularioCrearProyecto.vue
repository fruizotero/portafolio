<template>
  <ModalBaseComponent v-model="show" class="w-full max-w-2xl">
    <template #header>
      <h3 class="text-lg font-bold">Crear Proyecto</h3>
    </template>

    <!-- Form para poder resetear campos -->
    <form @submit.prevent="submit" class="space-y-4">
      <!-- Título -->
      <div class="form-control">
        <label class="label"><span class="label-text">Título</span></label>
        <input v-model="form.titulo" type="text" class="input input-bordered w-full" required />
      </div>

      <!-- Descripción -->
      <div class="form-control">
        <label class="label"><span class="label-text">Descripción</span></label>
        <textarea v-model="form.descripcion" class="textarea textarea-bordered w-full" rows="3" required></textarea>
      </div>

      <!-- Imagen Desktop -->
      <div class="form-control">
        <label class="label"><span class="label-text">Imagen Desktop</span></label>
        <input type="file" @change="onFileChange($event, 'imagenDesktop')" accept="image/*" class="file-input file-input-bordered w-full" />
      </div>

      <!-- Imagen Mobile -->
      <div class="form-control">
        <label class="label"><span class="label-text">Imagen Mobile</span></label>
        <input type="file" @change="onFileChange($event, 'imagenMobile')" accept="image/*" class="file-input file-input-bordered w-full" />
      </div>

      <!-- Repositorio URL -->
      <div class="form-control">
        <label class="label"><span class="label-text">Repositorio URL</span></label>
        <input v-model="form.repositorioUrl" type="url" class="input input-bordered w-full" />
      </div>

      <!-- Live URL -->
      <div class="form-control">
        <label class="label"><span class="label-text">Live URL</span></label>
        <input v-model="form.liveUrl" type="url" class="input input-bordered w-full" />
      </div>

      <!-- Orden -->
      <div class="form-control">
        <label class="label"><span class="label-text">Orden</span></label>
        <input v-model.number="form.orden" type="number" class="input input-bordered w-full" min="0" />
      </div>

      <!-- Habilidades -->
      <div class="form-control">
        <label class="label"><span class="label-text">Habilidades</span></label>
        <select v-model="form.habilidadesIds" multiple class="select select-bordered w-full">
          <option v-for="h in habilidades" :key="h.id" :value="h.id">{{ h.nombre }}</option>
        </select>
      </div>

      <!-- Conocimientos -->
      <div class="form-control">
        <label class="label"><span class="label-text">Conocimientos</span></label>
        <select v-model="form.conocimientosIds" multiple class="select select-bordered w-full">
          <option v-for="c in conocimientos" :key="c.id" :value="c.id">{{ c.nombre }}</option>
        </select>
      </div>

      <!-- Footer con botones -->
    </form>
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
import { ref, onMounted } from 'vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { usePost } from '@/comporsables/usePost'
import { useGet } from '@/comporsables/useGet'


const props = defineProps({
  usuarioAdministradorId: { type: Number, required: true }
})
const emit = defineEmits(['created'])

// Modal control
const show = ref(false)
function open() { resetForm(); show.value = true }
function close() { show.value = false }
defineExpose({ open, close })

// Form state and reset logic
const initialState = () => ({
  titulo: '',
  descripcion: '',
  imagenDesktop: null,
  imagenMobile: null,
  repositorioUrl: '',
  liveUrl: '',
  orden: 0,
  habilidadesIds: [],
  conocimientosIds: []
})
const form = ref(initialState())
function resetForm() { form.value = initialState() }

// File handlers
function onFileChange(e, field) {
  form.value[field] = e.target.files[0] || null
}

// Load options
const habilidades = ref([])
const conocimientos = ref([])
const { get: getSkills } = useGet()
const { get: getKnowledge } = useGet()

onMounted(async () => {
  const sk = await getSkills(`/habilidad/usuario/${props.usuarioAdministradorId}`)
  habilidades.value = sk.data.datos || []
  const kc = await getKnowledge(`/conocimiento/usuario/${props.usuarioAdministradorId}`)
  conocimientos.value = kc.data.datos || []
})

// Submit
const { post, isLoading } = usePost()
async function submit() {
  const data = new FormData()
  Object.entries(form.value).forEach(([key, val]) => {
    if (val instanceof File) data.append(key, val)
    else if (Array.isArray(val)) val.forEach(v => data.append(key, v))
    else data.append(key, val)
  })
  try {
    const res = await post(
      `/Proyecto/${props.usuarioAdministradorId}`,
      data,
      { headers: { 'Content-Type': 'multipart/form-data' } }
    )
    emit('created', res.data)
    close()
  } catch (err) {
    console.error(err)
  }
}

// Cancel handler resets and closes
function onCancel() {
  resetForm()
  close()
}
</script>

