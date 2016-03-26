(function () {

    var app = angular.module("diplomaPicker");
    var url = "http://localhost:51290/api/";
    var DisplayPick = function ($scope, $http) {
        // get user id
        // get year and term
        $http.get(url.concat("YearTerms"))
            .then(function (response) {
                var yearTerm;
                response.data.forEach(function (x) {
                    if (x.isDefault) {
                        yearTerm = x;
                    }
                });
                if (yearTerm.Term == 10) {
                    yearTerm.Term = "Winter";
                }
                else if (yearTerm.Term == 20) {
                    yearTerm.Term = "Spring/Summer";
                }
                else if (yearTerm.Term == 30) {
                    yearTerm.Term = "Fall";
                }
                $scope.yearTerm = yearTerm;
            });
        // get avaliable options 
        $http.get(url.concat("Options"))
            .then(function (response) {
                var array = [];
                response.data.forEach(function (x) {
                    if (x.isActive) {
                        array.push(x);
                    }
                });
                $scope.options = array;
            });
    }

    app.controller("DisplayPick", DisplayPick);
}());

(function () {
    var app = angular.module("diplomaPicker");
    var url = "http://localhost:51290/api/";

    var PostPick = function ($scope, $http) {
        $scope.createChoice = function (user) {
            window.alert(user.YearTermId);
            window.alert(user.StudentFirstName);
            //$http.post("http://w10.hsuanweifu.info/api/Students", angular.copy(student));
        }
    }

    app.controller("PostPick", PostPick);
}());