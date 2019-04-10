

app.controller('AddressBookController', function ($timeout,StoreSessionDetail,UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder) {

    if (!(UserDetail.customer_id > 0)) {
        window.location.href = 'home/accessdenied';
    }
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.NeedToShowAddressDetailForm = 0;
    $scope.GetCountryList=function()
    {
        $http.get(configurationService.basePath + "api/CountryApi/GetCountryList?StoreId=" + $scope.StoreDetailInSession.store_id + "&LoggedInUserId=" + UserDetail.customer_id + "&countryId=" + 0)
        .then(function (response) {            
            if (response.data.length > 0) {
                $scope.CountryList = response.data;
            }
        })
    .catch(function (response) {

    })
    .finally(function () {

    });
    }

    $scope.GetCustomerAddressList = function () {
        
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetCustomerAddressList_Layout?StoreId=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + UserDetail.customer_id)
          .then(function (response) {              
              $scope.AddressList = response.data;
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.AddORUpdateAddressBinding=function(AddressObj)
    {
        if (AddressObj != null && AddressObj != undefined)
        {
            $scope.AddressObj = AddressObj;
        }
        else {
            $scope.AddressObj = new Object();
            $scope.AddressObj.customer_id = UserDetail.customer_id;  
        }
        $scope.NeedToShowAddressDetailForm = 1;
    }

    $scope.CancelAddress=function()
    {
        $scope.AddressObj = new Object();
        $scope.NeedToShowAddressDetailForm = 0;
    }

    $scope.AddOrUpdateAddressDetail = function (form)
    {
        if (form.$valid)
        {
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/AddOrUpdateAddressDetail_Layout?StoreId=" + $scope.StoreDetailInSession.store_id, $scope.AddressObj)
           .then(function (response) {
                    $scope.AddressObj = new Object();
                    $scope.NeedToShowAddressDetailForm = 0;
                    $scope.AddressList = response.data;                   
                    toastr.success("Address book successfully updated.");
       })
        .catch(function (response) {

      })
      .finally(function () {

      });
        }      
    }

    $scope.DeleteAddress=function(AddressObj)
    {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/DeleteCustomerAddress?StoreId=" + $scope.StoreDetailInSession.store_id + "&customer_id=" + UserDetail.customer_id + "&address_id=" + AddressObj.address_id)
          .then(function (response) {              
              if (response.data > 0)
              {
                  toastr.success("Address successfully removed.");
                  $scope.GetCustomerAddressList();
                  $scope.AddressObj = new Object();
                  $scope.NeedToShowAddressDetailForm = 0;
              }
              else if (response.data == -1)
              {
                  toastr.warning("You can not remove default address.Please make another address to default.");
              }
              else {
                  toastr.error("Something wrong! Please try again later.");
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }
    
    $scope.GetCountryList();
    $scope.GetCustomerAddressList();


});