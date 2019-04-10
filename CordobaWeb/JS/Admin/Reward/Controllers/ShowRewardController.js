app.controller('ShowRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService) {
    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "Rewards";
    $scope.store_Id = $rootScope.storeId;
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    //$rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    $scope.GetRewardList = function () {
        MaintainLocalStorage();
            //Store
            if ($scope.StoreId == "" || $scope.StoreId == null) {
                localStorageService.set(userid + "_ShowReward_Store", "");
                $scope.StoreId = 0;
            }
            else {
                localStorageService.set(userid + "_ShowReward_Store", $scope.StoreId);
            }
            

        $http.get(configurationService.basePath + "api/RewardApi/GetRewardList?reward_id=0" + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              $scope.rewardList = response.data;
              
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    var userid = $scope.LoggedInUserId;
    function MaintainLocalStorage() {
        //Store
        if ((localStorageService.get(userid + "_ShowReward_Store") == "" || localStorageService.get(userid + "_ShowReward_Store") == null)) {
            localStorageService.set(userid + "_ShowReward_Store", $scope.StoreId);
        }
        else if (($scope.StoreId != "") ? (localStorageService.get(userid + "_ShowReward_Store") != $scope.StoreId) : false) {
            localStorageService.set(userid + "_ShowReward_Store", $scope.StoreId);
        }
        else if ((localStorageService.get(userid + "_ShowReward_Store") != null || localStorageService.get(userid + "_ShowReward_Store") != "")) {
            $scope.StoreId = localStorageService.get(userid + "_ShowReward_Store") == null  ? 0 : localStorageService.get(userid + "_ShowReward_Store");
        }
    }

    $scope.ContainStoreOrNot = function () {
        
        //MaintainLocalStorage();
    }

    function GetStoreList() {
        
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
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

    GetStoreList();

    $scope.GetRewardList();
});