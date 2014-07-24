var chatApp = angular.module('chatApp', ['ngRoute']);

chatApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', { controller: 'SessionListCtrl', templateUrl: '../Templates/SessionList.html' })
        .otherwise({ redirectTo: '/' });
});

chatApp.controller('SessionListCtrl', ['$scope', '$http', function ($scope, $http, $location) {
    $scope.sessions = [
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
    ];
}]);