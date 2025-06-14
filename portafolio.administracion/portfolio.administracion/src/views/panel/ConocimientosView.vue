<template>
  <button class="btn" @click="form.open()">Nuevo Conocimiento</button>
  <ListComponent title="Conocimientos" :items="conocimientos" :fields="{
      id: 'id',
      image: '',
      title: 'nombre',
      subtitle: '',
      description: '',
      enableLink: false,
    }" v-on:edit="handleEdit" v-on:delete="handleDelete" />

  <FormularioCrearConocimiento ref="form" :usuarioAdministradorId="usuarioId" @created="onCreated">
  </FormularioCrearConocimiento>
</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'
import FormularioCrearConocimiento from '../formularios/FormularioCrearConocimiento.vue'


const conocimientos = ref([])

let usuarioAdministradorId = localStorage.getItem('usuarioId')

const { data, isLoading, get, isSuccess } = useGet()

const usuarioId = Number(localStorage.getItem('usuarioId'))
const form = ref(null)

onMounted(async () => {
  await cargarConocimientos()
})
async function cargarConocimientos() {
  await get(`/Conocimiento/usuario/${usuarioAdministradorId}`)
  if (isSuccess.value) {

    conocimientos.value = data.value.datos || []
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
async function onCreated(response) {
  console.log('Conocimiento creado:', response)
  await cargarConocimientos()
}
</script>
