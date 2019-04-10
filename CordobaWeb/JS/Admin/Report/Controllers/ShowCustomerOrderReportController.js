app.controller('ShowCustomerOrderReportController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    createDatePicker();
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    //#endregion  
    $scope.dtOptions = DTOptionsBuilder.newOptions()
                     .withOption('bDestroy', true)

    $scope.PageTitle = "Customer Orders Report";

    $scope.CustomerOrderList = [
                { CustomerId: 1, CustomerName: 'Michael Field', Email: 'michael@workflo-solutions.co.uk', CustomerGroupName: 'Grenke Rewards', StatusName: 'Enabled', NoOfOrders: 65, NoOfProducts:104, Total: '90,746 Points' }
              , { CustomerId: 2, CustomerName: 'Liam Guinan', Email: 'lg@alphabp.co.uk', CustomerGroupName: 'Grenke Rewards', StatusName: 'Enabled', NoOfOrders: 41, NoOfProducts: 100, Total: '88,442 points' }
              , { CustomerId: 3, CustomerName: 'Vincent Mathieu', Email: 'vincent.mathieu@almas-industries.com', CustomerGroupName: 'Grenke Rewards', StatusName: 'Enabled', NoOfOrders: 65, NoOfProducts: 160, Total: '90,746 Points' }
              , { CustomerId: 4, CustomerName: 'John Naylor', Email: 'j.naylor@copyritesystems.co.uk', CustomerGroupName: 'Grenke Rewards', StatusName: 'Enabled', NoOfOrders: 65, NoOfProducts: 120, Total: '90,746 Points' }
              , { CustomerId: 5, CustomerName: 'Shaun Wilkinson', Email: 'shaun.wilkinson@utax.co.uk', CustomerGroupName: 'Grenke Rewards', StatusName: 'Enabled', NoOfOrders: 65, NoOfProducts: 140, Total: '90,746 Points' }
              , { CustomerId: 6, CustomerName: 'Julian Furne', Email: 'julian.furne@academia.co.uk', CustomerGroupName: 'Grenke Rewards', StatusName: 'Enabled', NoOfOrders: 65, NoOfProducts:250, Total: '90,746 Points' }
              , { CustomerId: 7, CustomerName: 'Paul Hughes', Email: 'phughes@copytextni.co.uk', CustomerGroupName: 'Grenke Rewards', StatusName: 'Enabled', NoOfOrders: 65, NoOfProducts:100, Total: '90,746 Points' }
    ];

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




});