<template>
  <AlertComponent
      v-model:visible="showAlert"
      :success="isSuccess"
      :message="alertMessage"
      :duration="5000"
      class="sticky top-4 right-4 z-50 ml-auto"
    />
  <button class="btn" @click="skillForm.open()">Nueva Habilidad</button>
  <ListComponent title="Habilidades" :items="habilidades" :fields="{
      id: 'id',
      title: 'nombre',
      image: 'logoUrl',
      enableLink: false,
    }" :onEdit="handleEdit" :onDelete="confirmDelete" />
  <FormularioCrearHabilidad ref="skillForm" :usuarioAdministradorId="usuarioId" @created="onSkillCreated">
  </FormularioCrearHabilidad>
<ModalBaseComponent v-model="showDeleteModal">
      <template #header>
        <h3 class="text-lg font-bold text-red-600">Confirmar Eliminación</h3>
      </template>

      <p>¿Estás seguro de que quieres eliminar “{{ habilidadToDelete?.nombre }}”?</p>

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
import FormularioCrearHabilidad from '../formularios/FormularioCrearHabilidad.vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { useDelete } from '@/comporsables/useDelete'
import AlertComponent from '@/components/AlertComponent.vue'

// Componente para mostrar la lista de proyectos
const habilidades = ref([])

const showDeleteModal = ref(false)
const habilidadToDelete = ref(null)
let usuarioAdministradorId = localStorage.getItem('usuarioId')
const showAlert = ref(false)
const alertMessage = ref('')
const isSuccess = ref(false)

const usuarioId = Number(localStorage.getItem('usuarioId'))
const skillForm = ref(null)

const { data, isLoading, get, isSuccess: fetchSuccess } = useGet()

onMounted(async () => {
  await cargarHabilidades()
})

async function cargarHabilidades() {
  await get(`/Habilidad/usuario/${usuarioAdministradorId}`)
  if (fetchSuccess.value) {

    habilidades.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
}

//TODO:: modificar para que muestre si una habilidad es actual

function handleEdit(item) {

  console.log(item);
}


// Composable DELETE
const { remove,  error: deleteError } = useDelete()

// Abre el modal con el ítem seleccionado
function confirmDelete(item) {
  habilidadToDelete.value = item
  showDeleteModal.value = true
}

// Cierra el modal sin borrar
function cancelDelete() {
  showDeleteModal.value = false
  habilidadToDelete.value = null
}

// Ejecuta el DELETE y recarga la lista
async function doDelete() {
  try {
    await remove(
      `/Habilidad/${habilidadToDelete.value.id}/usuario/${usuarioAdministradorId}`
    )
    habilidades.value = habilidades.value.filter(
      c => c.id !== habilidadToDelete.value.id
    )
    alertMessage.value = 'Habilidad eliminada correctamente.'
    isSuccess.value = true
    showAlert.value = true
  } catch (e) {
    alertMessage.value = deleteError.value || 'Error al eliminar la habilidad.'
    isSuccess.value = false
    showAlert.value = true
  } finally {
    cancelDelete()
  }
}

async function onSkillCreated(response) {


  if(response.exitoso === false) {
    alertMessage.value = response.mensaje || 'Error al crear la habilidad'
    showAlert.value = true
    isSuccess.value = false
    return
  }
  alertMessage.value = response.mensaje || 'Habilidad creada exitosamente'
  showAlert.value = true
  isSuccess.value = true
  await cargarHabilidades()
}
</script>
