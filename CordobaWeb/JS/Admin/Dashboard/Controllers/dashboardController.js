app.controller('DashboardController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    if ($rootScope.IsStoreAdmin == 1) {
        $state.go('StoreDashboard');
    }
    else {
        $state.go('Home');
    }
    $scope.store_id = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withOption('bDestroy', true)
        .withOption("deferRender", true);
    $scope.CatalogueList = [];


    //#endregion   
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

                var OrderSummary = document.getElementById('OrderSummary') ? ec.init(document.getElementById('OrderSummary'), limitless) : null;
                var SalesAnalytics = document.getElementById('SalesAnalytics') ? ec.init(document.getElementById('SalesAnalytics'), limitless) : null;
                var Top5Sales_Chart = document.getElementById('Top5Sales_Chart') ? ec.init(document.getElementById('Top5Sales_Chart'), limitless) : null;
                var Top5Customer_Chart = document.getElementById('Top5Customer_Chart') ? ec.init(document.getElementById('Top5Customer_Chart'), limitless) : null;

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
                            emphasis: {
                                label: {
                                    show: true,
                                    formatter: '{b}' + '\n\n' + '{c} ({d}%)',
                                    position: 'center',
                                    textStyle: {
                                        fontSize: '17',
                                        fontWeight: '500'
                                    }
                                }
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
                        formatter: "{a} <br/>{b}: {c} points"
                        , axisPointer: {
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
                    calculable: false,

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
                                    color: '#e59967'
                                }
                            },
                            data: $scope.DashboardSummary.DashboardSalesAnalyticsOrderCount
                        }
                        ,
                        {
                            name: 'Customers',
                            type: 'bar',
                            itemStyle: {
                                normal: {
                                    color: '#75f9ae'
                                }
                            },
                            data: $scope.DashboardSummary.DashboardSalesAnalyticsCustomerCount
                        }
                    ]
                };

                // Top 10 Sales - Bar Chart
                Top5Sales_Chart_option = {
                    //title: {
                    //    text: 'Store'
                    //    //subtext: 'dsfg'
                    //},
                    tooltip: {
                        trigger: 'axis'
                        , formatter: "{a} <br/>{b}: {c}£"
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
                            saveAsImage: { show: true, title: 'Save as image', lang: ['Save'] }
                        }
                    },
                    calculable: false,
                    xAxis: [
                        {
                            type: 'category',
                            boundaryGap: ['25%', '24%']
                            , data: $scope.DashboardSummary.DashboardTopSellStoreName
                            , rotated: true
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
                            name: 'Sales',
                            type: 'bar',
                            data: $scope.DashboardSummary.DashboardTopSellStoreValue,
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
                            saveAsImage: { show: true, title: 'Save as image', lang: ['Save'] }
                        }
                    },
                    calculable: false,
                    xAxis: [
                        {
                            type: 'category',
                            data: $scope.DashboardSummary.DashboardTopCustomerName
                            , rotated: true
                            , boundaryGap: ['25%', '24%']
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
                if (OrderSummary) {
                    OrderSummary.setOption(OrderSummary_options);
                }
                if (SalesAnalytics) {
                    SalesAnalytics.setOption(SalesAnalytics_options);
                }
                if (Top5Sales_Chart) {
                    Top5Sales_Chart.setOption(Top5Sales_Chart_option);
                }
                if (Top5Customer_Chart) {
                    Top5Customer_Chart.setOption(Top5Customer_Chart_option);
                }


                // Resize charts
                // ------------------------------

                window.onresize = function () {
                    setTimeout(function () {
                        if (OrderSummary)
                            OrderSummary.resize();
                        if (SalesAnalytics)
                            SalesAnalytics.resize();
                        if (Top5Sales_Chart)
                            Top5Sales_Chart.resize();
                        if (Top5Customer_Chart)
                            Top5Customer_Chart.resize();

                    }, 200);
                }
            }
        );
    }

    $scope.GetLatestOrderDetailsDashboard = function () {
        $http.get(configurationService.basePath + "api/DashboardApi/GetLatestOrderDetailsDashboard?storeId=" + $scope.store_id)
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

    $scope.GetDashboardTopHeaderFields = function () {
        $http.get(configurationService.basePath + "api/DashboardApi/GetDashboardTopHeaderFields?storeId=" + $scope.store_id + "&UserId=" + $scope.LoggedInUserId)
            .then(function (response) {

                if (response.data != null) {
                    $scope.DashboardSummary.DashboardHeaderSummary = response.data;
                    //$scope.DashboardHeaderSummary = response.data;
                }
            })
            .catch(function (response) {
            })
            .finally(function () {
            });
    }

    $scope.GetDashboardSummaryCharts = function (ChartOrFunctionTypeEnum) {
        $http.get(configurationService.basePath + "api/DashboardApi/GetDashboardSummaryCharts?storeId=" + $scope.store_id + "&ChartFiltertype=" + $scope.ChartFiltertype + "&ChartOrFunctionTypeEnum=" + ChartOrFunctionTypeEnum + "&UserId=" + $scope.LoggedInUserId)
            .then(function (response) {
                if (response.data != null) {

                    if ($scope.ChartOrFunctionTypeEnum.All == ChartOrFunctionTypeEnum) {
                        $scope.DashboardSummary.DashboardOrderSummary = [];
                        $scope.DashboardSummary.DashboardOrderSummaryMonthName = [];
                        debugger;
                        $scope.DashboardSummary.DashboardSalesAnalyticsOrderCount = [];
                        $scope.DashboardSummary.DashboardSalesAnalyticsCustomerCount = [];
                        $scope.DashboardSummary.DashboardSalesAnalyticsFilterValue = [];

                        $scope.DashboardSummary.DashboardTopSellStoreValue = [];
                        $scope.DashboardSummary.DashboardTopSellStoreName = [];

                        $scope.DashboardSummary.DashboardTopCustomerName = [];
                        $scope.DashboardSummary.DashboardTopCustomerValue = [];

                        for (var i = 0; i < response.data.dashboardTopCustomer.length; i++) {
                            $scope.DashboardSummary.DashboardTopCustomerValue.push(response.data.dashboardTopCustomer[i].value);
                            $scope.DashboardSummary.DashboardTopCustomerName.push(response.data.dashboardTopCustomer[i].customer);
                        }
                        for (var i = 0; i < response.data.dashboardTopSellStore.length; i++) {

                            $scope.DashboardSummary.DashboardTopSellStoreValue.push(response.data.dashboardTopSellStore[i].value);
                            $scope.DashboardSummary.DashboardTopSellStoreName.push(response.data.dashboardTopSellStore[i].store);
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
                    else if ($scope.ChartOrFunctionTypeEnum.Top5SellingStores == ChartOrFunctionTypeEnum) {
                        $scope.DashboardSummary.DashboardTopSellStoreValue = [];
                        $scope.DashboardSummary.DashboardTopSellStoreName = [];

                        for (var i = 0; i < response.data.dashboardTopSellStore.length; i++) {

                            $scope.DashboardSummary.DashboardTopSellStoreValue.push(response.data.dashboardTopSellStore[i].value);
                            $scope.DashboardSummary.DashboardTopSellStoreName.push(response.data.dashboardTopSellStore[i].store);
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
        $http.get(configurationService.basePath + "api/ActivityApi/GetActivityList?store_id=" + $scope.store_id)
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



    //$scope.GeneratePNG = function () {
    //     
    //    var node = document.getElementById("aaaaa");
    //    var canvas = document.createElement("canvas");
    //    canvas.height = node.offsetHeight;
    //    canvas.width = node.offsetWidth;
    //    var name = "test.png"

    //    rasterizeHTML.drawHTML(node.outerHTML, canvas)
    //         .then(function (renderResult) {
    //             if (navigator.msSaveBlob) {
    //                 window.navigator.msSaveBlob(canvas.msToBlob(), name);
    //             } else {
    //                 const a = document.createElement("a");
    //                 document.body.appendChild(a);
    //                 a.style = "display: none";
    //                 a.href = canvas.toDataURL();
    //                 a.download = name;
    //                 a.click();
    //                 document.body.removeChild(a);
    //             }
    //         });
    //}


    downloadPNG = function () {
        html2canvas($("#aaaaa"), {
            onrendered: function (canvas) {
                var url = canvas.toDataURL();
                $("<a>", {
                    href: url,
                    download: "Dashboard_" + $scope.store_id
                })
                    .on("click", function () { $(this).remove() })
                    .appendTo("body")[0].click()
            }

        });
    }


    $interval(function () { CheckVersionNumber(); }, 3000);
    function CheckVersionNumber() {
        $.ajax({
            method: "GET",
            url: "Home/CheckVersionNumber",
            async: false,
            success: function (data) {
                if (data != ApplicationVersion) {
                    angular.element("#Reloadlookup").modal('show');
                }
            }

        })
    };


    $scope.getActivityList();

    $scope.GetDashboardTopHeaderFields();
    $scope.GetLatestOrderDetailsDashboard();
    $scope.GetDashboardSummaryCharts($scope.ChartOrFunctionTypeEnum.All);

    $scope.printDiv = function () {
        window.print();
    }
});