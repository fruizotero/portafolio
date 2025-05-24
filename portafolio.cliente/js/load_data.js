const d = document;


export const loadData = async (containerClass, path) => {

    try {

        let resp = await fetch(path);
        let json = await resp.json();

        if (!resp.ok) throw { status: resp.status, statusText: resp.statusText }

        return json;

    } catch (error) {
        let message = error.statusText || "Ocurrió un error, no se pudo cargar el contenido";

        d.querySelector(containerClass).innerHTML = `<p class="error-message">${message}</p>`;
    }


}