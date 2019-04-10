app.controller('ShowOrdersController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.PageTitle = "Order List";

    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    //$rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    $scope.filter = {
        orderID: '',
        selectedOrderStatus: 0,
        dateAdded: '',
        Customer: '',
        //Total: '',
        dateModified: '',
        storeId: $scope.StoreId
    };

    if ($stateParams.OrderStatusId != undefined && $stateParams.OrderStatusId != null) {
        $scope.filter.selectedOrderStatus = parseInt($stateParams.OrderStatusId);
    }

    //$scope.GetOrderList = function () {
    //    $http.get(configurationService.basePath + "api/OrderApi/GetOrderList")
    //         .then(function (response) {   
    //             if (response.data.length > 0) {
    //                 $scope.orderList = response.data;
    //             }
    //         })
    //     .catch(function (response) {

    //     })
    //     .finally(function () {

    //     });
    //}
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

    var userid = $scope.LoggedInUserId;
    $scope.ContainValueOrNot = function () {
        //Order Id
        if ($scope.filter.orderID == "" || $scope.filter.orderID == null) {
            localStorageService.set(userid + "_ShowOrders_orderID", "");
        }
        //date Added
        if ($scope.filter.dateAdded == "" || $scope.filter.dateAdded == null) {
            localStorageService.set(userid + "_ShowOrders_dateAdded", "");
        }
        //Customer
        if ($scope.filter.Customer == "" || $scope.filter.Customer == null) {
            localStorageService.set(userid + "_ShowOrders_Customer", "");
        }
        //date Modified
        if ($scope.filter.dateModified == "" || $scope.filter.dateModified == null) {
            localStorageService.set(userid + "_ShowOrders_dateModified", "");
        }
        MaintainLocalStorage();
    }

    $scope.checkIfEnterKeyWasPressed = function ($event) {
        var keyCode = $event.which || $event.keyCode;
        if (keyCode === 13) {
            $scope.ContainValueOrNot();
            $scope.GetOrderList();
        }
    };

    $scope.ContainStoreOrNot = function () {
        //Store
        if ($scope.filter.storeId == "" || $scope.filter.storeId == null) {
            localStorageService.set(userid + "_ShowOrders_Store", "");
        }
        MaintainLocalStorage();
    }

    $scope.ContainOrderStatusOrNot = function () {
        //Order Status
        if ($scope.filter.selectedOrderStatus == "" || $scope.filter.selectedOrderStatus == null) {
            localStorageService.set(userid + "_ShowOrders_selectedOrderStatus", "");
        }
        MaintainLocalStorage();
    }

    function MaintainLocalStorage() {
        //Store
        if ((localStorageService.get(userid + "_ShowOrders_Store") == "" || localStorageService.get(userid + "_ShowOrders_Store") == null)) {
            localStorageService.set(userid + "_ShowOrders_Store", $scope.filter.storeId);
        }
        else if ($scope.filter.storeId != "" ? (localStorageService.get(userid + "_ShowOrders_Store") != $scope.filter.storeId) : false) {
            localStorageService.set(userid + "_ShowOrders_Store", $scope.filter.storeId);
        }
        else if ((localStorageService.get(userid + "_ShowOrders_Store") != null || localStorageService.get(userid + "_ShowOrders_Store") != "")) {
            $scope.filter.storeId = localStorageService.get(userid + "_ShowOrders_Store");
        }

        //Order Id
        if ((localStorageService.get(userid + "_ShowOrders_orderID") == "" || localStorageService.get(userid + "_ShowOrders_orderID") == null)) {
            localStorageService.set(userid + "_ShowOrders_orderID", $scope.filter.orderID);
        }
        else if ($scope.filter.orderID != "" ? (localStorageService.get(userid + "_ShowOrders_orderID") != $scope.filter.orderID) : false) {
            localStorageService.set(userid + "_ShowOrders_orderID", $scope.filter.orderID);
        }
        else if ((localStorageService.get(userid + "_ShowOrders_orderID") != null || localStorageService.get(userid + "_ShowOrders_orderID") != "")) {
            $scope.filter.orderID = localStorageService.get(userid + "_ShowOrders_orderID");
        }

        //Order Status
        if ((localStorageService.get(userid + "_ShowOrders_selectedOrderStatus") == "" || localStorageService.get(userid + "_ShowOrders_selectedOrderStatus") == null)) {
            localStorageService.set(userid + "_ShowOrders_selectedOrderStatus", $scope.filter.selectedOrderStatus);
        }
        else if ($scope.filter.selectedOrderStatus != "" ? (localStorageService.get(userid + "_ShowOrders_selectedOrderStatus") != $scope.filter.selectedOrderStatus) : false) {
            localStorageService.set(userid + "_ShowOrders_selectedOrderStatus", $scope.filter.selectedOrderStatus);
        }
        else if ((localStorageService.get(userid + "_ShowOrders_selectedOrderStatus") != null || localStorageService.get(userid + "_ShowOrders_selectedOrderStatus") != "")) {
            $scope.filter.selectedOrderStatus = localStorageService.get(userid + "_ShowOrders_selectedOrderStatus");
        }

        //date Added
        if ((localStorageService.get(userid + "_ShowOrders_dateAdded") == "" || localStorageService.get(userid + "_ShowOrders_dateAdded") == null)) {
            localStorageService.set(userid + "_ShowOrders_dateAdded", $scope.filter.dateAdded);
        }
        else if ($scope.filter.dateAdded != "" ? (localStorageService.get(userid + "_ShowOrders_dateAdded") != $scope.filter.dateAdded) : false) {
            localStorageService.set(userid + "_ShowOrders_dateAdded", $scope.filter.dateAdded);
        }
        else if ((localStorageService.get(userid + "_ShowOrders_dateAdded") != null || localStorageService.get(userid + "_ShowOrders_dateAdded") != "")) {
            $scope.filter.dateAdded = localStorageService.get(userid + "_ShowOrders_dateAdded");
        }

        //Customer
        if ((localStorageService.get(userid + "_ShowOrders_Customer") == "" || localStorageService.get(userid + "_ShowOrders_Customer") == null)) {
            localStorageService.set(userid + "_ShowOrders_Customer", $scope.filter.Customer);
        }
        else if ($scope.filter.Customer != "" ? (localStorageService.get(userid + "_ShowOrders_Customer") != $scope.filter.Customer) : false) {
            localStorageService.set(userid + "_ShowOrders_Customer", $scope.filter.Customer);
        }
        else if ((localStorageService.get(userid + "_ShowOrders_Customer") != null || localStorageService.get(userid + "_ShowOrders_Customer") != "")) {
            $scope.filter.Customer = localStorageService.get(userid + "_ShowOrders_Customer");
        }

        //date Modified
        if ((localStorageService.get(userid + "_ShowOrders_dateModified") == "" || localStorageService.get(userid + "_ShowOrders_dateModified") == null)) {
            localStorageService.set(userid + "_ShowOrders_dateModified", $scope.filter.dateModified);
        }
        else if ($scope.filter.dateModified != "" ? (localStorageService.get(userid + "_ShowOrders_dateModified") != $scope.filter.dateModified) : false) {
            localStorageService.set(userid + "_ShowOrders_dateModified", $scope.filter.dateModified);
        }
        else if ((localStorageService.get(userid + "_ShowOrders_dateModified") != null || localStorageService.get(userid + "_ShowOrders_dateModified") != "")) {
            $scope.filter.dateModified = localStorageService.get(userid + "_ShowOrders_dateModified");
        }
    }

    $scope.GetOrderList = function () {
        
        MaintainLocalStorage();

        if ($.fn.DataTable.isDataTable("#tblOrders")) {
            $('#tblOrders').DataTable().destroy();
            //$('#tblOrders').html('<table class="table grid table-condensed table-hover" id="tblOrders" width="100%"></table>');
        }

        //var table;
        var table = $('#tblOrders').DataTable({
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
            "sAjaxSource": configurationService.basePath + "api/OrderApi/GetOrderList?StoreId=" + ($scope.filter.storeId==null?0:$scope.filter.storeId) + "&LoggedInUserId=" + $scope.LoggedInUserId,
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {           
                //aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblOrders').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "&PageIndex=" + PageIndex + "&orderId=" + $scope.filter.orderID + "&order_status_id=" + $scope.filter.selectedOrderStatus + "&CustomerName=" + $scope.filter.Customer + "&DateAdded=" + $scope.filter.dateAdded + "&DateModified=" + $scope.filter.dateModified,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [
                {
                    "mData": "order_id", "bSortable": true, "sClass": " text-center"
                    //"render": function (data, type, row) {
                    //    return '<a data-Id=' + row.JobId + ' class="cursor-pointer" ng-click="EditJobCode($event)">' + data + '</a>'
                    //}
                },
                { "mData": "store_name", "bSortable": true },                
                { "mData": "customer", "bSortable": true },
                 { "mData": "OrderStatusName", "bSortable": true },
                 { "mData": "total", "bSortable": true, "sClass": "text-right" },
                   {
                       "mData": "date_added", "bSortable": true,
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
                     //    "mData": "date_modified", "bSortable": true,
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
                    "mData": null, "bSortable": false,
                    "sClass": "action text-center",
                    "render": function (data, type, row) {
                        return '<a ui-sref="Orders({OrderId:' + row.order_id + '})"><i class="glyphicon glyphicon-eye-close cursor-pointer" title="view"></i></a> &nbsp;  <a ui-sref="ManageOrder({orderId:' + row.order_id + '})"><i class="glyphicon glyphicon-edit cursor-pointer" title="edit"></i></a>  &nbsp;  <i class="glyphicon glyphicon-trash  cursor-pointer" title="delete"></i>'
                    }
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

    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {           
                  $scope.StoreList = response.data;
                  //$scope.CustomerFilter.storeId = $scope.StoreId;                 
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    function GetOrderStatus() {
        $http.get(configurationService.basePath + 'api/ProductPurchasedReportApi/GetOrderStatus?store_id=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId + '&language_id=1')
       .then(function (response) {
           if (response.data.length > 0) {
               $scope.OrderStatusList = response.data;
               var DefaultOption = new Object();
               DefaultOption.order_status_id = 0;
               DefaultOption.name = "All Status";
               $scope.OrderStatusList.push(DefaultOption);
           }
       })
   .catch(function (response) {
   })
   .finally(function () {

   });
    }

    GetOrderStatus();
    GetStoreList();


    $scope.GetOrderList();
});