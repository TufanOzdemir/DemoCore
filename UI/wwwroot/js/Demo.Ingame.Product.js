/// <reference path="Demo.Ingame.js" />

Demo.Ingame.Product = {
    edit: function () {
        Demo.Ingame.Loader.show();
        var form = $("#product-edit");
        form.append('pic', $('#pic')[0].files[0]);
        $.ajax({
            url: "/Product/Edit",
            type: 'POST',
            dataType: "json",
            data: form.serialize(),
            cache: false,
            success: function (result) {
                if (result.isSuccess) {
                    Demo.Ingame.Result.show(result);
                    setTimeout(function () { window.location.replace("/Product/Index/"); }, 3000);
                }
                Demo.Ingame.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!");
                Demo.Ingame.Loader.hide();
            }
        });
    }
}