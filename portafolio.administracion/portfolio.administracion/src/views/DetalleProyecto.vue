<script setup>
import { useRoute } from 'vue-router';

const route = useRoute()
const projectId = route.params.projectId

</script>
<template>
  <h1>{{ projectId }}</h1>
</template>
