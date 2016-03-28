(function () {

    var app = angular.module("diplomaPicker");
    var url = "http://optionswebapi.rickychen.me/api/";
    var DisplayPick = function ($scope, $http, $location) {
        if (checkCookie()) {
            // get user id
            $scope.username = getCookie("username");
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
                    setCookie("termId", yearTerm.Term, 1);
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
        else {
            $location.path("/login");
        }
    }

    app.controller("DisplayPick", DisplayPick);
}());

(function () {
    var app = angular.module("diplomaPicker");
    var url = "http://optionswebapi.rickychen.me/api/Choices";

    var PostPick = function ($scope, $http, $location) {
        $scope.createChoice = function (user) {
            //$http.post("http://w10.hsuanweifu.info/api/Students", angular.copy(student));
            param = {
                YearTermId: getCookie("termId"),
                StudentId: getCookie("username"),
                StudentFirstName: user.StudentFirstName,
                StudentLastName: user.StudentLastName,
                FirstChoiceOptionId: user.FirstChoiceOptionId,
                SecondChoiceOptionId: user.SecondChoiceOptionId,
                ThirdChoiceOptionId: user.ThirdChoiceOptionId,
                FourthChoiceOptionId: user.FourthChoiceOptionId
            };

            $.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/json;charset=utf-8',
                data: JSON.stringify(param)
            });
            $location.path("/summary");
        }
    }

    app.controller("PostPick", PostPick);
}());