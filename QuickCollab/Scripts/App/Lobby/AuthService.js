(function () {

    function AuthService($http) {

        this.verify = function (userName, password, sessionId) {
            // Authenticate user in back end
            // return true if successful
            // return false otherwise

            data = {
                id: sessionId,
                password: password,
            };

            $http.post('../api/AuthorizeSession', data)
                .success(function (reponse) {
                    return true;
                })
                .error(function (error) {
                    return false;
                });
        };
    }

    angular
      .module('chatApp.Security', [])
      .service('AuthService', AuthService);

})();

