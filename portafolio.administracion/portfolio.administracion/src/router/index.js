import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import PerfilView from '@/views/panel/PerfilView.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [

    {
      path: '/',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/panel',
      name: 'panel',
      component: () => import('../views/PanelView.vue'),
      meta: { requiresAuth: true },
      redirect: { name: 'perfil' },
      children:[
  {
          path: 'perfil',
          name: 'perfil',
          component: PerfilView,
        },
        {
          path: 'proyectos',
          name: 'proyectos',
          component: () => import('../views/panel/ProyectosView.vue'),
          meta: { requiresAuth: true },

        },

        {
          path: 'proyectos/:projectId',
          name: 'detalle-proyecto',
          component: () => import('../views/DetalleProyecto.vue'),
          meta: { requiresAuth: true },
        },
        {
          path: 'conocimientos',
          name: 'conocimientos',
          component: () => import('../views/panel/ConocimientosView.vue'),
          meta: { requiresAuth: true },
        },
        {
          path: 'educaciones',
          name: 'educaciones',
          component: () => import('../views/panel/EducacionesView.vue'),
          meta: { requiresAuth: true },
        },
        {
          path: 'empleos',
          name: 'empleos',
          component: () => import('../views/panel/EmpleosView.vue'),
          meta: { requiresAuth: true },
        },
        {
          path: 'habilidades',
          name: 'habilidades',
          component: () => import('../views/panel/HabilidadesView.vue'),
          meta: { requiresAuth: true },
        },
        {
          path: 'redes-sociales-contacto',
          name: 'redes-sociales-contacto',
          component: () => import('../views/panel/RedesSocialesContactoView.vue'),
          meta: { requiresAuth: true },
        }
      ]
    }
  ],
})

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')

  if (to.meta.requiresAuth && !token) {

    return next({ name: 'login' })
  }
  next()
})

export default router
