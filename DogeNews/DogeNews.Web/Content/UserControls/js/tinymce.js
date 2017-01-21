(function () {
    'use strict';

    function init() {
        tinymce.init({
            selector: 'textarea',
            plugins: [
            'advlist autolink lists link image charmap print preview hr anchor pagebreak',
            'searchreplace wordcount visualblocks visualchars code fullscreen',
            'insertdatetime media nonbreaking save table contextmenu directionality',
            'emoticons template paste textcolor colorpicker textpattern imagetools codesample toc'
            ],
            toolbar1: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            toolbar2: 'print preview media | forecolor backcolor emoticons | codesample',
        });

        let $textArea = $("#tinymce-textarea");
        $('.mce-container').keypress(() => {
            let html = tinyMCE.activeEditor.getContent();
            $textArea.text(html);
        });
    }

    init();
}());