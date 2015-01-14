(function () {
    "use strict";

    angular
        .module("piHome")
        .controller("StatsViewCtrl",
                    ["$scope", StatsViewCtrl]);

    function StatsViewCtrl($scope) {
        console.log('StatsViewCtrl controller created');
    }
} ());