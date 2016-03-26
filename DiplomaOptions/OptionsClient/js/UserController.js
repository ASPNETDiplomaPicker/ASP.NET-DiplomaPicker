(function () {

    var app = angular.module("diplomaPicker");

    var CreateUser = function ($scope, $http) {
        $scope.register = function (user) {
            $http.post("http://localhost:51290/api/Account/Register", angular.copy(user));
        }
    }

    app.controller("CreateUser", CreateUser);
}());
(function () {

    var app = angular.module("diplomaPicker");

    var Login = function ($scope, $http) {
        $http.get("http://w10.hsuanweifu.info/api/Students")
            .then(function (response) {
                $scope.students = response.data;
                console.log($scope.students);
            });
    }

    app.controller("Login", Login);
}());