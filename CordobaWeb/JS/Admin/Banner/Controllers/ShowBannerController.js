app.controller('ShowBannerController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.CatalogueList = [];
    //#endregion  
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Banners";

    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    $scope.GetBannerList = function () {
        $http.get(configurationService.basePath + "api/BannerApi/GetBannerList")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.BannerList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    $scope.GetBannerList();

});