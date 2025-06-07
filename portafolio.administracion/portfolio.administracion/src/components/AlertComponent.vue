<!-- src/components/Alert.vue -->
<template>
  <div
    v-if="isVisible"
    :class="['alert', success ? 'alert-success' : 'alert-error', 'sticky top-4 z-50']"
    role="alert"
  >
    <svg
      xmlns="http://www.w3.org/2000/svg"
      class="h-6 w-6 shrink-0 stroke-current"
      fill="none"
      viewBox="0 0 24 24"
    >
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        stroke-width="2"
        d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"
      />
    </svg>
    <span>{{ message }}</span>
  </div>
</template>

<script setup>
import { ref, watch, onBeforeUnmount } from 'vue'

const props = defineProps({
  success:  { type: Boolean, default: true },
  message:  { type: String,  default: 'Operación realizada con éxito' },
  duration: { type: Number,  default: 3000 },
  visible:  { type: Boolean, default: false }
})
const emit = defineEmits(['update:visible'])

const isVisible = ref(false)
let timer = null

function startTimer() {
  clearTimeout(timer)
  isVisible.value = true
  emit('update:visible', true)
  timer = setTimeout(() => {
    isVisible.value = false
    emit('update:visible', false)
  }, props.duration)
}

// Cuando la prop `visible` cambia:
watch(() => props.visible, val => {
  if (val) {
    startTimer()
  } else {
    clearTimeout(timer)
    isVisible.value = false
  }
})

onBeforeUnmount(() => clearTimeout(timer))
</script>

<style scoped>
.alert {
  transition: opacity 0.3s ease;
}
</style>
