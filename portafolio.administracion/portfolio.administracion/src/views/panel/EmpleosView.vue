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
    :onDelete="handleDelete"
  />
  <FormularioCrearEmpleo ref="empForm"
    :usuarioAdministradorId="usuarioId"
    @created="onCreated"></FormularioCrearEmpleo>

</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'
import FormularioCrearEmpleo from '../formularios/FormularioCrearEmpleo.vue'

// Componente para mostrar la lista de proyectos
const empleos = ref([])

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

function handleDelete(item) {
  // Lógica para eliminar el proyecto
  console.log('Eliminar proyecto:', item)
}


async function onCreated(response) {
  // actualizar lista de empleos…
  await caragarEmpleos()
  // o notificar al usuario
  console.log('Empleo creado:', response)
}
</script>
