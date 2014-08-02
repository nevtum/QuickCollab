(function () {

    Routes = function ($routeProvider) {
        $routeProvider
            .when('/', { controller: 'SessionListCtrl', templateUrl: '../Templates/SessionList.html' })
            .otherwise({ redirectTo: '/' });
    }

    angular
        .module('chatApp', ['ngRoute', 'Lobby.Controllers'])
        .config(Routes);

})();

