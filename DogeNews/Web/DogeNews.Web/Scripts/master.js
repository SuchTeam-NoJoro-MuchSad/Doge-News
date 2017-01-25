(function () {
    'use strict';

    $(document).ready(() => {
        $('#btn-sidebar').sideNav({ menuWidth: 300 });
        $('#collapsible-admin').collapsible();

        $('#btn-search').on('click', () => {
            $('#tb-search').fadeIn(1000);
        });
    });
}());