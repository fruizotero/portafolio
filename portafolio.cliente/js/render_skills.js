const d = document;

let $fragmentMain = d.createDocumentFragment();
let $fragmentSecondary = d.createDocumentFragment();

export const renderSkills = (skills = {}) => {
    let $templateSkill = d.querySelector(".template-skill").content;

    let skillElement = (skill) => {
        let path = skill["logoUrl"];

        $templateSkill.querySelector(".skill__image").setAttribute("src", path);
        $templateSkill.querySelector(".skill__title").textContent = skill["nombre"];

        let $clone = d.importNode($templateSkill, true);

        return $clone;
    }


    skills.forEach(skill => {
        if(skill["esActual"]){

            $fragmentMain.appendChild(skillElement(skill));
        } else{
            $fragmentSecondary.appendChild(skillElement(skill));
        }
    });

    d.querySelector(".skills__main__content").replaceChildren($fragmentMain);
    d.querySelector(".skills__secondary__content").replaceChildren($fragmentSecondary);


}