app.controller('AddOrUpdateUserController', function ($timeout,AdminUserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.user_id = 0;
    $scope.IsEditMode = false;
    if ($stateParams.UserID != undefined && $stateParams.UserID != null) {
        $scope.PageTitle = "Update User";
        $scope.IsEditMode = true;
        $scope.user_id = $stateParams.UserID;
    }
    else {
        $scope.PageTitle = "Add User";
    }
    //#endregion


    //Get Status List -- START
    $scope.UserGroupList = [
        { 'UserGroupId': 1, 'UserGroupName': 'Administrator' }
      , { 'UserGroupId': 11, 'UserGroupName': 'ClientAdmin' }
      , { 'UserGroupId': 10, 'UserGroupName': 'Demonstration' }
    ];
    //END

    //Get Status List -- START
    $scope.UserStatus = [
        { 'StatusId': 1, 'StatusName': 'Enabled' }
      , { 'StatusId': 0, 'StatusName': 'Disabled' }
    ];
    //END
    $scope.GetUserDetail = function () {
        $http.get(configurationService.basePath + "api/UserApi/GetUserDetail?UserID=" + $scope.user_id + '&StoreID=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              $scope.UserObj = response.data;
              if (!($scope.user_id>0)) //New User
              {
                  $scope.UserObj.status = 1;  // Temporary
                  $scope.UserObj.user_group_id = 1;  // Temporary
              }
            
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.SaveUser = function (form) {

        if (form.$valid) {      
            $scope.UserObj.user_id = $scope.user_id;
            $http.post(configurationService.basePath + "api/UserApi/CreateOrUpdateUser?LoggedInUserId=" + $scope.LoggedInUserId + '&StoreID=' + $scope.StoreId, $scope.UserObj)
         .then(function (response) {          
             if (response.data>0)
             {
                 toastr.success('Successfully Added.');
                 $state.go('Users');
             }
            
         })
      .catch(function (response) {

      })
     .finally(function () {

     });

        }
    }

    $scope.DeleteUser = function () {
        bootbox.dialog({
            message: "Do you want to remove User?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/UserApi/DeleteUserDetail?LoggedInUserId=" + $scope.LoggedInUserId +'&StoreID=' + $scope.StoreId + "&UserID=" + $scope.user_id)
                                        .then(function (response) {                                            
                                            if (response.data > 0) {
                                                toastr.success('User becomes Inactive.');
                                                $state.go('Users');
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
                    $state.go('Users');
                }

            });
        }
        else {
            $state.go('Users');
        }

    }
  


    $scope.GetUserDetail();

});