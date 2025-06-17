<script setup>
import { PencilIcon, TrashIcon } from '@heroicons/vue/24/solid'


/**
 * @prop {Object} item          — objeto con los datos
 * @prop {Object} fields        — configuración de campos:
 *   {
 *     id: string,
 *     title: string,
 *     subtitle?: string,
 *     description?: string,
 *     image?: string,
 *     fechainicio?: string,
 *     fechafin?: string
 *   }
 * @prop {Function} onEdit     — función callback para editar (recibe item)
 * @prop {Function} onDelete   — función callback para eliminar (recibe item)
 */
const props = defineProps({
  item:      { type: Object,   required: true },
  fields:    { type: Object,   required: true },
  onEdit:    { type: Function, required: true },
  onDelete:  { type: Function, required: true }
})

// Formatea fechas ISO a locale
function formatDate(iso) {
  if (!iso) return ''
  return new Date(iso).toLocaleDateString(undefined, { year: 'numeric', month: 'short', day: 'numeric' })
}
</script>

<template>
  <div class="relative list-row flex items-start p-4 bg-base-100 rounded-lg shadow-sm">
    <!-- Imagen opcional -->
    <div v-if="fields.image && item[fields.image]" class="flex-shrink-0 mt-1">
      <img :src="item[fields.image]" alt="" class="w-10 h-10 rounded-full object-cover" />
    </div>

    <!-- Contenido textual -->
    <div class="ml-4 flex-1">
      <div class="font-medium text-base">{{ item[fields.title] }}</div>
      <!-- Subtítulo y fechas en línea -->
      <div v-if="fields.subtitle || fields.fechainicio || fields.fechafin"
           class="text-xs uppercase font-semibold opacity-60 flex flex-wrap items-center space-x-2">
        <span v-if="fields.subtitle">{{ item[fields.subtitle] }}</span>
      </div>
      <span v-if="fields.fechainicio || fields.fechafin" class="text-xs text-gray-500 uppercase font-semibold">
        desde {{ formatDate(item[fields.fechainicio]) }} hasta {{ formatDate(item[fields.fechafin]) }}
      </span>
      <p v-if="fields.description" class="mt-2 text-sm text-gray-600">
        {{ item[fields.description] }}
      </p>
    </div>

    <!-- Botones Editar y Eliminar -->
   <div class="absolute top-2 right-2 flex space-x-2">
     <div class="tooltip" data-tip="Editar">
        <button
          type="button"
          class="btn btn-sm btn-outline btn-primary flex items-center space-x-1"
          @click="onEdit(item)"
        >
          <PencilIcon class="h-4 w-4 red-500" />

        </button>

      </div>
      <div class="tooltip" data-tip="Eliminar">

        <button
          type="button"
          class="btn btn-sm btn-outline btn-error flex items-center space-x-1"
          @click="onDelete(item)"
        >
          <TrashIcon class="h-4 w-4" />

        </button>
      </div>
    </div>
  </div>
</template>
