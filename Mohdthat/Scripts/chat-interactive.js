$(document).ready(function () {
    var userId = ''
    var toUserId
    var selected
    var tabs = []

    $('#input-message').hide()
    $('#btn-send').hide()
    //$('#img-logo').css('background', 'url(Content/img/Logo.png) no-repeat')
    var hub = $.connection.chatHub;

    //Event click inside this method
    function addUserContact(name,userId, img) {
        if (img == null) {
            img = 'Content/img/default-avatar.png'
        }
        var tag = '<div class="row sideBar-body" id="' + name + '"><div class="col-sm-3 col-xs-3 sideBar-avatar"><div class="avatar-icon"><img src="' + img + '" /></div></div><div class="col-sm-9 col-xs-9 sideBar-main"><div class="row"><div class="col-sm-8 col-xs-8 sideBar-name"><span class="name-meta">' + name + '</span></div><div class="col-sm-4 col-xs-4 pull-right sideBar-time"><span class="time-meta pull-right">18:1s</span></div></div></div></div>'
        $("#sideBar").append(tag)
        $('#' + name).click(function () {
            $('#input-message').show()
            $('#btn-send').show()
            if (selected != name) {
                $("#conversation").text('')
            }
            selected = name
            toUserId = userId
            currentConversion(name, img)
            $.connection.hub.start().done(function () {
                hub.server.getPrivateMessage(selected)
            })
        })
    }

    function deleteUserContact(name) {
        $('#'+name).remove()
    }

    function currentConversion(name, img) {
        if (img == null) {
            img = 'Content/img/default-avatar.png'
        }
        $('#current-conversion-image').attr('src', img);
        $('#current-conversion-name').text(name)
    }

    function currentUser(name, img) {
        if (img == null) {
            img = 'Content/img/default-avatar.png'
        }
        $('#current-user-image').attr('src', img);
        $('#current-user-name').text(name)
    }

    function sender(msg,sender) {
        var tag = '<div class="row message-body"><div class="col-sm-12 message-main-sender"><div class="sender"><div class="message-text">' + msg + '</div><span class="message-time pull-right"> ' + sender + '</span></div></div></div>'
        $("#conversation").append(tag);
        scrollDown()
    }

    function reciver(msg, sender) {
        var tag = '<div class="row message-body"><div class="col-sm-12 message-main-receiver"><div class="receiver"><div class="message-text">'+ msg +'</div><span class="message-time pull-right">'+sender+'</span></div></div></div>'
        $("#conversation").append(tag);
        scrollDown()
    }


    //scrollDown
    function scrollDown() {
        $('#conversation').animate({
            scrollTop: $('#conversation').get(0).scrollHeight
        }, 0);
    }

    hub.client.currentUser = function (userName) {
        currentUser(userName)
    }

    hub.client.onConnected = function (id, currentUser, connectedUsers) {
        console.log(connectedUsers)
        for (i = 0; i < connectedUsers.length; i++) {
            if (connectedUsers[i].UserName != currentUser) {
                addUserContact(connectedUsers[i].UserName, connectedUsers[i].ConnectionId);
            }
            
        }
    }

    hub.client.onNewUserConnected = function (id ,newUser) {
        addUserContact(newUser, id)
    }

   
    hub.client.onUserDisconnected = function (id,userName) {
        deleteUserContact(userName)
    }

    hub.client.recivePrivateMessageOthers = function (id, user, message) {
        console.log(id)
        console.log(user)
        console.log(message)
        if (user == selected) {
            $("#conversation").append(reciver(message, user));
        }

    }

    hub.client.recivePrivateMessageCaller = function (user, message) {
        $("#conversation").append(sender(message, user));
    }
    var rpmName = ''
    hub.client.recivePrivateMessageWhenClick = function (messages, currentUser, toUser) {
        if (rpmName != toUser) {
            rpmName = toUser
            messages.forEach(function (msg) {
                if (msg["Sender"] == currentUser) {
                    sender(msg["Message"], currentUser)
                } else {
                    reciver(msg["Message"], msg["Sender"])
                }
            })
        }
    }


    
   
    //Server
    $.connection.hub.start().done(function () {

        $('#btn-send').click(function () {
            var message = $("#input-message").val()
            
            hub.server.sendPrivateMessage(selected,toUserId, message);
            $("#input-message").val('')
        })

    })
})


