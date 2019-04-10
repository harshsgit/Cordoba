app.controller('AddOrUpdateLanguageController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    $scope.StatusForActive = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.LanguageId = 0;
    $scope.LanguageObj = {};
    $scope.IsEditMode = false;
    if ($stateParams.LanguageId != undefined && $stateParams.LanguageId != null) {
        $scope.PageTitle = "Update Language";
        $scope.IsEditMode = true;
        $scope.languageId = $stateParams.LanguageId;
        $http.get(configurationService.basePath + "api/LanguageApi/GetLanguageList?languageId=" + $scope.languageId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
         .then(function (response) {    
             $scope.LanguageObj = response.data[0];
         })
        .catch(function (response) {

        })
        .finally(function () {

        });
    }
    else {
        $scope.PageTitle = "Add Language";
    }
    //#endregion



    $scope.SaveLanguage = function (form) {
        if (form.$valid) {
            //$scope.LanguageObj.image = $scope.LanguageObj.code + '.png';
    
            $http.post(configurationService.basePath + "api/LanguageApi/InsertOrUpdateLanguage?StoreId=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId, $scope.LanguageObj)
           .then(function (response) {
             
               if (response.data == 0) {
                   //alert('already exists');
                   notificationFactory.customError("Language Code already Exists!!");
               }
               if (response.data > 0) {
                   notificationFactory.customSuccess("Language Saved Successfully.");
                   $state.go('Language');
               }
           })
            .catch(function (response) {

            })
            .finally(function () {

            });
        }
    }

    $scope.DeleteLanguage = function () {
        bootbox.dialog({
            message: "Do you want remove language?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/LanguageApi/DeleteLanguage?languageId=" + $scope.languageId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                                   .then(function (response) {
                                       if (response.data == 1)
                                       {
                                           notificationFactory.customSuccess("Successfully Deleted!.");
                                           $state.go('Language');
                                       }
                                       else if (response.data==-1)
                                       {
                                           toastr.error("This language is referenced in other table. So this cannot be delete.");
                                       }
                                       else {
                                           toastr.error("There is something wrong! please try again later.");
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
                    $state.go('Language');
                }

            });
        }
        else {
            $state.go('Language');
        }

    }

});