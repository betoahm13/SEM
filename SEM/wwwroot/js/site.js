// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function tableConstructor(datosJson, nombre, IdElementoPadre, dTables) {

    var mySum = 0;

    /*Construcción de tabla*/

    var r = datosJson.length;

    if (r > 0) {

        var c = Object.keys(datosJson[0]).length;

        var ElementoPadre = document.getElementById(IdElementoPadre);

        ElementoPadre.innerHTML = "";

        var i = null;

        t = "<table id='" + nombre + "' class='table table-bordered table-hover table-sm table-striped' style='width:100%'>";

        t += "<thead>";
        t += "<tr style='text-align:center'> ";

        //t += "<th></th>";

        for (i = 0; i < c; i++) {

            t += "<th>";
            t += Object.keys(datosJson[0])[i];
            t += "</th>";

        }

        t += "</tr>";
        t += "</thead>";
        t += "<tbody>";

        for (i = 0; i < r; i++) {

            t += "<tr id='" + (datosJson[i])[Object.keys(datosJson[0])[0]] + "' style='text-align:center'>";

            //t += "<td></td>";

            for (var j = 0; j < c; j++) {
                t += "<td>";
                t += (datosJson[i])[Object.keys(datosJson[0])[j]];
                t += "</td>";
            }

            t += "</tr>";

        }

        t += "</tbody>";

        t += "</table>";

        ElementoPadre.innerHTML = t;

        if (dTables) {
            customDataTables(nombre);
        }
    }
}

function customDataTables(nombre) {

    var btns;

    btns = [
        {
            text: '<i class="fas fa-receipt"></i> Imprimir recibo',
            className: 'clsRecibo btn-secondary',
            enabled: false,
            action: function (e, dt, node, config) {

                let arrRows = [];

                for (var z = 0; z < table.rows({ selected: true }).data().length; z++) {
                    arrRows.push(table.rows({ selected: true }).data()[z][0]);
                }

                Swal.fire({
                    title: '¿Imprimir recibo?',
                    text: "",
                    icon: 'question',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Continuar'
                }).then((result) => {
                    if (result.value) {

                        var wo = window.open("../Administracion/Recibo?l=" + arrRows[0] + "&itc=2");

                        setTimeout(function () {

                            wo.print();

                            GetCapturas(1, 2, 6, "CaptuGuard");

                            wo.close();

                        }, 2000);

                    }
                })
            }
        },
        {
            text: '<i class="fas fa-receipt"></i> Imprimir póliza manual',
            className: 'clsPoliza btn-secondary',
            enabled: false,
            action: function (e, dt, node, config) {

                let arrRows = [];

                let polVal = true;

                for (var z = 0; z < table.rows({ selected: true }).data().length; z++) {

                    if (table.rows({ selected: true }).data()[0][1] != table.rows({ selected: true }).data()[z][1]) {
                        polVal = false;
                        break;
                    }

                    arrRows.push(table.rows({ selected: true }).data()[z][0]);
                }

                if (polVal) {

                    Swal.fire({
                        title: '¿Imprimir póliza?',
                        text: "",
                        icon: 'question',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Continuar'
                    }).then((result) => {
                        if (result.value) {

                            var wo = window.open("../Administracion/Poliza?l=" + arrRows);

                            setTimeout(function () {

                                wo.print();

                                GetCapturas(1, 2, 6, "CaptuGuard");

                                wo.close();

                            }, 2000);

                        }
                    })

                }
                else {
                    alert("Todas las capturas seleccionadas deben ser de la misma contabilidad");
                }
            }
        },
        //{
        //    text: '<i class="fas fa-receipt"></i> Imprimir póliza automática',
        //    className: 'clsPolizaA btn-secondary',
        //    enabled: true,
        //    action: function (e, dt, node, config) {
        //        $('#modalSelContaPoliza').modal('show');
        //    }
        //}
    ]

    var table = $('#' + nombre).DataTable(
        {
            responsive: true,
            select: {
                style: "multi"
            },
            dom: 'Bfrtip',
            pageLength: 10,
            //buttons: {
            //    buttons: btns,
            //    dom: {
            //        button: { className: "btn btn-secondary" },
            //    }
            //},
            language: { //Cambio de lenguaje, no hay uno predefinido, tienes que personalizar tus mensajes
                lengthMenu: "Mostar _MENU_ registros",//Indica cantidad de paginas mostradas
                zeroRecords: "No se encontraron registros",//Mensaje que se desplegará su una busqueda no produce resultados
                info: "Mostrando página _PAGE_ de _PAGES_",//Indica cuantas paginas de el total de estas mostrando
                infoEmpty: "No registros disponibles",//Mensaje que se desplegará cuando no haya información en la tabla
                infoFiltered: "(Resultados de un total de  _MAX_ )",//Indica cuanto resultados se obtuvieron de filtrar por medio de una busqueda
                search: "Buscar:",//Mensaje de label en Buscar
                paginate: {
                    previous: "Anterior",
                    next: "Siguiente"
                },
            },
            columnDefs: [
                {
                    targets: [0],
                    visible: false
                },
            ],
            searching: true,
            bPaginate: true,
            bInfo: false,
            initComplete: function (settings, json) {



            },

            //////////////

            //"footerCallback": function (row, data, start, end, display) {
            //    var api = this.api(), data;

            //    // Remove the formatting to get integer data for summation
            //    var intVal = function (i) {
            //        return typeof i === 'string' ?
            //            i.replace(/[\$,]/g, '') * 1 :
            //            typeof i === 'number' ?
            //                i : 0;
            //    };

            //    // Total over all pages
            //    total = api
            //        .column(5)
            //        .data()
            //        .reduce(function (a, b) {
            //            return intVal(a) + intVal(b);
            //        }, 0);

            //    // Total over this page
            //    pageTotal = api
            //        .column(5, { page: 'current' })
            //        .data()
            //        .reduce(function (a, b) {
            //            return intVal(a) + intVal(b);
            //        }, 0);

            //    // Update footer
            //    $(api.column(5).footer()).html(
            //        '$' + pageTotal + ' ( $' + total + ' total)'
            //    );
            //}

            //////////////


        });

    table.on('select', function () {
        table.buttons(['.clsGrabar']).enable(
            table.rows({ selected: true }).indexes().length === 0 ?
                false :
                true
        );
        table.buttons(['.clsEliminar']).enable(
            table.rows({ selected: true }).indexes().length === 0 ?
                false :
                true
        );
        table.buttons(['.clsEditar']).enable(
            table.rows({ selected: true }).indexes().length === 1 ?
                true :
                false
        );
        table.buttons(['.clsRecibo']).enable(
            table.rows({ selected: true }).indexes().length === 1 ?
                true :
                false
        );
        table.buttons(['.clsPoliza']).enable(
            table.rows({ selected: true }).indexes().length === 0 ?
                false :
                true
        );
    });
    table.on('deselect', function () {
        table.buttons(['.clsGrabar']).enable(
            table.rows({ selected: true }).indexes().length === 0 ?
                false :
                true
        );
        table.buttons(['.clsEliminar']).enable(
            table.rows({ selected: true }).indexes().length === 0 ?
                false :
                true
        );
        table.buttons(['.clsEditar']).enable(
            table.rows({ selected: true }).indexes().length === 1 ?
                true :
                false
        );
        table.buttons(['.clsRecibo']).enable(
            table.rows({ selected: true }).indexes().length === 1 ?
                true :
                false
        );
        table.buttons(['.clsPoliza']).enable(
            table.rows({ selected: true }).indexes().length === 0 ?
                false :
                true
        );
    });
}