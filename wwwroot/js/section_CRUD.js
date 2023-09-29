function RegisterSection() {
    let properties = {
        Name: $("#section-name").val(),
        DivisionId: $("#fk-division").val(),
    };
    $.post("/Section/Register", properties)

        .done(function (output) {
            if (output.stats == "OK") {
                $(location).attr('href', '/Section/Index');

            } else if (output.stats == "ERROR") {
                $(".field-validation-error").html('Informe uma descrição para a sessão!');
            }
        })

        .fail(function () {
            alert("Ocorreu um erro!");
        });
}

$(document).ready(function () {
    $("#section-form").submit(function (e) {
        e.preventDefault();
        RegisterSection();
    });
});




// function EditSection() {
//     let properties = {
//         Id: $("#edit-section-id").val(),
//         Name: $("#edit-section-name").val(),
//         DivisionId: $("#edit-fk-division").val(),
//     };
//     $.post("/Section/Edit", properties)

//         .done(function (output) {
//             if (output.stats == "OK") {
//                 $(location).attr('href', '/Section/Index');

//             } else if (output.stats == "ERROR") {
//                 $(".field-validation-error").html('Informe uma descrição para a sessão!');
//             }
//         })

//         .fail(function () {
//             alert("Ocorreu um erro!");
//         });
// }

// $(document).ready(function () {
//     $("#edit-section-form").submit(function (e) {
//         e.preventDefault();
//         EditSection();
//     });
// });