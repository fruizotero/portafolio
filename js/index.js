import { loadData } from "./load_data.js";
import { renderSkills } from "./render_skills.js";
import { renderProjects } from "./render_projects.js";

const d = document;
let dataSkills;
let dataProjects;

d.addEventListener("DOMContentLoaded", async e => {
    //TODO::loader
    dataSkills = await loadData(".skills__content", "assets/data/skills.json");
    renderSkills(dataSkills);
    dataProjects = await loadData(".section__main-projects", "assets/data/projects.json");
    renderProjects(dataProjects);
    console.log(dataProjects);

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