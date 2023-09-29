function RegisterSector() {
    let properties = {
        Name: $("#sector-name").val(),
        SectionId: $("#fk-section").val(),
    };
    $.post("/Sector/Register", properties)

        .done(function (output) {
            if (output.stats == "OK") {
                $(location).attr('href', '/Sector/Index');

            } else if (output.stats == "ERROR") {
                $(".field-validation-error").html('Informe uma descrição para o setor!');
            }
        })

        .fail(function () {
            alert("Ocorreu um erro!");
        });
}

$(document).ready(function () {
    $("#sector-form").submit(function (e) {
        e.preventDefault();
        RegisterSector();
    });
});






// function EditSector() {
//     let properties = {
//         Id: $("#edit-sector-id").val(),
//         Name: $("#edit-sector-name").val(),
//         SectionId: $("#edit-fk-section").val(),
//     };
//     $.post("/Sector/Edit", properties)

//         .done(function (output) {
//             if (output.stats == "OK") {
//                 $(location).attr('href', '/Sector/Index');

//             } else if (output.stats == "ERROR") {
//                 $(".field-validation-error").html('Informe uma descrição para o setor!');
//             }
//         })

//         .fail(function () {
//             alert("Ocorreu um erro!");
//         });
// }

// $(document).ready(function () {
//     $("#edit-sector-form").submit(function (e) {
//         e.preventDefault();
//         EditSector();
//     });
// });