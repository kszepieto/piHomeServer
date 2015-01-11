(function () {
    "use strict";
    var app = angular.module("piHome",
        ["common.services", "ui.router"]);

    app.config(["$stateProvider",
                "$urlRouterProvider",
                "rootScopeService",
        function ($stateProvider, $urlRouterProvider, rootScopeService) {
            $urlRouterProvider.otherwise("/");

            $stateProvider
                .state("home", {
                    url: "/",
                    templateUrl: "js/app/welcomeView.html",
                    onEnter: function () { console.log("Home - Enter") },
                    onExit: function () { console.log("Home - Leave") }
                })
                .state("circuitsControl", {
                    url: "/circuitsList",
                    templateUrl: "js/app/circuits/circuitsListView.html",
                    //controller: "ProductListCtrl as vm"
                    onEnter: function () { console.log("Hejka") }
                });

            //var $injector = angular.injector();
            //var x1 = $injector.get('$rootScope');
            //var x2 = $injector.get('rootScope');
            //var x3 = $injector.get('$rootScopeProvider');
        } ]
    );


} ());