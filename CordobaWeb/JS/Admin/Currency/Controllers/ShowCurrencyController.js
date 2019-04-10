app.controller('ShowCurrencyController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval,DTColumnDefBuilder, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
   
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.UserList = [];
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true);

    $scope.dtColumnDefs = [
      DTColumnDefBuilder.newColumnDef(2).notSortable()
    ];

    $scope.PageTitle = "Show Currency";


    $scope.GetUserList = function () {
        $http.get(configurationService.basePath + "api/CurrencyApi/GetCurrencyList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {

              if (response.data.length > 0) {
                  $scope.CurrencyList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetUserList();




});