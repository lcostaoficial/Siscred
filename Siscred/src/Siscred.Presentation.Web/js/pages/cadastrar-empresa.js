"use strict";

$(function () {

    window.CadastrarEmpresa = window.CadastrarEmpresa || {};

    CadastrarEmpresa.Eventos = {
        Carregar: function Carregar() {
            $("form").submit(function () {
                var $btnRegistrar = $("#btn-registrar");
                var $btnRetornar = $("#btn-retornar");
                var isValid = $(this).valid();
                if (isValid) {
                    $btnRegistrar.prop("disabled", true);
                    $btnRetornar.prop("disabled", true);
                    $btnRegistrar.find("i").removeClass("glyphicon glyphicon-ok");
                    $btnRegistrar.find("i").addClass("fa fa-spinner fa-spin");
                    $btnRegistrar.find("span").text("Aguarde...");
                }
            });
        }
    };

    CadastrarEmpresa.Eventos.Carregar();
});