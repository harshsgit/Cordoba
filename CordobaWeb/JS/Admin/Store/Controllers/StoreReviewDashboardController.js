app.controller('StoreReviewDashboardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.StoreHTMLSummary = [];
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    var currentTime = new Date();
    $scope.Years = [{ id: currentTime.getFullYear(), name: currentTime.getFullYear() }, { id: currentTime.getFullYear() - 1, name: currentTime.getFullYear() - 1 }];

    $scope.selectedyear = $scope.Years[0].id;
    const MONTH_NAMES = [{ id: 1, name: "January" }, { id: 2, name: "February" }, { id: 3, name: "March" },
    { id: 4, name: "April" }, { id: 5, name: "May" }, { id: 6, name: "June" },
    { id: 7, name: "July" }, { id: 8, name: "August" }, { id: 9, name: "September" },
    { id: 10, name: "October" }, { id: 11, name: "November" }, { id: 12, name: "December" }];

    $scope.selectedmonth = 0;
    $scope.monthname = ''
    $scope.GetMonthData = function (year) {
        $scope.Months = [];
        if (year == currentTime.getFullYear()) {
            for (var i = 0; i < currentTime.getMonth() + 1; i++) {
                $scope.Months.push({ id: MONTH_NAMES[i].id, name: MONTH_NAMES[i].name });
            }
        }
        else {
            for (var i = 0; i < 12; i++) {
                $scope.Months.push({ id: MONTH_NAMES[i].id, name: MONTH_NAMES[i].name });
            }
        }
        $scope.selectedmonth = $scope.Months[0].id;
        $scope.monthname = $scope.Months[0].name;
    }
    //Model Popup
    $scope.OrderPlaceTypeList = [];
    $scope.OrderByTypeModalClick = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetOrderPlacedByTypeByStoreList?storeId=" + $scope.StoreId +
            "&Month=" + $scope.selectedmonth + "&Year=" + $scope.selectedyear + "&UserId=" + $scope.LoggedInUserId)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.OrderPlaceTypeList = response.data;
                }
                $("#OrderByTypeModal").modal('show');
            })
            .catch(function (response) {
            })
            .finally(function () {
            });

    }
    $scope.exportToExcelOrderPlaceType = function () {
        $http({
            url: configurationService.basePath + "api/StoreApi/GetOrderPlacedByTypeByStoreListExport?storeId=" + $scope.StoreId +
                "&Month=" + $scope.selectedmonth + "&Year=" + $scope.selectedyear + "&UserId=" + $scope.LoggedInUserId,
            method: "POST",
            'dataSrc': 'aaData',
            "dataType": 'json',
            headers: {
                'Content-type': 'application/json'
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {
            exportToExcelCommon(data, status, headers, config);
        }).error(function (data, status, headers, config) {
        });


    }



    $scope.TopPointsHolderList = [];
    $scope.TopPointsHolderModalClick = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetTOPPointsHoldersByStoreList?storeId=" + $scope.StoreId +
            "&UserId=" + $scope.LoggedInUserId)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.TopPointsHolderList = response.data;
                }
                $("#TopPointsHolderModel").modal('show');
            })
            .catch(function (response) {
            })
            .finally(function () {
            });

    }

    $scope.exportToExcelTopPointsHolder = function () {
        $http({
            url: configurationService.basePath + "api/StoreApi/GetTOPPointsHoldersByStoreListExport?storeId=" + $scope.StoreId +
                "&UserId=" + $scope.LoggedInUserId,
            method: "POST",
            'dataSrc': 'aaData',
            "dataType": 'json',
            headers: {
                'Content-type': 'application/json'
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {
            exportToExcelCommon(data, status, headers, config);
        }).error(function (data, status, headers, config) {
        });


    }
    function exportToExcelCommon(data, status, headers, config) {
        var type = headers('Content-Type');
        var disposition = headers('Content-Disposition');
        if (disposition) {
            var match = disposition.match(/.*filename=\"?([^;\"]+)\"?.*/);
            if (match[1])
                defaultFileName = match[1];
        }
        defaultFileName = defaultFileName.replace(/[<>:"\/\\|?*]+/g, '_');

        var blob = new Blob([data], { type: type });

        if (navigator.appVersion.toString().indexOf('.NET') > 0) // For IE 
            window.navigator.msSaveBlob(blob, defaultFileName);
        else {
            var objectUrl = URL.createObjectURL(blob);
            var downloadLink = document.createElement("a");
            downloadLink.href = objectUrl;
            downloadLink.download = defaultFileName;
            document.body.appendChild(downloadLink);
            downloadLink.click();
            document.body.removeChild(downloadLink);
        }
    }


    $scope.PointsRedeemList = [];
    $scope.PointsRedeemModalClick = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetPointsRedeemedByMonthByStoreList?StoreID=" + $scope.StoreId +
            "&Month=" + $scope.selectedmonth + "&Year=" + $scope.selectedyear + "&UserId=" + $scope.LoggedInUserId)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.PointsRedeemList = response.data;
                }
                $("#PointsRedeemModel").modal('show');
            })
            .catch(function (response) {
            })
            .finally(function () {
            });

    }
    $scope.exportToExcelPointsRedeem = function () {
        $http({
            url: configurationService.basePath + "api/StoreApi/GetPointsRedeemedByMonthByStoreListExport?StoreID=" + $scope.StoreId +
                "&Month=" + $scope.selectedmonth + "&Year=" + $scope.selectedyear + "&UserId=" + $scope.LoggedInUserId,
            method: "POST",
            'dataSrc': 'aaData',
            "dataType": 'json',
            headers: {
                'Content-type': 'application/json'
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {
            exportToExcelCommon(data, status, headers, config);
        }).error(function (data, status, headers, config) {
        });


    }


    $scope.VoucherOrderByTypeList = [];
    $scope.VoucherOrderByTypeModalClick = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetVoucherOrderByTypeList?StoreID=" + $scope.StoreId +
            "&UserId=" + $scope.LoggedInUserId)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.VoucherOrderByTypeList = response.data;
                }
                $("#VoucherOrderByTypeModel").modal('show');
            })
            .catch(function (response) {
            })
            .finally(function () {
            });

    }
    $scope.exportToExcelVoucherOrderByType = function () {
        $http({
            url: configurationService.basePath + "api/StoreApi/GetVoucherOrderByTypeListExport?StoreID=" + $scope.StoreId +
                "&UserId=" + $scope.LoggedInUserId,
            method: "POST",
            'dataSrc': 'aaData',
            "dataType": 'json',
            headers: {
                'Content-type': 'application/json'
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {
            exportToExcelCommon(data, status, headers, config);
        }).error(function (data, status, headers, config) {
        });


    }



    $scope.PaticipantsStoreSummaryList = [];
    $scope.activatedParticipants = 0;
    $scope.notActivatedParticipants = 0;
    $scope.activatedacounts = 0;
    $scope.notActivatedaccounts = 0;

    $scope.StoreSummaryModalClick = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetActiveInAciveCustomersByStoreList?StoreID=" + $scope.StoreId +
            "&UserId=" + $scope.LoggedInUserId)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.activatedParticipants = response.data.filter(x => x.Status == 'Active')[0].Count;
                    $scope.notActivatedParticipants = response.data.filter(x => x.Status == 'Not Active')[0].Count;
                }
                $("#StoreSummaryModel").modal('show');
            })
            .catch(function (response) {
            })
            .finally(function () {
            });

        $http.get(configurationService.basePath + "api/StoreApi/GetGetRemainingPointsByStoreList?StoreID=" + $scope.StoreId +
            "&UserId=" + $scope.LoggedInUserId)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.activatedacounts = response.data.filter(x => x.Status == 'Activated accounts')[0].Count;
                    $scope.notActivatedaccounts = response.data.filter(x => x.Status == 'Non activated accounts')[0].Count;
                }
                $("#StoreSummaryModel").modal('show');
            })
            .catch(function (response) {
            })
            .finally(function () {
            });
    }



    //Montly Points
    $scope.MonthlyPointsList = [];
    $scope.MonthlyPointModalClick = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetPointsLoadedByMonthByStoreList?StoreID=" + $scope.StoreId + "&Month=" +
            $scope.selectedmonth + "&Year=" + $scope.selectedyear + "&userId=" + $scope.LoggedInUserId
        )
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.MonthlyPointsList = response.data;
                }
                $("#MonthlyPointsModel").modal('show');
            })
            .catch(function (response) {
            })
            .finally(function () {
            });

    }
    $scope.exportToExcelMonthlyPoint = function () {
        $http({
            url: configurationService.basePath + "api/StoreApi/GetPointsLoadedByMonthByStoreListExport?StoreID=" + $scope.StoreId + "&Month=" +
                $scope.selectedmonth + "&Year=" + $scope.selectedyear + "&userId=" + $scope.LoggedInUserId,
            method: "POST",
            'dataSrc': 'aaData',
            "dataType": 'json',
            headers: {
                'Content-type': 'application/json'
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {
            exportToExcelCommon(data, status, headers, config);
        }).error(function (data, status, headers, config) {
        });


    }
});
