$(document).ready(function () {

    //-------------------------------------------------- javascript

    var inpUsr = document.getElementById("inpUsr");
    var inpPwd = document.getElementById("inpPwd");

    inpUsr.addEventListener("keyup", function (event) {
        inpLoginEnter(event);
    });
    inpPwd.addEventListener("keyup", function (event) {
        inpLoginEnter(event);
    });

    //--------------------------------------------------jquery functions

    $("#btnValidaUsrPwd").on("click", function () {
        vup();
    });

    //--------------------------------------------------js functions

    function vup() {

        ////Con ajax jquery
        //let par = `{u:'${$('#inpUsr').val()}', p: '${$('#inpPwd').val()}'}`;
        //$.ajax({
        //    url: "login/VUP",
        //    data: par,
        //    type: "POST",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (response) {
        //        if (response) {
        //            bootstrapLoginAlert("success", "✔️ Usuario y contraseña válidos");
        //            document.location.href = "/";
        //        }
        //        else {
        //            bootstrapLoginAlert("danger", "Usuario o contraseña incorrecto");
        //        }
        //    },
        //    error: function (response) {
        //        bootstrapLoginAlert("danger", response);
        //    }
        //});

        let data = new FormData();

        data.append('u', document.getElementById('inpUsr').value);
        data.append('p', document.getElementById('inpPwd').value);

        fetch('login/VUP', {
            method: 'POST',
            body: data
        })
            .then(function (response) {
                if (response.ok) {
                    return response.json();
                }
                else {
                    throw "Error en la llamada";
                }
            })
            .then(function (r) {
                if (r) {
                    bootstrapLoginAlert("success", "✔️ Usuario y contraseña válidos");
                    document.location.href = "/";
                }
                else {
                    bootstrapLoginAlert("danger", "Usuario o contraseña incorrecto");
                }
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    function bootstrapLoginAlert(tipo, mensaje) {
        var divAlert = document.createElement('div');
        divAlert.className = "alert alert-dismissible fade show";
        divAlert.classList.add("alert-" + tipo);
        divAlert.role = "alert";
        divAlert.style.textAlign = "center";
        divAlert.innerHTML = "<h6>" + mensaje + "</h6>";
        document.getElementById('divAlerts').appendChild(divAlert);
        setTimeout(function () {
            $(".alert").alert('close');
        }, 1500);
    }

    function inpLoginEnter(event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            document.getElementById("btnValidaUsrPwd").click();
        }
    }

});