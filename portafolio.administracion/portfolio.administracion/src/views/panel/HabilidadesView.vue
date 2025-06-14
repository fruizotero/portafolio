<template>
  <button class="btn" @click="skillForm.open()">Nueva Habilidad</button>
  <ListComponent title="Habilidades" :items="habilidades" :fields="{
      id: 'id',
      title: 'nombre',
      image: 'logoUrl',
      enableLink: false,
    }" :onEdit="handleEdit" :onDelete="handleDelete" />
  <FormularioCrearHabilidad ref="skillForm" :usuarioAdministradorId="usuarioId" @created="onSkillCreated">
  </FormularioCrearHabilidad>

</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'
import FormularioCrearHabilidad from '../formularios/FormularioCrearHabilidad.vue'

// Componente para mostrar la lista de proyectos
const habilidades = ref([])

let usuarioAdministradorId = localStorage.getItem('usuarioId')

const usuarioId = Number(localStorage.getItem('usuarioId'))
const skillForm = ref(null)

const { data, isLoading, get, isSuccess } = useGet()

onMounted(async () => {
  await cargarHabilidades()
})

async function cargarHabilidades() {
  await get(`/Habilidad/usuario/${usuarioAdministradorId}`)
  if (isSuccess.value) {

    habilidades.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
}

//TODO:: modificar para que muestre si una habilidad es actual

function handleEdit(item) {

  console.log(item);
}

function handleDelete(item) {
  // Lógica para eliminar el proyecto
  console.log('Eliminar proyecto:', item)
}

async function onSkillCreated(response) {
  console.log('Habilidad creada:', response)
  // refresca lista de habilidades…
  await cargarHabilidades()
}
</script>
