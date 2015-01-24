(function () {
    "use strict";
    var app = angular.module("piHome",
        ["ngResource", "ui.router"]);

    app.config(["$stateProvider",
                "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            console.log('config run');

            $urlRouterProvider.otherwise("/circuitsControl");

            $stateProvider
                .state("circuitsControl", {
                    url: "/circuitsControl",
                    templateUrl: "js/app/circuits/circuitsListView.html",
                    controller: "CircuitsListViewCtrl as vm",
                })
                .state("stats", {
                    url: "/stats",
                    templateUrl: "js/app/stats/statsView.html",
                    controller: "StatsViewCtrl as vm",
                });
        } ]
    );
} ());