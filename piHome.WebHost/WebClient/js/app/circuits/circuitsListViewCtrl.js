(function () {
    "use strict";

    angular
        .module("piHome")
        .controller("CircuitsListViewCtrl",
                    ["$scope", "$interval", "circuitsResourceService", CircuitsListViewCtrl]);

    function CircuitsListViewCtrl($scope, $interval, circuitsResourceService) {
        console.log('CircuitsListViewCtrl controller created');

        //var circuits = circuitsResourceService.getCircuitsRequest();
        //circuits.then(function(data) {
        //    $scope.CircuitStates = data;
        //}, function() {
        //    $scope.CircuitStates = undefined;
        //});


        var poolCircuitsPromise = $interval(function () {
            var circuits = circuitsResourceService.getCircuitsRequest();
            circuits.then(function (data) {
                $scope.CircuitStates = data;
            }, function () {
                $scope.CircuitStates = undefined;
            });
        }, 1000);

        $scope.$on('$destroy', function () {
            if (angular.isDefined(poolCircuitsPromise)) {
                $interval.cancel(poolCircuitsPromise);
                poolCircuitsPromise = undefined;
            }
        });
    }
}());