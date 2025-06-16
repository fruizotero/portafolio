import { renderSkills } from "./render_skills.js";
import { renderProjects } from "./render_projects.js";
import { obtenerEducacion, obtenerEmpleos, obtenerHabilidades, obtenerPerfil, obtenerProyectos, obtenerRedesSocialesContacto } from "./endpoints.js";

const d = document;

d.addEventListener("DOMContentLoaded", async e => {

    await init();
   
   

});

d.addEventListener("click", e => {

    if (e.target.matches(".button-menu")) {
        d.querySelector(".nav").classList.add("menu-visible");
    }

    if (e.target.matches(".button-close")) {
        d.querySelector(".nav").classList.remove("menu-visible");
    }

    if (e.target.matches(".nav__link") ||
        e.target.matches(".nav__link *")) {
        d.querySelector(".nav").classList.remove("menu-visible");
    }

});

d.addEventListener("mouseover", e => {
    // console.log(e.target);
    if (e.target.matches(".header") ||
        e.target.matches(".header *")) {

        d.querySelectorAll(".nav__text").forEach(el => {
            el.classList.add("text-visible");
        })
    }

});

d.addEventListener("mouseout", e => {
    if (e.target.matches(".header") ||
        e.target.matches(".header *")) {

        d.querySelectorAll(".nav__text").forEach(el => {
            el.classList.remove("text-visible");
        })
    }
});

d.addEventListener("submit", async e => {

    if (e.target.matches(".form-contact")) {

        e.preventDefault();
        d.querySelector(".loader").classList.remove("none");

        try {
            let form = new FormData(e.target);
            const obj = {};
            for (let [key, value] of form.entries()) {
                obj[key] = value;
            }

            let resp = await fetch("https://formsubmit.co/ajax/frankid74@gmail.com", {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(obj)
            });

            let json = await resp.json();

            // console.log(json);

            if (!json["success"]) throw { status: resp.status, statusText: resp.statusText }

            d.querySelector(".loader").classList.add("none");
            d.querySelector(".message-form").textContent = "Tu mensaje ha sido enviado";

            if (json["success"]) {
                e.target.reset();
            }

        } catch (error) {

            let message = error.statusText || "Ocurrió un error en el envío";
            d.querySelector(".loader").classList.add("none");
            d.querySelector(".message-form").textContent = message


        }


    }

})

async function renderizarPerfil(){
    const { datos } = await obtenerPerfil();
    const { nombre, apellidos, saludo, descripcion, acercaDeMi, fotoURL } = datos
    d.querySelector(".section__salute").innerHTML = saludo;
    const paragraphsContainerSection1 = document.querySelector('.section-1__paragraphs');
    paragraphsContainerSection1.innerHTML = addParagraphClass(descripcion);
    const paragraphsContainerSection2 = document.querySelector('.section-2__paragraphs');
    paragraphsContainerSection2.innerHTML = addParagraphClass(acercaDeMi);
}

function addParagraphClass(htmlString) {
    const parser = new DOMParser();
    const doc = parser.parseFromString(htmlString, 'text/html');

    doc.body.querySelectorAll('p').forEach(p => {
        p.classList.add('section__paragraph');
    });

    return doc.body.innerHTML;
}

async function renderizarEmpleos(empleos=[]){
    
    const sectionListaEmpleos = d.querySelector(".jobs__list");
    empleos.forEach(empleo => {
        const li = d.createElement("li");
        li.classList.add("jobs__item");
        li.innerText = `${empleo.empresa} - ${empleo.cargo} (${empleo.fechaInicio} - ${empleo.fechaFin})`;
        sectionListaEmpleos.appendChild(li);
    });
}

async function renderizarEducacion(educacion=[]){
    const sectionListaEducacion = d.querySelector(".education__list");
    educacion.forEach(edu => {
        const li = d.createElement("li");
        li.classList.add("education__item");
        li.innerText = `${edu.titulo} - ${edu.institucion} (${edu.fechaInicio} - ${edu.fechaFin})`;
        sectionListaEducacion.appendChild(li);
    });
}

async function renderizarRedesSocialesContacto(redesSociales = []) {
    const sectionRedeesSociales = d.querySelector(".footer__left");
    redesSociales.forEach(red => {
       const a = document.createElement('a');
    a.href = red.url;
    a.target = '_blank';
    a.className = 'footer__link';

    // <div class="icon">
    const iconDiv = document.createElement('div');
    iconDiv.className = 'icon';

    // <img>
    const img = document.createElement('img');
    img.src = red.iconUrl;
    img.alt = red.plataforma;
    img.className = 'icon__image';

    // Anidamos y añadimos al DOM
    iconDiv.appendChild(img);
    a.appendChild(iconDiv);
    sectionRedeesSociales.appendChild(a);
    });


}

async function init() {
    await renderizarPerfil();
    const { datos: habilidades } = await obtenerHabilidades();
    renderSkills(habilidades);
    const { datos: proyectos } = await obtenerProyectos();
    renderProjects(proyectos);
     const {datos:empleos} = await obtenerEmpleos();
     renderizarEmpleos(empleos);
     const {datos:educacion} = await obtenerEducacion();
     renderizarEducacion(educacion);
     const {datos:redesSociales} = await obtenerRedesSocialesContacto();
     renderizarRedesSocialesContacto(redesSociales);
}