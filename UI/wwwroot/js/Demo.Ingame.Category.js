/// <reference path="Demo.Ingame.js" />

Demo.Ingame.Category = {
    create:  function () {
        Demo.Ingame.Loader.show();
        var form = $("#form-category");
            $.ajax({
                url: "/Category/Create",
                type: 'POST',
                dataType: "json",
                data: form.serialize(),
                cache: false,
                success: function (result) {
                    if (result.isSuccess) {
                        window.location.replace("/Category/Index/");
                    }
                    Demo.Result.show(result);
                    Demo.Loader.hide();
                },
                error: function (xhr, status, error) {
                    alert("Bir problem oluştu!");
                    Demo.Loader.hide();
                }
            });
    },
    subCategoryActivated: function () {
        var checkBox = document.getElementById("checkSubCategory");
        var text = document.getElementById("subCategorySelector");
        if (checkBox.checked == true) {
            text.style.display = "block";
        } else {
            text.style.display = "none";
        }
    }
};