 const URL_BASE = 'https://localhost:7161';
const usuarioId = "1";
// Endpoints
export const URL_CONOCIMIENTOS = `${URL_BASE}/conocimientos/usuario/${usuarioId}`;
export const URL_EDUCACION = `${URL_BASE}/educacion/usuario/${usuarioId}`;
export const URL_EMPLEOS = `${URL_BASE}/empleo/usuario/${usuarioId}`;
export const URL_HABILIDADES = `${URL_BASE}/habilidad/usuario/${usuarioId}`;
export const URL_PERFIL =`${URL_BASE}/perfil/${usuarioId}`;
export const URL_PROYECTOS = `${URL_BASE}/proyecto/usuario/${usuarioId}`;
export const URL_REDSOCIALESCONTACTO = `${URL_BASE}/redsocialcontacto/usuario/${usuarioId}`;

// funciones para obtener los datos de los endpoints
export async function obtenerConocimientos() {
    const response = await fetch(URL_CONOCIMIENTOS);
    return response.json();
}

export async function obtenerEducacion() {
    const response = await fetch(URL_EDUCACION);
    return response.json();
}

export async function obtenerEmpleos() {
    const response = await fetch(URL_EMPLEOS);
    return response.json();
}

export async function obtenerHabilidades() {
    const response = await fetch(URL_HABILIDADES);
    return response.json();
}

export async function obtenerPerfil() {
    const response = await fetch(URL_PERFIL);
    return response.json();
}

export async function obtenerProyectos() {
    const response = await fetch(URL_PROYECTOS);
    return response.json();
}

export async function obtenerRedesSocialesContacto() {
    const response = await fetch(URL_REDSOCIALESCONTACTO);
    return response.json();
}