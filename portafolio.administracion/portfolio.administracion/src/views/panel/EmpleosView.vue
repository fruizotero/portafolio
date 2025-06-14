<template>
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

// Componente para mostrar la lista de proyectos
const empleos = ref([])
const showDeleteModal = ref(false)
const empleoToDelete = ref(null)
const usuarioId = Number(localStorage.getItem('usuarioId'))
const empForm = ref(null)

let usuarioAdministradorId = localStorage.getItem('usuarioId')

const { data, isLoading, get, isSuccess } = useGet()

onMounted(async () => {
  await caragarEmpleos()
})

async function caragarEmpleos() {
  await get(`/Empleo/usuario/${usuarioAdministradorId}`)
  if (isSuccess.value) {

    empleos.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
}

function handleEdit(item) {

  console.log(item);
}

// Composable DELETE
const { remove, isLoading: deleting, error: deleteError } = useDelete()

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
  } catch (e) {
    console.error('Error borrando empleo:', deleteError.value)
  } finally {
    cancelDelete()
  }
}


async function onCreated(response) {
  // actualizar lista de empleos…
  await caragarEmpleos()
  // o notificar al usuario
  console.log('Empleo creado:', response)
}
</script>
