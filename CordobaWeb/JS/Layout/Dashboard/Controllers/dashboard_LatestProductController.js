app.controller('Dashboard_LatestProductController', function (StoreSessionDetail,$timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.LatestProductList = [];
    //#endregion  
    $scope.StoreDetailInSession = StoreSessionDetail;
    var totalproducts;
    if ($scope.StoreDetailInSession != null) {
        totalproducts = $scope.StoreDetailInSession.template == "_Layout2" ? 3 : 4;
    }

    $scope.LatestProductIndexStart = 0;
    $scope.LatestProductIndexEnd = 0;

    $scope.LatestProduct = [];
    $scope.LatestProductList = [];
    $scope.GetLatestProductByStoreId = function () {

        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetLatestProductByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id + "&Customer_Id=" + $rootScope.CustomerDetail.customer_id)
          .then(function (response) {  
              if (response.data.length > 0) {
                  $scope.LatestProduct = response.data;
                  $scope.NextLatestProduct();
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    $scope.LatestProduct = $scope.LatestProductList.length;

    $scope.NextLatestProduct = function () {
        if ($scope.LatestProductIndexEnd == $scope.LatestProduct.length)
        {
            return false;
        }

        $scope.LatestProductList = [];
        
        var LastIndex = $scope.LatestProductIndexStart;
        for (var i = $scope.LatestProductIndexStart; i < $scope.LatestProductIndexStart + totalproducts; i++) {
            if (i < $scope.LatestProduct.length) {
                $scope.LatestProductList.push($scope.LatestProduct[i]);
                LastIndex = LastIndex + 1;
            }
            else {
                i = -1;
                $scope.LatestProductIndexEnd = LastIndex;
                LastIndex = 0;
                break;
            }
        }
        
        $scope.LatestProductIndexStart = LastIndex;
        if ($scope.LatestProductIndexStart != 0) {
            $scope.LatestProductIndexEnd = $scope.LatestProductIndexStart;
        }
    }

    $scope.PreviousLatestProduct = function () {
        if ($scope.LatestProductIndexEnd == 0 || $scope.LatestProductIndexEnd == totalproducts)
        {
            return false;
        }
        
        $scope.LatestProductList = [];
        var previousproductindex = $scope.LatestProductIndexEnd % totalproducts == 0 ? totalproducts : $scope.LatestProductIndexEnd % totalproducts == 1 ? 1 : 2;
        var LastIndex = $scope.LatestProductIndexEnd;
        var temp = totalproducts;
        for (var i = LastIndex - previousproductindex ; temp > 0 ; i--) {
            if (i > 0) {
                $scope.LatestProductList.push($scope.LatestProduct[i-1]);
                LastIndex = LastIndex - 1;
                temp = temp - 1;
            }
        }
        if (i==0) {
            LastIndex = 0;
            $scope.LatestProductIndexStart = totalproducts;
        }
        else {
            $scope.LatestProductIndexStart = LastIndex;
        }
        $scope.LatestProductList.reverse();
        $scope.LatestProductIndexEnd = LastIndex;
    }


    $scope.GetLatestProductByStoreId();

});