<template>
  <button class="btn" @click="form.open()">Nuevo Proyecto</button>
  <ListComponent title="Proyectos" :items="proyectos" :fields="{
      id: 'id',
      title: 'titulo',
      description: 'descripcion',
      image: 'imagenDesktopUrl',
      enableLink: true,
      routeName: 'detalle-proyecto',
      idParam: 'projectId'
    }" :onEdit="handleEdit" :onDelete="handleDelete" />


  <FormularioCrearProyecto ref="form" :usuarioAdministradorId="usuarioId" @created="onProjectCreated">
  </FormularioCrearProyecto>

</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import FormularioCrearProyecto from '../formularios/FormularioCrearProyecto.vue'
// Componente para mostrar la lista de proyectos
const proyectos = ref([])
const router = useRouter()

const form = ref(null)
const usuarioId = Number(localStorage.getItem('usuarioId'))


let usuarioAdministradorId = localStorage.getItem('usuarioId')

const { data, isLoading, get, isSuccess } = useGet()

onMounted(async () => {
  await cargarProyectos()
})

async function cargarProyectos() {
  await get(`/proyecto/usuario/${usuarioAdministradorId}`)
  if (isSuccess.value) {

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

function handleDelete(item) {
  // Lógica para eliminar el proyecto

  console.log('Eliminar proyecto:', item)
}

async function  onProjectCreated(data) {
  // actualizar lista, notificar, etc.
  console.log('Proyecto creado:', data)
await cargarProyectos()


}
</script>
