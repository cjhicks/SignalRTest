/// <reference path="../jquery-1.10.2.min.js" />
/// <reference path="../knockout-3.4.0.js" />
/// <reference path="../jquery.signalR-2.2.0.js" />

var HomeViewModel = function() {
    var self = this;

    self.allMessage = ko.observable("");
    self.othersMessage = ko.observable("");

    self.signalRData = ko.observableArray([]);

    // this is our hub to communicate with SignalR
    self.notificationHub = $.connection.notificationHub;

    self.sendMessageToOthers = function() {
        self.notificationHub.server.clientMessage(false, self.othersMessage());
        self.othersMessage("");
    };

    self.sendMessageToAll = function () {
        self.notificationHub.server.clientMessage(true, self.allMessage());
        self.allMessage("");
    };

    // this method handles messages from the server
    self.notificationHub.client.sendMessage = function(message) {
        self.signalRData.push({ message: message });
    };

    // this works, just don't need the functionality right now
    //self.generateNewMessage = function() {
    //    $.ajax({
    //        url: "/home/fakepost",
    //        type: "POST",
    //        contentType: "application/json",
    //        dataType: "json",
    //        data: JSON.stringify({ fakeData : 1 }),
    //        success: function() {
    //            //self.signalRData.push({ message: "POST success" });
    //        },
    //        error: function() {
    //            //self.signalRData.push({ message: "POST failure" });
    //        }
    //    });
    //};

    // Start the connection
    $.connection.hub.start()
        .done(function() {
            self.signalRData.push({ message: "Now connected, connection ID=" + $.connection.hub.id });
        });
};

ko.applyBindings(HomeViewModel);