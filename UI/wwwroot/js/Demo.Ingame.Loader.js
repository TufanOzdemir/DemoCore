/// <reference path="Demo.Ingame.js" />

Demo.Ingame.Loader = {
    show: function () {
        $.blockUI({
            message: '<h3 class="fldHead">Loading... <img src="/app/images/winliveprog.gif"/></h3>'
        });
    },
    hide: function () {
        $.blockUI({
            message: '<h3 class="fldHead">Loading... <img src="/app/images/winliveprog.gif"/></h3>'
        });
    },
    showInPopup: function () {

        $.blockUI({
            message: '<h3 class="fldHead">Loading... <img src="/app/images/winliveprog.gif"/></h3>'
        });
        $('.blockUI.blockMsg.blockPage').css('opacity', 1);
        $('.blockUI.blockMsg.blockPage').css('z-index', 111111);
    }
};