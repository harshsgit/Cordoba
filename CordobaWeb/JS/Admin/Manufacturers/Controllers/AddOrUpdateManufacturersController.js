app.controller('AddOrUpdateManufacturersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {

    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.manufacturer_id = 0;
    $scope.ManufacturerObj = new Object();
    $scope.IsEditMode = false;
    if ($stateParams.ManufacturerID != undefined && $stateParams.ManufacturerID != null && $stateParams.ManufacturerID>0)  {
        $scope.PageTitle = "Update Manufacturer";
        $scope.IsEditMode = true;
        $scope.manufacturer_id = $stateParams.ManufacturerID;
    }
    else {
        $scope.PageTitle = "Add Manufacturer";
    }
    //#endregion



    $scope.GetManufaturerDetail = function () {
        $http.get(configurationService.basePath + "api/ManufacturersApi/GetManufaturerDetail?manufacturer_id=" + $scope.manufacturer_id + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              $scope.ManufacturerObj = response.data;         
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    function GetSelectedStoreListCSV(StoreListObj)
    {
        var StoreIdCSV = "";
        var SelectedStoreList = $filter('filter')(StoreListObj, { IsSelected: true }, true);
        StoreIdCSV = GetCSVFromJsonArray(SelectedStoreList, "store_id");
        return StoreIdCSV;

    }
    $scope.InsertUpdateManufacture = function (form) {
        if (form.$valid) {          
            $scope.ManufacturerObj.StoreIdCSV = GetSelectedStoreListCSV($scope.ManufacturerObj.ManufacturerStoreList.ManufacturerStore);
            var manufacturersEntity = JSON.stringify($scope.ManufacturerObj);
            $http.post(configurationService.basePath + "api/ManufacturersApi/InsertUpdateManufacture?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, manufacturersEntity)
              .then(function (response) {
                  if (response.data > 0) {
                      notificationFactory.customSuccess("Manufacturer Saved Successfully.");
                      $state.go('ShowManufacturer');
                  }
                  else if (response.data == -1) {
                      notificationFactory.customError("Manufacturer name already Exists!");
                  }
              })
          .catch(function (response) {
              notificationFactory.error("Error occur during save record.");
          })
          .finally(function () {

          });
        }
    }

    $scope.DeleteManufacturer = function () {
        bootbox.dialog({
            message: "Do you want remove Manufacturer?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {

                                $http.get(configurationService.basePath + "api/ManufacturersApi/DeleteManufacturer?manufacturer_id=" + $scope.manufacturer_id + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                                  .then(function (response) {
                                      if (response.data > 0)
                                          notificationFactory.successDelete();
                                      $state.go('ShowManufacturer');
                                  })
                              .catch(function (response) {
                                  notificationFactory.errorEdit();
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
                    $state.go('ShowManufacturer');
                }

            });
        }
        else {
            $state.go('ShowManufacturer');
        }

    }

    $scope.toggleAll = function (id) {
        if (id == 0)
        {
            if($scope.ManufacturerObj.ManufacturerStoreList.ManufacturerStore[0]["IsSelected"] == true){
                angular.forEach($scope.ManufacturerObj.ManufacturerStoreList.ManufacturerStore, function (itm) { itm.IsSelected = true; });
            }
            else {
                angular.forEach($scope.ManufacturerObj.ManufacturerStoreList.ManufacturerStore, function (itm) { itm.IsSelected = false; });
            }
          
        }
        else {
            $scope.ManufacturerObj.ManufacturerStoreList.ManufacturerStore[0]["IsSelected"] = false;
            return true;
        }
       
        
        

    }

    $scope.getCatagoryList = function () {

    }


    $scope.GetManufaturerDetail();

});