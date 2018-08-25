var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection
    .start()
    .then(() => {
        $("#submit-button").click(e => {
            e.preventDefault();
            let username = $("#username").val();
            let question = $("#question").val();
            connection.invoke("PostQuestion", username, question);
        });

        connection.on("showQuestion", (user, question) => {
            $("#questions").append(
                $("<div>").html("<strong>" + user + "</strong>: " + question));
        });
    });