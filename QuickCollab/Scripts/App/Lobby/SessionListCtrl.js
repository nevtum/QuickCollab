(function () {

    SessionList = function ($scope, $http, AuthService, ContentHub) {

        $scope.authenticate = function (session) {
            // show modal view to fill in password
            console.log("open modal dialog to enter password " + session.Uri);

            // post registration on click of ok button
            // if failed display error message
            // if passed display verified icon then close modal dialog
            var authenticated = AuthService.verify("password", session.SessionName);

            if (authenticated === true)
                console.log("success!");
            else {
                session.IsUserAuthorized = true;
                console.log("fail! Just verify with AuthService to make sure!");
            }

        };

        $scope.join = function (session) {
            // add session to current tabs open
            // subscribe to signalr chat room
            // make sure to not allow to open another tab if same session open already

            if (!session.IsUserAuthorized)
                console.log("Not authorized to join " + session.SessionName);
            else
                ContentHub.join(session.SessionName);
        };

        $http.get('../api/Sessions').success(function (data) {
            $scope.sessions = data;
        });
    }

    angular
        .module('Lobby.Controllers', ['chatApp.Security', 'chatApp.SignalR'])
        .controller('SessionListCtrl', ['$scope', '$http', 'AuthService', 'ContentHub', SessionList]);
})();