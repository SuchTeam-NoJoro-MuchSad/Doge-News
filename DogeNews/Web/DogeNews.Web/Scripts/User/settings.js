(function () {
    'use strict';

    document
        .getElementById('MainContent_Message')
        .addEventListener('DOMAttrModified', e => {
            if (e.target.style.display !== 'none') {
                setTimeout(() => $this.hide(), 3000);
            }
        });
} ());