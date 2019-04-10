app.controller('StoreHTMLController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.StoreHTMLSummary = [];

    var currentTime = new Date();
    $scope.Years = [{ id: currentTime.getFullYear(), name: currentTime.getFullYear() }, { id: currentTime.getFullYear() - 1, name: currentTime.getFullYear() - 1 }];

    $scope.selectedyear = $scope.Years[0].id;
    //const MONTH_NAMES = ["January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"];
    const MONTH_NAMES = [{ id: 1, name: "January" }, { id: 2, name: "February" }, { id: 3, name: "March" },
                         { id: 4, name: "April" }, { id: 5, name: "May" }, { id: 6, name: "June" },
                         { id: 7, name: "July" }, { id: 8, name: "August" }, { id: 9, name: "September" },
                         { id: 10, name: "October" }, { id: 11, name: "November" }, { id: 12, name: "December" }];

    $scope.selectedmonth = 0;
    $scope.monthname =''
    $scope.GetMonthData = function (year) {
        $scope.Months = [];
        if (year == currentTime.getFullYear()) {
            for (var i = 0 ; i < currentTime.getMonth() + 1 ; i++) {
                $scope.Months.push({ id: MONTH_NAMES[i].id, name: MONTH_NAMES[i].name });
            }
        }
        else {
            for (var i = 0 ; i < 12 ; i++) {
                $scope.Months.push({ id: MONTH_NAMES[i].id, name: MONTH_NAMES[i].name });
            }
        }
        $scope.selectedmonth = $scope.Months[0].id;
        $scope.monthname = $scope.Months[0].name;
        $scope.GetStoreHTMLCharts();

    }

    $scope.GetChartData = function (year, month) {
        $scope.selectedyear = year;
        $scope.selectedmonth = month;
        $scope.monthname = MONTH_NAMES[month-1].name;
        //$state.go('StoreHTML');
        $scope.GetStoreHTMLCharts();
        
    }

    function LoadCharts() {
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
                 var StoreSummary = ec.init(document.getElementById('StoreHTMLStoreSummary'), limitless);
                 var PointsRemaining = ec.init(document.getElementById('PointsRemaining'), limitless);
                 var ParticipantsLoadedByMonth = ec.init(document.getElementById('ParticipantsLoadedByMonth'), limitless);
                 var PointsLoadedByMonth = ec.init(document.getElementById('PointsLoadedByMonth'), limitless);
                 var PointsRedeemedByMonth = ec.init(document.getElementById('PointsRedeemedByMonth'), limitless);
                 var OrdersPlacedByType = ec.init(document.getElementById('OrdersPlacedByType'), limitless);
                 //window.onresize = OrdersPlacedByType.resize;

                 //global._preResize && $(window).off('resize', global._preResize);
                 //global._preResize = OrdersPlacedByType.resize;
                 //$(window).on('resize', OrdersPlacedByType.resize);
                 // Charts setup
                 // ------------------------------                    
                 StoreSummary_options = {

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
                         data: $scope.StoreHTMLSummary.StoreHTMLStoreSummary
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
                             //magicType: {
                             //    show: true,
                             //    title: {
                             //        pie: 'Switch to pies',
                             //        funnel: 'Switch to funnel',
                             //    },
                             //    type: ['pie', 'funnel']
                             //},
                             //restore: {
                             //    show: true,
                             //    title: 'Restore'
                             //},
                             //saveAsImage: {
                             //    show: true,
                             //    title: 'Save as image',
                             //    lang: ['Save']
                             //}
                         }
                     },

                     // Enable drag recalculate
                     calculable: false,

                     // Add series
                     series: [
                         {
                             name: '',
                             type: 'pie',
                             radius: ['50%', '70%'],
                             avoidLabelOverlap: false,
                             label: {
                                 normal: {
                                     show: false,
                                     position: 'center'
                                 },
                                 emphasis: {
                                     show: true,
                                     textStyle: {
                                         fontSize: '30',
                                         fontWeight: 'bold'
                                     }
                                 }
                             },
                             labelLine: {
                                 normal: {
                                     show: false
                                 }
                             },
                             data: $scope.StoreHTMLSummary.StoreHTMLStoreSummary

                         }
                     ]
                 };

                 //PointsRemaining

                 PointsRemaining_options = {

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
                         data: $scope.StoreHTMLSummary.PointsRemaining
                     },

                     // Display toolbox
                     toolbox: {
                         show: true,
                         orient: 'vertical',
                         feature: {

                             //}
                         }
                     },

                     // Enable drag recalculate
                     calculable: false,

                     // Add series
                     series: [
                         {
                             name: '',
                             type: 'pie',
                             radius: ['50%', '70%'],
                             avoidLabelOverlap: false,
                             label: {
                                 normal: {
                                     show: false,
                                     position: 'center'
                                 },
                                 emphasis: {
                                     show: true,
                                     textStyle: {
                                         fontSize: '30',
                                         fontWeight: 'bold'
                                     }
                                 }
                             },
                             labelLine: {
                                 normal: {
                                     show: false
                                 }
                             },
                             data: $scope.StoreHTMLSummary.PointsRemaining

                         }
                     ]
                 };

                 console.log($scope.ParticipantsLoadedByMonthname);
                 console.log($scope.ParticipantsLoadedByMonthvalue);
                 //ParticipantsLoadedByMonth
                 ParticipantsLoadedByMonth_options = {
                     title: {
                         x: 'center',
                         text: '',
                         subtext: '',
                         link: 'http://echarts.baidu.com/doc/example.html'
                     },
                     tooltip: {
                         trigger: 'item'
                     },
                     toolbox: {
                         //show: true,
                         //feature: {
                         //    dataView: { show: true, readOnly: false },
                         //    restore: { show: true },
                         //    saveAsImage: { show: true }
                         //}
                     },
                     calculable: true,
                     grid: {
                         borderWidth: 0,
                         y: 80,
                         y2: 60
                     },
                     xAxis: [
                         {
                             type: 'category',
                             show: false,
                             data: $scope.ParticipantsLoadedByMonthname
                         }
                     ],
                     yAxis: [
                         {
                             type: 'value',
                             show: false
                         }
                     ],
                     series: [
                         {
                             name: '',
                             type: 'bar',
                             itemStyle: {
                                 normal: {
                                     color: function (params) {
                                         // build a color map as your need.
                                         var colorList = [
                                           '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                                            '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                                            '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0'
                                         ];
                                         return colorList[params.dataIndex]
                                     },
                                     label: {
                                         show: true,
                                         position: 'top',
                                         formatter: '{b}\n{c}'
                                     }
                                 }
                             },
                             data: $scope.ParticipantsLoadedByMonthvalue,
                             markPoint: {
                                 tooltip: {
                                     trigger: 'item',
                                     backgroundColor: 'rgba(0,0,0,0)',
                                     formatter: function (params) {
                                         return '<img src="'
                                                 + params.data.symbol.replace('image://', '')
                                                 + '"/>';
                                     }
                                 },
                                 data: [
                                     { xAxis: 0, y: 350, name: 'Line', symbolSize: 20, symbol: 'image://../asset/ico/折线图.png' },
                                     { xAxis: 1, y: 350, name: 'Bar', symbolSize: 20, symbol: 'image://../asset/ico/柱状图.png' },
                                     { xAxis: 2, y: 350, name: 'Scatter', symbolSize: 20, symbol: 'image://../asset/ico/散点图.png' },
                                     { xAxis: 3, y: 350, name: 'K', symbolSize: 20, symbol: 'image://../asset/ico/K线图.png' },
                                     { xAxis: 4, y: 350, name: 'Pie', symbolSize: 20, symbol: 'image://../asset/ico/饼状图.png' },
                                     { xAxis: 5, y: 350, name: 'Radar', symbolSize: 20, symbol: 'image://../asset/ico/雷达图.png' },
                                     { xAxis: 6, y: 350, name: 'Chord', symbolSize: 20, symbol: 'image://../asset/ico/和弦图.png' },
                                     { xAxis: 7, y: 350, name: 'Force', symbolSize: 20, symbol: 'image://../asset/ico/力导向图.png' },
                                     { xAxis: 8, y: 350, name: 'Map', symbolSize: 20, symbol: 'image://../asset/ico/地图.png' },
                                     { xAxis: 9, y: 350, name: 'Gauge', symbolSize: 20, symbol: 'image://../asset/ico/仪表盘.png' },
                                     { xAxis: 10, y: 350, name: 'Funnel', symbolSize: 20, symbol: 'image://../asset/ico/漏斗图.png' },
                                 ]
                             }
                         }
                     ]




                     //tooltip: {
                     //    trigger: 'axis'
                     //},
                     ////legend: {
                     ////    data: ['sdfds']
                     ////},
                     //toolbox: {
                     //    show: true,
                     //    orient: 'vertical',
                     //    feature: {
                     //        //mark: { show: true },
                     //        //dataView: { show: true, readOnly: false },
                     //        //magicType: { show: true, type: ['line', 'bar'], title: 'Switch to line', },
                     //        //magicType: {
                     //        //    show: true,
                     //        //    title: {
                     //        //        bar: 'Switch to pies',
                     //        //        line: 'Switch to line',
                     //        //    },
                     //        //    type: ['bar', 'line']
                     //        //},
                     //        //restore: { show: true, title: 'Restore' },
                     //        //saveAsImage: { show: true, title: 'Save as image' }
                     //    }
                     //},
                     //calculable: true,
                     //xAxis: [
                     //    {
                     //        type: 'category',
                     //        data: $scope.ParticipantsLoadedByMonthname
                     //         , axisLabel: {
                     //             show: true,
                     //             interval: 0,    // {number}
                     //             rotate: 45,
                     //             margin: -20,
                     //             formatter: $scope.ParticipantsLoadedByMonthname,
                     //             textStyle: {
                     //                 color: 'blue',
                     //                 fontFamily: 'sans-serif',
                     //                 fontSize: 10,
                     //                 fontStyle: 'italic',
                     //                 fontWeight: 'bold'
                     //             }

                     //         }
                     //    }
                     //],
                     //yAxis: [
                     //    {
                     //        type: 'Month'
                     //    }
                     //],
                     //series: [
                     //    {
                     //        name: 'Points',
                     //        type: 'bar',
                     //        data: $scope.ParticipantsLoadedByMonthvalue,
                     //        itemStyle: {
                     //            normal: {
                     //                color: function (param) {
                     //                    var colorList = ['#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B'];
                     //                    return colorList[param.dataIndex]
                     //                }
                     //            },
                     //            label: {
                     //                show: true,
                     //                position: 'top',
                     //                formatter: '{b}\n{c}'
                     //            }
                     //        }
                     //    }
                     //]
                 };



                 //PointsLoadedByMonth
                 PointsLoadedByMonth_options = {
                     tooltip: {
                         trigger: 'axis',
                         axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                             type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                         }
                     },
                     //legend: {
                     //    data: $scope.StoreHTMLSummary.PointsLoadedByMonthPoints
                     //},
                     //toolbox: {
                     //    show: true,
                     //    feature: {
                     //        mark: { show: true },
                     //        dataView: { show: true, readOnly: false },
                     //        magicType: { show: true, type: ['line', 'bar'] },
                     //        restore: { show: true },
                     //        saveAsImage: { show: true }
                     //    }
                     //},
                     calculable: true,
                     xAxis: [
                         {
                             type: 'value'
                         }
                     ],
                     yAxis: [
                         {
                             type: 'category',
                             axisTick: { show: false },
                             data: $scope.StoreHTMLSummary.PointsLoadedByMonth
                         }
                     ],
                     series: [
                         {
                             name: 'Points',
                             type: 'bar',
                             itemStyle: { normal: { label: { show: true } } },
                             data: $scope.StoreHTMLSummary.PointsLoadedByMonthPoints
                         }//,
                     ]

                     //tooltip: {
                     //    trigger: 'axis',
                     //    axisPointer: {
                     //        type: 'shadow'
                     //    }
                     //},
                     //legend: {
                     //    data: ['Month', 'Points']
                     //},
                     //grid: {
                     //    left: '3%',
                     //    right: '4%',
                     //    bottom: '3%',
                     //    containLabel: true
                     //},
                     //xAxis: {
                     //    type: 'value',
                     //    boundaryGap: [0, 0.01]
                     //},
                     //yAxis: {
                     //    type: 'category',
                     //    data: $scope.StoreHTMLSummary.PointsLoadedByMonth
                     //},
                     //series: [

                     //    {
                     //        name: 'Points',
                     //        type: 'bar',
                     //        data: $scope.StoreHTMLSummary.PointsLoadedByMonthPoints
                     //    }
                     //]
                 };

                 //PointsRedeemedByMonth
                 PointsRedeemedByMonth_options = {
                     tooltip: {
                         trigger: 'axis',
                         axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                             type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                         }
                     },
                     //legend: {
                     //    data: 'Points'
                     //},
                     //toolbox: {
                     //    show: true,
                     //    feature: {
                     //        mark: { show: true },
                     //        dataView: { show: true, readOnly: false },
                     //        magicType: { show: true, type: ['line', 'bar'] },
                     //        restore: { show: true },
                     //        saveAsImage: { show: true }
                     //    }
                     //},
                     calculable: true,
                     xAxis: [
                         {
                             type: 'value'
                         }
                     ],
                     yAxis: [
                         {
                             type: 'category',
                             axisTick: { show: false },
                             data: $scope.StoreHTMLSummary.PointsRedeemedByMonth
                         }
                     ],
                     series: [
                         {
                             name: 'Points',
                             type: 'bar',
                             itemStyle: { normal: { label: { show: true } } },
                             data: $scope.StoreHTMLSummary.PointsRedeemedByMonthPoints
                         }//,
                     ]


                     //tooltip: {
                     //    trigger: 'axis',
                     //    axisPointer: {
                     //        type: 'shadow'
                     //    }
                     //},
                     //legend: {
                     //    data: ['Month', 'Points']
                     //},
                     //grid: {
                     //    left: '3%',
                     //    right: '4%',
                     //    bottom: '3%',
                     //    containLabel: true
                     //},
                     //xAxis: {
                     //    type: 'value',
                     //    boundaryGap: [0, 0.01]
                     //},
                     //yAxis: {
                     //    type: 'category',
                     //    data: $scope.StoreHTMLSummary.PointsRedeemedByMonth
                     //},
                     //series: [

                     //    {
                     //        name: 'Points',
                     //        type: 'bar',
                     //        data: $scope.StoreHTMLSummary.PointsRedeemedByMonthPoints
                     //    }
                     //]
                 };

                 //Orders placed by Type
                 console.log($scope.StoreHTMLSummary.OrdersPlacedByTypeName);
                 console.log($scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount);

                 OrdersPlacedByType_options = {
                     //title: {
                     //    x: 'center',
                     //    text: '',
                     //    subtext: '',
                     //    link: 'http://echarts.baidu.com/doc/example.html'
                     //},
                     //tooltip: {
                     //    trigger: 'item'
                     //},
                     //toolbox: {
                     //    //show: true,
                     //    //feature: {
                     //    //    dataView: { show: true, readOnly: false },
                     //    //    restore: { show: true },
                     //    //    saveAsImage: { show: true }
                     //    //}
                     //},
                     //calculable: true,
                     //grid: {
                     //    borderWidth: 0,
                     //    y: 80,
                     //    y2: 60
                     //},
                     //xAxis: [
                     //    {
                     //        type: 'category',
                     //        show: false,
                     //        rotate: 45,
                     //        data: $scope.StoreHTMLSummary.OrdersPlacedByTypeName
                     //    }
                     //],
                     //yAxis: [
                     //    {
                     //        type: 'value',
                     //        show: false,
                     //        rotate: 45
                     //    }
                     //],
                     //series: [
                     //    {
                     //        name: '',
                     //        type: 'bar',
                     //        itemStyle: {
                     //            normal: {
                     //                color: function (params) {
                     //                    // build a color map as your need.
                     //                    var colorList = [
                     //                      '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                     //                       '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                     //                       '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0',
                     //                       '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                     //                       '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                     //                       '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0',
                     //                       '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                     //                       '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                     //                       '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0'
                     //                    ];
                     //                    return colorList[params.dataIndex]
                     //                },
                     //                label: {
                     //                    show: true,
                     //                    position: 'top',
                     //                    formatter: '{b}\n{c}',
                     //                    rotate: 45
                     //                }
                     //            }
                     //        },
                     //        data: $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount,
                     //        markPoint: {
                     //            tooltip: {
                     //                trigger: 'item',
                     //                backgroundColor: 'rgba(0,0,0,0)',
                     //                formatter: function (params) {
                     //                    return '<img src="'
                     //                            + params.data.symbol.replace('image://', '')
                     //                            + '"/>';
                     //                }
                     //            },
                     //            data: [
                     //                { xAxis: 0, y: 350, name: 'Line', symbolSize: 20, symbol: 'image://../asset/ico/折线图.png' },
                     //                { xAxis: 1, y: 350, name: 'Bar', symbolSize: 20, symbol: 'image://../asset/ico/柱状图.png' },
                     //                { xAxis: 2, y: 350, name: 'Scatter', symbolSize: 20, symbol: 'image://../asset/ico/散点图.png' },
                     //                { xAxis: 3, y: 350, name: 'K', symbolSize: 20, symbol: 'image://../asset/ico/K线图.png' },
                     //                { xAxis: 4, y: 350, name: 'Pie', symbolSize: 20, symbol: 'image://../asset/ico/饼状图.png' },
                     //                { xAxis: 5, y: 350, name: 'Radar', symbolSize: 20, symbol: 'image://../asset/ico/雷达图.png' },
                     //                { xAxis: 6, y: 350, name: 'Chord', symbolSize: 20, symbol: 'image://../asset/ico/和弦图.png' },
                     //                { xAxis: 7, y: 350, name: 'Force', symbolSize: 20, symbol: 'image://../asset/ico/力导向图.png' },
                     //                { xAxis: 8, y: 350, name: 'Map', symbolSize: 20, symbol: 'image://../asset/ico/地图.png' },
                     //                { xAxis: 9, y: 350, name: 'Gauge', symbolSize: 20, symbol: 'image://../asset/ico/仪表盘.png' },
                     //                { xAxis: 10, y: 350, name: 'Funnel', symbolSize: 20, symbol: 'image://../asset/ico/漏斗图.png' },
                     //                { xAxis: 11, y: 350, name: 'Line', symbolSize: 20, symbol: 'image://../asset/ico/折线图.png' },
                     //                { xAxis: 12, y: 350, name: 'Bar', symbolSize: 20, symbol: 'image://../asset/ico/柱状图.png' },
                     //                { xAxis: 13, y: 350, name: 'Scatter', symbolSize: 20, symbol: 'image://../asset/ico/散点图.png' },
                     //                { xAxis: 14, y: 350, name: 'K', symbolSize: 20, symbol: 'image://../asset/ico/K线图.png' },
                     //                { xAxis: 15, y: 350, name: 'Pie', symbolSize: 20, symbol: 'image://../asset/ico/饼状图.png' },
                     //                { xAxis: 16, y: 350, name: 'Radar', symbolSize: 20, symbol: 'image://../asset/ico/雷达图.png' },
                     //                { xAxis: 17, y: 350, name: 'Chord', symbolSize: 20, symbol: 'image://../asset/ico/和弦图.png' },
                     //                { xAxis: 18, y: 350, name: 'Force', symbolSize: 20, symbol: 'image://../asset/ico/力导向图.png' },
                     //                { xAxis: 19, y: 350, name: 'Map', symbolSize: 20, symbol: 'image://../asset/ico/地图.png' },
                     //                { xAxis: 20, y: 350, name: 'Gauge', symbolSize: 20, symbol: 'image://../asset/ico/仪表盘.png' },
                     //                { xAxis: 21, y: 350, name: 'Funnel', symbolSize: 20, symbol: 'image://../asset/ico/漏斗图.png' },
                     //            ]
                     //        }
                     //    }
                     //]
                     

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
                             //magicType: {
                             //    show: true,
                             //    title: {
                             //        bar: 'Switch to pies',
                             //        line: 'Switch to line',
                             //    },
                             //    type: ['bar', 'line']
                             //},
                             //restore: { show: true, title: 'Restore' },
                             //saveAsImage: { show: true, title: 'Save as image' }
                         }
                     },
                     calculable: true,
                     xAxis: [
                         {
                             type: 'category',
                             data: $scope.StoreHTMLSummary.OrdersPlacedByTypeName
                              , axisLabel: {
                                  show: true,
                                  interval: 0,    // {number}
                                  rotate: 45,
                                  margin: -20,
                                  formatter: $scope.StoreHTMLSummary.OrdersPlacedByTypeName,
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
                             type: 'OrderCount'
                             
                         }
                     ],
                     series: [
                         {
                             name: 'OrderCount',
                             type: 'bar',
                             data: $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount,
                             itemStyle: {
                                 normal: {
                                     color: function (param) {
                                         var colorList = ['#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B', '#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B',
                                                         '#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B', '#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B',
                                                          '#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B', '#1976D2', '#00BCD4', '#C0CA33', '#795548', '#D7504B'];
                                         return colorList[param.dataIndex]
                                     }
                                 }
                             }
                         }
                     ]
                 };

                 StoreSummary.setOption(StoreSummary_options);
                 PointsRemaining.setOption(PointsRemaining_options);
                 ParticipantsLoadedByMonth.setOption(ParticipantsLoadedByMonth_options);
                 PointsLoadedByMonth.setOption(PointsLoadedByMonth_options);
                 PointsRedeemedByMonth.setOption(PointsRedeemedByMonth_options);
                 OrdersPlacedByType.setOption(OrdersPlacedByType_options);


                 $timeout(function () {

                     StoreSummary.resize();
                     PointsRemaining.resize();
                     ParticipantsLoadedByMonth.resize();
                     PointsLoadedByMonth.resize();
                     PointsRedeemedByMonth.resize();
                     OrdersPlacedByType.resize();
                 }, 0);


                 // Resize charts
                 // ------------------------------

                 window.onresize = function () {
                     $timeout(function () {

                         StoreSummary.resize();
                         PointsRemaining.resize();
                         ParticipantsLoadedByMonth.resize();
                         PointsLoadedByMonth.resize();
                         PointsRedeemedByMonth.resize();
                         OrdersPlacedByType.resize();
                     }, 0);
                 }
             }
        );
    }

    

    $scope.GetStoreHTMLCharts = function () {
        //$http({
        //    url: configurationService.basePath +  "api/StoreApi/GetStoreHTMLCharts?StoreID=" + $scope.StoreId + "&Month=" + $scope.selectedmonth + "&Year=" + $scope.selectedyear,
        //    method: "GET",
        //    //data: storeentity,
        //    async: false,
        //    responseType: 'arraybuffer'
        //}).success(function (data, status, headers, config) {
        //     
        //    var type = headers('Content-Type');
        //    var disposition = headers('Content-Disposition');
        //    if (disposition) {
        //        var match = disposition.match(/.*filename=\"?([^;\"]+)\"?.*/);
        //        if (match[1])
        //            defaultFileName = match[1];
        //    }
        //    defaultFileName = defaultFileName.replace(/[<>:"\/\\|?*]+/g, '_');
        //    var blob = new Blob([data], { type: type });
        //    if (navigator.appVersion.toString().indexOf('.NET') > 0) // For IE 
        //        window.navigator.msSaveBlob(blob, defaultFileName);
        //    else {
        //        var objectUrl = URL.createObjectURL(blob);
        //        var downloadLink = document.createElement("a");
        //        downloadLink.href = objectUrl;
        //        downloadLink.download = defaultFileName;
        //        document.body.appendChild(downloadLink);
        //        downloadLink.click();
        //        document.body.removeChild(downloadLink);
        //    }
        //}).error(function (data, status, headers, config) {
        //    toastr.error("Some error has occured, please contact to admin");
        //});


        //$http.get(configurationService.basePath + "api/StoreApi/GetStoreHTMLCharts?StoreID=" + $scope.StoreId + "&Month=" + $scope.selectedmonth + "&Year=" + $scope.selectedyear)
        //.then(function (response) {
           
        //     
        //    if (response.data != null) {
        //        $scope.StoreHTMLSummary.StoreHTMLStoreSummary = [];

        //        for (var i = 0; i < response.data.storeSummary.length; i++) {
        //            $scope.StoreHTMLSummary.StoreHTMLStoreSummary.push({ value: response.data.storeSummary[i].Count, name: response.data.storeSummary[i].Status })
        //        }
        //        $scope.StoreHTMLSummary.PointsRemaining = [];
        //        $scope.StoreHTMLSummary.PointsRemaining.push({ value: response.data.pointsRemaining[0].Count, name: response.data.pointsRemaining[0].Status })

        //        $scope.ParticipantsLoadedByMonthvalue = [];
        //        $scope.ParticipantsLoadedByMonthname = [];
        //        $scope.ParticipantsLoadedByMonth = [];
        //        for (var i = 0; i < response.data.participantsLoadedByMonth.length; i++) {
        //            $scope.ParticipantsLoadedByMonthvalue.push(response.data.participantsLoadedByMonth[i].CustomerCount);
        //            //$scope.ParticipantsLoadedByMonthname.push(response.data.participantsLoadedByMonth[i].Month)
        //            $scope.ParticipantsLoadedByMonthname.push(response.data.participantsLoadedByMonth[i].Month.split("-")[0].substring(0, 3) + "-" + response.data.participantsLoadedByMonth[i].Month.split("-")[1].substring(0, 4));
        //            $scope.ParticipantsLoadedByMonth.push({ CustomerCount: response.data.participantsLoadedByMonth[i].CustomerCount, Month: response.data.participantsLoadedByMonth[i].Month });
        //        }
        //        
        //        $scope.StoreHTMLSummary.PointsLoadedByMonth = [];
        //        $scope.StoreHTMLSummary.PointsLoadedByMonthPoints = [];
        //        for (var i = 0; i < response.data.pointsLoadedByMonth.length; i++) {
        //            $scope.StoreHTMLSummary.PointsLoadedByMonth.push(response.data.pointsLoadedByMonth[i].Month.split("-")[0].substring(0, 3) + "-" + response.data.pointsLoadedByMonth[i].Month.split("-")[1].substring(0, 4));
        //            //$scope.StoreHTMLSummary.PointsLoadedByMonth.push(response.data.pointsLoadedByMonth[i].Month);
        //            $scope.StoreHTMLSummary.PointsLoadedByMonthPoints.push(response.data.pointsLoadedByMonth[i].Points)
        //        }

        //        $scope.StoreHTMLSummary.PointsRedeemedByMonth = [];
        //        $scope.StoreHTMLSummary.PointsRedeemedByMonthPoints = [];
        //        for (var i = 0; i < response.data.pointsRedeemedByMonth.length; i++) {
        //            //$scope.StoreHTMLSummary.PointsRedeemedByMonth.push(response.data.pointsRedeemedByMonth[i].Month);
        //            $scope.StoreHTMLSummary.PointsRedeemedByMonth.push(response.data.pointsRedeemedByMonth[i].Month.split("-")[0].substring(0, 3) + "-" + response.data.pointsRedeemedByMonth[i].Month.split("-")[1].substring(0, 4));
        //            $scope.StoreHTMLSummary.PointsRedeemedByMonthPoints.push(response.data.pointsRedeemedByMonth[i].Points)
        //        }

        //        $scope.StoreHTMLSummary.TopPointsHolders = response.data.topPointsHolders;
        //         
                
        //            $scope.StoreHTMLSummary.OrdersPlacedByType = [];
        //            $scope.StoreHTMLSummary.OrdersPlacedByTypeName = [];
        //            $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount = [];
        //             

        //            for (var i = 0; i < response.data.orderPlacedByType.length; i++) {
        //                $scope.StoreHTMLSummary.OrdersPlacedByType.push({ OrderCount: response.data.orderPlacedByType[i].OrderCount, Name: response.data.orderPlacedByType[i].Name });
        //                $scope.StoreHTMLSummary.OrdersPlacedByTypeOrderCount.push(response.data.orderPlacedByType[i].OrderCount);
        //                $scope.StoreHTMLSummary.OrdersPlacedByTypeName.push(response.data.orderPlacedByType[i].Name)
        //            }
        //             
        //            $scope.storelogo = response.data.logo;
        //            $scope.myObj = {
        //                "width": "456px",
        //                "height": "90px",
        //                "float": "right",
        //                "background-image": "url(" + $scope.storelogo + ")"
        //            }
        //    }
            
        //    LoadCharts();
        //})
        //.catch(function (response) {

        //})
        //.finally(function (response) {

        //});
    }


    $scope.ExportStoreHTMLPDF = function () {
         
        $http({
            url: configurationService.basePath + "api/StoreApi/GetStoreHTMLCharts?StoreID=" + $scope.StoreId + "&Month=" + $scope.selectedmonth + "&Year=" + $scope.selectedyear,
            method: "GET",
            //data: storeentity,
            async: false,
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
            }
        }).error(function (data, status, headers, config) {
            toastr.error("Some error has occured, please contact to admin");
        });


        ////html2canvas($("#pdf"), {
        ////    onrendered: function (canvas) {
        ////         
        ////        theCanvas = canvas;
        ////        theCanvas.setAttribute("id", "Div1");
        ////        document.body.appendChild(canvas);

        ////        // Convert and download as image 
        ////        //Canvas2Image.saveAsPNG(canvas);
        ////        $("#img-out").html(canvas);
        ////        

        ////        var base64 = $('#Div1')[0].toDataURL();
        ////        $("#imgCapture").attr("src", base64);
        ////        $("#imgCapture").show();

        ////        $("#img-out").hide();
        ////        $("#img-capture").hide();
        ////        // Clean up 
        ////        //document.body.removeChild(canvas);
        ////    }
        ////});

        ////Heading
        //html2canvas($("#heading"), {
        //    onrendered: function (canvas) {
        //         
        //        theCanvas = canvas;
        //        theCanvas.setAttribute("id", "Div1");
        //        document.body.appendChild(canvas);

        //        // Convert and download as image 
        //        //Canvas2Image.saveAsPNG(canvas);
        //        $("#img-out").html(canvas);
        //         

        //        var base64 = $('#Div1')[0].toDataURL();
        //        $("#imgCapture").attr("src", base64);
        //        $("#imgCapture").show();

        //        $("#img-out").hide();
        //        $("#img-capture").hide();
        //        // Clean up 
        //        //document.body.removeChild(canvas);

        //    }
        //});

        ////Store Image
        //html2canvas($("#storeimage"), {
        //    onrendered: function (canvas1) {
        //         
        //        theCanvas1 = canvas1;
        //        theCanvas1.setAttribute("id", "Divstoreimage");
        //        document.body.appendChild(canvas1);

        //        // Convert and download as image 
        //        //Canvas2Image.saveAsPNG(canvas);
        //        $("#img-outstoreimage").html(canvas1);
        //         

        //        var base64 = $('#Divstoreimage')[0].toDataURL();
        //        $("#imgCapturestoreimage").attr("src", base64);
        //        $("#imgCapturestoreimage").show();

        //        $("#img-outstoreimage").hide();
        //        $("#img-capturestoreimage").hide();
        //        // Clean up 
        //        //document.body.removeChild(canvas);

        //    }
        //});

        ////Store Summary
        //html2canvas($("#storesummary"), {
        //    onrendered: function (canvasStoreSummary) {
        //        
        //        theCanvas1 = canvasStoreSummary;
        //        theCanvas1.setAttribute("id", "DivStoreSummary");
        //        document.body.appendChild(canvasStoreSummary);

        //        // Convert and download as image 
        //        //Canvas2Image.saveAsPNG(canvas);
        //        $("#img-outStoreSummary").html(canvasStoreSummary);
        //         

        //        var base64 = $('#DivStoreSummary')[0].toDataURL();
        //        $("#imgCaptureStoreSummary").attr("src", base64);
        //        $("#imgCaptureStoreSummary").show();

        //        $("#img-outStoreSummary").hide();
        //        $("#img-captureStoreSummary").hide();
        //        // Clean up 
        //        //document.body.removeChild(canvas);

        //    }
        //});

        ////Points Remaining
        //html2canvas($("#pointsremaining"), {
        //    onrendered: function (canvasPointsRemaining) {
        //         
        //        theCanvas1 = canvasPointsRemaining;
        //        theCanvas1.setAttribute("id", "DivPointsRemaining");
        //        document.body.appendChild(canvasPointsRemaining);

        //        // Convert and download as image 
        //        //Canvas2Image.saveAsPNG(canvas);
        //        $("#img-outPointsRemaining").html(canvasPointsRemaining);
        //        

        //        var base64 = $('#DivPointsRemaining')[0].toDataURL();
        //        $("#imgCapturePointsRemaining").attr("src", base64);
        //        $("#imgCapturePointsRemaining").show();

        //        $("#img-outPointsRemaining").hide();
        //        $("#img-capturePointsRemaining").hide();
        //        // Clean up 
        //        //document.body.removeChild(canvas);

        //    }
        //});

        ////Participants Loaded By Month
        //html2canvas($("#participantsloadedbymonth"), {
        //    onrendered: function (canvasParticipantsLoadedByMonth) {
        //         
        //        theCanvas1 = canvasParticipantsLoadedByMonth;
        //        theCanvas1.setAttribute("id", "DivParticipantsLoadedByMonth");
        //        document.body.appendChild(canvasParticipantsLoadedByMonth);

        //        // Convert and download as image 
        //        //Canvas2Image.saveAsPNG(canvas);
        //        $("#img-outParticipantsLoadedByMonth").html(canvasParticipantsLoadedByMonth);
        //         

        //        var base64 = $('#DivParticipantsLoadedByMonth')[0].toDataURL();
        //        $("#imgCaptureParticipantsLoadedByMonth").attr("src", base64);
        //        $("#imgCaptureParticipantsLoadedByMonth").show();

        //        $("#img-outParticipantsLoadedByMonth").hide();
        //        $("#img-captureParticipantsLoadedByMonth").hide();
        //        // Clean up 
        //        //document.body.removeChild(canvas);

        //    }
        //});



        //$timeout(function () { $("#Generatepdf").trigger("click"); }, 3000);


    }
    $("#Generatepdf").click(function () {
        //function ExportStoreHTMLPDF1() {
         
        var template = $('#img-capture').html();
        var description = $('#img-capturestoreimage').html();
        var address = $('#img-captureStoreSummary').html();
        var county = $('#img-capturePointsRemaining').html();
        var telephone = $('#img-captureParticipantsLoadedByMonth').html();
        //var heading = $("#heading").html();
        //var storeimage = $("#storeimage").html();

         
        //var storeentity = { template: template };
        var storeentity = { template: template, description: description, address: address, county: county, telephone: telephone };
        $http({
            url: configurationService.basePath + 'api/StoreApi/ExportStoreHTMLPDF',
            method: "POST",
            data: storeentity,
            async: false,
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
            }
        }).error(function (data, status, headers, config) {
            toastr.error("Some error has occured, please contact to admin");
        });
    });
    $scope.getStoreWidth =function() {
        return '1270px';   
    }

});