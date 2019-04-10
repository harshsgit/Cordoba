app.controller('AddOrUpdateCustomerGroupController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {



    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.customer_group_id = $stateParams.CustomerGroupId;
    $scope.IsEditMode = false;
    if ($stateParams.CustomerGroupId != undefined && $stateParams.CustomerGroupId != null) {

        $scope.PageTitle = "Update Customer Group";
        $scope.IsEditMode = true;
        $scope.customer_group_id = $stateParams.CustomerGroupId;
    }
    else {
        $scope.PageTitle = "Add Customer Group";
    }
    //#endregion


    $scope.GetCustomerGroupDetail = function () {


        $http.get(configurationService.basePath + "api/CustomerGroupApi/GetCustomerGroupDetail?StoreId=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId + "&CustomerGroupId=" + $scope.customer_group_id)
          .then(function (response) {
         
              $scope.CustomerGroupObj = response.data;
              if (!($scope.user_id > 0)) //New User
              {
                  //$scope.CustomerGroupObj.status = 1;  // Temporary
                  //$scope.CustomerGroupObj.user_group_id = 1;  // Temporary
              }

          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.SaveCustomerGroup = function (form) {


        if (form.$valid) {
     
            $scope.CustomerGroupObj.status = 1;
            $scope.CustomerGroupObj.customer_group_id = $scope.customer_group_id;
            $http.post(configurationService.basePath + "api/CustomerGroupApi/CreateOrUpdateCustomerGroup?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, $scope.CustomerGroupObj)
         .then(function (response) {
             if (response.data > 0) {
                 if ($scope.customer_group_id == 0)
                     {
                 toastr.success('Successfully Added.');
                 }
                 else {
                     toastr.success('Successfully Updated.');
                 }
                 $state.go('CustomerGroup');
             }

         })
      .catch(function (response) {

      })
     .finally(function () {

     });

        }
    }




    $scope.DeleteCustomerGroup = function () {
        bootbox.dialog({
            message: "Do you want remove Customer Group?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/CustomerGroupApi/DeleteCustomerGroup?CustomerGroupId=" + $scope.customer_group_id + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                                        .then(function (response) {
                                            if (response.data > 0) {
                                                toastr.success('Successfully Deleted.');
                                                $state.go('CustomerGroup');
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
    };
    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('CustomerGroup');
                }

            });
        }
        else {
            $state.go('CustomerGroup');
        }

    }



    $scope.GetCustomerGroupDetail();



});