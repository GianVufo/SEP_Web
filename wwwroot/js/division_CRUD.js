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