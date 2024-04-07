let routeOggetti = "https://localhost:7258/Oggetto";
let routeSistemi = "https://localhost:7258/Sistema";

const stampaOggetti = () => {
  $.ajax({
    url: routeOggetti,
    type: "GET",
    success: function (risultato) {
      let contenuto = "";
      for (let [idx, oggetto] of risultato.entries()) {
        contenuto += `
                <tr data-codice="${oggetto.codO}" >
                <td>${idx + 1}</td>
                <td>${oggetto.codO}</td>
                <td>${oggetto.nomO}</td>
                <td>${oggetto.datO}</td>
                <td>${oggetto.scoO}</td>
                <td>${oggetto.tipO}</td>
                <td>${oggetto.ddtO}</td>
                <td>${oggetto.modO}</td>
                <td>${oggetto.aziO}</td>
                <td style="text-align : end">
                <button type="button" class="btn" data-toggle="modal" data-target="#inserimento-oggetto-sistema" style="width:40px; text-aling:center"><i class="fa fa-plus"></i></button>
                <button type="button" class="btn" data-toggle="modal" data-target="#modifica-sistema" style="width:40px"><i class="fa fa-pencil"></i></button>
                <button type="button" class="btn" data-toggle="modal" data-target="#sistemiPerOggetto" style="width:40px" onclick ="visualizzaSistemiPerOggetto(this)"><i class="fa fa-user-astronaut"></i></button>
                <button class="btn" onclick="popUpEliminaOggetto(this)" style="width:40px"><i class="fa fa-trash"></i></button>
                </td>
                </tr>
                `;
      }

      $("#tabella-oggetti").html(contenuto);
    },
    error: function (risultato) {
      alert("ERRORE");
      console.log(risultato);
    },
  });
};

const eliminaOggetto = (codice) => {
  $.ajax({
    url: routeOggetti + "/elimina/" + codice,
    type: "DELETE",
    success: function (risultato) {
      console.log(risultato);
      stampaOggetti();
    },
    error: function (risultato) {
      alert("ERRORE");
      console.log(risultato);
    },
  });
};

const stampaSistemi = () => {
  $.ajax({
    url: routeSistemi,
    type: "GET",
    success: function (risultato) {
      let contenuto = "";

      let elencoSistemi = [];

      for (let [idx, oggetto] of risultato.entries()) {
        let sistema = {
          codS: oggetto.codS,
          nomS: oggetto.nomS,
          tipS: oggetto.tipS,
        };

        elencoSistemi.push(sistema);

        contenuto += `
                <tr data-codice="${oggetto.codS}">
                <td>${idx + 1}</td>
                <td>${oggetto.codS}</td>
                <td>${oggetto.nomS}</td>
                <td>${oggetto.tipS}</td>
                <td style="text-align: right;"><button class="btn" onclick="popUpEliminaOggetto(this)"><i class="fa fa-trash"</button>
                <button type="button" class="btn" data-toggle="modal" data-target="#modifica-sistema"><i class="fa fa-pencil"></i></button></td>
                </tr>
                `;
      }

      localStorage.setItem("elencoSistemi", JSON.stringify(elencoSistemi));
      $("#tabella-sistemi").html(contenuto);
    },
    error: function (risultato) {
      alert("ERRORE");
      console.log(risultato);
    },
  });
};

const eliminaSistema = (codice) => {
  $.ajax({
    url: routeSistemi + "/elimina/" + codice,
    type: "DELETE",
    success: function (risultato) {
      console.log(risultato);
      stampaSistemi();
    },
    error: function (risultato) {
      alert(risultato);
      console.log(risultato);
    },
  });
};

const popUpEliminaSistema = (cod) => {
  let codice = $(cod).closest("tr").data("codice");

  Swal.fire({
    title: "..voi davero elimina un sistema planetario?",
    imageUrl:
      "https://www.amoremare.it/wp-content/uploads/2017/11/Emoicon-Dubbioso.jpg",
    imageWidth: 400,
    imageHeight: 200,
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    cancelButtonText: "Zero",
    confirmButtonText: "Se",
  }).then((result) => {
    if (result.isConfirmed) {
      eliminaSistema(codice);

      Swal.fire({
        title: "Daje!",
        text: "Sistema planetario eliminato...",
        icon: "success",
      });
    }
  });
};

