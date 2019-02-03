$("#searchIcon").on('click', function () {
    let searchBar = $("#searchBar");
    if (searchBar.is(':hidden')) {
        searchBar.show();
    } else {
        searchBar.hide();
    }
});