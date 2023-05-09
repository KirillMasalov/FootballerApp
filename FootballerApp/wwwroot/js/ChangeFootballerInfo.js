"use strict";

let infoPanels = document.querySelectorAll(".footballer-info");
let changeBtns = document.querySelectorAll(".open-change-form-btn");
let changeForms = document.querySelectorAll(".footballer-form");

console.log(changeBtns.length);
console.log(changeForms.length);

for (let i = 0; i < changeBtns.length; i++) {
    let btn = changeBtns[i]
    btn.addEventListener("click", function () {
        for (let j = 0; j < changeBtns.length; j++) {
            if (changeBtns[j] === btn) {
                infoPanels[j].classList.add("display-off");
                changeForms[j].classList.remove("display-off");
            } else {
                infoPanels[j].classList.remove("display-off");
                changeForms[j].classList.add("display-off");
            }
        }

    });
}