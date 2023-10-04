$('#evaluator-register').show(function () {
    $('#evaluator-instituition').change(function () {
        var selectedInstituitionId = $(this).val();
        if (selectedInstituitionId) {
            $.get('GetDivisionsByInstituition', { instituitionId: selectedInstituitionId }, function (data) {
                $('#evaluator-division').empty();
                $('#evaluator-division').append($('<option>').text(' -- selecione uma divisão -- ').val(''));
                $.each(data, function (index, item) {
                    $('#evaluator-division').append($('<option>').text(item.text).val(item.value));
                });
            });
        } else {
            $('#evaluator-division').empty();
            $('#evaluator-division').append($('<option>').text(' -- selecione uma divisão -- ').val(''));
        }
    });
});

$('#evaluator-register').show(function () {
    $('#evaluator-division').change(function () {
        var selectedSectionId = $(this).val();
        if (selectedSectionId) {
            $.get('GetSectionsByDivisions', { DivisionId: selectedSectionId }, function (data) {
                $('#evaluator-section').empty();
                $('#evaluator-section').append($('<option>').text(' -- selecione uma seção -- ').val(''));
                $.each(data, function (index, item) {
                    $('#evaluator-section').append($('<option>').text(item.text).val(item.value));
                });
            });
        } else {
            $('#evaluator-section').empty();
            $('#evaluator-section').append($('<option>').text(' -- selecione uma seção -- ').val(''));
        }
    });
});

$('#evaluator-register').show(function () {
    $('#evaluator-section').change(function () {
        var selectedSectorId = $(this).val();
        if (selectedSectorId) {
            $.get('GetSectorsBySections', { SectionId: selectedSectorId }, function (data) {
                $('#evaluator-sector').empty();
                $('#evaluator-sector').append($('<option>').text(' -- selecione um setor -- ').val(''));
                $.each(data, function (index, item) {
                    $('#evaluator-sector').append($('<option>').text(item.text).val(item.value));
                });
            });
        } else {
            $('#evaluator-sector').empty();
            $('#evaluator-sector').append($('<option>').text(' -- selecione um setor -- ').val(''));
        }
    });
});