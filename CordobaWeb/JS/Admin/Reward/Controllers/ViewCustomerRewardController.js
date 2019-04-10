app.controller('ViewCustomerRewardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "View Reward";
    $scope.ViewRewards = [];
    $scope.rewardId = $stateParams.rewardId;

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.GetRewardList = function () {
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardList?reward_id=" + $scope.rewardId + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
       
              if (response.data.length > 0) {
                  $scope.rewardTypeId = response.data[0].reward_type_id;
                  $scope.RewardName = response.data[0].Title;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetRewardList();


    $scope.ViewCustomerRewards = function () {
        if ($scope.rewardId != undefined && $scope.rewardId != null) {
            $http.get(configurationService.basePath + "api/RewardApi/ViewCustomerRewards?reward_id=" + $scope.rewardId + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
            .then(function (response) {
         
                $scope.ViewRewards = response.data;
            })
            .catch(function (response) {

            })
            .finally(function () {

            });
        }
    }

    $scope.ViewCustomerRewards();


    //////region star ratting Directive deafult setting
    $scope.isReadonly = false; // default test value
    $scope.changeOnHover = false; // default test value
    $scope.maxValue = 5; // default test value
    //////region star ratting Directive deafult setting

    $scope.RewardWinnerModal = function (CustomerName, reward_user_id, Rewards) {
        $scope.WinnerName = CustomerName;
        $scope.reward_user_id = reward_user_id;
        $scope.WinnerMedal = Rewards;
        angular.element('#UserWinnerModal').modal('show');
    }


    $scope.Declare_RewardWinner = function () {
    

        bootbox.dialog({
            message: "Do you want declare this user as winner?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/RewardApi/Declare_RewardWinner?reward_id=" + $scope.rewardId + "&reward_user_id=" + $scope.reward_user_id + "&admin_comment=" + $scope.AdminComment + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                                  .then(function (response) {
                              
                                      if (response.data > 0) {
                                          notificationFactory.customSuccess("Reward Winner Declared Successfully.");
                                          angular.element('#UserWinnerModal').modal('hide');
                                          $scope.ViewCustomerRewards();
                                      }
                                  })
                                  .catch(function (response) {

                                  })
                                  .finally(function () {

                                  });
                            }
                        }
                    },
                danger:
                    {
                        label: "No",
                        className: "btn btn-default",
                        callback: function () {
                            return true;
                        }
                    }
            }
        });
    }


    $scope.Delete_RewardWinner = function (reward_user_id) {
        bootbox.dialog({
            message: "Do you want delete this user as winner?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/RewardApi/Delete_RewardWinner?reward_user_id=" + reward_user_id + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                                  .then(function (response) {
                         
                                      if (response.data > 0) {
                                          notificationFactory.customSuccess("Reward Winner Deleted Successfully.");
                                          $scope.ViewCustomerRewards();
                                      }
                                  })
                                  .catch(function (response) {

                                  })
                                  .finally(function () {

                                  });
                            }
                        }
                    },
                danger:
                    {
                        label: "No",
                        className: "btn btn-default",
                        callback: function () {
                            return true;
                        }
                    }
            }
        });
    }
});