$('.table-style').show(function () {
    getDatatableClass('.table-style')
});

/* função de configuração do plugin data-table e aplicação do plugin de acordo com a classe capturada */
function getDatatableClass(tableClass) {
    $(tableClass).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}

/* Evento para fechar o alert de secesso ou erro das validações */
$('.close-alert').click(function () {
    $('.alert').hide('hide')
});

/* 
    esta dnção também realiza o fechamneto de alert porém de forma automática utilizando o setTimeout();

    // setTimeout(function () {
    //     $('.alert').hide('hide').fadeOut(500);
    // }, 1000);

*/

/* função de exbir e ocultar senha na tela de login */
document.addEventListener("DOMContentLoaded", function () {

    const passwordInput = document.getElementById("login-password");
    const togglePasswordButton = document.getElementById("toggle-password");
    const passwordIcon = document.getElementById("password-icon");

    if (togglePasswordButton != null) {
        togglePasswordButton.addEventListener("mousedown", function () {

            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                passwordIcon.classList.remove("bi-eye-slash-fill");
                passwordIcon.classList.add("bi-eye-fill");
            }
        });
    }

    if (togglePasswordButton != null) {
        togglePasswordButton.addEventListener("mouseup", function () {
            if (passwordInput.type === "text") {
                passwordInput.type = "password";
                passwordIcon.classList.remove("bi-eye-fill");
                passwordIcon.classList.add("bi-eye-slash-fill");
            }
        });
    }

});


/* Phone-Mask */
const handlePhone = (event) => {
    let input = event.target
    input.value = phoneMask(input.value)
}

const phoneMask = (value) => {
    if (!value) return ""
    value = value.replace(/\D/g, '')
    value = value.replace(/(\d{2})(\d)/, "($1) $2")
    value = value.replace(/(\d)(\d{4})$/, "$1-$2")
    return value
}
/* Phone-Mask */
