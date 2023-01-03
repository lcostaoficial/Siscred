"use strict";

$(function () {

    window.RecuperacaoSenha = window.RecuperacaoSenha || {};

    RecuperacaoSenha.ModalDiv = $("#modalRecuperarSenha");

    RecuperacaoSenha.Url = RecuperacaoSenha.ModalDiv.data("url");

    RecuperacaoSenha.Apoio = {
        ExecutarModal: function ExecutarModal() {
            $.ajax({
                method: "GET",
                url: RecuperacaoSenha.Url,
                dataType: "html",
                success: function success(result) {
                    RecuperacaoSenha.ModalDiv.html(result);
                    RecuperacaoSenha.ModalDiv.modal("show");
                },
                error: function error(XMLHttpRequest, textStatus, errorThrown) {
                    swal("Mensagem", errorThrown, "error");
                }
            });
        },
        DesabilitarBotao: function DesabilitarBotao(valor) {
            $("#btn-confirmar").prop("disabled", valor);
            $("#btn-cancelar").prop("disabled", valor);
            if (valor === true) {
                $("#btn-confirmar").find("i").removeClass("glyphicon glyphicon-ok");
                $("#btn-confirmar").find("i").addClass("fa fa-spinner fa-spin");
                $("#btn-confirmar span").text("Aguarde...");
            } else {
                $("#btn-confirmar").find("i").removeClass("fa fa-spinner fa-spin");
                $("#btn-confirmar").find("i").addClass("glyphicon glyphicon-ok");
                $("#btn-confirmar span").text("Confirmar");
            }
        }
    };

    RecuperacaoSenha.Eventos = {
        Carregar: function Carregar() {

            $(document).on("click", "#btnRecuperarSenha", function () {
                RecuperacaoSenha.Apoio.ExecutarModal();
            });

            $(document).on("submit", "form#formRecuperarSenha", function (e) {
                e.preventDefault();
                RecuperacaoSenha.Apoio.DesabilitarBotao(true);
                $.ajax({
                    type: "POST",
                    url: $(this).attr("action"),
                    data: $(this).serialize(),
                    success: function success(result) {
                        if (result.Success) {
                            window.location.replace(result.Url);
                        }
                        if (result.Error) {
                            swal("Mensagem", result.Error, "warning");
                        }
                    },
                    error: function error(XMLHttpRequest, textStatus, errorThrown) {
                        swal("Mensagem", errorThrown, "error");
                    },
                    complete: function complete() {
                        RecuperacaoSenha.Apoio.DesabilitarBotao(false);
                    }
                });
            });
        }
    };

    RecuperacaoSenha.Eventos.Carregar();
});