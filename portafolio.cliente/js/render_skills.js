const d = document;

let $fragmentMain = d.createDocumentFragment();
let $fragmentSecondary = d.createDocumentFragment();

export const renderSkills = (skills = {}) => {
    let $templateSkill = d.querySelector(".template-skill").content;

    let skillElement = (skill) => {
        let path = `assets/img/icons/${skill["image"]}`;

        $templateSkill.querySelector(".skill__image").setAttribute("src", path);
        $templateSkill.querySelector(".skill__title").textContent = skill["name"];

        let $clone = d.importNode($templateSkill, true);

        return $clone;
    }


    skills["current"].forEach(skill => {
        $fragmentMain.appendChild(skillElement(skill));
    });
    skills["familiar"].forEach(skill => {
        $fragmentSecondary.appendChild(skillElement(skill));
    });
    d.querySelector(".skills__main__content").replaceChildren($fragmentMain);
    d.querySelector(".skills__secondary__content").replaceChildren($fragmentSecondary);


}