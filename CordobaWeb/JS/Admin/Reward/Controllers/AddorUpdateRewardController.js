app.controller('AddOrUpdateRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    createDatePicker();

    $scope.StatusForActive = [{ ID: 1, Name: 'Active' }, { ID: 0, Name: 'InActive' }];
    decodeParams($stateParams);
    BindToolTip();
    $scope.StoreId = $rootScope.storeId;
    
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.RewardId = 0;
    $scope.RewardObj = {};
    $scope.RewardObj.store_id = $scope.StoreId;
    $scope.RewardTypeList = [];
    $scope.IsEditMode = false;
    $scope.invalidEndDate = false;

    GetRewardList();
    function GetRewardList() {
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardTypeList?StoreID=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
         .then(function (response) {
             if (response.data.length > 0) {
                 $scope.RewardTypeList = response.data;
             }
         })
         .catch(function (response) {

         })
         .finally(function () {

         });
    }


    if ($stateParams.rewardId != undefined && $stateParams.rewardId != null) {
        $scope.PageTitle = "Update Reward";
        $scope.IsEditMode = true;
        $scope.RewardId = $stateParams.rewardId;
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardList?reward_id=" + $stateParams.rewardId + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.RewardObj = response.data[0];
                  $scope.RewardObj.start_date = $filter('date')($scope.RewardObj.start_date, $rootScope.GlobalDateFormat);
                  $scope.RewardObj.end_date = $filter('date')($scope.RewardObj.end_date, $rootScope.GlobalDateFormat);
                  //$('#startdate').data($scope.RewardObj.start_date);
                  //$('#enddate').data($scope.RewardObj.end_date);

                  $('.startdate').bootstrapDP('update', $scope.RewardObj.start_date);

                  $('.enddate').bootstrapDP('update', $scope.RewardObj.end_date);
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    else {
        $scope.PageTitle = "Add Reward";
    }

    $scope.SaveReward = function (form) {
        var custom = !$scope.checkCustomError();
        if (form.$valid && custom) {
            var isAddMode = ($scope.IsEditMode == true ? 0 : 1);
            $http.post(configurationService.basePath + "api/RewardApi/InsertOrUpdateReward?isAddMode=" + isAddMode + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, $scope.RewardObj)
           .then(function (response) {
               if (response.data > 0) {
                   notificationFactory.customSuccess("Reward Saved Successfully.");
                   $state.go('ShowReward');
               }
           })
           .catch(function (response) {
           })
           .finally(function () {
           });
        }
    }


    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('ShowReward');
                }
            });
        }
        else {
            $state.go('ShowReward');
        }

    }

    $scope.DeleteReward = function (id) {
        if (id > 0) {
            $http.get(configurationService.basePath + "api/RewardApi/DeleteReward?reward_id=" + id + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
            .then(function (response) {
                if (response.data == 0) {
                    notificationFactory.customError("Unable to Delete Reward.It's already started.");
                }
                if (response.data == 1) {
                    $state.go('ShowReward');
                    notificationFactory.customSuccess("Reward Deleted Successfully.");
                }
            })
            .catch(function (response) {

            })
            .finally(function () {

            });
        }
    }

    $scope.checkCustomError = function () {
       
        if (!angular.isUndefined($scope.RewardObj.end_date) && !angular.isUndefined($scope.RewardObj.start_date)) {

            var startDate = new Date($scope.RewardObj.start_date);
            var endDate = new Date($scope.RewardObj.end_date);

            if (startDate > endDate) {
                $scope.invalidEndDate = true;
                return true;
            }
            else {
                $scope.invalidEndDate = false;
                return false;
            }
        }
        else {
            return false;
        }
    }

    $scope.NeedTodisableRewardType = function () {
        var startDate = new Date($scope.RewardObj.start_date);
        var todayDate = new Date();

        if ($scope.IsEditMode == true && (startDate < todayDate)) {
            return true;
        }
        else {
            return false;
        }
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

});