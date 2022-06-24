// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    $('.logout').click(function (event) {
        var url = 'Game/Logout'
        $.get(url).done(function (data) {
            location.reload();
        })
    })

    var PlaceHolderElement = $('#PlaceHolderHere');
    $('.addGame').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    $('.editGame').click(function (event) {
        var reqData = $(this).attr('value');
        var url = $(this).data('url') + '/' + reqData;

        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    var deleteId;
    $('.removeGame').click(function (event) {
        var url = $(this).data('url');
        deleteId = $(this).attr('value');

        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-dismiss="modal"]', function (event) {
        PlaceHolderElement.find('.modal').modal('hide');
    })

    PlaceHolderElement.on('click', '.model-view', function (event) {
        var form = $(this).parents('.modal').find('form');
        var url = form.attr('action');
        var reqData = form.serialize();

        $.ajax({
            type: "POST",
            url: url,
            data: reqData,
            success: function (data) {
                if (data != null) {
                    PlaceHolderElement.find('.modal').modal('hide');
                    location.reload();
                }
            },
            error: function (data) {
                alert("Data is invalid!")
            }
        });
    })

    PlaceHolderElement.on('click', '.confirm-delete', function (event) {
        var url = 'Game/Delete';

        $.post(url, { id: deleteId }).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
            location.reload();
        })
    })
})

