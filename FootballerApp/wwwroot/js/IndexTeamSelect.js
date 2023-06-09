﻿"use strict";

let sel = document.getElementById("team-select").querySelector("select");
let name_input = document.getElementById("team-input");

if (sel.value == "") {
    name_input.classList.remove("input-off");
}

sel.addEventListener("change", function () {
    if (sel.value == "") {
        name_input.classList.remove("input-off");
        name_input.focus();
    }
    else {
        name_input.classList.add("input-off");
    }
})

name_input.addEventListener("change", function () {
    sel.value = name_input.querySelector("input").value;
})