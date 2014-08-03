(function () {

    function SignalRService() {

        //Starting connection
        $.connection.hub.start();

        var chat = $.connection.contentHub;

        this.join = function (sessionId) {
            chat.server.joinSession(sessionId);
        };

        this.leave = function (sessionId) {
            chat.server.leaveSession(sessionId);
        };

        this.sendMessage = function (sessionId, message) {
            chat.server.sendMessage(sessionId, message);
        };

        this.broadcastMessage = function (message) {
            chat.server.broadcastMessage(message);
        };

        this.clientBinding = chat.client;
    }

    angular
        .module('chatApp.SignalR', [])
        .service('ContentHub', SignalRService);

})();