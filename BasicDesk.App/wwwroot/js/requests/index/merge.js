$("#mergeReq").click(function (event) {
    checkCheckboxesForMerge();
    let ids = [];
    let data = getCheckedIds(event);
    for (let id of data) {
        ids.push(id)
    };
    let url = "/requests/merge";

    $.ajax({
        url: url,
        type: "POST",
        data: {ids},
        success: function (data) {
            location.reload();
        }
    });
});
