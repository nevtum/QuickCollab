var chatApp = angular.module('chatApp', ['ngRoute', 'Lobby.Controllers']);

chatApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', { controller: 'SessionListCtrl', templateUrl: '../Templates/SessionList.html' })
        .otherwise({ redirectTo: '/' });
});