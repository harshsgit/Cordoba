app.controller('StoreReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {

    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();

    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.store_id = $rootScope.storeId;

    $scope.StoreReportObj = new Object();
    $scope.StoreReportObj.DateStart = null;
    $scope.StoreReportObj.DateEnd = null;
    $scope.StoreReportObj.store_id = $scope.store_id;
    $scope.PageTitle = "Reports - Store Report";
    $scope.CustomerByStoreForChart = {
        "Month": [],
        "Customer":[]
    };

    $scope.ChartFilterTypeEnum =
        [
            { id: 1, name: 'Month' },
            { id: 2, name: 'Year' }
        ];

    $scope.ChartOrFunctionTypeEnum = {
              All: 0
            , ParticipantSummary: 1
            , PointsActivatedSummery: 2
            , PointsRedeemedSummery: 3
    };

    $scope.ChartFiltertype = 2;


    $scope.CurrentYearOrderData = [
                                    { value: 300, name: 'January' },
                                    { value: 200, name: 'February' },
                                    { value: 350, name: 'March' },
                                    { value: 250, name: 'April' },
                                    { value: 210, name: 'May' },
                                    { value: 350, name: 'June' },
                                    { value: 300, name: 'July' },
                                    { value: 430, name: 'August' },
                                    { value: 400, name: 'September' },
                                    { value: 450, name: 'October' },
                                    { value: 430, name: 'November' },
                                    { value: 200, name: 'December' }
    ];



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

    $scope.GetCustomerByStoreForChart = function () {
        $http.get(configurationService.basePath + "api/ReportApi/GetCustomerByStoreForChart?store_id=" + $scope.store_id + '&ChartFilterType=' + $scope.ChartFiltertype)
          .then(function (response) {
              if (response.data.length > 0) {
                  if ($scope.ChartFiltertype == 2)
                  {
                      for (var i = 0; i < response.data.length; i++) {
                          $scope.CustomerByStoreForChart.Month.push(response.data[i]["MonthName"]);
                          $scope.CustomerByStoreForChart.Customer.push(response.data[i]["CustomerCount"]); 
                      }
                      console.log($scope.CustomerByStoreForChart.Customer);
                  }
                  LoadCharts(2);
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

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

    function LoadCharts(ChartOrFunctionTypeEnum) {

        // Set paths
        // ------------------------------

        require.config({
            paths: {
                echarts: 'Scripts/admin/js/plugins/visualization/echarts'
            }
        });

        // Configuration
        // ------------------------------

        require(
            [
                'echarts',
                'echarts/theme/limitless',
                'echarts/chart/pie',
                 'echarts/chart/bar',
                'echarts/chart/funnel',
                'echarts/chart/line'
            ],


    function (ec, limitless) {

        // Initialize charts

        var customerChart = ec.init(document.getElementById('StoreByYear'), limitless);

        // Charts setup
        // ------------------------------                   

        customerChart_options = {

            // Setup grid
            grid: {
                x: 75,
                x2: 35,
                y: 35,
                y2: 25
            },

            // Add tooltip
            tooltip: {
                trigger: 'axis',
                formatter: "{a} <br/>{b}: {c} points"
                , axisPointer: {
                    type: 'shadow'
                }
            },



            //Add legend
            legend: {
                data: ['Participants']
            },
            // Display toolbox
            toolbox: {
                show: true,
                orient: 'horizontal',
                feature: {
                    magicType: {
                        show: true,
                        title: {
                            bar: 'Switch to bar',
                            line: 'Switch to line',
                        },
                        type: ['line', 'bar']
                    },
                    restore: {
                        show: true,
                        title: 'Restore'
                    },
                    saveAsImage: {
                        show: true,
                        title: 'Save as image',
                        lang: ['Save']
                    }
                }
            },
            // Enable drag recalculate
            calculable: false,

            // Horizontal axis
            xAxis: [{
                type: 'category',
                data:
                $scope.CustomerByStoreForChart.Month
            }],

            // Vertical axis
            //yAxis: [{
            //    type: 'value',
            //    boundaryGap: [0, 0.01]
            //}],

            // Add series
            series: [
                {
                    name: 'Orders',
                    type: 'bar',
                    itemStyle: {
                        normal: {
                            color: '#e59967'
                        }
                    },
                    data: $scope.CustomerByStoreForChart.Customer
                },
                //{
                //    name: 'Customers',
                //    type: 'bar',
                //    itemStyle: {
                //        normal: {
                //            color: '#75f9ae'
                //        }
                //    },
                //    data: $scope.DashboardSummary.DashboardSalesAnalyticsCustomerCount
                //}
            ]
        };

        // END - Top 5 Customer   =  Top5Customer_Chart

        // Apply options
        // ------------------------------

        customerChart.setOption(customerChart_options);
                
        // Resize charts
        // ------------------------------

        window.onresize = function () {
            setTimeout(function () {

                customerChart.resize();
              
            }, 200);
        }
    }
        );
}

    $scope.GetStoreReport = function () {
        if ($.fn.DataTable.isDataTable("#tblStoreReport")) {
            $('#tblStoreReport').DataTable().destroy();
        }

        //var table;
        var table = $('#tblStoreReport').DataTable({
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
            "sAjaxSource": configurationService.basePath + 'api/ReportApi/GetStoreReportList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                aoData = BindSearchCriteria(aoData);

                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblStoreReport').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + '?PageIndex=' + PageIndex + '&DateStart=' + $scope.StoreReportObj.DateStart + '&DateEnd=' + $scope.StoreReportObj.DateEnd + '&StoreId=' + $scope.StoreReportObj.store_id,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        //exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [
                {
                    "mData": "firstname",
                    "bSortable": true
                },
                {
                    "mData": "email",
                    "bSortable": true
                },
                {
                    "mData": "points",
                    "bSortable": true
                },
                {
                    "mData": "status",
                    "bSortable": true
                },
                {
                    "mData": "activated",
                    "bSortable": true
                },
                {
                    "mData": "Date_Added",
                    "bSortable": true
                },


            ],
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
                //console.log($scope.StoreReportForChart);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        })
    }



    $scope.GetStoreReport();
    $scope.GetCustomerByStoreForChart();

});