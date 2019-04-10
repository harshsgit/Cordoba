app.controller('Dashboard_BestSellerController', function (StoreSessionDetail,$timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  
    //InitChart();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);


    $scope.StoreDetailInSession = StoreSessionDetail;
    var totalproducts;
    if ($scope.StoreDetailInSession != null) {
        totalproducts = $scope.StoreDetailInSession.template == "_Layout2" ? 4 : 3;
    }

    $scope.BestSellerIndexStart = 0;
    $scope.BestSellerIndexEnd = 0;

    $scope.BestSeller = [];

    $scope.BestSellerList = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetBestSellerListByStore?StoreID=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + $rootScope.CustomerDetail.customer_id)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.BestSeller = response.data;
                  $scope.NextBestSeller();
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.BestSeller = $scope.BestSellerList.length;

    $scope.NextBestSeller = function () {
        
        if ($scope.BestSellerIndexEnd == $scope.BestSeller.length) {
            return false;
        }
        $scope.BestSellerList = [];

        var LastIndex = $scope.BestSellerIndexStart;
        for (var i = $scope.BestSellerIndexStart; i < $scope.BestSellerIndexStart + totalproducts; i++) {
            if (i < $scope.BestSeller.length) {
                $scope.BestSellerList.push($scope.BestSeller[i]);
                LastIndex = LastIndex + 1;
            }
            else {
                i = -1;
                $scope.BestSellerIndexEnd = LastIndex;
                LastIndex = 0;
                break;
            }
        }
        $scope.BestSellerIndexStart = LastIndex;
        if ($scope.BestSellerIndexStart != 0) {
            $scope.BestSellerIndexEnd = $scope.BestSellerIndexStart;
        }
    }


    //$scope.PreviousBestSeller = function () {
    //     
    //    $scope.BestSellerList = [];
    //    var count = 1;
    //    //if ($scope.BestSellerIndexEnd - 5 < 0) {
    //    //    $scope.BestSellerIndexEnd = $scope.BestSeller.length ;
    //    //}
    //    //if ($scope.BestSellerIndexEnd == 4) {
    //    //    $scope.BestSellerIndexEnd = $scope.BestSeller.length;
    //    //}

    //    for (var i = $scope.BestSellerIndexEnd - 5 ; ; i--) {

    //        if (count < 5) {
    //            if (i < 0) {
    //                i = $scope.BestSeller.length - 1;
    //            }
    //            $scope.BestSellerList.push($scope.BestSeller[i]);
    //            count = count + 1;
    //            $scope.BestSellerIndexEnd = i;
    //        }
    //        else {
    //            break;
    //        }


    //    }
    //    $scope.BestSellerList.reverse();

    //}

    $scope.PreviousBestSeller = function () {
        
        if ($scope.BestSellerIndexEnd == 0 || $scope.BestSellerIndexEnd == totalproducts)
        {
            return false;
        }
        $scope.BestSellerList = [];
        var previousproductindex = $scope.BestSellerIndexEnd % totalproducts == 0 ? totalproducts : $scope.BestSellerIndexEnd % totalproducts == 1 ? 1 : 2;
        var LastIndex = $scope.BestSellerIndexEnd;
        var temp = totalproducts;
        for (var i = LastIndex - previousproductindex ; temp > 0 ; i--) {
            if (i > 0) {
                $scope.BestSellerList.push($scope.BestSeller[i - 1]);
                LastIndex = LastIndex - 1;
                temp = temp - 1;
            }
        }
        if (i == 0) {
            LastIndex = 0;
            $scope.BestSellerIndexStart = totalproducts;
        }
        else {
            $scope.BestSellerIndexStart = LastIndex;
        }
        $scope.BestSellerList.reverse();
        $scope.BestSellerIndexEnd = LastIndex;
    }

    $scope.BestSellerList();


    //$scope.BestSellerList = [
    //     { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points:  "41 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "42 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "43 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "44 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "45 Points" }
    //    , { BestSellerImgSrc: "/Content/layout1/images/bestSeller.jpg", BestSellerName: "Amazon e-voucher", Points: "46 Points" }
    
    //];


});