app.controller('Dashboard_RewardWinnerController', function ($timeout,StoreSessionDetail,UserDetail,$state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
    $scope.StoreDetailInSession = StoreSessionDetail;   
    $scope.Dashboard_RewardWinner = function () {
        $http.get(configurationService.basePath + "api/RewardApi/Dashboard_RewardWinner?StoreId=" + $scope.StoreDetailInSession.store_id + "&LoggedInUserId=" + UserDetail.customer_id)
        .then(function (response) {     
          if (response.data.length > 0) {
              $rootScope.Make_rewardWinnerVisible = true;
          }
          else {
              $rootScope.Make_rewardWinnerVisible = false;
          }
          $scope.RewardWinners = response.data;
      })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.Dashboard_RewardWinner();

});