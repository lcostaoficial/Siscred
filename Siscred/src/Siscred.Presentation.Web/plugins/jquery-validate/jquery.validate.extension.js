$(function () {

    "use strict";

    window.ValidationExtension = window.ValidationExtension || {};

    ValidationExtension.Events = {
        Load: function Load() {
            $.extend($.validator.methods, {
                date: function (value, element) {
                    return this.optional(element) || /^\d\d?\/\d\d?\/\d\d\d?\d?$/.test(value);
                }
            });            
        }
    }    

    ValidationExtension.Events.Load();

});