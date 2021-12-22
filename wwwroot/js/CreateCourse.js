$(function () {
   // $("#areaCkEditor").text("hello!");
    ClassicEditor
        .create(document.querySelector('#editor'))
        .catch(error => {
            console.error(error);
        });

    //ClassicEditor
    //    .create(document.querySelector('#editor'), {
    //        plugins: [
    //            Autosave

    //            // ... other plugins
    //        ],

    //        Autosave: {
    //            save(editor) {
    //                $("#areaCkEditor").text(editor.getData());
    //            }
    //        },

    //        // ... other configuration options
    //    });


    

    
});