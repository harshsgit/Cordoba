app.controller('TransactionReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();

    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.store_id = $rootScope.storeId;

    $scope.TransactionReportObj = new Object();
    $scope.TransactionReportObj.DateStart = null;
    $scope.TransactionReportObj.DateEnd = null;
    $scope.TransactionReportObj.store_id = $rootScope.storeId;
   

    $scope.PageTitle = "Reports - Transaction Report";

    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    $scope.GetStoreList = function () {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=" + $scope.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId)
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

    $scope.GetStoreList();


    function BindSearchCriteria(aoData) {

        aoData.push({ 'name': 'searchKey', 'value': '' });
        aoData.push({ 'name': 'Operation', 'value': '' });
        aoData.push({ 'name': 'searchValue', 'value': '' });

        return aoData;
    }


    function BindSorting(aoData, oSettings) {

        angular.forEach(oSettings.aaSorting, function (row, i) {
            var sortObj = new Object();
            sortObj.Column = oSettings.aoColumns[row[0]].mData;
            sortObj.Desc = row[1] == 'desc';
            aoData.push({ 'name': 'SortColumns', 'value': JSON.stringify(sortObj) });
            return;
        });
        return aoData;
    }

    $scope.GetTransactionReportList = function () {   
        if ($.fn.DataTable.isDataTable("#tblTransactionReport")) {
            $('#tblTransactionReport').DataTable().destroy();
        }

        //var table;
        var table = $('#tblTransactionReport').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "autoWidth": false,
            "searching": true,
            "dom": '<"table-responsive"><"top"lrt><"bottom"ip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[0, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/ReportApi/GetTransactionReportList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblTransactionReport').DataTable().page.info().page) + 1;
                $scope.GridParams = aoData;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + '?PageIndex=' + PageIndex + '&DateStart=' + $scope.TransactionReportObj.DateStart + '&DateEnd=' + $scope.TransactionReportObj.DateEnd + '&StoreId=' + $scope.TransactionReportObj.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [

                {
                    "mData": "timestamp", "bSortable": true,
                    "render": function (data, type, row) {
                        if (data != null) {
                            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                        }
                        else {
                            return "";
                        }
                    }
                },
                   //{
                   //    "mData": "DateEnd", "bSortable": true,
                   //    "render": function (data, type, row) {
                   //        if (data != null) {
                   //            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                   //        }
                   //        else {
                   //            return "";
                   //        }
                   //    }
                   //},
                {
                    "mData": "firstname",
                    "bSortable": true
                },
                {
                    "mData": "lastname",
                    "bSortable": true
                },
                {
                    "mData": "email",
                    "bSortable": true
                },
                {
                    "mData": "store",
                    "bSortable": true
                },
                {
                    "mData": "adjustment",
                    "bSortable": true
                },
                {
                    "mData": "type_of_points",
                    "bSortable": true
                },
                {
                    "mData": "comments",
                    "bSortable": true
                },


            ],
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }

    //export Transaction Report List to Excel
    $scope.TransactionReportExportToExcel = function () {
        var column = "";
        if ($scope.GridParams.length != undefined) {
            column = $filter('filter')($scope.GridParams, { name: "SortColumns" }, true);
            //  alert(JSON.stringify(column));
        }

        $http({
            url: configurationService.basePath + 'api/Reportapi/TransactionReportExportToExcel?PageIndex=' + 1 + "&DateStart=" + $scope.TransactionReportObj.DateStart + "&DateEnd=" + $scope.TransactionReportObj.DateEnd + "&StoreId=" + ($scope.TransactionReportObj.store_id == null ? 0 : $scope.TransactionReportObj.store_id),
            //"url": sSource + '?PageIndex=' + PageIndex + '&DateStart=' + $scope.TransactionReportObj.DateStart + '&DateEnd=' + $scope.TransactionReportObj.DateEnd + '&StoreId=' + $scope.TransactionReportObj.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId,
            method: "POST",
            'dataSrc': 'aaData',
            "dataType": 'json',
            data: column != "" ? column[0].value : "",
            headers: {
                'Content-type': 'application/json'
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {

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
                //window.open(objectUrl);
            }
        }).error(function (data, status, headers, config) {
        });

    }

    function init() {
        $scope.GetTransactionReportList();
    }

    init();
});