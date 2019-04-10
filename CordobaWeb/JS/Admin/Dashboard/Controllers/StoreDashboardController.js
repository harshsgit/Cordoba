app.controller('StoreDashboardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    if ($rootScope.IsStoreAdmin == 0) {
        $state.go('Home');
    }


    $scope.storeId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;


    $scope.CatalogueList = [];
    $scope.DashboardSummary = [];

    $scope.ChartFilterTypeEnum =
        [
            { id: 1, name: 'Today' },
            { id: 2, name: 'Week' },
            { id: 3, name: 'Month' },
            { id: 4, name: 'Year' }
        ];

    $scope.ChartOrFunctionTypeEnum = {
        All: 0
                                    , OrderSummary: 1
                                    , SalesAnalytics: 2
                                    , Top5SellingStores: 3
                                    , Top5Customers: 4
                                    , Top5PurchaseItem: 5
    };

    $scope.ChartFiltertype = 4;

    //#endregion  
    //InitChart();
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)
                     .withOption("deferRender", true);


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


            // Charts setup
            function (ec, limitless) {
                // Initialize charts

                var OrderSummary = ec.init(document.getElementById('StoreOrderSummary'), limitless);
                var SalesAnalytics = ec.init(document.getElementById('StoreSalesAnalytics'), limitless);
                //var Top5Sales_Chart = ec.init(document.getElementById('Top5Sales_Chart'), limitless);
                var Top5Customer_Chart = ec.init(document.getElementById('Top5Customer_Chart'), limitless);

                var Top5Product_Chart = ec.init(document.getElementById('Top5Product_Chart'), limitless);

                // Charts setup
                // ------------------------------                    
                OrderSummary_options = {

                    //// Add title
                    //title: {
                    //    text: 'Current year Order details',
                    //    subtext: 'Senior front end developer',
                    //    x: 'center'
                    //},

                    // Add tooltip
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b}: {c} ({d}%)"
                    },

                    // Add legend
                    legend: {
                        x: 'left',
                        y: 'top',
                        orient: 'vertical',
                        data: $scope.DashboardSummary.DashboardOrderSummaryMonthName
                    },

                    // Display toolbox
                    toolbox: {
                        show: true,
                        orient: 'vertical',
                        feature: {
                            //mark: {
                            //    show: true,
                            //    title: {
                            //        mark: 'Markline switch',
                            //        markUndo: 'Undo markline',
                            //        markClear: 'Clear markline'
                            //    }
                            //},
                            //dataView: {
                            //    show: true,
                            //    readOnly: false,
                            //    title: 'View data',
                            //    lang: ['View chart data', 'Close', 'Update']
                            //},
                            magicType: {
                                show: true,
                                title: {
                                    pie: 'Switch to pies',
                                    funnel: 'Switch to funnel',
                                },
                                type: ['pie', 'funnel']
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

                    // Add series
                    series: [
                        {
                            //name: 'Increase (brutto)',
                            type: 'pie',
                            radius: ['15%', '73%'],
                            center: ['50%', '59%'],
                            roseType: 'radius',

                            // Funnel
                            width: '40%',
                            height: '78%',
                            x: '30%',
                            y: '17.5%',
                            max: 400,

                            itemStyle: {
                                normal: {
                                    label: {
                                        show: false
                                    },
                                    labelLine: {
                                        show: false
                                    }
                                },
                                emphasis: {
                                    label: {
                                        show: true
                                    },
                                    labelLine: {
                                        show: true
                                    }
                                }
                            },
                            data: $scope.DashboardSummary.DashboardOrderSummary
                        }
                    ]
                };



                //
                // Basic bars options
                //

                SalesAnalytics_options = {

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
                        formatter: "{a} <br/>{b}: {c} points",
                        axisPointer: {
                            type: 'shadow'
                        }
                    },

                    //Add legend
                    legend: {
                        data: ['Total Orders', 'Customers With Orders']
                    },

                    // Display toolbox
                    toolbox: {
                        show: true,
                        orient: 'vertical',
                        feature: {
                            //mark: {
                            //    show: true,
                            //    title: {
                            //        mark: 'Markline switch',
                            //        markUndo: 'Undo markline',
                            //        markClear: 'Clear markline'
                            //    }
                            //},
                            //dataView: {
                            //    show: true,
                            //    readOnly: false,
                            //    title: 'View data',
                            //    lang: ['View chart data', 'Close', 'Update']
                            //},
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
                    calculable: true,

                    // Horizontal axis
                    xAxis: [{
                        type: 'value',
                        boundaryGap: [0, 0.01]
                    }],

                    // Vertical axis
                    yAxis: [{
                        type: 'category',
                        data:
                         $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue
                    }],

                    // Add series
                    series: [
                        {
                            name: 'Orders',
                            type: 'bar',
                            itemStyle: {
                                normal: {
                                    color: '#EF5350'
                                }
                            },
                            data: $scope.DashboardSummary.DashboardSalesAnalyticsOrderCount
                        },
                        {
                            name: 'Customers',
                            type: 'bar',
                            itemStyle: {
                                normal: {
                                    color: '#66BB6A'
                                }
                            },
                            data: $scope.DashboardSummary.DashboardSalesAnalyticsCustomerCount
                        }
                    ]
                };




                // Top 5 Customer   =  Top5Customer_Chart
                Top5Customer_Chart_option = {
                    //title: {
                    //    text: 'Store'
                    //    //subtext: 'dsfg'
                    //},
                    tooltip: {
                        trigger: 'axis'
                    },
                    //legend: {
                    //    data: ['sdfds']
                    //},
                    toolbox: {
                        show: true,
                        orient: 'vertical',
                        feature: {
                            //mark: { show: true },
                            //dataView: { show: true, readOnly: false },
                            //magicType: { show: true, type: ['line', 'bar'], title: 'Switch to line', },
                            magicType: {
                                show: true,
                                title: {
                                    bar: 'Switch to pies',
                                    line: 'Switch to line',
                                },
                                type: ['bar', 'line']
                            },
                            restore: { show: true, title: 'Restore' },
                            saveAsImage: { show: true, title: 'Save as image' }
                        }
                    },
                    calculable: false,
                    xAxis: [
                        {
                            type: 'category',
                            data: $scope.DashboardSummary.DashboardTopCustomerName
                            , rotated: true
                            , boundaryGap: ['30%', '30%']
                            , axisLabel: {
                                show: true,
                                interval: 0,    // {number}
                                rotate: 45,
                                margin: -10,
                                formatter: '{value}',
                                textStyle: {
                                    color: 'blue',
                                    fontFamily: 'sans-serif',
                                    fontSize: 10,
                                    fontStyle: 'italic',
                                    fontWeight: 'bold'
                                }

                            }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value'
                        }
                    ],
                    series: [
                        {
                            name: 'purchase',
                            type: 'bar',
                            data: $scope.DashboardSummary.DashboardTopCustomerValue,
                            itemStyle: {
                                normal: {
                                    color: function (param) {
                                        var colorList = ['#B5C334', '#FCCE10', '#E87C25', '#27727B', '#FE8463'];
                                        return colorList[param.dataIndex]
                                    }
                                }
                            }
                        }
                    ]
                };



                // END - Top 5 Customer   =  Top5Customer_Chart

                // Top 5 Customer   =  Top5Customer_Chart
                Top5Product_Chart_option = {
                    //title: {
                    //    text: 'Store'
                    //    //subtext: 'dsfg'
                    //},
                    tooltip: {
                        trigger: 'axis'
                    },
                    //legend: {
                    //    data: ['sdfds']
                    //},
                    toolbox: {
                        show: true,
                        orient: 'vertical',
                        feature: {
                            //mark: { show: true },
                            //dataView: { show: true, readOnly: false },
                            //magicType: { show: true, type: ['line', 'bar'], title: 'Switch to line', },
                            magicType: {
                                show: true,
                                title: {
                                    bar: 'Switch to pies',
                                    line: 'Switch to line',
                                },
                                type: ['bar', 'line']
                            },
                            restore: { show: true, title: 'Restore' },
                            saveAsImage: { show: true, title: 'Save as image' }
                        }
                    },
                    calculable: false,
                    xAxis: [
                        {
                            type: 'category',
                            data: $scope.DashboardSummary.DashboardTopPurchaseProductName
                             , axisLabel: {
                                 show: true,
                                 interval: 0,    // {number}
                                 rotate: 45,
                                 margin: -20,
                                 formatter: '{value}',
                                 textStyle: {
                                     color: 'blue',
                                     fontFamily: 'sans-serif',
                                     fontSize: 10,
                                     fontStyle: 'italic',
                                     fontWeight: 'bold'
                                 }

                             }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value'
                        }
                    ],
                    series: [
                        {
                            name: 'Product',
                            type: 'bar',
                            data: $scope.DashboardSummary.DashboardTopPurchaseProductValue,
                            itemStyle: {
                                normal: {
                                    color: function (param) {
                                        var colorList = ['#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B'];
                                        return colorList[param.dataIndex]
                                    }
                                }
                            }
                        }
                    ]
                };



                // END - Top 5 Customer   =  Top5Customer_Chart



                // Apply options
                // ------------------------------

                OrderSummary.setOption(OrderSummary_options);
                SalesAnalytics.setOption(SalesAnalytics_options);

                Top5Customer_Chart.setOption(Top5Customer_Chart_option);
                Top5Product_Chart.setOption(Top5Product_Chart_option);

                setTimeout(function () {

                    OrderSummary.resize();
                    SalesAnalytics.resize();
                    Top5Customer_Chart.resize();
                    Top5Product_Chart.resize();
                }, 100);


                // Resize charts
                // ------------------------------

                window.onresize = function () {
                    setTimeout(function () {

                        OrderSummary.resize();
                        SalesAnalytics.resize();
                        Top5Customer_Chart.resize();
                        Top5Product_Chart.resize();
                    }, 200);
                }
            }
        );
    }


    $scope.GetLatestOrderDetailsDashboard = function () {
        $http.get(configurationService.basePath + "api/DashboardApi/GetLatestOrderDetailsDashboard?storeId=" + $scope.storeId)
         .then(function (response) {

             if (response.data.length > 0) {
                 $scope.Orders = response.data;
             }
         })
         .catch(function (response) {
         })
         .finally(function () {
         });
    }

    $scope.GetLatestOrderDetailsDashboard();

    $scope.GetDashboardTopHeaderFields = function () {
        $http.get(configurationService.basePath + "api/DashboardApi/GetDashboardTopHeaderFields?storeId=" + $scope.storeId)
        .then(function (response) {
            if (response.data != null) {
                $scope.DashboardSummary.DashboardHeaderSummary = response.data;
            }
        })
         .catch(function (response) {
         })
         .finally(function () {
         });
    }

    $scope.GetDashboardTopHeaderFields();


    $scope.GetDashboardSummaryCharts = function (ChartOrFunctionTypeEnum) {
        $http.get(configurationService.basePath + "api/DashboardApi/GetDashboardSummaryCharts?storeId=" + $scope.storeId + "&ChartFiltertype=" + $scope.ChartFiltertype + "&ChartOrFunctionTypeEnum=" + ChartOrFunctionTypeEnum)
        .then(function (response) {
            if (response.data != null) {
                if ($scope.ChartOrFunctionTypeEnum.All == ChartOrFunctionTypeEnum) {
                    $scope.DashboardSummary.DashboardOrderSummary = [];
                    $scope.DashboardSummary.DashboardOrderSummaryMonthName = [];

                    $scope.DashboardSummary.DashboardSalesAnalyticsOrderCount = [];
                    $scope.DashboardSummary.DashboardSalesAnalyticsCustomerCount = [];
                    $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue = [];

                    $scope.DashboardSummary.DashboardTopPurchaseProductName = [];
                    $scope.DashboardSummary.DashboardTopPurchaseProductValue = [];

                    $scope.DashboardSummary.DashboardTopCustomerName = [];
                    $scope.DashboardSummary.DashboardTopCustomerValue = [];

                    for (var i = 0; i < response.data.dashboardTopCustomer.length; i++) {
                        $scope.DashboardSummary.DashboardTopCustomerValue.push(response.data.dashboardTopCustomer[i].value);
                        $scope.DashboardSummary.DashboardTopCustomerName.push(response.data.dashboardTopCustomer[i].customer);
                    }
                    for (var i = 0; i < response.data.dashboardTopPurchaseProduct.length; i++) {
                        $scope.DashboardSummary.DashboardTopPurchaseProductValue.push(response.data.dashboardTopPurchaseProduct[i].value);
                        $scope.DashboardSummary.DashboardTopPurchaseProductName.push(response.data.dashboardTopPurchaseProduct[i].product);
                    }
                    for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                        $scope.DashboardSummary.DashboardSalesAnalyticsOrderCount.push(response.data.dashboardSalesAnalytics[i].OrderCount);
                        $scope.DashboardSummary.DashboardSalesAnalyticsCustomerCount.push(response.data.dashboardSalesAnalytics[i].CustomerCount);

                    }

                    for (var i = 0; i < response.data.dashboardOrderSummary.length; i++) {
                        $scope.DashboardSummary.DashboardOrderSummary.push({ value: response.data.dashboardOrderSummary[i].orderCount, name: response.data.dashboardOrderSummary[i].Month })
                        $scope.DashboardSummary.DashboardOrderSummaryMonthName.push(response.data.dashboardOrderSummary[i].Month);
                    }

                    if ($scope.ChartFiltertype == 1) {
                        //$scope.DashboardSummary.DashboardSalesAnalyticsFilterValue
                        for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                            $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue.push(response.data.dashboardSalesAnalytics[i].HourOfDay)
                        }
                    }
                    else if ($scope.ChartFiltertype == 2) {
                        for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                            $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue.push(response.data.dashboardSalesAnalytics[i].WeekDayName)
                        }
                    }
                    else if ($scope.ChartFiltertype == 3) {

                        for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                            $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue.push(response.data.dashboardSalesAnalytics[i].DayNumberOfMonth)
                        }
                    }
                    else if ($scope.ChartFiltertype == 4) {
                        for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                            $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue.push(response.data.dashboardSalesAnalytics[i].MonthName)
                        }
                    }

                }
                else if ($scope.ChartOrFunctionTypeEnum.OrderSummary == ChartOrFunctionTypeEnum) {
                    $scope.DashboardSummary.DashboardOrderSummary = [];
                    $scope.DashboardSummary.DashboardOrderSummaryMonthName = [];
                    for (var i = 0; i < response.data.dashboardOrderSummary.length; i++) {
                        $scope.DashboardSummary.DashboardOrderSummary.push({ value: response.data.dashboardOrderSummary[i].orderCount, name: response.data.dashboardOrderSummary[i].Month })
                        $scope.DashboardSummary.DashboardOrderSummaryMonthName.push(response.data.dashboardOrderSummary[i].Month);
                    }

                }
                else if ($scope.ChartOrFunctionTypeEnum.SalesAnalytics == ChartOrFunctionTypeEnum) {
                    $scope.DashboardSummary.DashboardSalesAnalyticsOrderCount = [];
                    $scope.DashboardSummary.DashboardSalesAnalyticsCustomerCount = [];
                    $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue = [];
                    for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                        $scope.DashboardSummary.DashboardSalesAnalyticsOrderCount.push(response.data.dashboardSalesAnalytics[i].OrderCount);
                        $scope.DashboardSummary.DashboardSalesAnalyticsCustomerCount.push(response.data.dashboardSalesAnalytics[i].CustomerCount);

                    }

                    if ($scope.ChartFiltertype == 1) {
                        //$scope.DashboardSummary.DashboardSalesAnalyticsFilterValue
                        for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                            $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue.push(response.data.dashboardSalesAnalytics[i].HourOfDay)
                        }
                    }
                    else if ($scope.ChartFiltertype == 2) {
                        for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                            $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue.push(response.data.dashboardSalesAnalytics[i].WeekDayName)
                        }
                    }
                    else if ($scope.ChartFiltertype == 3) {

                        for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                            $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue.push(response.data.dashboardSalesAnalytics[i].DayNumberOfMonth)
                        }
                    }
                    else if ($scope.ChartFiltertype == 4) {
                        for (var i = 0; i < response.data.dashboardSalesAnalytics.length; i++) {
                            $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue.push(response.data.dashboardSalesAnalytics[i].MonthName)
                        }
                    }
                }
                else if ($scope.ChartOrFunctionTypeEnum.Top5PurchaseItem == ChartOrFunctionTypeEnum) {
                    $scope.DashboardSummary.DashboardTopPurchaseProductValue = [];
                    $scope.DashboardSummary.DashboardTopPurchaseProductName = [];

                    for (var i = 0; i < response.data.dashboardTopPurchaseProduct.length; i++) {
                        $scope.DashboardSummary.DashboardTopPurchaseProductValue.push(response.data.dashboardTopPurchaseProduct[i].value);
                        $scope.DashboardSummary.DashboardTopPurchaseProductName.push(response.data.dashboardTopPurchaseProduct[i].product);
                    }

                }
                else if ($scope.ChartOrFunctionTypeEnum.Top5Customers == ChartOrFunctionTypeEnum) {
                    $scope.DashboardSummary.DashboardTopCustomerName = [];
                    $scope.DashboardSummary.DashboardTopCustomerValue = [];

                    for (var i = 0; i < response.data.dashboardTopCustomer.length; i++) {
                        $scope.DashboardSummary.DashboardTopCustomerValue.push(response.data.dashboardTopCustomer[i].value);
                        $scope.DashboardSummary.DashboardTopCustomerName.push(response.data.dashboardTopCustomer[i].customer);
                    }
                }

            }

            LoadCharts(ChartOrFunctionTypeEnum);

        })
        .catch(function (response) {
        })
         .finally(function () {
         });
    }
    $scope.getActivityList = function () {
        $http.get(configurationService.basePath + "api/ActivityApi/GetActivityList?store_id=" + $scope.storeId)
         .then(function (response) {
             if (response.data.length > 0) {
                 $scope.activityList = response.data;
             }
         })
         .catch(function (response) {

         })
         .finally(function () {

         })

    }

    $scope.getActivityList();

    $scope.GetDashboardSummaryCharts($scope.ChartOrFunctionTypeEnum.All);

    $scope.GotoStorePDF = function () {
        var url = $state.href('StoreHTML');
        window.open(url, '_blank');
    }
    
    $scope.printDiv = function () {
        //var printContents = document.getElementById(divName).innerHTML;
        //var popupWin = window.open('', '_blank', 'width=300,height=300');
        //popupWin.document.open();
        //popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="http://localhost:1012/Content/admin/css/colors.css" />' +
            
        //    '</head><body onload="window.print()">' + printContents + '</body></html>');
        //popupWin.document.close();

    //    var css = '@page { size: landscape; }',
    //head = document.head || document.getElementsByTagName('head')[0],
    //style = document.createElement('style');

    //    style.type = 'text/css';
    //    style.media = 'print';

    //    if (style.styleSheet) {
    //        style.styleSheet.cssText = css;
    //    } else {
    //        style.appendChild(document.createTextNode(css));
    //    }

    //    head.appendChild(style);
        window.print();
    }

});