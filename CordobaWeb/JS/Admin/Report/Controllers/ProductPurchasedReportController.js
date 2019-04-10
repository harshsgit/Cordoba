app.controller('ProductPurchasedReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.store_id = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
    $scope.PageTitle = "Products Purchased List";


    $scope.GridParams = new Object();

    $scope.ProductFilter = new Object();
    $scope.ProductFilter.order_status_id = 0;
    $scope.ProductFilter.store_id = $rootScope.storeId;
    $scope.ProductFilter.DateStart = '';
    $scope.ProductFilter.DateEnd = '';

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


    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.StoreList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.store_id = 0;
                  DefaultOption.name = "All Stores";
                  $scope.StoreList.push(DefaultOption);
              }
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }

    function GetOrderStatus() {
        $http.get(configurationService.basePath + 'api/ProductPurchasedReportApi/GetOrderStatus?store_id=' + $scope.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId + '&language_id=1')
       .then(function (response) {
           if (response.data.length > 0) {
               $scope.OrderStatusList = response.data;
               var DefaultOption = new Object()
               DefaultOption.order_status_id = 0;
               DefaultOption.name = "All Statuses";
               $scope.OrderStatusList.push(DefaultOption);
           }
       })
   .catch(function (response) {
   })
   .finally(function () {

   });
    }


    $scope.GetProductPurchasedList = function () {
        if ($.fn.DataTable.isDataTable("#tblProductPurchased")) {
            $('#tblProductPurchased').DataTable().destroy();
        }
        var table = $('#tblProductPurchased').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "autoWidth": false,
            "searching": false,
            "dom": '<"table-responsive"><"top"lrt><"bottom"ip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[1, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/ProductPurchasedReportApi/GetProductPurchasedList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                //aoData = BindSearchCriteria(aoData);
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblProductPurchased').DataTable().page.info().page) + 1;
                $scope.GridParams = aoData;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?PageIndex=" + PageIndex + "&order_status_id=" + $scope.ProductFilter.order_status_id + "&store_id=" + $scope.ProductFilter.store_id + "&LoggedInUserId=" +$scope.LoggedInUserId  + "&DateStart=" + $scope.ProductFilter.DateStart + "&DateEnd=" + $scope.ProductFilter.DateEnd,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [
                { "mData": "name", "bSortable": true },
                { "mData": "model", "bSortable": true },
                { "mData": "categoryname", "bSortable": true },
                 { "mData": "storename", "bSortable": true },
                {
                    "mData": "quantity", "bSortable": true
                },
                 {
                     "mData": "purchaseddate", "bSortable": true
                  , "render": function (data, type, row) {
                      return $filter('date')(data, $rootScope.GlobalDateFormat);
                  }
                 },
                  {
                      "mData": "total", "bSortable": true
                        , "render": function (data, type, row) {
                            if (data != null)
                            {
                                return data +" "+ row.total_title;
                            }
                            
                        }
                  },
            ],
            "initComplete": function () {
                $compile(angular.element("#tblProductPurchased").contents())($scope);
            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }

    //export Employee List to Excel
    $scope.ExportProductPurchasedList = function () {
        var column = "";
        if ($scope.GridParams.length != undefined) {
            column = $filter('filter')($scope.GridParams, { name: "SortColumns" }, true);
        }

        $http({
            url: configurationService.basePath + 'api/ProductPurchasedReportApi/ExportToExcelProductPurchasedList?PageIndex=' + 1 + '&order_status_id=' + $scope.ProductFilter.order_status_id + '&store_id=' + $scope.ProductFilter.store_id + '&LoggedInUserId=' + $scope.LoggedInUserId +'&DateStart=' + $scope.ProductFilter.DateStart + '&DateEnd=' + $scope.ProductFilter.DateEnd,
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

    //#endregion


    function init() {
        GetOrderStatus();
        GetStoreList();

        $scope.GetProductPurchasedList();
    }

    init();

});