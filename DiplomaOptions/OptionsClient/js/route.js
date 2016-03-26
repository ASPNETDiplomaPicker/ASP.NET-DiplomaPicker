(function () {
    var app = angular.module("diplomaPicker", ["ngRoute"]);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/login", {
                templateUrl: "view/login.html",
                controller: "Login"
            })
            .when("/register", {
                templateUrl: "view/register.html",
                controller: "CreateUser"
            })
            .when("/pick", {
                templateUrl: "view/pick.html",
                controller: "DisplayPick"
            })
            .when("/summary", {
                templateUrl: "view/summary.html",
                controller: "CreateController"
            })
            .otherwise({
                redirectTo: "/login"
            });
    });
}());