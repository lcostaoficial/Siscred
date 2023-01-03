$(function () {

    "use strict";

    window.GlobalMask = window.GlobalMask || {}

    GlobalMask.mask = function () {  
        
        $(".dinheiro").maskMoney({
            showSymbol: false,
            decimal: ",",
            thousands: "",
            allowZero: true,
            allowNegative: true
        }).maskMoney("mask", $(".money").val());
      
        $(".data").mask("99/99/9999");
       
        $(".cpf").mask("999.999.999-99");
       
        $(".rg").mask("99.999.999-9");
       
        $(".cnpj").mask("99.999.999/9999-99");
        
        $(".cep").mask("99999-999");
        
        $(".telefone").mask("(99) 9999-9999");
       
        $(".celular").mask("(99) 99999-9999");
        
        $(".numero").keydown(function (e) {
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey == true)) ||
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                return;
            }
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

    }    

    GlobalMask.mask();   

});