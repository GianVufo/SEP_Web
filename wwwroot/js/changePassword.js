function ChangeUserPass() {
    let properties = {
        Id: $("#password-Id").val(),
        Password: $("#changedPass").val(),
        ComparePassword: $("#confPass").val(),
    };
    $.post("/UserAdministrator/ChangeUserPassword", properties)

        .done(function (output) {
            if (output.stats == "OK") {
                $(location).attr('href', '/UserAdministrator/Edit/' + properties.Id);
            } else if (output.stats == "ERROR") {

                if (properties.Password == "") {
                    $(".field-validation-error").html('Informe uma senha!');
                } else {
                    $(".field-validation-error").html('');
                }
                if (properties.ComparePassword == "") {
                    $(".compPass").html('Confirme sua senha!');
                } else {
                    $(".compPass").html('');
                }

            } else if (output.stats == "INVALID") {
                $(".field-validation-error").html('');
                $(".compPass").html('As senhas diferentes !');
                return false;
            }
        })

        .fail(function () {
            alert("Ocorreu um erro!");
        });
}

$(document).ready(function () {
    $("#passwordForm").submit(function (e) {
        e.preventDefault();
        ChangeUserPass();
    });
});