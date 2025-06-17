const d = document;

let $fragment = d.createDocumentFragment();
let $fragmentSkills = d.createDocumentFragment();
let $fragmentList = d.createDocumentFragment();

export const renderProjects = (proyectos = []) => {
    let $templateProject = d.querySelector(".template-project").content;
    let $templateSkill = d.querySelector(".template-skill").content;

    proyectos.forEach(proyecto => {
        $templateProject.querySelector(".monitor__image").setAttribute("src", proyecto.imagenDesktopUrl);
        $templateProject.querySelector(".mobile__image").setAttribute("src", proyecto.imagenMobileUrl);
        $templateProject.querySelector(".project__title").textContent = proyecto.titulo;
         $templateProject.querySelector(".project__description").textContent = proyecto.descripcion;
        //Lenguajes utilizados
        proyecto["habilidades"].forEach(habilidad => {
            let path = habilidad["logoUrl"];
            $templateSkill.querySelector(".skill").classList.add("skill--tech");
            $templateSkill.querySelector(".skill__image").setAttribute("src", path);
            let $cloneSkill = d.importNode($templateSkill, true);
            $fragmentSkills.appendChild($cloneSkill);
        });
        $templateProject.querySelector(".project__techs__content").replaceChildren($fragmentSkills);
        //Características
        proyecto["conocimientos"].forEach(conocimiento => {
            let $li = d.createElement("li");
            $li.classList.add("project__item");
            $li.textContent = conocimiento.nombre;
            $fragmentList.appendChild($li);
        });
        $templateProject.querySelector(".project__list").replaceChildren($fragmentList);
        $templateProject.querySelector(".project__link__repo").setAttribute("href", proyecto.repositorioUrl);
        $templateProject.querySelector(".project__link__live").setAttribute("href",proyecto.liveUrl);

        let $cloneProject = d.importNode($templateProject, true);
        $fragment.appendChild($cloneProject);
    });

    d.querySelector(".section__main-projects").appendChild($fragment);
}
