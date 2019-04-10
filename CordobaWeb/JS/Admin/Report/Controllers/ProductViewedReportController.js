app.controller('ProductViewedReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    createDatePicker();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)

    $scope.PageTitle = "Products Viewed Report";
    $scope.selectedStore_id = $scope.StoreId;


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
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
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


    $scope.GetProductViewedList = function () {
        if ($.fn.DataTable.isDataTable("#tblProductViewed")) {
            $('#tblProductViewed').DataTable().destroy();
        }
        var table = $('#tblProductViewed').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "searching": false,
            "dom": '<"table-responsive"rt><"bottom"lip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[2, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/ProductPurchasedReportApi/GetProductViewedList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                //aoData = BindSearchCriteria(aoData);
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblProductViewed').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?store_id=" + $scope.selectedStore_id + "&LoggedInUserId=" + $scope.LoggedInUserId + "&PageIndex=" + PageIndex,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [
                { "mData": "name", "bSortable": true },
                { "mData": "model", "bSortable": true },
                {
                    "mData": "viewed", "bSortable": true
                },
                  {
                      "mData": "percent", "bSortable": true
                        , "render": function (data, type, row) {
                            if (data != null) {
                                return data + '%';
                            }
                        }
                  },
            ],
            "initComplete": function () {
                $compile(angular.element("#tblProductViewed").contents())($scope);
            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }

    function init() {
        $scope.GetProductViewedList();
    }



    init();


});