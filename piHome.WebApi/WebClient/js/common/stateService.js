(function () {
    "use strict";

    var piHomeModule = angular.module("piHome");

    piHomeModule.factory('stateService', function ($rootScope) {

    var state;

    var broadcast = function (state) {
      $rootScope.$broadcast('state.update', state);
    };

    var update = function (newState) {
      state = newState;
      broadcast(state);
    };
    
    return {
      update: update,
      state: state,
    };
  });
}())