$(function () {
   // $("#areaCkEditor").text("hello!");
   
    ClassicEditor
        .create(document.querySelector('#editor'), {
            language: {
                ui: 'en',
                content: 'ar'
            }
        })
        .catch(error => {
            console.error(error);
        });
        //////
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