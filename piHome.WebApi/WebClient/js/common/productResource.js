(function () {
    "use strict";

    var commonModule = angular.module("common.services");

    commonModule.factory("circuitsResource", ["$resource", circuitsResource]);

    function circuitsResource($resource) {
        return $resource("/api/products/:productId");
    }

    commonModule.factory('rootScopeService', ['$rootScope', rootScopeService]);

    function rootScopeService($rootScope) {
        return $rootScope;
    }
} ());
