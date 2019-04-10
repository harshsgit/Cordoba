app.controller('ManageCategoryController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions

    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.IsEditMode = false;

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.Category_Id = 0;
    $scope.CategoryStatus = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];

    $scope.CategoryObj = new Object();
    $scope.CategoryObj.CategoryDescriptionList = [];

    if ($stateParams.CategoryId != undefined && $stateParams.CategoryId != null) {
        $scope.PageTitle = "Update Category";
        $scope.Category_Id =parseInt($stateParams.CategoryId);
        $scope.IsEditMode = true;
    }
    else {
        $scope.PageTitle = "Add Category";
    }

    GetLanguageList();
    GetParentCategoryList();
    GetReportCategories();

    //Delete Category
    $scope.DeleteCategory = function () {
        bootbox.dialog({
            message: "Do you want remove category?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {

                            if (result) {
                                $http.post(configurationService.basePath + "api/CategoryApi/DeleteCategory?Category_Id=" + $scope.Category_Id + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                               .then(function (response) {
                                   if (response.data > 0)
                                       notificationFactory.successDelete();
                                   else {
                                       notificationFactory.FKReferenceDelete();
                                   }
                                   $state.go('ShowCategory');
                               })
                               .catch(function (response) {
                                   notificationFactory.errorDelete(response.data.ExceptionMessage);
                               })
                               .finally(function () {
                               });
                            }
                        }
                    },
                danger:
                    {
                        label: "NO",
                        className: "btn btn-default",
                        callback: function () {
                            return true;
                        }
                    }
            }
        });
    }

    //Get language list

    function GetLanguageList() {
        $http.get(configurationService.basePath + 'api/CategoryApi/GetLanguageList?StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                  .then(function (response) {
                      $scope.LanguageList = response.data;
                      $scope.language_id = $scope.LanguageList[0].language_id
                  })
                  .catch(function (response) {
                  })
                  .finally(function () {

                  });

    }

    function GetReportCategories() {
        $http.get(configurationService.basePath + "api/CategoryApi/GetReportCategories")
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.ReportCategoryList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetCategoryById = function () {
        $http.get(configurationService.basePath + "api/CategoryApi/GetCategoryById?Category_Id=" + $scope.Category_Id + "&StoreId=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId)
                  .then(function (response) {
                      $scope.CategoryObj = response.data;
                      $scope.CategoryObj.ReportCategoryID = response.data.ReportCategoryId;
                      CreateDescriptionObject();
                  })
                  .catch(function (response) {
                  })
                  .finally(function () {

                  });
    }
    $scope.GetCategoryById();
    //$http.get(configurationService.basePath + "api/CategoryApi/GetCategoryById?Category_Id=" + $scope.Category_Id)
    //  .then(function (response) {
    //      $scope.CategoryObj = response.data;
    //      CreateDescriptionObject();

    //  })
    //  .catch(function (response) {
    //  })
    //  .finally(function () {

    //  });


    $scope.CheckUnCheckList = function (index) {
        var temp = 0;
        if (index == 0) {
            if ($scope.CategoryObj.StoreList[0]['IsSelected'] == true) {
                for (var i = 1; i < $scope.CategoryObj.StoreList.length; i++) {
                    $scope.CategoryObj.StoreList[i]['IsSelected'] = true;
                }
            }
            else {
                for (var i = 1; i < $scope.CategoryObj.StoreList.length; i++) {
                    $scope.CategoryObj.StoreList[i]['IsSelected'] = false;
                }
            }
        }
        else {
            for (var i = 1; i < $scope.CategoryObj.StoreList.length; i++) {
                if($scope.CategoryObj.StoreList[i]['IsSelected'] == true)
                {
                    temp++;
                }
            }
            if (temp != $scope.CategoryObj.StoreList.length-1) {
                $scope.CategoryObj.StoreList[0]['IsSelected'] = false;
            }
            else {
                $scope.CategoryObj.StoreList[0]['IsSelected'] = true;
            }
        }
    }


    function CreateDescriptionObject() {
        var TempDescObject = [];
        angular.copy($scope.CategoryObj.CategoryDescriptionList, TempDescObject);
        $scope.CategoryObj.CategoryDescriptionList = [];
        angular.forEach($scope.LanguageList, function (col, i) {
            var CategoryDescObj = $filter('filter')(TempDescObject, { language_id: col.language_id }, true);
            if (CategoryDescObj == undefined || CategoryDescObj == null || CategoryDescObj.length == 0) {
                var DescObj = new Object();
                DescObj.language_id = col.language_id;
                DescObj.description = "";
                DescObj.name = "";
                //DescObj.CategoryDescription = "";
                $scope.CategoryObj.CategoryDescriptionList.push(DescObj);
            }
            else {
                $scope.CategoryObj.CategoryDescriptionList.push(CategoryDescObj[0]);
            }
        });

    }

    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('ShowCategory');
                }
            });
        }
        else {
            $state.go('ShowCategory');
        }
    }



    function GetParentCategoryList() {
         $http.get(configurationService.basePath + "api/CategoryApi/GetParentCategoryList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
        .then(function (response) {
            if (response.data.length > 0) {
                $scope.ParentCategoryList = response.data;
            }
        })

      .catch(function (response) {

      })
      .finally(function () {

      });
    }



    $scope.InsertOrUpdateCategory = function (form) {

        if (form.$valid) {
            $scope.CategoryObj.StoreIdCSV = "";
            $scope.CategoryObj.StoreIdCSV = GetSelectedStoreListCSV($scope.CategoryObj.StoreList);
            var categoryEntity = JSON.stringify($scope.CategoryObj);
            $http.post(configurationService.basePath + "api/CategoryApi/InsertOrUpdateCategory?StoreId=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId, categoryEntity)
              .then(function (response) {

                  if (response.data > 0) {
                      notificationFactory.customSuccess("Category Saved Successfully.");
                      $state.go('ShowCategory');                 
                  }
                  else if (response.data == -1) {
                      notificationFactory.customError("Category name is already Exists!");
                  }
              })
          .catch(function (response) {
              notificationFactory.error("Error occur during save record.");
          })
          .finally(function () {

          });

        }
    }

    function GetSelectedStoreListCSV(StoreObj) {
        var StoreIdCSV = "";
        var SelectedStoreList = $filter('filter')(StoreObj, { IsSelected: true }, true);
        StoreIdCSV = GetCSVFromJsonArray(SelectedStoreList, "store_id");
        return StoreIdCSV;
    }

    $scope.UploadImage = function () {
        var data = new FormData();
        var files = $("#ImageUpload").get(0).files;
        if (files.length == 0) {
            notificationFactory.customError("Please select atleast one file.");
            return notificationFactory;
        }
        var filename = files[0].name;
        //var extention = filename.substr(filename.lastIndexOf(".") + 1).toLowerCase();
        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("UploadedFile", files[0]);
        }

        var ajaxRequest = $.ajax({
            type: "POST",
            url: configurationService.basePath + 'api/CategoryApi/UploadCategoryImage?Category_Id=' + $scope.Category_Id,
            contentType: false,
            processData: false,
            data: data,
            success: function (response) {
                notificationFactory.customSuccess("Category Image Upload Successfully.");
                $('#ImageUpload').val('');
            },
            error: function (response) {
                notificationFactory.error("Error occur during image upload.");
            }
        });

        return ajaxRequest;

        //$http.post(configurationService.basePath + "api/CategoryApi/UploadCategoryImage?Category_Id=" + $scope.Category_Id, data)
        //      .then(function (response) {
        //          if (response.data > 0) {

        //              notificationFactory.customSuccess("Category Image Upload Successfully.");
        //              $state.go('ShowCategory');
        //          }
        //      })
        //  .catch(function (response) {
        //      notificationFactory.error("Error occur during image upload.");
        //  })
        //  .finally(function () {

        //  });





    }




})