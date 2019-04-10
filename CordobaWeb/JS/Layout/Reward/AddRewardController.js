app.controller('AddRewardController', function (UserDetail, StoreSessionDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {

    decodeParams($stateParams);
    BindToolTip();
    $scope.PageTitle = "Rewards";

    $scope.reward_type_id = $stateParams.type;
    $scope.loginUserid = UserDetail.customer_id;
    $scope.reward_id = $stateParams.rewardId;
    $scope.StoreDetailInSession = StoreSessionDetail;

    //$scope.$on('broadcastAddCustomerRating', function () { AddCustomerRating(); });


    $scope.GetRewardGroupCustomers = function () {
        $http.get(configurationService.basePath + "api/RewardApi/GetRewardGroupCustomers?StoreId=" + $scope.StoreDetailInSession.store_id + "&loginUserId=" + $scope.loginUserid + "&reward_id=" + $scope.reward_id)
           .then(function (response) {
               if (response.data.length > 0) {
                   $scope.Customers = response.data;
               }
           })
          .catch(function (response) {

          })
          .finally(function () {

          });
    }

    $scope.GetRewardGroupCustomers();

    $scope.MakeWriteCmtFadeIn = function (id) {
        $('#write-cmt' + id).fadeIn();
    }
    $scope.MakeWriteCmtFadeOut = function (id) {
        $('#write-cmt' + id).fadeOut();
    }

    //$scope.MarkAlreadyStargiven = function () {

    //    $scope.isreadonlyAleardyAssigned = false;
    //    for (var i = 0; i < $scope.Customers.length; i++) {

    //        if ($scope.Customers[i].IsRewarded == true) {

    //            $("#ratingValue" + i).find('i').removeClass('fa-star-o').addClass('fa-star')
    //            $("#ratingValue" + i + " *").attr("disabled", "disabled").off('click');
    //        }
    //    }
    //}



    //////region star ratting Directive deafult setting
    $scope.isReadonly = false; // default test value
    $scope.changeOnHover = false; // default test value
    $scope.maxValue = 5; // default test value
    //////region star ratting Directive deafult setting
    $scope.ratingValue = 0;
    $scope.AddReward = function (item, index, IsComment) { 
        if (parseInt($scope.reward_type_id) == 2) { 
            var ratingValue = $("#medalDiv" + index).find('input[type=radio]:checked').val();
            if ($("#medalDiv" + index).find('input[type=radio]:checked').length > 0) {
                var objChecked = $("#medalDiv" + index).find('input[type=radio]:checked');
                $scope.AddRewardObj = item;

                if ($scope.AddRewardObj.IsRewarded != 1) {

                    $scope.AddRewardObj.reward_id = parseInt($scope.reward_id);
                    $scope.AddRewardObj.reward_name = ratingValue;
                    $scope.AddRewardObj.IsWinner = false;
                    $scope.AddRewardObj.loginUserid = parseInt($scope.loginUserid);
                    $scope.AddRewardObj.Comment = $("#writeTxtArea" + index).val();
                    $scope.AddRewardObj.reward_type_id = parseInt($scope.reward_type_id);

                    bootbox.dialog({
                        message: "Do you want Give Reward to selected User? Once you will give reward you can't modify it. Please make sure once again.",
                        title: "Confirmation",
                        className: "model",
                        buttons: {
                            success:
                                {
                                    label: "Yes",
                                    className: "btn btn-primary theme-btn",
                                    callback: function (result) {
                                        if (result) {
                                            $http.post(configurationService.basePath + "api/RewardApi/AddCustomer_Reward?StoreId=" + $scope.StoreDetailInSession.store_id + "&LoggedInUserId=" + $scope.loginUserid, $scope.AddRewardObj)
                                              .then(function (response) {

                                                  if (response.data > 0) {
                                                      notificationFactory.customSuccess("Reward Saved Successfully.");
                                                      $scope.GetRewardGroupCustomers();
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
                                        $("#" + objChecked.attr('id')).prop('checked', false);
                                        return true;
                                    }
                                }
                        }
                    });
                }
                if (IsComment == 1) {
                    $scope.AddRewardObj.reward_id = parseInt($scope.reward_id);
                    $scope.AddRewardObj.NoOfStars = parseInt(ratingValue);
                    $scope.AddRewardObj.IsWinner = false;
                    $scope.AddRewardObj.loginUserid = parseInt($scope.loginUserid);
                    $scope.AddRewardObj.Comment = $("#writeTxtArea" + index).val();
                    $scope.AddRewardObj.reward_type_id = parseInt($scope.reward_type_id);

                    $http.post(configurationService.basePath + "api/RewardApi/AddCustomer_Reward?StoreId=" + $scope.StoreDetailInSession.store_id + "&LoggedInUserId=" + $scope.loginUserid, $scope.AddRewardObj)
                                                .then(function (response) {
                                                    if (response.data > 0) {
                                                        notificationFactory.customSuccess("Reward Comment Saved Successfully.");

                                                        $scope.GetRewardGroupCustomers();
                                                    }
                                                })
                                                 .catch(function (response) {

                                                 })
                                                  .finally(function () {

                                                  });

                }
            }
            else {
                notificationFactory.customError("Please select Medal.");
            }
        }

        if (parseInt($scope.reward_type_id) == 1) {         
            var ratingValue = $("#ratingValue" + index + " .fa-star").find('i').prevObject.size();
            if (ratingValue > 0) {
                $scope.AddRewardObj = item;
                if ($scope.AddRewardObj.IsRewarded != 1) {

                    $scope.AddRewardObj.reward_id = parseInt($scope.reward_id);
                    $scope.AddRewardObj.NoOfStars = parseInt(ratingValue);
                    $scope.AddRewardObj.IsWinner = false;
                    $scope.AddRewardObj.loginUserid = parseInt($scope.loginUserid);
                    $scope.AddRewardObj.Comment = $("#writeTxtArea" + index).val();
                    $scope.AddRewardObj.reward_type_id = parseInt($scope.reward_type_id);


                    bootbox.dialog({
                        message: "Do you want Give Reward to selected User? Once you will give reward you can't modify it. Please make sure once again.",
                        title: "Confirmation",
                        className: "model",
                        buttons: {
                            success:
                                {
                                    label: "Yes",
                                    className: "btn btn-primary theme-btn",
                                    callback: function (result) {
                                        if (result) {
                                            $http.post(configurationService.basePath + "api/RewardApi/AddCustomer_Reward?StoreId=" + $scope.StoreDetailInSession.store_id + "&LoggedInUserId=" + $scope.loginUserid, $scope.AddRewardObj)
                                                .then(function (response) {
                                                    if (response.data > 0) {
                                                        notificationFactory.customSuccess("Reward Saved Successfully.");
                                                        //$scope.MakeWriteCmtFadeIn(index);
                                                        $scope.GetRewardGroupCustomers();
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
                                        $("#ratingValue" + index + " .fa-star").removeClass('ng-scope fa fa-star fa-2x $$hashKey').addClass('ng-scope fa fa-star-o fa-2x $$hashKey');
                                   
                               
                                                                              
                                        return true;
                                    }
                                }
                        }
                    });
                }
                if (IsComment == 1) {
                    $scope.AddRewardObj.reward_id = parseInt($scope.reward_id);
                    $scope.AddRewardObj.NoOfStars = parseInt(ratingValue);
                    $scope.AddRewardObj.IsWinner = false;
                    $scope.AddRewardObj.loginUserid = parseInt($scope.loginUserid);
                    $scope.AddRewardObj.Comment = $("#writeTxtArea" + index).val();
                    $scope.AddRewardObj.reward_type_id = parseInt($scope.reward_type_id);

                    $http.post(configurationService.basePath + "api/RewardApi/AddCustomer_Reward?StoreId=" + $scope.StoreDetailInSession.store_id + "&LoggedInUserId=" + $scope.loginUserid, $scope.AddRewardObj)
                                                .then(function (response) {
                                                    if (response.data > 0) {
                                                        notificationFactory.customSuccess("Reward Comment Saved Successfully.");
                                                         
                                                        $scope.GetRewardGroupCustomers();
                                                    }
                                                })
                                                 .catch(function (response) {

                                                 })
                                                  .finally(function () {

                                                  });

                }
            }
            else {
                notificationFactory.customError("Please select rating.");           
            }
        }
    }
});