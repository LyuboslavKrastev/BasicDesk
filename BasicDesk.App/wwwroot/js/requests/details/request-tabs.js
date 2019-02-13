$('#btn_res').on('click', function () {
    $('#history').hide();
    $('#approvals').hide();
    $('#request').hide();
    $('#resolution').show();
    $('#btn_desc').removeClass('btn-danger');
    $('#btn_appr').removeClass('btn-danger');
    $('#btn_hist').removeClass('btn-danger');
    $('#btn_res').addClass('btn-danger');
});

$('#btn_hist').on('click', function () {
    $('#request').hide();
    $('#approvals').hide();
    $('#resolution').hide();
    $('#history').show();
    $('#btn_desc').removeClass('btn-danger');
    $('#btn_appr').removeClass('btn-danger');
    $('#btn_res').removeClass('btn-danger');
    $('#btn_hist').addClass('btn-danger');
});

$('#btn_desc').on('click', function () {
    $('#history').hide();
    $('#approvals').hide();
    $('#resolution').hide();
    $('#request').show();
    $('#btn_res').removeClass('btn-danger');
    $('#btn_appr').removeClass('btn-danger');
    $('#btn_hist').removeClass('btn-danger');
    $('#btn_desc').addClass('btn-danger');
});

$('#btn_appr').on('click', function () {
    $('#history').hide();
    $('#resolution').hide();
    $('#request').hide();
    $('#approvals').show();
    $('#btn_res').removeClass('btn-danger');
    $('#btn_hist').removeClass('btn-danger');
    $('#btn_desc').removeClass('btn-danger');
    $('#btn_appr').addClass('btn-danger');

});