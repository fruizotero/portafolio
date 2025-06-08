<script setup>
import { defineProps } from 'vue'
import ListComponentElement from './ListComponentElement.vue';



/**
 * @prop {String}           title      — texto que aparece como cabecera de la lista
 * @prop {Array<Object>}    items      — array de objetos a renderizar
 * @prop {Object}           fields     — mapeo de propiedades:
 *                                      { id, image, title, subtitle, description }
 * @prop {String}           routeName  — nombre de la ruta de detalle (router)
 * @prop {String} [idParam] — nombre del param en la ruta (por defecto 'id')
 */

const props = defineProps({
  title:      { type: String,  required: true },
  items:      { type: Array,   required: true },
  fields:     { type: Object,  required: true },
})

</script>

<template>
  <ul class="list bg-base-100 rounded-box shadow-md divide-y">
    <!-- Cabecera -->
    <li class="p-4 pb-2 text-xs opacity-60 tracking-wide">
      {{ title }}
    </li>

    <!-- Elementos -->
    <li
      v-for="item in items"
      :key="item[fields.id]"
      class="p-0"
    >
      <ListComponentElement
        :item="item"
        :fields="fields"
      />
    </li>
  </ul>
</template>
