(function () {

    function AuthService($http) {
        this.verify = authenticate;

        var authenticate = function (userName, password, sessionId) {
            // Authenticate user in back end
            // return true if successful
            // return false otherwise

            data = {
                sessionId: sessionId,
                password: password,
            };

            //$http.post()

            console.log(userName);
            console.log(password);
            console.log(sessionId);
        };
    }

    angular
      .module('chatApp.Security', [])
      .service('AuthService', AuthService);

})();

