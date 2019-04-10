app.controller('ShowStoreController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval,DTColumnDefBuilder,DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.CountryList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)

    $scope.dtColumnDefs = [
   DTColumnDefBuilder.newColumnDef(2).notSortable()
    ];

    $scope.PageTitle = "Show Stores";

    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    $scope.GetStoreList = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.StoreList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
});