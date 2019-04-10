app.controller('PointsImporterController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion  

    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.store_id = $rootScope.storeId;
    $scope.IsSendEmail = false;
    $scope.IsStoreDropDownEnabled = false;
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);

    $scope.PageTitle = "Customer Points Importer";
    
    //Remove local storage of ther pages
    //$rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    //$rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    //$rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId)
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



    $scope.PointsImporter = function () {
        if ($scope.store_id == '' || $scope.files == undefined) {
            toastr.error("Select Store & file");
            return;
        }

        var fd = new FormData();
        for (var i in $scope.files) {
            fd.append("uploadedFile", $scope.files[i]);
        }
        var xhr = new XMLHttpRequest();
        //xhr.upload.addEventListener("progress", uploadProgress, false);
        xhr.addEventListener("load", uploadComplete, false);
        xhr.addEventListener("error", uploadFailed, false);
        xhr.addEventListener("abort", uploadCanceled, false);

        xhr.open("POST", configurationService.basePath + "api/CustomerApi/PointsImporter?store_id=" + $scope.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId + '&IsSendEmail=' + $scope.IsSendEmail);

        $scope.progressVisible = true;

        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) {

                if (xhr.status == 200) {
                    if (xhr.response == "0") {
                        toastr.error("Records Not found!");
                    } else {
                        var invalidEmails = JSON.parse(xhr.response);
                        //console.log(invalidEmails[0]["invalidEmail"])
                        if (invalidEmails[0]["invalidEmail"]!='' || invalidEmails[0]["MinusPointTotalEmail"]!='') {
                            //var uploadHtml = "<p>This Email does not exist: " + invalidEmails[0]["invalidEmail"].substring(0, invalidEmails[0]["invalidEmail"].length - 1) + "</p>";
                            var uploadHtml = "";
                            
                            if (invalidEmails[0]["invalidEmail"] != '')
                            {
                                uploadHtml += "<p>This Email does not exist: " + invalidEmails[0]["invalidEmail"] + "</p></br>";
                            }
                            if (invalidEmails[0]["MinusPointTotalEmail"] != '') {
                                uploadHtml += "<p>This Email has minus Total Points : " + invalidEmails[0]["MinusPointTotalEmail"] + "</p>";
                            }
                            bootbox.dialog({
                                message: uploadHtml,
                                title: "Alert",
                                buttons: {
                                    success: {
                                        label: "Ok",
                                        className: "btn btn-info",
                                        //callback: function () {
                                        //    uploadUserImage();
                                        //}
                                    }
                                }
                            });
                        }
                        toastr.success("File Successfully Submitted.");
                        $("#upload").val(null);
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
            })

            toastr.error("There is improper data in .xlsx  OR .xls file.");

        };
        xhr.send(fd);
    }

    $scope.setFiles = function (element) {
        checkfile(element);
        if (!checkfile(element)) {
            return;
        }
        $scope.$apply(function ($scope) {
            $scope.files = [];
            var validExts = new Array(".xlsx", ".xls");
            for (var i = 0; i < element.files.length; i++) {
                $scope.files.push(element.files[i]);
            }
            $scope.progressVisible = false
        });
    };

    function checkfile(sender) {
        var validExts = new Array(".xlsx", ".xls");
        var fileExt = sender.value;
        fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
        if (validExts.indexOf(fileExt) < 0) {
            toastr.error("Invalid file selected, valid files are of " +
                     validExts.toString() + " types.");
            return false;
        }
        else return true;
    }

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

    $scope.CheckStoreDropDownEnabled = function () {
        if (!($scope.store_id > 0)) {
            $scope.IsStoreDropDownEnabled = true;
        }

    }




    GetStoreList();
});