$(document).ready(function () {
    var userId = ''
    var toUserId
    var selected
    //group
    var selectedGroup
    var groupID
    var pConnectedUsers = []
    isGroup = false
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
            isGroup = false
            $('#input-message').show()
            $('#btn-send').show()
            if (selected != name) {
                $("#conversation").text('')
            }
            selected = name
            selectedGroup = ' '
            //toUserId = userId
            currentConversion(name, img)

            var currentUserYouTalkToHimConID = pConnectedUsers.filter(x => x.UserName == name)
            if (currentUserYouTalkToHimConID[0] == undefined) {
                //User is deisconected
                console.log("No coneection")
            }else{
                //User is connected
                console.log(currentUserYouTalkToHimConID[0].UserName)
                toUserId = currentUserYouTalkToHimConID[0].ConnectionId
            }

            $.connection.hub.start().done(function () {
                hub.server.getPrivateMessage(selected)
            })
        })
    }

    //Event click inside this method
    function addGroup(name,groupid,numberOfMember, img) {
        if (groupID != groupid) {
            if (img == null) {
                img = 'Content/img/Group.png'
            }
            var tag = '<div class="row sideBar-body" id="' + underscoreBS(name) + '"><div class="col-sm-3 col-xs-3 sideBar-avatar"><div class="avatar-icon"><img src="' + img + '" /></div></div><div class="col-sm-9 col-xs-9 sideBar-main"><div class="row"><div class="col-sm-8 col-xs-8 sideBar-name"><span class="name-meta">' + name + '</span></div><div class="col-sm-4 col-xs-4 pull-right sideBar-time"><span class="badge badge-primary" style="background:#3E9EF8"><span class="glyphicon glyphicon-user" style="color:white"> '+numberOfMember+'</span></span></div></div></div></div>'
            $("#sideBar").append(tag)
        
            $('#' + underscoreBS(name)).click(function () {
                groupID = groupid
                isGroup = true
                $('#input-message').show()
                $('#btn-send').show()
                if (selectedGroup != name) {
                    $("#conversation").text('')
                }
                selectedGroup = name
                selected = ' '
                //toUserId = userId
                currentConversion(name, img)

                //var currentUserYouTalkToHimConID = pConnectedUsers.filter(x => x.UserName == name)
                //if (currentUserYouTalkToHimConID[0] == undefined) {
                //    //User is deisconected
                //    console.log("No coneection")
                //}else{
                //    //User is connected
                //    console.log(currentUserYouTalkToHimConID[0].UserName)
                //    toUserId = currentUserYouTalkToHimConID[0].ConnectionId
                //}

                $.connection.hub.start().done(function () {
                    hub.server.getGroupMessage(groupid)
                })
            })
        }
        
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

    //To make name at Id works when click
    function underscoreBS(name){
        var str = name;
        var replacedWithUnderScore = str.split(' ').join('_');
        var replaceWithWQuestionMark = replacedWithUnderScore.split('?').join('');
        return replaceWithWQuestionMark
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
        connectedUsers.forEach(function (cuser) {
            pConnectedUsers.push({
                ConnectionId: cuser["ConnectionId"],
                UserName: cuser["UserName"]
            })
        })
        //for (i = 0; i < connectedUsers.length; i++) {
        //    if (connectedUsers[i].UserName != currentUser) {
        //        addUserContact(connectedUsers[i].UserName, connectedUsers[i].ConnectionId);
        //    }
            
        //}
    }

    hub.client.userContact = function (usersContact) {
        usersContact.forEach(function (user) {
            addUserContact(user["UserSelected"])
        })
    }

    hub.client.onNewUserConnected = function (id ,newUser) {
        //addUserContact(newUser, id)
        console.log("wlecome " + newUser)
        pConnectedUsers.push({
            ConnectionId: id,
            UserName: newUser
        })
    }

    hub.client.onUserDisconnected = function (id,userName) {
        //deleteUserContact(userName)
        console.log(userName + " Is out")

        $.each(pConnectedUsers, function (i) {
            if (pConnectedUsers[i].UserName === userName) {
                pConnectedUsers.splice(i, 1);
                return false;
            }
        });
    }

    hub.client.recivePrivateMessageOthers = function (id, user, message) {
        //console.log(id)
        //console.log(user)
        //console.log(message)
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
            rpmGroup = ''
            messages.forEach(function (msg) {
                if (msg["Sender"] == currentUser) {
                    sender(msg["Message"], currentUser)
                } else {
                    reciver(msg["Message"], msg["Sender"])
                }
            })
        }else{
            console.log("Double click")
        }
    }

    //Group When click get data from server
    var rpmGroup = ''
    hub.client.reciveGroupMessageWhenClick = function(messages,currnetUserName,gId){
        if(rpmGroup != gId){
            rpmGroup = gId
            rpmName = ''
            console.log(gId)
            messages.forEach(function(msg){
                if (msg["Sender"]["UserName"] == currnetUserName) {
                    sender(msg["Message"], currnetUserName)
                }else{
                    reciver(msg["Message"],msg["Sender"]["UserName"])
                }
            })
        }else{
            console.log("Double click " + rpmGroup)
        }
    }

    //Get Groups
    hub.client.groups = function(groups,numberOfUsers){

        groups.forEach(function(group){
            var nOfU = numberOfUsers.filter(n => n.RoomID == group["RoomID"])
            addGroup(group["Room"]["Name"],group["RoomID"],nOfU.length)
        })
    }

    hub.client.reciveGroupMessageCaller = function(cuser, msg){
        $("#conversation").append(sender(msg, cuser));
    }
   
    //Server
    $.connection.hub.start().done(function () {

        $('#btn-send').click(function () {
            var message = $("#input-message").val()
            if(message != ""){
                if (isGroup) {
                    console.log(groupID)
                    hub.server.sendToGroup(message,groupID)
                }else{
                    hub.server.sendPrivateMessage(selected, toUserId, message);
                }

                
                $("#input-message").val('')
            }
            
        })

        $('#input-message').on('keypress', function (e) {
            if (e.which === 13) {

                var message = $("#input-message").val()
                if (message != "") {
                    hub.server.sendPrivateMessage(selected, toUserId, message);
                    $("#input-message").val('')
                }
                //Disable textbox to prevent multiple submit
                $(this).attr("disabled", "disabled");

                //Do Stuff, submit, etc..

                //Enable the textbox again if needed.
                $(this).removeAttr("disabled");
            }
        });


    })
})


