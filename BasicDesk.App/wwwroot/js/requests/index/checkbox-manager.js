function checkCheckboxes(event) {
    var anyBoxesChecked = false;
    $('input[type="checkbox"]').each(function () {
        if ($(this).is(":checked")) {
            anyBoxesChecked = true;
        }
    });

    if (anyBoxesChecked == false) {
        alert('Please select request[s] to delete');
        event.preventDefault();
        event.stopPropagation();
    }
}

function checkCheckboxesForMerge(event) {
    var checkedCount = 0;
    $('input[type="checkbox"]').each(function () {
        if ($(this).is(":checked")) {
            checkedCount++;
        }
    });
    if (checkedCount < 2) {
        alert('Please select request[s] to merge');
        event.preventDefault();
        event.stopPropagation();
    }
}

$("#checkAll").click(function () {
    $(".check").prop('checked', $(this).prop('checked'));
});

function getCheckedIds(event) {
    event.preventDefault();
    var checkedIds = $("input:checkbox:checked").map(function () {
        return $(this).val();
    }).get();

    return checkedIds;
};