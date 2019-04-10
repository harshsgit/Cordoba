app.controller('AddProductToCartController', function (StoreSessionDetail, $timeout, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q, localStorageService) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.LoggedInUserId = 0;
    $scope.StoreDetailInSession = StoreSessionDetail;
    //#endregion  
    $scope.AddProductToCart = function (ProductObj, e) { 
        ////////////////   
        $http.get(configurationService.basePath + "API/ProductApi/AddProductToCart?store_id=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + UserDetail.customer_id + '&LoggedInUserId=' + UserDetail.customer_id + "&product_id=" + ProductObj.product_id + "&qty=1&cartgroup_id=" + (UserDetail.cartgroup_id == null ? 0 : UserDetail.cartgroup_id))
          .then(function (response) {
              CartAnimation($(e.target), ProductObj.Image);
              //toastr.success("Item successfully added in cart.");
              UserDetail.cartgroup_id = response.data.cartgroup_id;
              UserDetail.TotalItemAdded = response.data.TotalItemAdded;
              $rootScope.CustomerDetail = UserDetail;
              localStorageService.set("loggedInUser", UserDetail);             
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

   
    



});