"use strict";

let deleteBtns = document.querySelectorAll(".delete-footboller-btn");
let deleteContainers = document.querySelectorAll(".delete-container");
let forms = document.querySelectorAll(".change-form");


for (let i = 0; i < deleteBtns.length; i++) {
    let btn = deleteBtns[i];
    let container = deleteContainers[i];
    let form = forms[i];
    btn.addEventListener("click", function () {
        container.querySelector("input").value = true;
        form.submit();
    });
}