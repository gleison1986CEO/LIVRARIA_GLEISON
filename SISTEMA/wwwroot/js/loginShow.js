document.getElementById("login-submit").addEventListener(
  "click",
  function(event) {
    if (event.target.value === "Entrar") {
      event.target.value = "Requisitando Dados...";
    } else {
      event.target.value = "Entrar";
    }
  },
  false
);


document.getElementById("login-submit2").addEventListener(
  "click",
  function(event) {
    if (event.target.value === "Solicitar Novo Token") {
      event.target.value = "Requisitando Token...";
    } else {
      event.target.value = "Solicitar Novo Token";
    }
  },
  false
);



