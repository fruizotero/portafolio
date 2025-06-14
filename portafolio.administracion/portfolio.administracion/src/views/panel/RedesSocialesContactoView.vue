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
    :onDelete="confirmDelete"
  />
  <FormularioCrearRedSocialContacto  ref="form"
    :usuarioAdministradorId="usuarioId"
    @created="onCreated" />

    <ModalBaseComponent v-model="showDeleteModal">
      <template #header>
        <h3 class="text-lg font-bold text-red-600">Confirmar Eliminación</h3>
      </template>

      <p>¿Estás seguro de que quieres eliminar “{{ redSocialToDelete?.plataforma }}”?</p>

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
import FormularioCrearRedSocialContacto from '../formularios/FormularioCrearRedSocialContacto.vue'
import ModalBaseComponent from '@/components/ModalBaseComponent.vue'
import { useDelete } from '@/comporsables/useDelete'


// Componente para mostrar la lista de proyectos
const redesSocialesYContacto = ref([])
const showDeleteModal = ref(false)
const redSocialToDelete = ref(null)

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


// Composable DELETE
const { remove, isLoading: deleting, error: deleteError } = useDelete()

// Abre el modal con el ítem seleccionado
function confirmDelete(item) {
  redSocialToDelete.value = item
  showDeleteModal.value = true
}

// Cierra el modal sin borrar
function cancelDelete() {
  showDeleteModal.value = false
  redSocialToDelete.value = null
}

// Ejecuta el DELETE y recarga la lista
async function doDelete() {
  try {
    await remove(
      `/RedSocialContacto/${redSocialToDelete.value.id}/usuario/${usuarioAdministradorId}`
    )
    redesSocialesYContacto.value = redesSocialesYContacto.value.filter(
      c => c.id !== redSocialToDelete.value.id
    )
  } catch (e) {
    console.error('Error borrando red social:', deleteError.value)
  } finally {
    cancelDelete()
  }
}

async function onCreated(response) {
  console.log('Red social creada:', response)
  // refresca tu listado…
  await cargarRedesSocialesYContacto()
}
</script>
