$(function () {
    'use strict';

    const notificationType = {
        success: 'Success',
        error: 'Error',
        info: 'Info'
    },
        notificationTypeClass = {
            success: 'toast-success',
            info: 'toast-info',
            error: 'toast-error'
        };

    function initSignalR() {
        let connection = $.connection,
            hub = connection.notificationHub;

        hub.client.toast = (message, dellay, type) => {
            console.log('CALLEEEEEEEEEEEED')

            if (type === notificationType.success) {
                Materialize.toast(message, dellay, notificationTypeClass.success)
            } else if (type === notificationType.info) {
                Materialize.toast(message, dellay, notificationTypeClass.info)
            } else {
                Materialize.toast(message, dellay, notificationTypeClass.error)
            }
        };

        connection.hub.start().done(() => hub.server.init());
    }

    initSignalR();
}());