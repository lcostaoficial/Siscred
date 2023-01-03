$(function () {
    $(".data").datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: "pt-BR",
        todayHighlight: true,
        todayBtn: true,
        orientation: "bottom"
    }).on("change", function () {
        $(this).valid(); 
    });
});