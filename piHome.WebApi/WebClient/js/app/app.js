(function () {
    "use strict";
    var app = angular.module("piHome",
        ["ngResource", "ui.router"]);

    app.config(["$stateProvider",
                "$urlRouterProvider",
                "$rootScopeProvider",
        function ($stateProvider, $urlRouterProvider, $rootScopeProvider) {
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
                })
        } ]
    );

    app.run(function ($rootScope) {
        console.log('app run');

        $rootScope.$on('$stateChangeSuccess',
            function (event, toState, toParams, fromState, fromParams, error) {
                if (toState.name == 'circuitsControl') {
                    $rootScope.refreshCircuitsList = true;
                } else {
                    $rootScope.refreshCircuitsList = false;
                }
            });
    });

} ());