$(document).ready(function () {
    let maxLength = 20;
    let subjects = $(".subject").each(function () {
        let subject = $(this);
        let text = subject.text();
        if (text.length > maxLength) {

            let modifiedText = text.substring(0, maxLength - 3) + '...';
            subject.text(modifiedText);
        }
    });
});