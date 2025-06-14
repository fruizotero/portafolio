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
    }" v-on:edit="handleEdit" v-on:delete="handleDelete" />

  <FormularioCrearEducacion ref="educForm" :usuarioAdministradorId="usuarioId" @created="onEducCreated">
  </FormularioCrearEducacion>
</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'
import FormularioCrearEducacion from '../formularios/FormularioCrearEducacion.vue'


const educacion = ref([])

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

function handleDelete(item) {
  // Lógica para eliminar la educación
  console.log('Eliminar educación:', item)
}
async function onEducCreated(response) {
  // refrescar lista, notificar, etc.
  console.log('Educación creada:', response)
  await cargarEducacion()
}
</script>
