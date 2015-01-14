(function () {
    "use strict";

    angular
        .module("piHome")
        .controller("CircuitsListViewCtrl",
                    ["$scope", "$rootScope", "$interval", "circuitsResourceService", "stateService", CircuitsListViewCtrl]);

    function CircuitsListViewCtrl($scope, $rootScope, $interval, circuitsResourceService, stateService) {
        console.log('CircuitsListViewCtrl controller created');

        stateService.update(true);
        //$scope.$watch('IsCheckBoxSelected', function (newValue, oldValue) {
        //    console.log('value changed from: ' + oldValue + ' to: ' + newValue);
        //    stateService.update({NewValue: newValue, OldValue: oldValue});
        //});

        var poolCircuitsPromise = $interval(function () {
            $scope.CircuitStates = circuitsResourceService.getCircuitsState();
        }, 1000);

        $rootScope.$watch('refreshCircuitsList', function () {
            if ($scope.refreshCircuitsList === false) {
                $interval.cancel(poolCircuitsPromise);
            }
        });

        $scope.ChangeCircuitState = function (circuit) {
            alert(circuit.Circuit);
        };
    }
} ());