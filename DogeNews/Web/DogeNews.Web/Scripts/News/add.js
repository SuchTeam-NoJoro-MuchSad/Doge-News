(function () {
    'use strict';

    $(document).ready(() => $('select').material_select());
} ());

function hideMessage() {
    let seconds = 3;
    setTimeout(() => $('#MessageContainer').hide(), seconds);
}