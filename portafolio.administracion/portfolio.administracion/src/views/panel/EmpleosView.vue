<template>
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
</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'
// Componente para mostrar la lista de proyectos
const empleos = ref([])

let usuarioAdministradorId = localStorage.getItem('usuarioId')

const { data, isLoading, get, isSuccess } = useGet()

onMounted(async () => {
  await get(`/Empleo/usuario/${usuarioAdministradorId}`)
  if (isSuccess.value) {

    empleos.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
})

function handleEdit(item) {

  console.log(item);
}

function handleDelete(item) {
  // Lógica para eliminar el proyecto
  console.log('Eliminar proyecto:', item)
}
</script>
