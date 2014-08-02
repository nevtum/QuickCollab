(function () {

    function AuthService($http) {

        // Authenticate user in back end
        // return true if successful
        // return false otherwise
        this.verify = function (password, sessionId) {

            data = {
                SessionId: sessionId,
                Password: password,
            };

            authenticated = false;

            $http.post('../api/Sessions/Authorize', data)
                .success(OnSuccess);

            // Not working, don't know why it doesn't return true
            return authenticated;
        };

        var OnSuccess = function (response, authenticated) {
            console.log(response);
            authenticated = true;
        };
    }

    angular
      .module('chatApp.Security', [])
      .service('AuthService', AuthService);

})();

