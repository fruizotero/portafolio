<template>
  <AlertComponent
      v-model:visible="showAlert"
      :success="isSuccess"
      :message="alertMessage"
      :duration="5000"
      class="sticky top-4 right-4 z-50 ml-auto"
    />
  <button class="btn" @click="form.open()">Nuevo Conocimiento</button>
  <ListComponent title="Conocimientos" :items="conocimientos" :fields="{
      id: 'id',
      image: '',
      title: 'nombre',
      subtitle: '',
      description: '',
      enableLink: false,
    }" v-on:edit="handleEdit" v-on:delete="confirmDelete" />

  <FormularioCrearConocimiento ref="form" :usuarioAdministradorId="usuarioId" @created="onCreated">
  </FormularioCrearConocimiento>
 <!-- Modal de confirmación -->
    <ModalBaseComponent v-model="showDeleteModal">
      <template #header>
        <h3 class="text-lg font-bold text-red-600">Confirmar Eliminación</h3>
      </template>

      <p>¿Estás seguro de que quieres eliminar “{{ conocimientoToDelete?.nombre }}”?</p>

      <template #footer>
        <button class="btn btn-secondary mr-2" @click="cancelDelete">
          Cancelar
        </button>
        <button class="btn btn-error" @click="doDelete">
          Eliminar
        </button>
      </template>
    </ModalBaseComponent>

</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'
import FormularioCrearConocimiento from '../formularios/FormularioCrearConocimiento.vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { useDelete } from '@/comporsables/useDelete'
import AlertComponent from '@/components/AlertComponent.vue'


const conocimientos = ref([])
const showDeleteModal = ref(false)
const conocimientoToDelete = ref(null)
let usuarioAdministradorId = localStorage.getItem('usuarioId')
const showAlert = ref(false)
const alertMessage = ref('')
const isSuccess = ref(false)


const { data, get, isSuccess: fetchSuccess } = useGet()
const usuarioId = Number(localStorage.getItem('usuarioId'))
const form = ref(null)

onMounted(async () => {
  await cargarConocimientos()
})
async function cargarConocimientos() {
  await get(`/Conocimiento/usuario/${usuarioAdministradorId}`)
  if (fetchSuccess.value) {

    conocimientos.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
}

function handleEdit(item) {
  // Lógica para editar la educación
  console.log('Editar educación:', item)
}

// Composable DELETE
const { remove, error: deleteError } = useDelete()

// Abre el modal con el ítem seleccionado
function confirmDelete(item) {
  conocimientoToDelete.value = item
  showDeleteModal.value = true
}

// Cierra el modal sin borrar
function cancelDelete() {
  showDeleteModal.value = false
  conocimientoToDelete.value = null
}

// Ejecuta el DELETE y recarga la lista
async function doDelete() {
  try {
    await remove(
      `/Conocimiento/${conocimientoToDelete.value.id}/usuario/${usuarioAdministradorId}`
    )
    conocimientos.value = conocimientos.value.filter(
      c => c.id !== conocimientoToDelete.value.id
    )
    alertMessage.value = 'Conocimiento eliminado exitosamente'
    showAlert.value = true
    isSuccess.value = true
  } catch (e) {

    alertMessage.value = deleteError.value || 'Error al eliminar el conocimiento'
    showAlert.value = true
    isSuccess.value = false
  } finally {
    cancelDelete()
  }
}
async function onCreated(response) {

  if(response.exitoso === false) {
    alertMessage.value = response.mensaje || 'Error al crear el conocimiento'
    showAlert.value = true
    isSuccess.value = false
    return
  }
  alertMessage.value = response.mensaje || 'Conocimiento creado exitosamente'
  showAlert.value = true
  isSuccess.value = true

  await cargarConocimientos()
}
</script>
