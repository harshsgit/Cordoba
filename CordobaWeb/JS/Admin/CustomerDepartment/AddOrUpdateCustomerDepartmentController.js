app.controller('AddOrUpdateCustomerDepartmentController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.CustomerDepartmentId = 0;
    $scope.CustomerDepartmentObj = {};
    $scope.CustomerDepartmentObj.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.IsEditMode = false;

    $scope.CustomerDeptStatus = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];

    if ($stateParams.CustomerDepartmentId != undefined && $stateParams.CustomerDepartmentId != null) {
        $scope.PageTitle = "Update Customer Department";
        $scope.IsEditMode = true;
        $scope.CustomerDepartmentId = $stateParams.CustomerDepartmentId;
        $http.get(configurationService.basePath + "api/CustomerDepartmentApi/GetCustomerDepartmentById?CustomerDepartmentId=" + $stateParams.CustomerDepartmentId)
          .then(function (response) {          
              if (response.data.length > 0) {
                  $scope.CustomerDepartmentObj = response.data[0];
                  $scope.CustomerDepartmentObj.LoggedInUserId = $rootScope.loggedInUserId;
                  if ($scope.CustomerDepartmentObj.status == true)
                  {
                      $scope.CustomerDepartmentObj.statuscode = 1;
                  }
                  else if ($scope.CustomerDepartmentObj.status == false)
                  {
                      $scope.CustomerDepartmentObj.statuscode = 0;
                  }
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    else {
        $scope.PageTitle = "Add Customer Department";
    }
    //#endregion



    $scope.SaveCustomerDepartment = function (form) {

        if (form.$valid) {
            if ($scope.CustomerDepartmentObj.statuscode == 1)
            {
                $scope.CustomerDepartmentObj.status = true;
            }
            else {
                $scope.CustomerDepartmentObj.status = false;
            }
            $http.post(configurationService.basePath + "api/CustomerDepartmentApi/InsertOrUpdateCustomerDepartment", $scope.CustomerDepartmentObj)
            .then(function (response) {
                if (response.data == 0) {
                    notificationFactory.customError("Customer Department is already Exists!!");
                }
                if (response.data > 0) {
                    notificationFactory.customSuccess("Customer Department Saved Successfully.");
                    $state.go('ShowCustomerDepartment');
                }
            })
             .catch(function (response) {

             })
      .finally(function () {

      });

        }
    }

    $scope.DeleteCustomerDepartment = function () {
        bootbox.dialog({
            message: "Do you want remove customer department?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/CustomerDepartmentApi/DeleteCustomerDepartment?customerDepartmentId=" + $scope.CustomerDepartmentId + '&StoreId=' + $scope.StoreId)
                                   .then(function (response) {
                                       notificationFactory.customSuccess("Successfully Deleted!.");
                                       $state.go('ShowCustomerDepartment');
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
                    $state.go('ShowCustomerDepartment');
                }

            });
        }
        else {
            $state.go('ShowCustomerDepartment');
        }

    }

    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId)
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