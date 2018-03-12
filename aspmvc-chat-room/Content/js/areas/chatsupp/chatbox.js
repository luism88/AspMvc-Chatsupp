$(function () {

    var visitorData = null;

    var visitorHub = null;

    var createChatBox = function () {
        $.ajax({
            url: "/Chatsupp/Visitor/ChatBoxVisitor",
            type: "GET",
            cache: false,
            success: function (resp) {
                $("body").append(resp);
                $("#btnNameOk").click(registerVisitor);
                $("#chat form").submit();
               
            }
        })
    }

    var sendMessage = function () {
        visitorHub.server.registerUser(name);
    }

    var registerVisitor = function () {
        console.log("gato")
        var name = $("input[name='visitorName']").val();
        visitorHub.server.registerUser(name);
    }
   
    var init = function () {

        // Reference the auto-generated proxy for the hub.
        visitorHub = $.connection.visitorHub;

        // Start the connection.
        $.connection.hub.start().done(function () {
            console.log("successful connection");
           
            createChatBox();
            //visitorHub.server.registerUser("anonanonymous");
        });

        //ChtuHub callbacks
        visitorHub.client.receiveMessage = function (agentData, message) {
            createChatBox()
        };

        visitorHub.client.registerResult = function (visitorData) {

            visitorData = visitorData;
            //aca agregar un coockie con el datos del usuario
            console.log(visitorData);
            $("input[name='message']").attr("disabled", false);
        };


    }

    init();

});

/*
 // Reference the auto-generated proxy for the hub.
            var chat = $.connection.visitorHub;
            // Create a function that the hub can call back to display messages.
            chat.client.addNewMessageToPage = function (name, message) {
                // Add the message to the page.
                $('#discussion').append('<li><strong>' + htmlEncode(name)
                    + '</strong>: ' + htmlEncode(message) + '</li>');
            };

            chat.client.registerResult = function (userId) {
                // Add the message to the page.
                alert("userId: " + userId);
            };
            // Get the user name and store it to prepend to messages.

            // Set initial focus to message input box.
            $('#message').focus();
            // Start the connection.
            $.connection.hub.start().done(function () {
                $('#sendmessage').click(function () {
                    // Call the Send method on the hub.
                    chat.server.send($('#displayname').val(), $('#message').val());
                    // Clear text box and reset focus for next comment.
                    $('#message').val('').focus();
                });
                //register user
                $('#displayname').val(function () {
                    var nick = prompt('Enter your name:', '');
                    chat.server.registerUser(nick);
                    return nick;
                })
            });


*/