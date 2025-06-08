<template>
  <ListComponent
    title="Proyectos"
    :items="proyectos"
    :fields="{
      id: 'id',
      image: 'imagenDesktopUrl',
      title: 'titulo',
      subtitle: 'titulo',
      description: 'descripcion'
    }"
    route-name="detalle-proyecto"
    id-param="projectId"
  />
</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'

const proyectos = ref([])

let usuarioAdministradorId = localStorage.getItem('usuarioId')

const { data, isLoading, get, isSuccess } = useGet()

onMounted(async () => {
  await get(`/proyecto/${usuarioAdministradorId}`)
  if (isSuccess.value) {

    proyectos.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
})
</script>