const popUpEliminaOggetto = (cod) => {
  let codice = $(cod).closest("tr").data("codice");

  Swal.fire({
    title: "..voi davero elimina un oggetto celeste?",
    imageUrl:
      "https://www.amoremare.it/wp-content/uploads/2017/11/Emoicon-Dubbioso.jpg",
    imageWidth: 400,
    imageHeight: 200,
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    cancelButtonText: "Zero",
    confirmButtonText: "Se",
  }).then((result) => {
    if (result.isConfirmed) {
      eliminaOggetto(codice);

      Swal.fire({
        title: "Daje!",
        text: "Oggetto celeste eliminato...",
        icon: "success",
      });
    }
  });
};
const inserimentoSistema = () => {
  if (
    !!document.getElementById("input-nome-sistema").value &&
    !!document.getElementById("input-tipo-sistema").value
  ) {
    $.ajax({
      url: routeSistemi,
      type: "POST",
      contentType: "application/json",
      data: JSON.stringify({
        nomS: document.getElementById("input-nome-sistema").value,
        tipS: document.getElementById("input-tipo-sistema").value,
      }),
      success: function (risultato) {
        console.log(risultato);
        stampaSistemi();
        Swal.fire({
          position: "center",
          icon: "success",
          title: "Inserimento avvenuto con successo",
          showConfirmButton: false,
          timer: 1000,
        });
      },
      error: function (risultato) {
        console.log(risultato);
        Swal.fire({
          position: "center",
          icon: "error",
          title: "Errore nell'inserimento",
          showConfirmButton: false,
          timer: 1000,
        });
      },
    });
  } else {
    alert("TUTTI I CAMPI DEVONO ESSERE RIEMPITI");
  }

  document.getElementById("input-nome-sistema").value = "";
  document.getElementById("input-tipo-sistema").value = "";
};

const inserimentoOggetto = () => {
  if (
    !!document.getElementById("input-nome-oggetto").value &&
    !!document.getElementById("input-data-oggetto").value &&
    !!document.getElementById("input-scopritore-oggetto").value &&
    !!document.getElementById("input-tipologia-oggetto").value &&
    !!document.getElementById("input-distanza-oggetto").value &&
    !!document.getElementById("input-modulo-oggetto").value
  ) {
    $.ajax({
      url: routeOggetti,
      type: "POST",
      contentType: "application/json",
      data: JSON.stringify({
        nomO: document.getElementById("input-nome-oggetto").value,
        datO: document.getElementById("input-data-oggetto").value,
        scoO: document.getElementById("input-scopritore-oggetto").value,
        tipO: document.getElementById("input-tipologia-oggetto").value,
        ddtO: document.getElementById("input-distanza-oggetto").value,
        modO: document.getElementById("input-modulo-oggetto").value,
        aziO: document.getElementById("input-azimut-oggetto").value,
      }),
      success: function (risultato) {
        console.log(document.getElementById("input-data-oggetto").value);
        stampaOggetti();
        Swal.fire({
          position: "center",
          icon: "success",
          title: "Inserimento avvenuto con successo",
          showConfirmButton: false,
          timer: 1000,
        });
      },
      error: function (risultato) {
        console.log(risultato);
        Swal.fire({
          position: "center",
          icon: "error",
          title: "Errore nell'inserimento",
          showConfirmButton: false,
          timer: 1000,
        });
      },
    });
  } else {
    alert("TUTTI I CAMPI DEVONO ESSERE RIEMPITI");
  }

  document.getElementById("input-nome-oggetto").value = "";
  document.getElementById("input-data-oggetto").value = "";
  document.getElementById("input-scopritore-oggetto").value = "";
  document.getElementById("input-tipologia-oggetto").value = "";
  document.getElementById("input-distanza-oggetto").value = "";
  document.getElementById("input-modulo-oggetto").value = "";
  document.getElementById("input-azimut-oggetto").value = "";
};

const visualizzaSistemiPerOggetto = (cod) => {
  let codice = $(cod).closest("tr").data("codice");

  $.ajax({
    url: routeOggetti + "/sistemi/" + codice,
    type: "GET",
    success: function (risposta) {
      let codice = "";
      for (let [i, o] of risposta.entries()) {
        codice += `
          <tr>
            <td id="">${o.codS}</td>
            <td>${o.nomS}</td>
            <td>${o.tipS}</td>
          </tr>
          `;
      }

      document.getElementById("corpo-tabella-sistemi-per-oggetto").innerHTML =
        codice;
      console.log(risposta);
    },
    error: function (risposta) {
      alert("ERRORE");
      console.log(risposta);
    },
  });
};

// const elencoSistema = () => {
//   stampaSistemi();

//   let sistemi = localStorage.get("elenco_sistemi");

//   let codice = "";
//   for (let [i, o] of sistemi.entries()) {
//     codice += `<option value="${o.codS}">${o.nomS} ${o.tipS}</option>`;
//   }

//   document.getElementById("corpo-select-sistema").innerHTML = codice;
// };

$(document).ready(function x() {
  stampaOggetti();
  stampaSistemi();
});

// const modificaSistema = (cod) => {
//   let codice = $(cod).closest("tr").data("codice");

//   console.log(codice);
// };
