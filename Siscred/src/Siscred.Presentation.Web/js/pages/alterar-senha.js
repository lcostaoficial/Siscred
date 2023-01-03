$(function () {

    "use strict";

    window.AlterarSenha = window.AlterarSenha || {};   

    AlterarSenha.ModalDiv = $("#modalAlterarSenha");

    AlterarSenha.Url = AlterarSenha.ModalDiv.data("url");

    AlterarSenha.Apoio = {
        ExecutarModal: function ExecutarModal() {
            $.ajax({
                method: "GET",
                url: AlterarSenha.Url,
                dataType: "html",
                success: function success(result) {
                    AlterarSenha.ModalDiv.html(result);  
                    $.validator.unobtrusive.parse(AlterarSenha.ModalDiv.find("form"));
                    AlterarSenha.ModalDiv.modal("show");
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

    AlterarSenha.Eventos = {
        Carregar: function Carregar() {

            $(document).on("click", "#btnAlterarSenha", function () {
                AlterarSenha.Apoio.ExecutarModal();
            });

            $(document).on("submit", "form#formAlterarSenha", function (e) {
                e.preventDefault();
                AlterarSenha.Apoio.DesabilitarBotao(true);
                $.ajax({
                    type: "POST",
                    url: $(this).attr("action"),
                    data: $(this).serialize(),
                    success: function success(result) {
                        if (result.Success) {
                            swal("Mensagem", result.Success, "success");
                            AlterarSenha.ModalDiv.modal("hide");
                        }
                        if (result.Error) {
                            swal("Mensagem", result.Error, "warning");
                            AlterarSenha.ModalDiv.modal("hide");
                        }
                    },
                    error: function error(XMLHttpRequest, textStatus, errorThrown) {
                        swal("Mensagem", errorThrown, "error");
                    },
                    complete: function complete() {
                        AlterarSenha.Apoio.DesabilitarBotao(false);
                    }
                });
            });
        }
    };

    AlterarSenha.Eventos.Carregar();
});