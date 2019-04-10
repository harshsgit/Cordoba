app.controller('Dashboard_SpecialOffersController', function ($timeout,StoreSessionDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion     

    $scope.SpecialOfferList = [];
    //#endregion  
    $scope.StoreDetailInSession = StoreSessionDetail;
    var totalproducts;
    if ($scope.StoreDetailInSession != null) {
        
        totalproducts = $scope.StoreDetailInSession.template == "_Layout2" ? 3 : 2;
    }

    $scope.SpecialOfferIndexStart = 0;
    $scope.SpecialOfferIndexEnd = 0;

    $scope.SpecialOffer = [];

    $scope.GetSpecialOfferListByStoreId = function () {

        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetSpecialOfferListByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + $rootScope.CustomerDetail.customer_id)
          .then(function (response) {          
              if (response.data.length > 0) {                  
                  $scope.SpecialOffer = response.data;
                  $scope.NextSpecialOffer();
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

     $scope.SpecialOffer = $scope.SpecialOfferList.length;

    $scope.NextSpecialOffer = function () {
        
        if ($scope.SpecialOfferIndexEnd == $scope.SpecialOffer.length) {
            return false;
        }
        $scope.SpecialOfferList = [];
        
        var LastIndex = $scope.SpecialOfferIndexStart;
        for (var i = $scope.SpecialOfferIndexStart; i < $scope.SpecialOfferIndexStart + totalproducts; i++) {
            if (i < $scope.SpecialOffer.length) {
                $scope.SpecialOfferList.push($scope.SpecialOffer[i]);
                LastIndex = LastIndex + 1;
            }
            else {
                i = -1;
                $scope.SpecialOfferIndexEnd = LastIndex;
                LastIndex = 0;
                break;
            }
        }
        $scope.SpecialOfferIndexStart = LastIndex;
        if ($scope.SpecialOfferIndexStart != 0) {
            $scope.SpecialOfferIndexEnd = $scope.SpecialOfferIndexStart;
        }
    }

    $scope.PreviousSpecialOffer = function () {
        
        if ($scope.SpecialOfferIndexEnd == 0 || $scope.SpecialOfferIndexEnd == totalproducts) {
            return false;
        }
        $scope.SpecialOfferList = [];
        var previousproductindex = $scope.SpecialOfferIndexEnd % totalproducts == 0 ? totalproducts : $scope.SpecialOfferIndexEnd % totalproducts == 1 ? 1 : 2;
        var LastIndex = $scope.SpecialOfferIndexEnd;
        var temp = totalproducts;
        for (var i = LastIndex - previousproductindex ; temp > 0 ; i--) {
            if (i > 0) {
                $scope.SpecialOfferList.push($scope.SpecialOffer[i-1]);
                LastIndex = LastIndex - 1;
                temp = temp - 1;
            }
        }
        if (i==0) {
            LastIndex = 0;
            $scope.SpecialOfferIndexStart = totalproducts;
        }
        else {
            $scope.SpecialOfferIndexStart = LastIndex;
        }
        $scope.SpecialOfferList.reverse();
        $scope.SpecialOfferIndexEnd = LastIndex;
    }


    $scope.GetSpecialOfferListByStoreId();

    //$scope.HideUnHideLink = function () {
    //     
    //    //{ { (SpecialOffer.length <= 3 && StoreDetailInSession.template == '_Layout2') ? 'true' : ((SpecialOffer.length <= 2 && StoreDetailInSession.template == '_Layout1') ? 'true' : 'false') } }
    //    if ($scope.SpecialOffer.length <= 3 && $scope.StoreDetailInSession.template == '_Layout2') {
    //        return true;
    //    }
    //    else if ($scope.SpecialOffer.length <= 2 && $scope.StoreDetailInSession.template == '_Layout1') {
    //        return true;
    //    }
    //    return false;
    //}




});