<template>
  <AlertComponent
   v-model:visible="showAlert"
    :success="isSuccess"
    :message="alertMessage"
    :duration="5000"
  />
  <div class="min-h-screen bg-base-200 flex items-center justify-center px-4">
    <div class="w-full max-w-md">
      <div class="card shadow-lg bg-base-100">
        <div class="card-body">
          <h2 class="card-title justify-center text-2xl font-bold mb-4">Iniciar sesión</h2>
          <form @submit.prevent="handleLogin" class="space-y-4">
            <div>
              <label class="label">
                <span class="label-text">Correo electrónico</span>
              </label>
              <input
                type="email"
                v-model="email"
                placeholder="usuario@ejemplo.com"
                class="input input-bordered w-full"
                required
              />
            </div>
            <div>
              <label class="label">
                <span class="label-text">Contraseña</span>
              </label>
              <input
                type="password"
                v-model="password"
                placeholder="********"
                class="input input-bordered w-full"
                required
              />
            </div>
            <div>
              <button class="btn btn-primary w-full" type="submit">
                <LoadingComponent v-if="isLoading" />
                <span v-else>Ingresar</span>

              </button>
            </div>
          </form>

        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useAuth } from '@/comporsables/useAuth'
import { ref } from 'vue'
import AlertComponent from './AlertComponent.vue'
import LoadingComponent from './LoadingComponent.vue'
import { useRouter } from 'vue-router'


const { login, error, isLoading } = useAuth()
const email = ref('admin@portafolio.com')
const password = ref('1234')
const isSuccess = ref(true)
const alertMessage = ref('')
const showAlert = ref(false)
const router     = useRouter()

const handleLogin = async () => {
  await login(email.value, password.value)
  if (!error.value) {
    // Redireccionar a dashboard o mostrar éxito
    alertMessage.value = 'Inicio de sesión exitoso'
    isSuccess.value = true
    showAlert.value = true
    // Aquí podrías redirigir al usuario a otra página, por ejemplo:
   router.push({ name: 'panel' })
  } else{
    alertMessage.value = error.value
    isSuccess.value = false
    showAlert.value = true
  }
}
</script>
