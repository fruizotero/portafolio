<template>
  <AlertComponent
      v-model:visible="showAlert"
      :success="isSuccess"
      :message="alertMessage"
      :duration="5000"
      class="sticky top-4 right-4 z-50 ml-auto"
    />
  <button class="btn" @click="empForm.open()">Nuevo Empleo</button>
  <ListComponent
    title="Empleos"
    :items="empleos"
    :fields="{
      id: 'id',
      title: 'cargo',
      subtitle: 'empresa',
      description: 'descripcion',
      fechainicio: 'fechaInicio',
      fechafin: 'fechaFin',
      enableLink: false,
    }"
    :onEdit="handleEdit"
    :onDelete="confirmDelete"
  />
  <FormularioCrearEmpleo ref="empForm"
    :usuarioAdministradorId="usuarioId"
    @created="onCreated"></FormularioCrearEmpleo>

    <ModalBaseComponent v-model="showDeleteModal">
      <template #header>
        <h3 class="text-lg font-bold text-red-600">Confirmar Eliminación</h3>
      </template>

      <p>¿Estás seguro de que quieres eliminar “{{ empleoToDelete?.cargo }}”?</p>

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
import FormularioCrearEmpleo from '../formularios/FormularioCrearEmpleo.vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { useDelete } from '@/comporsables/useDelete'
import AlertComponent from '@/components/AlertComponent.vue'

// Componente para mostrar la lista de proyectos
const empleos = ref([])
const showDeleteModal = ref(false)
const empleoToDelete = ref(null)
const usuarioId = Number(localStorage.getItem('usuarioId'))
const showAlert = ref(false)
const alertMessage = ref('')
const isSuccess = ref(false)
const empForm = ref(null)

let usuarioAdministradorId = localStorage.getItem('usuarioId')

const { data, get, isSuccess: fetchSuccess } = useGet()

onMounted(async () => {
  await caragarEmpleos()
})

async function caragarEmpleos() {
  await get(`/Empleo/usuario/${usuarioAdministradorId}`)
  if (fetchSuccess.value) {

    empleos.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
}

function handleEdit(item) {

  console.log(item);
}

// Composable DELETE
const { remove,  error: deleteError } = useDelete()

// Abre el modal con el ítem seleccionado
function confirmDelete(item) {
  empleoToDelete.value = item
  showDeleteModal.value = true
}

// Cierra el modal sin borrar
function cancelDelete() {
  showDeleteModal.value = false
  empleoToDelete.value = null
}

// Ejecuta el DELETE y recarga la lista
async function doDelete() {
  try {
    await remove(
      `/Empleo/${empleoToDelete.value.id}/usuario/${usuarioAdministradorId}`
    )
    empleos.value = empleos.value.filter(
      c => c.id !== empleoToDelete.value.id
    )
    alertMessage.value = 'Empleo eliminado correctamente.'
    isSuccess.value = true
    showAlert.value = true
  } catch (e) {
    alertMessage.value = deleteError.value || 'Error al eliminar el empleo.'
    isSuccess.value = false
    showAlert.value = true
  } finally {
    cancelDelete()
  }
}


async function onCreated(response) {

 if(response.exitoso === false) {
    alertMessage.value = response.mensaje || 'Error al crear el empleo.'
    showAlert.value = true
    isSuccess.value = false
  } else {
    alertMessage.value = 'Empleo creado exitosamente.'
    showAlert.value = true
    isSuccess.value = true
  }
  await caragarEmpleos()
}
</script>
