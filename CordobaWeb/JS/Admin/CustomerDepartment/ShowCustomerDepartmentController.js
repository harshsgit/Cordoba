app.controller('ShowCustomerDepartmentController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, DTColumnDefBuilder, $http, $log, $q, localStorageService) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId; 
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.IsStoreDropDownDisabled = false;

    $scope.CustomerDepartmentList = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef(2).notSortable()
    ];

    $scope.PageTitle = "Customer Department";

    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    var userid = $scope.LoggedInUserId;
    function MaintainLocalStorage() {
        //Store
        if ((localStorageService.get(userid + "_ShowCustomerDepartment_Store") == "" || localStorageService.get(userid + "_ShowCustomerDepartment_Store") == null)) {
            localStorageService.set(userid + "_ShowReward_Store", $scope.StoreId);
        }
        else if (localStorageService.get(userid + "_ShowCustomerDepartment_Store") != $scope.StoreId) {
            localStorageService.set(userid + "_ShowCustomerDepartment_Store", $scope.StoreId);
        }
        else if ((localStorageService.get(userid + "_ShowCustomerDepartment_Store") != null || localStorageService.get(userid + "_ShowCustomerDepartment_Store") != "")) {
            $scope.StoreId = localStorageService.get(userid + "_ShowCustomerDepartment_Store") == null ? 0 : localStorageService.get(userid + "_ShowCustomerDepartment_Store");
        }
    }

    $scope.GetCustomerDepartmentList = function () {
        MaintainLocalStorage();
        //Store
        if ($scope.StoreId == "" || $scope.StoreId == null) {
            localStorageService.set(userid + "_ShowCustomerDepartment_Store", "");
            //$scope.StoreId = 0;
        }
        else {
            localStorageService.set(userid + "_ShowCustomerDepartment_Store", $scope.StoreId);
        }

        $http.get(configurationService.basePath + "api/CustomerDepartmentApi/GetCustomerDepartmentList?StoreId=" + $scope.StoreId)
          .then(function (response) {
              if (response.data != undefined && response.data != null) {
                  $scope.CustomerDepartmentList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetStoreList = function () {
        $http.get(configurationService.basePath + "api/CategoryApi/GetStoreNameList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
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

    $scope.CheckIsStoreDropDownDisabled=function()
    {        
        if ($scope.StoreId!=0)
        {
              $scope.IsStoreDropDownDisabled = true;
        }
    }

    $scope.GetStoreList();
    $scope.GetCustomerDepartmentList();

});