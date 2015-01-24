(function () {
    "use strict";

    var piHomeModule = angular.module("piHome");

    piHomeModule.factory("circuitsResourceService", ["$q", "$http", circuitsResourceService]);

    function circuitsResourceService($q, $http) {
        console.log('circuitsResourceService service created');

        var getCircuitsRequest = function() {
            var deferred = $q.defer();

            $http.get('/testData/ControlCircuits.json')
                .success(function(data) {
                    deferred.resolve(data);
                })
                .error(function(reason) {
                    deferred.reject(reason);
                });

            return deferred.promise;
        };

        return {
            getCircuitsRequest: getCircuitsRequest
        };
    };
} ());
