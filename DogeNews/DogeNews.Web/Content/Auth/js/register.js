(function () {
    'use strict';

    function addErrorMessagesClasses() {
        let errorMessages = $('#registrationFormContainer span');
        let classes = [
            'usernameErrorMessage',
            'firstnameErrorMessage',
            'lastnameErrorMessage',
            'emailErrorMessage',
            'passwordErrorMessage',
            'confirmPasswordErrorMessage'
        ];

        let classIndex = 0;
        for (let i = 0, len = errorMessages.length; i < len; i += 1) {
            if (i != 0 && i % 2 === 0) {
                classIndex += 1;
            }

            $(errorMessages[i]).addClass(`${classes[classIndex]} error-message-container`);                    
        }

        console.log(errorMessages.length);
    }

    addErrorMessagesClasses();
}());