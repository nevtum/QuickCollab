var chatApp = angular.module('chatApp', ['ngRoute']);

chatApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', { controller: 'SessionListCtrl', templateUrl: '../Templates/SessionList.html' })
        .otherwise({ redirectTo: '/' });
});

chatApp.controller('SessionListCtrl', ['$scope', '$http', function ($scope, $http, $location) {
    $scope.sessions = [
        { DateCreated: "12/12/12", SessionName: "The coolies", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "Wassup!", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "--== Naruto Fans ==--", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: true, Secured: false },
        { DateCreated: "12/12/12", SessionName: "Fort Knox", PersistHistory: true, Secured: true, Uri: '/Fort%Knox'},
        { DateCreated: "12/12/12", SessionName: "My mockup Session ABC", PersistHistory: false, Secured: false },
        { DateCreated: "12/12/12", SessionName: "Testing testing", PersistHistory: true, Secured: true, Uri: '/Testing%Testing' },
        { DateCreated: "12/12/12", SessionName: "Hey there!", PersistHistory: true, Secured: false },
    ];

    $scope.authenticate = function (session) {
        // show modal view to fill in password
        // post registration on click of ok button
        // if failed display error message
        // if passed display verified icon then close modal dialog
        console.log("open authentication dialog for session " + session.Uri);
        console.log(session);
    };

    $scope.open = function (session) {
        // add session to current tabs open
        // subscribe to signalr chat room
        // make sure to not allow to open another tab if same session open already
        console.log("Joining " + session.SessionName);
    };
}]);