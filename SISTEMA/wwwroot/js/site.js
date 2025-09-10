let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".sidebarBtn");
sidebarBtn.onclick = function () {
    sidebar.classList.toggle("active");
    if (sidebar.classList.contains("active")) {
        sidebarBtn.classList.replace("bx-menu", "bx-menu-alt-right");
    } else
        sidebarBtn.classList.replace("bx-menu-alt-right", "bx-menu");
}

function limComments(obj) {
var charLimit = [obj.getAttribute('minlength'), obj.getAttribute('maxlength')];
var fieldID = obj.getAttribute('id');

if (obj.value.length >= charLimit[0] && obj.value.length <= charLimit[1]) {
    console.log("Esta descrição do laudo " + fieldID + " esta correto.");
} else if (obj.value.length > charLimit[1]) {
    console.log("Esta descrição do laudo " + fieldID + " é muito longa.");
    obj.value = obj.value.substring(0, charLimit[1]);
} else {
    console.log("Esta descrição do laudo" + fieldID + " é muito curta.");
}}







function multiSearchKeyup(inputElement) {
    if(event.keyCode === 32 || event.keyCode === 0) {
        inputElement.parentNode
            .insertBefore(createFilterItem(inputElement.value), inputElement)
            ;
        inputElement.value = "";
    }
    function createFilterItem(text) {
        const item = document.createElement("div");
        item.setAttribute("class", "multi-search-item");
        const span = `<span>${text}</span>`;
        const close = `<div class="fa fa-close" onclick="this.parentNode.remove()"></div>`;
        item.innerHTML = span+close;
        return item;
    }
}


function spaces(){
    var el  = document.getElementsByName("Name")[0];
    $(el).keypress(function(e) {
        if (e.keyCode == 0 || e.keyCode == 32) {
            alert("Não é permitido espaços no cadastro do USERNAME");
        
        }
    });   }

function virgulas(){
        var el  = document.getElementsByName("Description")[0];
        $(el).keypress(function(e) {
            if (e.keyCode == 0 || e.keyCode == 32) {
                alert("Não é permitido espaços apenas vírgulas para as palavras chave");
            
            }
        });   }
    

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
    return false;
    return true;
}

