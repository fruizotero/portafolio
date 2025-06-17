<template>
  <Teleport to="body">
    <div v-if="modelValue" class="modal modal-open">
      <div class="modal-box relative">
        <!-- Botón de cierre -->
        <button
          type="button"
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="close"
        >✕</button>

        <!-- Header slot opcional -->
        <header v-if="$slots.header" class="mb-4">
          <slot name="header" />
        </header>

        <!-- Body slot principal -->
        <section class="mb-4">
          <slot />
        </section>

        <!-- Footer slot opcional -->
        <footer v-if="$slots.footer" class="mt-4 text-right">
          <slot name="footer" />
        </footer>
      </div>
    </div>
  </Teleport>
</template>

<script setup>
import { defineProps, defineEmits, defineExpose } from 'vue'

const props = defineProps({
  modelValue: { type: Boolean, default: false }
})
const emit = defineEmits(['update:modelValue'])

// Cierra el modal
function close() {
  emit('update:modelValue', false)
}

// Exponemos métodos open/close al padre si hace falta
defineExpose({
  open() {
    emit('update:modelValue', true)
  },
  close
})
</script>

<style scoped>
/* Añadir separación arriba/abajo en el modal-box */
.modal-box.relative {
 scale: 0.95;
}
</style>
