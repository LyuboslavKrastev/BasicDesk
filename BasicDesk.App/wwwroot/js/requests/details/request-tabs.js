$('#btn_res').on('click', function () {
    $('#history').hide();
    $('#request').hide();
    $('#resolution').show();
    $('#btn_desc').removeClass('btn-danger');
    $('#btn_hist').removeClass('btn-danger');
    $('#btn_res').addClass('btn-danger');
});

$('#btn_hist').on('click', function () {
    $('#request').hide();
    $('#resolution').hide();
    $('#history').show();
    $('#btn_desc').removeClass('btn-danger');
    $('#btn_res').removeClass('btn-danger');
    $('#btn_hist').addClass('btn-danger');
});

$('#btn_desc').on('click', function () {
    $('#history').hide();
    $('#resolution').hide();
    $('#request').show();
    $('#btn_res').removeClass('btn-danger');
    $('#btn_hist').removeClass('btn-danger');
    $('#btn_desc').addClass('btn-danger');
});