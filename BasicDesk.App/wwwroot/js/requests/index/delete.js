$("#deleteReq").click(function (event) {
    checkCheckboxes();
    let ids = [];
    let data = getCheckedIds(event);
    for (let id of data) {
        ids.push(id)
    };
    let url = "/requests/delete";
    $.post(url, { '': ids }, function (data) {
        window.location.assign(data.redirectUrl)
    });
});