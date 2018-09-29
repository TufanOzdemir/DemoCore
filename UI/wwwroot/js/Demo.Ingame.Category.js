/// <reference path="Demo.Ingame.js" />

Demo.Ingame.Category = {
    list: function () {
        Demo.Ingame.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Category/List",
            data: null,
            cache: false,
            success: function (result) {
                $("#list-category").html(result.html);
                Demo.Ingame.Result.show(result);
                Demo.Ingame.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!" + error);
                Demo.Ingame.Loader.hide();
            }
        });
    },
    new: function () {
        Demo.Ingame.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Category/CreatePage",
            data: null,
            cache: false,
            success: function (result) {
                $("#create-category").html(result.html);
                Demo.Ingame.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!");
                Demo.Ingame.Loader.hide();
            }
        });
    },
    update: function (id) {
        Demo.Ingame.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Category/Update",
            data: { 'id': id },
            cache: false,
            success: function (result) {
                $("#update-category").html(result.html);
                Demo.Ingame.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!");
                Demo.Ingame.Loader.hide();
            }
        });
    },
    create: function () {
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
                    Demo.Ingame.Result.show(result);
                    setTimeout(function () { window.location.replace("/Category/Index/"); }, 3000);
                }
                Demo.Ingame.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!");
                Demo.Ingame.Loader.hide();
            }
        });
    },
    edit: function () {
        Demo.Ingame.Loader.show();
        var form = $("#form-category");
        $.ajax({
            url: "/Category/Edit",
            type: 'POST',
            dataType: "json",
            data: form.serialize(),
            cache: false,
            success: function (result) {
                if (result.isSuccess) {
                    Demo.Ingame.Result.show(result);
                    setTimeout(function () { window.location.replace("/Category/Index/"); }, 3000);
                }
                Demo.Ingame.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!");
                Demo.Ingame.Loader.hide();
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
    },
    categoryTree: function () {
        Demo.Ingame.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Category/CategoryTreePage",
            data: null,
            cache: false,
            success: function (result) {
                $("#categoryTree").html(result.html);
                Demo.Ingame.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!");
                Demo.Ingame.Loader.hide();
            }
        });
    },
    categoryTreeList: function () {
        Demo.Ingame.Loader.show();
        $.ajax({
            type: 'GET',
            url: "/Category/CategoryTreeAsync",
            data: null,
            cache: false,
            success: function (result) {
                $("#categoryTreeList").html(result.html);
                Demo.Ingame.Loader.hide();
            },
            error: function (xhr, status, error) {
                alert("Bir problem oluştu!");
                Demo.Ingame.Loader.hide();
            }
        });
    }
};