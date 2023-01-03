"use strict";

$(function () {

    window.TableData = window.TableData || {};

    TableData.confirmExclusion = function (event, element) {
        event.preventDefault();
        var url = $(element).data("remove");
        swal({
            title: "Confirmar exclusão de item?",
            text: "O item será perdido permanentemente!",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: "Sim, tenho certeza!",
            cancelButtonText: "Não, quero cancelar"
        }).then(function (result) {
            if (result.value) {
                window.location.href = url;
            }
        });
    };

    TableData.draw = function () {
        $(".datatables").dataTable({
            "language": {
                "lengthMenu": "_MENU_  Itens por página",
                "zeroRecords": "Nenhum registro encontrado",
                "info": "Exibindo página _PAGE_ de _PAGES_ do total de _TOTAL_ registro(s)",
                "infoEmpty": "Mostrando 0 a 0 de 0 entradas",
                "infoFiltered": "(filtro de _MAX_ registros)",
                "search": "Pesquisar:",
                "loadingRecords": "Carregando...",
                "paginate": {
                    "first": "Primeiro",
                    "last": "Último",
                    "next": "Próximo",
                    "previous": "Anterior"
                },
                "aria": {
                    "sortAscending": ": ativar para classificar em ordem ascendente",
                    "sortDescending": ": ativar para classificar em ordem descendente"
                }
            }
        }).draw();
    };

    TableData.load = function () {
        $(".datatables").dataTable({
            "language": {
                "lengthMenu": "_MENU_  Itens por página",
                "zeroRecords": "Nenhum registro encontrado",
                "info": "Exibindo página _PAGE_ de _PAGES_ do total de _TOTAL_ registro(s)",
                "infoEmpty": "Mostrando 0 a 0 de 0 entradas",
                "infoFiltered": "(filtro de _MAX_ registros)",
                "search": "Pesquisar:",
                "loadingRecords": "Carregando...",
                "paginate": {
                    "first": "Primeiro",
                    "last": "Último",
                    "next": "Próximo",
                    "previous": "Anterior"
                },
                "aria": {
                    "sortAscending": ": ativar para classificar em ordem ascendente",
                    "sortDescending": ": ativar para classificar em ordem descendente"
                }
            }
        });
    };

    TableData.load();
});