<template>
  <div class="card bg-base-100 shadow-lg rounded-lg">
    <div class="card-body p-4">

      <!-- Tabs -->
      <div class="tabs tabs-boxed mb-3 text-sm">
        <a class="tab flex-1" :class="{ 'tab-active': selectedTab === 'editor' }"
          @click="selectedTab = 'editor'">Editor</a>
        <a class="tab flex-1" :class="{ 'tab-active': selectedTab === 'html' }" @click="selectedTab = 'html'">HTML</a>
      </div>

      <!-- Editor -->
      <div v-show="selectedTab === 'editor'"
        class="tiptap border border-base-300 rounded-lg bg-base-50 overflow-y-auto p-2" :style="{ height }">
        <EditorContent :editor="editor" />
      </div>

      <div v-show="selectedTab === 'html'"
        class="border border-base-300 rounded-lg bg-base-50 overflow-y-auto p-2 font-mono text-xs whitespace-pre-wrap"
        :style="{ height }">{{ html }}</div>

    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount, watch } from 'vue'
import { Editor, EditorContent } from '@tiptap/vue-3'
import StarterKit from '@tiptap/starter-kit'

// Props: modelValue para v-model y height para la altura del área
const props = defineProps({
  modelValue: { type: String, default: '' },
  height:     { type: String, default: '16rem' }  // valor CSS, p.ej. '16rem', '200px', '50vh'
})
const emit = defineEmits(['update:modelValue'])

const selectedTab = ref('editor')
const html = ref(props.modelValue)
let editor = null

onMounted(() => {
  editor = new Editor({
    extensions: [StarterKit],
    content: props.modelValue,
    onUpdate: ({ editor: ed }) => {
      const updated = ed.getHTML()
      html.value = updated
      emit('update:modelValue', updated)
    }
  })
})


watch(
  () => props.modelValue,
  (value) => {
    if (editor && editor.getHTML() !== value) {
      editor.commands.setContent(value, false)
    }
    html.value = value
  }
)

onBeforeUnmount(() => {
  if (editor) editor.destroy()
})
</script>

<style scoped>
/* Ajuste de la tipografía interna del editor */
.tiptap :global(.ProseMirror) {
  font-size: 0.875rem; /* text-sm */
  line-height: 1.4;
}
</style>
