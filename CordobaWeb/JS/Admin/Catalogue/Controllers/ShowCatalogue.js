app.controller('ShowCatalogue', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder,DTColumnDefBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CatalogueList = [];
    //#endregion  
    $scope.StoreId = $rootScope.StoreId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);
    $scope.dtColumnDefs = [
     DTColumnDefBuilder.newColumnDef(1).notSortable()
    ];

    $scope.PageTitle = "Product Catalogues";

    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    $scope.GetCatalogueList = function () {    
        $http.get(configurationService.basePath + "api/CatalogueApi/GetCatalogueList?StoreId=" + $scope.storeId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {              
                  $scope.CatalogueList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });


    }
    $scope.GetCatalogueList();



});