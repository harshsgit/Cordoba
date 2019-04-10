app.controller('ProductDetailLayoutController', function (StoreSessionDetail, UserDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions   
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.CustomerId = $scope.CustomerDetail != null && $scope.CustomerDetail.customer_id != null ? $scope.CustomerDetail.customer_id : 0;

    //#endregion   
    if ($stateParams.ProductId != undefined && $stateParams.ProductId != null) {
        $scope.SelectedProductId = parseInt($stateParams.ProductId);
    }

    $scope.GetProductDetail = function () {
        $http.get(configurationService.basePath + "API/ProductApi/GetProductDetailForLayout?StoreID=" + $scope.StoreDetailInSession.store_id + "&ProductId=" + $scope.SelectedProductId + "&CustomerId=" + $scope.CustomerId)
            .then(function (response) {
              $scope.ProductObj = response.data;
              $scope.ProductObj.descriptionHtml = $scope.ProductObj.description;
              $scope.ProductObj.description = $('<div />').html($scope.ProductObj.description).text();
              $scope.SelectedProductId = $scope.ProductObj.product_id;
              $scope.GetRelatedProductList($scope.ProductObj.product_id);
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetRelatedProductList = function (ProductId) {
        $http.get(configurationService.basePath + "API/ProductApi/GetRelatedProductList?StoreID=" + $scope.StoreDetailInSession.store_id + "&SelectedProductId=" + $scope.SelectedProductId + "&RelatedProductId=" + ProductId + "&customer_id=" + $rootScope.CustomerDetail.customer_id)
          .then(function (response) {
              $scope.RelatedProductList = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.DecreaseQuantity = function (Product) {
        if (Product.CartQuantity > 1) {
            Product.CartQuantity = Product.CartQuantity - 1;
        }

    }

    $scope.IncreaseQuantity = function (Product) {
        Product.CartQuantity = Product.CartQuantity + 1;
    }

    $scope.AddtoWishList = function (productObj) {
        if (UserDetail.customer_id > 0) {
            var WishObj = new Object();
            WishObj.customer_id = UserDetail.customer_id;
            WishObj.product_id = productObj.product_id;
            WishObj.store_id = $scope.StoreDetailInSession.store_id;


            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/AddtoWishList", WishObj)
                  .then(function (response) {
                      if (response.data == -1) {
                          toastr.warning('Item already present in wish list.');
                      }
                      else if (response.data > 0) {
                          toastr.success('Item successfully added in wish list.');
                          $scope.GetProductDetail();
                      }
                  })
              .catch(function (response) {

              })
              .finally(function () {

              });
        }
        else {
            $scope.OpenLoginPopUp();
        }

    }


    $scope.GetProductDetail();


});