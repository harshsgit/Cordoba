app.controller('ShippingReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    //#endregion  
    //$scope.dtOptions = DTOptionsBuilder.newOptions()
    //                 .withOption('bDestroy', true)

    $scope.PageTitle = "Sales Reports - Shipping";

    $scope.GroupBy = [
       { id: 0, name: 'Years' },
       { id: 1, name: 'Months' },
       { id: 2, name: 'Weeks' },
       { id: 3, name: 'Days' }
    ];


    $scope.OrderStatus = [
       { id: 0, name: 'All Statuses' },
       { id: 1, name: 'Processing' },
       { id: 2, name: 'Shipped' },
       { id: 3, name: 'PartiallyShipped' },
       { id: 4, name: 'Returned' },
       { id: 5, name: 'Cancelled' }
    ];


    $scope.filter = {
        DateStart: '',
        DateEnd: '',
        ShippingTitle: '',
        NoOrders: '',
        Total: ''
    };

    //$scope.GetManufacturersList = function () {
    //    $http.get(configurationService.basePath + "api/ManufacturersApi/GetManufacturersList?ManufacturersID=0")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.ManufacturersList = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
    //}
    //$scope.GetManufacturersList();
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

    $scope.GetOrderList = function () {

        if ($.fn.DataTable.isDataTable("#tblShippingReport")) {
            $('#tblShippingReport').DataTable().destroy();
        }

        //var table;
        var table = $('#tblShippingReport').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "searching": true,
            "dom": '<"table-responsive"rt><"bottom"lip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[0, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/OrderApi/GetOrderList?StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId,
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblShippingReport').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aoData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?PageIndex=" + PageIndex,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [

                {
                    "mData": "date_start",
                    "bSortable": true,
                    "type": "date",
                    "format": 'DD-MM-YYYY',
                },
                {
                    "mData": "date_end",
                    "bSortable": true
                },
                //{
                //    "mData": "order_id",
                //    "bSortable": true,
                //    "sClass": " text-right"
                //    //"render": function (data, type, row) {
                //    //    return '<a data-Id=' + row.JobId + ' class="cursor-pointer" ng-click="EditJobCode($event)">' + data + '</a>'
                //    //}
                //},
                {
                    "mData": "shippingTitle",
                    "bSortable": true
                },
                {
                    "mData": "noOfOrders",
                    "bSortable": true
                },
                {
                    "mData": "total",
                    "bSortable": true,
                    "sClass": "text-right"
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

    $scope.GetOrderList();



});