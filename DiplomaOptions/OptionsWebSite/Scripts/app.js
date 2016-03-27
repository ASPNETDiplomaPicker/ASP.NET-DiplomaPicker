(function () {
    var app = angular.module('reports', []);

    app.controller('ReportsController', function ($scope, $http) {

        // Chart data
        var data1 = {
            labels: [],
            series: []
        };
        var data2 = {
            labels: [],
            series: []
        };
        var data3 = {
            labels: [],
            series: []
        };
        var data4 = {
            labels: [],
            series: []
        };

        var parseChoiceData = function (data) {
            // Reset the current counters
            data1.labels = [];
            data1.series = [];
            data2.labels = [];
            data2.series = [];
            data3.labels = [];
            data3.series = [];
            data4.labels = [];
            data4.series = [];

            for (var i in data) {
                if (data[i].YearTermId == $scope.yearterm) {
                    if (data1.labels.indexOf(data[i].FirstOption.Title) == -1) {
                        data1.labels.push(data[i].FirstOption.Title);
                        data1.series.push(1);
                    } else {
                        var index = data1.labels.indexOf(data[i].FirstOption.Title);
                        data1.series[index]++;
                    }
                    if (data2.labels.indexOf(data[i].SecondOption.Title) == -1) {
                        data2.labels.push(data[i].SecondOption.Title);
                        data2.series.push(1);
                    } else {
                        var index = data2.labels.indexOf(data[i].SecondOption.Title);
                        data2.series[index]++;
                    }
                    if (data3.labels.indexOf(data[i].ThirdOption.Title) == -1) {
                        data3.labels.push(data[i].ThirdOption.Title);
                        data3.series.push(1);
                    } else {
                        var index = data3.labels.indexOf(data[i].ThirdOption.Title);
                        data3.series[index]++;
                    }
                    if (data4.labels.indexOf(data[i].FourthOption.Title) == -1) {
                        data4.labels.push(data[i].FourthOption.Title);
                        data4.series.push(1);
                    } else {
                        var index = data4.labels.indexOf(data[i].FourthOption.Title);
                        data4.series[index]++;
                    }
                }
            }
        };
        var onUserComplete = function (response) {
            $scope.choices = response.data;
            parseChoiceData(response.data);
            chartCreate();
        };
        var onChoicesComplete = function (response) {
            $scope.yearterm = 1;
            for (var i in response.data) {
                if (response.data[i].IsDefault == true) {
                    $scope.yearterm = response.data[i].YearTermId;
                }
            }

            $scope.yearterms = response.data;
        };
        var onError = function (error) {
            $scope.error = error;
        };

        // Set the two report types
        $scope.reporttypes = [
             { Id: 0, Type: 'Details Report' },
             { Id: 1, Type: 'Chart' }
        ];
        $scope.reporttype = 0;

        // Grab the yearterms immediatly
        $http.get("http://localhost:51290/api/YearTerms")
             .then(onChoicesComplete, onError);

        $scope.generateReports = function () {
            $http.get("http://localhost:51290/api/Choices")
            .then(onUserComplete, onError);
        };

        $scope.changeYearTerm = function () {
            parseChoiceData($scope.choices);
            chartCreate();
        };

        var chartCreate = function () {
            var options = {
                labelInterpolationFnc: function (value) {
                    return value[0]
                },
                width: 400,
                height: 200
            };

            var responsiveOptions = [
              ['screen and (min-width: 640px)', {
                  chartPadding: 30,
                  labelOffset: 10,
                  labelDirection: 'explode',
                  labelInterpolationFnc: function (value) {
                      return value;
                  }
              }],
              ['screen and (min-width: 1024px)', {
                  labelOffset: 10,
                  chartPadding: 30
              }]
            ];

            new Chartist.Pie('#chart1', data1, options, responsiveOptions);
            new Chartist.Pie('#chart2', data2, options, responsiveOptions);
            new Chartist.Pie('#chart3', data3, options, responsiveOptions);
            new Chartist.Pie('#chart4', data4, options, responsiveOptions);
        };
    });
})();