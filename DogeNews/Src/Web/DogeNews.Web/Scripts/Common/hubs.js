$(function () {
    'use strict';

    $.ajax({
        url: "/signalr/hubs",
        dataType: "script",
        async: false
    });
}());