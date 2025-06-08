<script setup>
import { defineProps, computed } from 'vue'
import { RouterLink } from 'vue-router'

/**
 * @prop {Object} item    — objeto con los datos
 * @prop {Object} fields  — configuración de campos:
 *   {
 *     id: string,
 *     title: string,
 *     subtitle?: string,
 *     description?: string,
 *     image?: string,
 *     fechainicio?: string,
 *     fechafin?: string,
 *     enableLink?: boolean,    // por defecto true
 *     routeName?: string,      // nombre de la ruta
 *     idParam?: string         // nombre del parámetro en la ruta (por defecto 'id')
 *   }
 */
const props = defineProps({
  item:   { type: Object, required: true },
  fields: { type: Object, required: true },
})

// Determina si debe envolverse en RouterLink
const enableLink = computed(() => props.fields.enableLink !== false && !!props.fields.routeName)

// Construye la ruta si corresponde
const linkTo = computed(() => {
  if (!enableLink.value) return null
  const paramKey = props.fields.idParam || 'id'
  return { name: props.fields.routeName, params: { [paramKey]: props.item[props.fields.id] } }
})

// Formatea fechas
function formatDate(iso) {
  if (!iso) return ''
  return new Date(iso).toLocaleDateString(undefined, { year: 'numeric', month: 'short', day: 'numeric' })
}
</script>

<template>
  <!-- Usa RouterLink si enableLink y linkTo, sino un div -->
  <component
    :is="enableLink ? RouterLink : 'div'"
    v-bind="enableLink ? { to: linkTo } : {}"
    class="relative list-row flex items-start p-4 hover:bg-base-200 transition cursor-pointer rounded-lg"
  >
    <!-- Fechas en esquina superior derecha -->
    <div v-if="fields.fechainicio || fields.fechafin" class="absolute top-2 right-2 text-xs opacity-60 text-right">
      <div v-if="fields.fechainicio">{{ formatDate(item[fields.fechainicio]) }}</div>
      <div v-if="fields.fechafin">{{ formatDate(item[fields.fechafin]) }}</div>
    </div>

    <!-- Imagen opcional -->
    <div v-if="fields.image && item[fields.image]" class="flex-shrink-0 mt-1">
      <img :src="item[fields.image]" alt="" class="w-10 h-10 rounded-full object-cover" />
    </div>

    <!-- Contenido textual -->
    <div class="ml-4 flex-1">
      <div class="font-medium">{{ item[fields.title] }}</div>
      <div v-if="fields.subtitle" class="text-xs uppercase font-semibold opacity-60">
        {{ item[fields.subtitle] }}
      </div>
      <p v-if="fields.description" class="mt-2 text-xs list-col-wrap">
        {{ item[fields.description] }}
      </p>
    </div>
  </component>
</template>
