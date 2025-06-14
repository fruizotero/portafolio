<template>
  <button class="btn" @click="form.open()">Nueva Red Social</button>
  <ListComponent
    title="Redes Sociales y Contacto"
    :items="redesSocialesYContacto"
    :fields="{
      id: 'id',
      title: 'plataforma',
      image: 'iconUrl',
      description: 'url',
      enableLink: false,
    }"
    :onEdit="handleEdit"
    :onDelete="handleDelete"
  />
  <FormularioCrearRedSocialContacto  ref="form"
    :usuarioAdministradorId="usuarioId"
    @created="onCreated" />
</template>

<script setup>
import ListComponent from '@/components/ListComponent.vue'
import { useGet } from '@/comporsables/useGet'
import { ref, onMounted } from 'vue'
import FormularioCrearRedSocialContacto from '../formularios/FormularioCrearRedSocialContacto.vue'
// Componente para mostrar la lista de proyectos
const redesSocialesYContacto = ref([])


let usuarioAdministradorId = localStorage.getItem('usuarioId')

const usuarioId = Number(localStorage.getItem('usuarioId'))
const form = ref(null)

const { data, isLoading, get, isSuccess } = useGet()

onMounted(async () => {
  await cargarRedesSocialesYContacto()
})

async function cargarRedesSocialesYContacto() {
  await get(`/RedSocialContacto/usuario/${usuarioAdministradorId}`)
  if (isSuccess.value) {

    redesSocialesYContacto.value = data.value.datos || []
  } else {
    console.error('Error al cargar los proyectos')
  }
}

//TODO:: modificar para que muestre que se refiere a la url de contacto

function handleEdit(item) {

  console.log(item);
}

function handleDelete(item) {
  // Lógica para eliminar el proyecto
  console.log('Eliminar proyecto:', item)
}

async function onCreated(response) {
  console.log('Red social creada:', response)
  // refresca tu listado…
  await cargarRedesSocialesYContacto()
}
</script>
