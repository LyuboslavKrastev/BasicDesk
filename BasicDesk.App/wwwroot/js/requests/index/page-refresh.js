function setRefresh(miliseconds) {
    $("timeSelector option:selected").attr("selected");
    location.reload();
    setInterval("location.reload(true)", miliseconds);
}

var timer = 0;
function startTimer() {
    setInterval("timerUp()", 1000);
}

function timerUp() {
    timer++;
    var resetat = 100000000000; //change this number to adjust the length of time in seconds
    if (timer == resetat) {
        window.location.reload();
    }
    var tleft = resetat - timer;
    document.getElementById('timer').innerHTML = tleft;
}

$(document).ready(startTimer());