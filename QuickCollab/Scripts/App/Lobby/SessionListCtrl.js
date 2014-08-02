﻿(function () {

    SessionList = function ($scope, $http, AuthService) {

        $scope.authenticate = function (session) {
            // show modal view to fill in password
            // post registration on click of ok button
            // if failed display error message
            // if passed display verified icon then close modal dialog
            console.log("open authentication dialog for session " + session.Uri);
            
            console.log(AuthService);

            if (AuthService.verify("hey", "myPassword", session.SessionName))
                console.log("success!");
            else
                console.log("fail!");
        };

        $scope.open = function (session) {
            // add session to current tabs open
            // subscribe to signalr chat room
            // make sure to not allow to open another tab if same session open already
            console.log("Joining " + session.SessionName);
        };

        $http.get('../api/SessionList').success(function (data) {
            $scope.sessions = data;
        });

    }

    angular
        .module('Lobby.Controllers', ['chatApp.Security'])
        .controller('SessionListCtrl', ['$scope', '$http', 'AuthService', SessionList]);
})();