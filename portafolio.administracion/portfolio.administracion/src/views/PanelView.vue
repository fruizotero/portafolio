<script setup></script>

<template>
  <div class="drawer md:drawer-open">
    <input id="my-drawer-2" type="checkbox" class="drawer-toggle" />
    <section class="drawer-content p-8">
      <!-- Page content here -->
      <router-view />
      <label for="my-drawer-2" class="btn btn-primary drawer-button absolute  left-4 top-4 md:hidden">
        Open drawer
      </label>
    </section>
    <div class="drawer-side">
      <label for="my-drawer-2" aria-label="close sidebar" class="drawer-overlay"></label>
      <ul class="menu bg-base-200 text-base-content min-h-full w-80 p-4">
        <!-- Sidebar content here -->

        <li>
          <RouterLink to="/panel" class="menu-item">Perfil</RouterLink>
        </li>
        <li>
          <RouterLink to="/panel/proyectos" class="menu-item">Proyectos</RouterLink>
        </li>
        <li>
          <RouterLink to="/panel/educaciones" class="menu-item">Educación</RouterLink>
        </li>
        <li>
          <RouterLink to="/panel/conocimientos" class="menu-item">Conocimientos</RouterLink>
        </li>
        <li>
          <RouterLink to="/panel/empleos" class="menu-item">Empleos</RouterLink>
        </li>
        <li>
          <RouterLink to="/panel/habilidades" class="menu-item">Habilidades</RouterLink>
        </li>
        <li>
          <RouterLink to="/panel/redes-sociales-contacto" class="menu-item">Redes Sociales y Contacto</RouterLink>
        </li>
      </ul>
    </div>
  </div>
</template>
