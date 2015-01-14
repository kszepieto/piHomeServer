(function () {
    "use strict";

    var piHomeModule = angular.module("piHome");

    piHomeModule.factory("circuitsResourceService", ["$resource", circuitsResourceService]);

    function circuitsResourceService($resource) {
        console.log('circuitsResourceService service created');
        
        //var resource = $resource('/testData/:id', {id: '@id'}, {"getCircuitsState": {method: "GET", isArray:true, params: {something: "foo"}}});
        
        var resource = $resource('/testData/ControlCircuits.json', {id: '@id'}, {"getCircuitsState": {method: "GET", isArray:true}});
        return {
            getCircuitsState: function() {
                return resource.getCircuitsState(1);
            }            
        };
    }
} ());
