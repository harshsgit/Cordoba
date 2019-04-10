app.controller('OrderReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.OrderReportObj = new Object();
    $scope.OrderReportObj.DateStart = null;
    $scope.OrderReportObj.DateEnd = null;
    $scope.OrderReportObj.GroupById = null;
    $scope.OrderReportObj.StatusId = null;
    $scope.OrderReportObj.StoreId = $scope.StoreId;
    //#endregion  
    //$scope.dtOptions = DTOptionsBuilder.newOptions()
    //                 .withOption('bDestroy', true)

    $scope.PageTitle = "Reports - Orders Summary";


    $scope.GroupBy = [
       { id: 1, name: 'Years' },
       { id: 2, name: 'Months' },
       { id: 3, name: 'Weeks' },
       { id: 4, name: 'Days' }
    ];

    $scope.GetOrderStatus = function () {
        $http.get(configurationService.basePath + 'api/ProductPurchasedReportApi/GetOrderStatus?store_id=' + ($scope.OrderReportObj.StoreId==null?0:$scope.OrderReportObj.StoreId) + '&LoggedInUserId=' + $scope.LoggedInUserId + '&Language_Id=1')
       .then(function (response) {           
           if (response.data.length > 0) {
               $scope.OrderStatusList = response.data;             
           }
       })
   .catch(function (response) {
   })
   .finally(function () {

   });
    }

    $scope.filter = {
        DateStart: '',
        DateEnd: '',
        NoReturns: '',
        NoProducts: '',
        Total: '',
        Tax: ''
    };


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

    $scope.GetOrderReportList = function () {        
        if ($.fn.DataTable.isDataTable("#tblOrderReport")) {
            $('#tblOrderReport').DataTable().destroy();
        }

        //var table;
        var table = $('#tblOrderReport').DataTable({
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
            "sAjaxSource": configurationService.basePath + 'api/ReportApi/GetOrderReportList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                aoData = BindSearchCriteria(aoData); 
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblOrderReport').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + '?PageIndex=' + PageIndex + '&DateStart=' + $scope.OrderReportObj.DateStart + '&DateEnd=' + $scope.OrderReportObj.DateEnd + '&GroupById=' + ($scope.OrderReportObj.GroupById == null ? 0 : $scope.OrderReportObj.GroupById) + '&StatusId=' + ($scope.OrderReportObj.StatusId == null ? 0 : $scope.OrderReportObj.StatusId) + '&StoreId=' + ($scope.OrderReportObj.StoreId == null ? 0 : $scope.OrderReportObj.StoreId) + '&LoggedInUserId=' + $scope.LoggedInUserId,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [

                {
                    "mData": "DateStart", "bSortable": true,
                    "render": function (data, type, row) {
                        if (data != null) {
                            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                        }
                        else {
                            return "";
                        }
                    }
                },
                   {
                       "mData": "DateEnd", "bSortable": true,
                       "render": function (data, type, row) {
                           if (data != null) {
                               return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                           }
                           else {
                               return "";
                           }
                       }
                   },
                {
                    "mData": "No_Of_Orders",
                    "bSortable": true
                },
                {
                    "mData": "No_Of_Products",
                    "bSortable": true
                },
                {
                    "mData": "Total",
                    "bSortable": true
                },
                {
                    "mData": "Tax",
                    "bSortable": true
                },


                //{
                //    "mData": null, "bSortable": false,
                //    "sClass": "action text-right",
                //    "render": function (data, type, row) {
                //        return '<a ui-sref="Orders({OrderId:' + row.order_id + '})"><i class="glyphicon glyphicon-eye-close cursor-pointer" title="view"></i></a> &nbsp;  <a ui-sref="ManageOrder({orderId:' + row.order_id + '})"><i class="glyphicon glyphicon-edit cursor-pointer" title="edit"></i></a>  &nbsp;  <i class="glyphicon glyphicon-erase cursor-pointer" title="delete"></i>'
                //    }
                //},
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

    GetStoreList();

    $scope.GetOrderStatus();
    $scope.GetOrderReportList();
});