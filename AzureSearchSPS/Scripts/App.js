/// <reference path="vendor/angular.min.js" />

angular.module("AzureSearch.services", []);
angular.module("AzureSearch.controllers", ['AzureSearch.services']);
angular.module("AzureSearch.directives", []);

angular.module("AzureSearch", ['AzureSearch.controllers', 'AzureSearch.directives']);
