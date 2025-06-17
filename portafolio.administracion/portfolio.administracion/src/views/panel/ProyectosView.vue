<template>
  <AlertComponent
      v-model:visible="showAlert"
      :success="isSuccess"
      :message="alertMessage"
      :duration="5000"
      class="sticky top-4 right-4 z-50 ml-auto"
    />
  <button class="btn" @click="form.open()">Nuevo Proyecto</button>
  <ListComponent title="Proyectos" :items="proyectos" :fields="{
      id: 'id',
      title: 'titulo',
      description: 'descripcion',
      image: 'imagenDesktopUrl',
      enableLink: true,
      routeName: 'detalle-proyecto',
      idParam: 'projectId'
    }" :onEdit="handleEdit" :onDelete="confirmDelete" />


  <FormularioCrearProyecto ref="form" :usuarioAdministradorId="usuarioId" @created="onProjectCreated">
  </FormularioCrearProyecto>

<!-- Modal de confirmación -->
    <ModalBaseComponent v-model="showDeleteModal">
      <template #header>
        <h3 class="text-lg font-bold text-red-600">Confirmar Eliminación</h3>
      </template>

      <p>¿Estás seguro de que quieres eliminar “{{ proyectoToDelete?.titulo }}”?</p>

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
import { useRouter } from 'vue-router'
import FormularioCrearProyecto from '../formularios/FormularioCrearProyecto.vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { useDelete } from '@/comporsables/useDelete'
import AlertComponent from '@/components/AlertComponent.vue'

// Componente para mostrar la lista de proyectos
const proyectos = ref([])
const router = useRouter()

const form = ref(null)
const usuarioId = Number(localStorage.getItem('usuarioId'))

const showDeleteModal = ref(false)
const proyectoToDelete = ref(null)
let usuarioAdministradorId = localStorage.getItem('usuarioId')
const showAlert = ref(false)
const alertMessage = ref('')
const isSuccess = ref(false)

const { data,  get, isSuccess: fetchSuccess } = useGet()
const { remove,  error: deleteError } = useDelete()


onMounted(async () => {
  await cargarProyectos()
})

async function cargarProyectos() {
  await get(`/proyecto/usuario/${usuarioAdministradorId}`)
  if (fetchSuccess.value) {

    proyectos.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
}

function handleEdit(item) {
  // Lógica para editar el proyecto

  // Aquí podrías redirigir a una página de edición o abrir un modal
router.push({
    name: 'detalle-proyecto',
    params: { projectId: item.id }
  })
}

function confirmDelete(item) {
  proyectoToDelete.value = item
  showDeleteModal.value = true
}

// El usuario cancela
function cancelDelete() {
  proyectoToDelete.value = null
  showDeleteModal.value = false
}

async function doDelete() {
  try {
    await remove(`/Proyecto/${proyectoToDelete.value.id}/usuario/${usuarioAdministradorId}`)
    proyectos.value = proyectos.value.filter(p => p.id !== proyectoToDelete.value.id)
    showAlert.value = true
    alertMessage.value = `Proyecto "${proyectoToDelete.value.titulo}" eliminado correctamente.`
    isSuccess.value = true
  } catch (e) {
    alertMessage.value = deleteError.value || 'Error al eliminar el proyecto.'
    isSuccess.value = false
    showAlert.value = true
  } finally {
    cancelDelete()
  }
}

async function  onProjectCreated(response) {

  if(response.exitoso === false) {
    alertMessage.value = response.mensaje || 'Error al crear el proyecto'
    showAlert.value = true
    isSuccess.value = false
    return
  }
  alertMessage.value = response.mensaje || 'Proyecto creado exitosamente'
  showAlert.value = true
  isSuccess.value = true

  await cargarProyectos()


}
</script>
