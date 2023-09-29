function RegisterInstituition() {
    let properties = {
        Name: $("#instituition-name").val(),
    };
    $.post("/Instituition/Register", properties)

        .done(function (output) {
            if (output.stats == "OK") {
                $(location).attr('href', '/Instituition/Index');

            } else if (output.stats == "ERROR") {
                $(".field-validation-error").html('Informe uma descrição para o órgão');
            }
        })

        .fail(function () {
            alert("Ocorreu um erro!");
        });
}

$(document).ready(function () {
    $("#instituition-form").submit(function (e) {
        e.preventDefault();
        RegisterInstituition();
    });
});





function RegisterDivision() {
    let properties = {
        Name: $("#division-name").val(),
        InstituitionId: $("#fk-instituition").val(),
    };
    $.post("/Division/Register", properties)

        .done(function (output) {
            if (output.stats == "OK") {
                $(location).attr('href', '/Division/Index');

            } else if (output.stats == "ERROR") {
                $(".field-validation-error").html('Informe uma descrição para a divisão!');
            }
        })

        .fail(function () {
            alert("Ocorreu um erro!");
        });
}

$(document).ready(function () {
    $("#division-form").submit(function (e) {
        e.preventDefault();
        RegisterDivision();
    });
});





// function EditDivision() {
//     let properties = {
//         Id: $("#edit-division-id").val(),
//         Name: $("#edit-division-name").val(),
//         InstituitionId: $("#edit-fk-instituition").val(),
//     };
//     $.post("/Division/Edit", properties)

//         .done(function (output) {
//             if (output.stats == "OK") {
//                 $(location).attr('href', '/Division/Index');

//             } else if (output.stats == "ERROR") {
//                 $(".field-validation-error").html('Informe uma descrição para a divisão!');
//             }
//         })

//         .fail(function () {
//             alert("Ocorreu um erro!");
//         });
// }

// $(document).ready(function () {
//     $("#edit-division-form").submit(function (e) {
//         e.preventDefault();
//         EditDivision();
//     });
// });