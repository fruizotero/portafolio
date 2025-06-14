<template>
  <button class="btn" @click="educForm.open()">Nueva Educación</button>
  <ListComponent title="Educación" :items="educacion" :fields="{
      id: 'id',
      image: 'imagenDesktopUrl',
      title: 'titulo',
      subtitle: 'institucion',
      description: 'descripcion',
      fechainicio: 'fechaInicio',
      fechafin: 'fechaFin',
      enableLink: false,
    }" v-on:edit="handleEdit" v-on:delete="confirmDelete" />

  <FormularioCrearEducacion ref="educForm" :usuarioAdministradorId="usuarioId" @created="onEducCreated">
  </FormularioCrearEducacion>

   <ModalBaseComponent v-model="showDeleteModal">
      <template #header>
        <h3 class="text-lg font-bold text-red-600">Confirmar Eliminación</h3>
      </template>

      <p>¿Estás seguro de que quieres eliminar “{{ educacionToDelete?.titulo }}”?</p>

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
import FormularioCrearEducacion from '../formularios/FormularioCrearEducacion.vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { useDelete } from '@/comporsables/useDelete'


const educacion = ref([])

const showDeleteModal = ref(false)
const educacionToDelete = ref(null)
let usuarioAdministradorId = localStorage.getItem('usuarioId')

const { data, isLoading, get, isSuccess } = useGet()
const usuarioId = Number(localStorage.getItem('usuarioId'))
const educForm = ref(null)

onMounted(async () => {
  await cargarEducacion()
})
async function cargarEducacion() {
  await get(`/Educacion/usuario/${usuarioAdministradorId}`)
  if (isSuccess.value) {

    educacion.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
}

function handleEdit(item) {
  // Lógica para editar la educación
  console.log('Editar educación:', item)
}

// Composable DELETE
const { remove, isLoading: deleting, error: deleteError } = useDelete()

// Abre el modal con el ítem seleccionado
function confirmDelete(item) {
  educacionToDelete.value = item
  showDeleteModal.value = true
}

// Cierra el modal sin borrar
function cancelDelete() {
  showDeleteModal.value = false
  educacionToDelete.value = null
}

// Ejecuta el DELETE y recarga la lista
async function doDelete() {
  try {
    await remove(
      `/Educacion/${educacionToDelete.value.id}/usuario/${usuarioAdministradorId}`
    )
    educacion.value = educacion.value.filter(
      c => c.id !== educacionToDelete.value.id
    )
  } catch (e) {
    console.error('Error borrando educación:', deleteError.value)
  } finally {
    cancelDelete()
  }
}
async function onEducCreated(response) {
  // refrescar lista, notificar, etc.
  console.log('Educación creada:', response)
  await cargarEducacion()
}
</script>
