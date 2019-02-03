$("#noteBtn").click(function (event) {
    checkCheckboxes();
    debugger;
    let ids = [];
    let data = getCheckedIds(event);
    for (let id of data) {
        ids.push(id)
    };
    let url = "/requests/addnotefromtable";
    let noteDescription = $('#noteDescription').val();


    $.post(url, { '': ids, noteDescription }, function (data) {
        window.location.assign(data.redirectUrl)
    });
});