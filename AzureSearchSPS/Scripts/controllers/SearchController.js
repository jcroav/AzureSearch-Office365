angular.module("AzureSearch.controllers").controller("SearchController", ["$scope","$sce", "SearchService", function ($scope, $sce, SearchService) {

    $scope.content = "";
    $scope.results = [];

    $scope.sort = "asc";
    $scope.type = "Name";

    $scope.Search = function () {

        SearchService.Search($scope.content,$scope.type + " " + $scope.sort).then(function (response) {
            $scope.results = response;
        },
        function () {
        })
    };

    $scope.InitSearch = function () {
        SearchService.Search("*", $scope.type + " " + $scope.sort).then(function (response) {
            $scope.results = response;
        },
        function () {
        })
    }

    $scope.toTrustedHTML = function (html) {
        return $sce.trustAsHtml(html);
    }

}]);