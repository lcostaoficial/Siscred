"use strict";

$(function () {

    window.InscricaoVisualizacao = window.InscricaoVisualizacao || {};

    window.InscricaoVisualizacao.helpers = {
        isStringNullOrEmpty: function isStringNullOrEmpty(val) {
            switch (val) {
                case "":
                case 0:
                case "0":
                case null:
                case false:
                case undefined:
                case typeof this === 'undefined':
                    return true;
                default:
                    return false;
            }
        },
        isStringNullOrWhiteSpace: function isStringNullOrWhiteSpace(val) {
            return this.isStringNullOrEmpty(val) || val.replace(/\s/g, "") === '';
        },
        nullIfStringNullOrEmpty: function nullIfStringNullOrEmpty(val) {
            if (this.isStringNullOrEmpty(val)) {
                return null;
            }
            return val;
        }
    };

    InscricaoVisualizacao.Eventos = {
        Tabela: function Tabela() {
            var $url = $(".panel-body").data("url");
            var $container = $(".panel-body");
            var id = $("#EditalId").val();
            $container.html("<h1><i class='fa fa-spinner fa-spin'></i> Carregando...</h1>");
            $.ajax({
                url: $url,
                data: { editalId: id },
                method: "GET",
                success: function success(data) {
                    $container.html(data);
                    TableData.load();
                },
                error: function error(jqXhr, textStatus, errorThrown) {
                    $container.html("<p>Selecione um parâmetro de filtro acima...</p>");
                    swal("Mensagem", errorThrown, "error");
                }
            });
        },
        Carregar: function Carregar() {
            var $container = $(".panel-body");
            $("#EditalId").change(function () {
                var id = $(this).val();
                if (window.InscricaoVisualizacao.helpers.isStringNullOrEmpty(id)) {
                    $container.html("<p>Selecione um parâmetro de filtro acima...</p>");
                } else {
                    InscricaoVisualizacao.Eventos.Tabela();
                }
            });
        },
        Visualizar: function Visualizar(id, button) {
            var $container = $("#modalExibirInscricao");
            var $button = $(button);
            var $url = $container.data("url");
            $button.find("i").removeAttr("class");
            $button.find("i").addClass("fa fa-spinner fa-spin"); 
            $.ajax({
                url: $url,
                data: { inscricaoId: id },
                method: "GET",
                success: function success(data) {
                    $container.html(data);
                    $container.modal("show");
                },
                error: function error(jqXhr, textStatus, errorThrown) {
                    swal("Mensagem", errorThrown, "error");
                },
                complete: function complete() {
                    $button.find("i").removeAttr("class");
                    $button.find("i").addClass("fa fa-eye");
                }
            });
        },
        Aprovar: function Aprovar(id, status) {
            swal({
                title: "Deseja realmente continuar com ação?",
                text: "A inscri\xE7\xE3o ser\xE1 colocada no estado de " + status + "!",
                type: "warning",
                showCancelButton: true,
                confirmButtonText: "Sim, tenho certeza!",
                cancelButtonText: "Não, quero cancelar"
            }).then(function (result) {
                if (result.value) {
                    var $url = $("#resultado").data("url");
                    var $form = $("#form-resultado");
                    var $justificativa = $("#Justificativa").val().trim();
                    if (!window.InscricaoVisualizacao.helpers.isStringNullOrEmpty($justificativa)) {
                        $.ajax({
                            url: $url,
                            data: { inscricaoId: id, justificativa: $("#Justificativa").val(), situacao: status },
                            method: "GET",
                            success: function success(data) {
                                if (data.success) {
                                    var $msg = data.success;
                                    $.ajax({
                                        url: $("#resultado").data("situacao"),
                                        data: { situacao: status },
                                        method: "GET",
                                        success: function success(data) {
                                            $(".datatables").DataTable().destroy();
                                            $("#situacao" + id).html(data);
                                            swal("Mensagem", $msg, "success");
                                            TableData.draw();
                                        },
                                        error: function error(jqXhr, textStatus, errorThrown) {
                                            swal("Mensagem", errorThrown, "error");
                                        }
                                    });
                                }
                                if (data.error) {
                                    swal("Mensagem", data.error, "error");
                                }
                            },
                            error: function error(jqXhr, textStatus, errorThrown) {
                                swal("Mensagem", errorThrown, "error");
                            },
                            complete: function complete() {
                                $("#modalExibirInscricao").modal("toggle");
                            }
                        });
                    } else {
                        $("#Justificativa").val("");
                        swal("Mensagem", "Preencha a justificativa!", "warning");
                    }
                }
            });
        }
    };

    InscricaoVisualizacao.Eventos.Carregar();

});