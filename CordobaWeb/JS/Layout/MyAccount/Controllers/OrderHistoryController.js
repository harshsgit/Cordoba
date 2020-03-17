

app.controller('OrderHistoryController', function ($timeout, StoreSessionDetail, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    if (!(UserDetail.customer_id > 0)) {
        window.location.href = 'home/accessdenied';
    }
    //$scope.dtOptions = DTOptionsBuilder.newOptions()
    //               .withOption('bDestroy', true)
    //               .withOption("deferRender", true);

    //$scope.dtColumnDefs = [
    //DTColumnDefBuilder.newColumnDef(5).notSortable()
    //];


    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.StoreDetailInSession = StoreSessionDetail;

    $scope.GetOrderHistory = function () {
        var encryptStoreId = encryptedServerString(StoreSessionDetail.store_id);
        var encryptCustomerId = encryptedServerString(UserDetail.customer_id);


        $http.get(configurationService.basePath + "API/OrderApi/GetOrderHistory?StoreId=" + encryptStoreId + "&LoggedInUserId=" + encryptCustomerId + "&customer_id=" + encryptCustomerId)
          .then(function (response) {              
              $scope.OrderHisotry = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetOrderHistory();


});