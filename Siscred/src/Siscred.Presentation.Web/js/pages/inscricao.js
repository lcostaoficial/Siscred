"use strict";

$(function () {

    window.Inscricao = window.Inscricao || {};

    window.Inscricao.helpers = {
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

    Inscricao.regionsValidate = function (regionId, removeSelect) {
        var drops = $(".selectpicker.microregiao");
        $.each(drops, function (key, value) {
            var item = $(value).val();
            if (item === regionId && $(value).attr("id") !== removeSelect) {
                var selectCurrent = $("#" + removeSelect);
                selectCurrent.prop("selectedIndex", 0);
                swal("Mensagem", "Voc\xEA j\xE1 selecionou esta microrregi\xE3o como " + (key + 1) + "\xBA op\xE7\xE3o de interesse, por favor selecione outra!", "warning");
                selectCurrent.focus();
                return false;
            }
        });
    };

    $(".microregiao").change(function () {
        var select = $(this);
        var regionId = select.val();
        var removeSelect = select.attr("id");
        Inscricao.regionsValidate(regionId, removeSelect);
    });

    Inscricao.partitionValidation = function (form, div) {
        div.find("input, textarea, select").each(function (i, item) {
            form.validate().element(item);
        });
    };

    Inscricao.setValidation = function () {
        var $form = $("form");
        var $active = $(".wizard .nav-tabs li.active");
        var $div = $active.closest("div");
        Inscricao.partitionValidation($form, $div);
    };

    Inscricao.uploadValidation = function (fileName, fileSize, fileSizeMax, fileExtentions) {
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

    Inscricao.Eventos = {

        Carregar: function Carregar() {

            $(".nav-tabs > li a[title]").tooltip();

            $(".cnpj").blur(function () {
                var input = $(this);
                var $url = input.data("url");
                var $value = input.val();
                var $editalId = $("#EditalId").val();
                if ($value !== "" && $value !== null) {
                    $.ajax({
                        method: "GET",
                        url: $url,
                        data: { cnpj: $value, editalId: $editalId },
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

            $("a[data-toggle='tab']").on("show.bs.tab", function (e) {
                var $target = $(e.target);
                var $form = $("form");
                var $tab = $(this).data("info");
                var $isValid = true;
                Inscricao.setValidation();
                if ($target.parent().hasClass("disabled")) {
                    return false;
                }
                if (!$form.valid()) {
                    swal("Mensagem", "Preencha todos os campos corretamente!", "warning");
                    return false;
                }
                $(".file").each(function () {
                    var $input = $(this);
                    var $required = $input.data("required");
                    if ($input.get(0).files.length === 0 && $required === true) {
                        $isValid = false;
                    }
                });
                if (!$isValid && $tab === "completo") {
                    swal("Mensagem", "Preencha todos os arquivos!", "warning");
                    return false;
                }
            });

            $(".next-info").click(function (e) {
                var $form = $("form");
                var $active = $(".wizard .nav-tabs li.active");
                Inscricao.setValidation();
                if ($form.valid()) {
                    $active.next().removeClass("disabled");
                    Inscricao.Eventos.NextTab($active);
                } else {
                    swal("Mensagem", "Preencha todos os campos corretamente!", "warning");
                }
            });

            $(".next-file").click(function (e) {
                var $form = $("form");
                var $active = $(".wizard .nav-tabs li.active");
                var $isValid = true;
                $(".file").each(function () {
                    var $input = $(this);
                    var $required = $input.data("required");
                    if ($input.get(0).files.length === 0 && $required === true) {
                        $isValid = false;
                    }
                });
                if ($isValid) {
                    $active.next().removeClass("disabled");
                    Inscricao.Eventos.NextTab($active);
                } else {
                    swal("Mensagem", "Preencha todos os arquivos!", "warning");
                    return false;
                }
            });

            $(".file").change(function () {
                var input = $(this);
                var $fileSizeMax = input.data("maximumsize");
                var $fileSize = input.get(0).files[0].size;
                var $fileExtentions = input.data("extensions");
                var $filename = input.val().match(/^.*\\(.*)$/)[1];
                var $div = input.parent().parent().parent().find(".caption");
                if (Inscricao.uploadValidation($filename, $fileSize, $fileSizeMax, $fileExtentions)) {
                    $div.html($filename);
                } else {
                    input.val(null);
                    $div.html(null);
                }
            });

            $(".chosen").on("change", function () {
                $(this).valid();
            });

            $(".search-choice-close").on("click", function () {
                alert('ok');
            });
        },

        NextTab: function NextTab(elem) {
            $(elem).next().find("a[data-toggle='tab']").click();
        },

        PrevTab: function PrevTab(elem) {
            $(elem).prev().find("a[data-toggle='tab']").click();
        }

    };

    Inscricao.Eventos.Carregar();

    Inscricao.setValidation();
});