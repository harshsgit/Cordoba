app.controller('ManageCatalogueController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.IsEditMode = false;
    $scope.CatalogueObj = new Object();
    $scope.catalogue_id = 0;
    if ($stateParams.CatalogueId != undefined && $stateParams.CatalogueId != null) {
        $scope.PageTitle = "Update Product Catalogue";
        $scope.catalogue_id = $stateParams.CatalogueId;
        $scope.IsEditMode = true;
        GetCatalogueById();
    }
    else {
        $scope.PageTitle = "Add Product Catalogue";
    }
    //#endregion


    $scope.InsertUpdateCatalogue = function (form) {
        if (form.$valid) {
            var catalogueEntity = JSON.stringify($scope.CatalogueObj);
            $http.post(configurationService.basePath + "api/CatalogueApi/InsertUpdateCatalogue?StoreId=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId, catalogueEntity)
                .then(function (response) {
                    if (response.data > 0) {
                        notificationFactory.customSuccess("Product Catalogue Saved Successfully.");
                        $state.go('ShowProductCatalogue');
                    }
                    else if (response.data == -1) {
                        notificationFactory.customError("Product Catalogue name already Exists!");
                    }
                })
                .catch(function (response) {
                })
                .finally(function () {
                    
                });
        }
    }

    $scope.DeleteCatalogue = function () {
        bootbox.dialog({
            message: "Do you want remove product Catalogue?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.get(configurationService.basePath + "api/CatalogueApi/DeleteCatalogue?catalogue_id=" + $scope.catalogue_id+ "&StoreId=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId)
                                   .then(function (response) {
                                       if (response.data > 0)
                                           notificationFactory.successDelete();
                                       $state.go('ShowProductCatalogue');
                                   })
                               .catch(function (response) {
                                   notificationFactory.errorDelete();
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

    function GetCatalogueById() {

        $http.get(configurationService.basePath + "api/CatalogueApi/GetCatalogueById?catalogue_id=" + $scope.catalogue_id + "&StoreId=" + $scope.StoreId + "&LoggedInUserId=" + $scope.LoggedInUserId)
          .then(function (response) {

              $scope.CatalogueObj = response.data;
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }


    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('ShowProductCatalogue');
                }
            });
        }
        else {
            $state.go('ShowProductCatalogue');
        }
    }

   

});