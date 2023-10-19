$('.evaluator-register').show(function () {

    $('select').val('');

    $('.evaluator-instituition').change(function () {
        var selectedInstituitionId = $(this).val();
        if (selectedInstituitionId) {
            $.get('/UserEvaluator/GetDivisionsByInstituition', { instituitionId: selectedInstituitionId }, function (data) {
                $('.evaluator-division').empty();
                $('.evaluator-division').append($('<option>').text(' -- selecione a divisão -- ').val(''));
                $.each(data, function (index, item) {
                    $('.evaluator-division').append($('<option>').text(item.text).val(item.value));
                });
            });
        } else {
            $('.evaluator-division').empty();
            $('.evaluator-division').append($('<option>').text(' -- selecione a divisão -- ').val(''));
        }
    })

    $('.evaluator-division').change(function () {
        var selectedSectionId = $(this).val();
        if (selectedSectionId) {
            $.get('/UserEvaluator/GetSectionsByDivisions', { DivisionId: selectedSectionId }, function (data) {
                $('.evaluator-section').empty();
                $('.evaluator-section').append($('<option>').text(' -- selecione a seção -- ').val(''));
                $.each(data, function (index, item) {
                    $('.evaluator-section').append($('<option>').text(item.text).val(item.value));
                });
            });
        } else {
            $('.evaluator-section').empty();
            $('.evaluator-section').append($('<option>').text(' -- selecione a seção -- ').val(''));
        }
    });

    $('.evaluator-section').change(function () {
        var selectedSectorId = $(this).val();
        if (selectedSectorId) {
            $.get('/UserEvaluator/GetSectorsBySections', { SectionId: selectedSectorId }, function (data) {
                $('.evaluator-sector').empty();
                $('.evaluator-sector').append($('<option>').text(' -- selecione o setor -- ').val(''));
                $.each(data, function (index, item) {
                    $('.evaluator-sector').append($('<option>').text(item.text).val(item.value));
                });
            });
        } else {
            $('.evaluator-sector').empty();
            $('.evaluator-sector').append($('<option>').text(' -- selecione o setor -- ').val(''));
        }
    });

});