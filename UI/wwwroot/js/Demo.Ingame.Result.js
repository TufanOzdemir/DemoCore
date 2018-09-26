/// <reference path="Demo.Ingame.js" />
Demo.Ingame.ResultEnum = {
    INFORMATION: 1,
    SUCCESS: 2,
    WARNING: 3,
    ERROR: 4
};

Demo.Ingame.Result = {
    show: function (result) {
        var languageMessage = result.languageMessage;
        var message = result.message;
        var resultType = result.resultType;
        var isSuccess = result.isSuccess;
        toastr.options = {
            "closeButton": true,
        }
        if (resultType == RTG.FE.ResultEnum.INFORMATION) {
            toastr["info"](languageMessage, message);
        }
        if (resultType == RTG.FE.ResultEnum.SUCCESS) {
            toastr["success"](languageMessage, "Başarılı!");
        }
        if (resultType == RTG.FE.ResultEnum.WARNING) {
            toastr["warning"](languageMessage, "Uyarı!");
        }
        if (resultType == RTG.FE.ResultEnum.ERROR) {
            toastr["error"](languageMessage, "Hata!");
        }
    }
};