"use strict";

$(function () {

    window.Indicado = window.Indicado || {};

    window.Indicado.helpers = {
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

    Indicado.uploadValidation = function (fileName, fileSize, fileSizeMax, fileExtentions) {
        var postfix = fileName.substr(fileName.lastIndexOf("."));
        var sizeMax = fileSizeMax / (1024 * 1024);
        if (fileExtentions.indexOf(postfix.toLowerCase()) > -1) {
            if (fileSize > fileSizeMax) {
                swal("Mensagem", "Tamanho m\xE1ximo permitido: " + sizeMax + " MB", "warning");
                return false;
            }
            return true;
        } else {
            swal("Mensagem", "\xC9 permitido apenas arquivos com as seguintes extens\xF5es: " + fileExtentions, "warning");
            return false;
        }
    };

    Indicado.Eventos = {
        Carregar: function Carregar() {
            $(".file").change(function () {
                var input = $(this);
                var $fileSizeMax = input.data("maximumsize");
                var $fileSize = input.get(0).files[0].size;
                var $fileExtentions = input.data("extensions");
                var $filename = input.val().match(/^.*\\(.*)$/)[1];
                var $div = input.parent().parent().parent().find(".caption");
                if (Indicado.uploadValidation($filename, $fileSize, $fileSizeMax, $fileExtentions)) {
                    $div.html($filename);
                } else {
                    input.val(null);
                    $div.html(null);
                }
            });

            $(".cpf").blur(function () {
                var input = $(this);
                var $url = input.data("url");
                var $value = input.val();
                var $editalId = $("#EditalId").val();
                if ($value !== "" && $value !== null) {
                    $.ajax({
                        method: "GET",
                        url: $url,
                        data: { cpf: $value, editalId: $editalId },
                        dataType: "json",
                        success: function success(result) {    
                            if (result.valid === false) {
                                input.val("");
                                input.focus();
                                swal("Mensagem", result.msg, "error");
                            }
                        },
                        error: function error(XMLHttpRequest, textStatus, errorThrown) {
                            swal("Mensagem", errorThrown, "error");
                        }
                    });
                }
            });

            $("form").submit(function () {
                var documents = $(".documentos");
                var valid = true;
                $.each(documents, function (key, value) {
                    var item = $(value).val();
                    var required = $(value).data("required");
                    if (Indicado.helpers.isStringNullOrEmpty(item) && required == true) {
                        valid = false;
                    }
                });
                if (valid === false) {
                    swal("Mensagem", "Você não inseriu todos os arquivos exigidos, por favor insira todos os arquivos que são obrigatórios!", "error");
                    return false;
                } else {
                    return true;
                }
            });
        }
    };

    Indicado.confirmFinalization = function (event, element) {
        event.preventDefault();
        var url = $(element).data("finalization");
        swal({
            title: "Confirmar finalização de inscrição?",
            text: "Não será possível alterar os dados após a finalização da inscrição!",
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

    Indicado.confirmExclusion = function (event, element) {
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

    Indicado.Eventos.Carregar();
});