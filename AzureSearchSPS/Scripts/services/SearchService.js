angular.module("AzureSearch.services").factory('SearchService', ['$http', '$q', function ($http, $q) {

    var SearchService = {};

    SearchService.Search = function (content,sort) {

        var deferred = $q.defer();

        $http({
            method: "GET",
            url: "/api/Search?value=" + content + "&sort=" + sort
        }).then(function (response) {

            deferred.resolve(response.data);

        },
        function () {
            deferred.reject();
        });

        return deferred.promise;

    }

    return SearchService;

}]);