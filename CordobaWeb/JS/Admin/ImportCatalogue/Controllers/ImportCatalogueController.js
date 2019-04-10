app.controller('ImportCatalogueController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, DTColumnDefBuilder) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.CatalogueList = [];
    $scope.LanguageList = [];
    $scope.SupplierList = [];
    $scope.NotValidRecordsInImport = [];
    //#endregion  

    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true)
                     .withOption('searching', false)
                     .withOption('paging', false);

    $scope.dtColumnDefs = [DTColumnDefBuilder.newColumnDef(2).notSortable()];
                              
    $scope.PageTitle = "Import Catalogue";

    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    $scope.ImportCatalogueObject = new Object();
    GetSupplierList();
    GetLanguageList();
    GetCatalogueList();

    function GetSupplierList() {
        $http.get(configurationService.basePath + "api/SupplierApi/GetSupplierList?storeId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId+'&SupplierID=0')    
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.SupplierList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.supplier_id = 0;
                  DefaultOption.name = " --- None --- ";
                  $scope.SupplierList.push(DefaultOption);
                  $scope.ImportCatalogueObject.SupplierId = 0;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    function GetLanguageList() {
        $http.get(configurationService.basePath + 'api/CategoryApi/GetLanguageList?StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                  .then(function (response) {
                      $scope.LanguageList = response.data;
                      $scope.ImportCatalogueObject.language_id = $scope.LanguageList[0].language_id;
                  })
                  .catch(function (response) {
                  })
                  .finally(function () {

                  });

    }

    function GetCatalogueList() {
        $http.get(configurationService.basePath + "api/CatalogueApi/GetCatalogueList?StoreId=" + $scope.storeId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.CatalogueList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.catalogue_Id = 0;
                  DefaultOption.Name = " --- None --- ";
                  $scope.CatalogueList.push(DefaultOption);
                  $scope.ImportCatalogueObject.catalogue_Id = 0;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.ImportCatalogue = function (IsConfirmToIgnore) {
        if ($scope.ImportCatalogueObject.SupplierId == 0 || $scope.ImportCatalogueObject.language_id == 0 || $scope.ImportCatalogueObject.catalogue_Id == 0) {
            return false;
        }
        else {
            if ($scope.files == null || $scope.files == undefined) {
                toastr.error("Please select file for import");
            }
            else {
                var fd = new FormData();

                for (var i in $scope.files) {
                    fd.append("uploadedFile", $scope.files[i]);
                }

                var xhr = new XMLHttpRequest();
                //xhr.upload.addEventListener("progress", uploadProgress, false);
                xhr.addEventListener("load", uploadComplete, false);
                xhr.addEventListener("error", uploadFailed, false);
                xhr.addEventListener("abort", uploadCanceled, false);


                xhr.open("POST", configurationService.basePath + "api/CatalogueApi/ImportCatalogue?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId +
                    "&supplier_id=" + $scope.ImportCatalogueObject.SupplierId + "&language_id=" + $scope.ImportCatalogueObject.language_id + "&catalogue_id=" + $scope.ImportCatalogueObject.catalogue_Id +
                    "&IsConfirmToIgnore=" + IsConfirmToIgnore);

                $scope.progressVisible = true;

                xhr.onreadystatechange = function () {                  
                    if (xhr.readyState == 4) {
                        if (xhr.status == 200) {                            
                            if ($.parseJSON(xhr.responseText).length > 0) {                                                           
                                $scope.$apply(function () {
                                    $scope.NotValidRecordsInImport = $.parseJSON(xhr.responseText);
                                })

                            }
                            else {
                                toastr.success("File Successfully Submitted.");
                                location.reload(true);
                            }
                        } else {
                            $scope.$apply(function () {
                                $scope.progress = "Improper data in file";
                            })
                            toastr.error("There is improper data in .xlsx  OR .xls file.");
                        }
                    }
                };
                xhr.onerror = function () {
                    $scope.$apply(function () {
                        $scope.progress = "Improper data in file";
                    });
                    toastr.error("There is improper data in .xlsx  OR .xls file.");
                };
                xhr.send(fd);
            }

        }
    }

    $scope.setFiles = function (element) {
        $scope.$apply(function ($scope) {
            $scope.files = [];
            for (var i = 0; i < element.files.length; i++) {
                $scope.files.push(element.files[i]);
            }
            $scope.progressVisible = false
        });
    };

    function uploadComplete(evt) {
        /* This event is raised when the server send back a response */
        //alert(evt.target.responseText);

    }

    function uploadFailed(evt) {
        alert("There was an error attempting to upload the file.");
    }

    function uploadCanceled(evt) {
        $scope.$apply(function () {
            $scope.progressVisible = false;
        })
        alert("The upload has been canceled by the user or the browser dropped the connection.");
    }

    $scope.ConfirmIgnoreDialog = function () {
        angular.element("#DivConfirmIgnore").modal('show');
    }
});