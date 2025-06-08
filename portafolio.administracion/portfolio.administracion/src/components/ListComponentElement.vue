<script setup>
import { defineProps } from 'vue'
import { RouterLink } from 'vue-router'

/**
 * @prop {Object} item           — objeto con los datos
 * @prop {Object} fields         — { id, image, title, subtitle, description }
 * @prop {String} routeName      — nombre de la ruta de detalle
 * @prop {String} idParam        — nombre del parámetro en la ruta (por defecto 'id')
 */
const props = defineProps({
  item:       { type: Object, required: true },
  fields:     { type: Object, required: true },
  routeName:  { type: String, required: true },
  idParam:    { type: String, default: 'id' },
})

</script>

<template>
  <RouterLink
    :to="{ name: routeName, params: { [idParam]: item[fields.id] } }"
    class="list-row flex items-start p-4 hover:bg-base-200 transition"
  >
    <!-- Imagen -->
    <div class="flex-shrink-0">
      <img
        v-if="item[fields.image]"
        :src="item[fields.image]"
        alt=""
        class="w-10 h-10 rounded-full object-cover"
      />
      <div
        v-else
        class="w-10 h-10 rounded-full bg-base-200 flex items-center justify-center text-xs text-base-content/50"
      >
        N/A
      </div>
    </div>

    <!-- Contenido textual -->
    <div class="ml-4 flex-1">
      <div class="font-medium">
        {{ item[fields.title] }}
      </div>
      <div v-if="fields.subtitle" class="text-xs uppercase font-semibold opacity-60">
        {{ item[fields.subtitle] }}
      </div>
      <p v-if="fields.description" class="mt-2 text-xs list-col-wrap">
        {{ item[fields.description] }}
      </p>
    </div>
  </RouterLink>
</template>
