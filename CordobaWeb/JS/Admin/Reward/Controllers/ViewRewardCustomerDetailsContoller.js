app.controller('ViewRewardCustomerDetailsContoller', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.PageTitle = "Customer Reward Details";
    $scope.CustomerName = $stateParams.customername;
    //$scope.RewardName = $stateParams.name;
    $scope.CustomerRewards = [];

    if ($stateParams.rewarduserid != undefined && $stateParams.rewarduserid != null) {
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardCustomerDetails?reward_user_id=" + $stateParams.rewarduserid + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
        .then(function (response) {
           
            $scope.CustomerRewards = response.data;
        })
        .catch(function (response) {

        })
        .finally(function () {

        });
    }
    $scope.DeleteRewardUser = function (id) {
        if ($stateParams.rewarduserid != null) {
       
            bootbox.dialog({
                message: "Do you want remove Reward given by User?",
                title: "Confirmation",
                className: "model",
                buttons: {
                    success:
                        {
                            label: "Yes",
                            className: "btn btn-primary theme-btn",
                            callback: function (result) {
                                if (result) {
                                    $http.get(configurationService.basePath + "api/RewardApi/DeleteRewardUser?id=" + id + "&reward_user_id=" + $stateParams.rewarduserid + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                                       .then(function (response) {
                                           $state.go('ShowReward');
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
    }

    
});