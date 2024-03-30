const d = document;

let $fragment = d.createDocumentFragment();
let $fragmentSkills = d.createDocumentFragment();
let $fragmentList = d.createDocumentFragment();

export const renderProjects = (projects = {}) => {
    let $templateProject = d.querySelector(".template-project").content;
    let $templateSkill = d.querySelector(".template-skill").content;

    projects.forEach(project => {
        $templateProject.querySelector(".monitor__image").setAttribute("src", `assets/img/images/${project["images"]["desktop"]}`.toLowerCase());
        $templateProject.querySelector(".mobile__image").setAttribute("src", `assets/img/images/${project["images"]["mobile"]}`.toLowerCase());
        $templateProject.querySelector(".project__title").textContent = project["name"];
        $templateProject.querySelector(".project__description").textContent = project["description"];
        //Lenguajes utilizados
        project["technologies"].forEach(tech => {
            let path = `assets/img/icons/${tech}.svg`;
            $templateSkill.querySelector(".skill").classList.add("skill--tech");
            $templateSkill.querySelector(".skill__image").setAttribute("src", path);
            let $cloneSkill = d.importNode($templateSkill, true);
            $fragmentSkills.appendChild($cloneSkill);
        });
        $templateProject.querySelector(".project__techs__content").replaceChildren($fragmentSkills);
        //Características
        project["list"].forEach(el => {
            let $li = d.createElement("li");
            $li.classList.add("project__item");
            $li.textContent = el;
            $fragmentList.appendChild($li);
        });
        $templateProject.querySelector(".project__list").replaceChildren($fragmentList);
        $templateProject.querySelector(".project__link__repo").setAttribute("href", project["links"]["repo"]);
        $templateProject.querySelector(".project__link__live").setAttribute("href",project["links"]["live"]);

        let $cloneProject = d.importNode($templateProject, true);
        $fragment.appendChild($cloneProject);
    });

    d.querySelector(".section__main-projects").appendChild($fragment);
}

// {
//     "images": {
//         "desktop": "restcountries.png",
//         "mobile": "restcountries-mobile.PNG"
//     },
//     "name": "Countries con Rest",
//     "technologies": [
//         "html",
//         "css",
//         "javascript",
//         "apirest"
//     ],
//     "description": "Web de países creada utilizando datos de una apirest externa. Se puede cambiar de tema, filtrar por continentes y buscar los países que se requieran",
//     "list": [
//         "Paginación con Javascript",
//         "Flex para la maquetación",
//         "Fetch para cargar headers y peticiones",
//         "Uso de sessionstorage y localstorage"
//     ],
//     "links": {
//         "live": "https://fruizotero.github.io/REST-Countries/",
//         "repo": "https://github.com/fruizotero/REST-Countries"
//     }
// },