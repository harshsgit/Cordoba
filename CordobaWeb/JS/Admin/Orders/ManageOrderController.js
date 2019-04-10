app.controller('ManageOrderController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    if ($stateParams.orderId != undefined && $stateParams.orderId != null) {
        $scope.PageTitle = "Update Order";
    }
    else {
        $scope.PageTitle = "Add Order";
    }
    $scope.countryId = 0;
    $scope.StoreList = [];
    $scope.CustomerGroupList = [];
    $scope.RegionStateListShipping = [];
    $scope.RegionStateListPayment = [];
    $scope.CurrencyList = [];
    $scope.CountryList = [];
    $scope.OrderDetails = [];
    $scope.CustomerList = [];

    $scope.selectedOrderStatus = 1;
    $scope.selectedShippingCountry = 0;
    $scope.selectedPaymentCountry = 0;

    $scope.GetZoneListByCountryPayment = function (countryId) {

        $http.get(configurationService.basePath + "api/OrderApi/GetZoneListByCountry?countryId=" + countryId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
        .then(function (response) {

            $scope.RegionStateListPayment = [];
            if (response.data.length > 0) {
                $scope.RegionStateListPayment = response.data;
                $scope.selectedPaymentZone = $scope.RegionStateListPayment[0].zone_id;
            }
            else {
                $scope.selectedPaymentZone = 0;
            }
        })
       .catch(function (response) {

       })
       .finally(function () {

       });
    }

    //$scope.GetZoneListByCountryShipping = function (countryId) {

    //    $http.get(configurationService.basePath + "api/OrderApi/GetZoneListByCountry?countryId=" + countryId)
    //    .then(function (response) {

    //        $scope.RegionStateListShipping = [];
    //        if (response.data.length > 0) {
    //            $scope.RegionStateListShipping = response.data;
    //            $scope.selectedShippingZone = $scope.RegionStateListShipping[0].zone_id;
    //            //$scope.selectedPaymentZone = $scope.RegionStateList[0].zone_id;
    //        }
    //        else {
    //            $scope.selectedShippingZone = 0;
    //        }
    //    })
    //   .catch(function (response) {

    //   })
    //   .finally(function () {

    //   });
    //}

    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreID=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
         .then(function (response) {
             if (response.data.length > 0) {
                 //$scope.StoreList.push({ store_id: 0, name: 'Default' });
                 for (var i = 0; i < response.data.length; i++) {
                     $scope.StoreList.push(response.data[i]);
                 }
                 $scope.selectedStore = 0;

             }
         })
        .catch(function (response) {

        })
        .finally(function () {

        });
    }

    function GetCustomerGroupList() {
        $http.get(configurationService.basePath + "api/OrderApi/GetCustomerGroupList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
         .then(function (response) {

             if (response.data.length > 0) {
                 $scope.CustomerGroupList = response.data;
                 $scope.selectedCustomerGroup = $scope.CustomerGroupList[0].customer_group_id;
             }
         })
        .catch(function (response) {

        })
        .finally(function () {

        });

    }

    function GetCurrencyList() {
        $http.get(configurationService.basePath + "api/OrderApi/GetCurrencyList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {

              if (response.data.length > 0) {
                  $scope.CurrencyList = response.data;
                  $scope.selctedCurrency = $scope.CurrencyList[0].currency_id;
              }
          })
         .catch(function (response) {

         })
         .finally(function () {

         });
    }

    function GetCountryList() {
        $http.get(configurationService.basePath + "api/OrderApi/GetCountryList?countryId=0" + $scope.countryId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
         .then(function (response) {
             if (response.data.length > 0) {
                 $scope.CountryList = response.data;
                 $scope.selectedPaymentCountry = $scope.CountryList[0].country_id;
                 $scope.selectedShippingCountry = $scope.CountryList[0].country_id;
             }
         })
        .catch(function (response) {

        })
        .finally(function () {

        });
    }

    function GetCustomerAddress() {
        $http.get(configurationService.basePath + "api/OrderApi/GetCustomerAddress?orderId=" + $stateParams.orderId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
         .then(function (response) {
             if (response.data.length > 0) {
                 $scope.AddressListPayment = response.data;
                 $scope.AddressListShipping = response.data;
                 //$scope.selectedCountry = $scope.CountryList[0].country_id;
                 //$scope.GetZoneListByCountry($scope.selectedCountry);
             }
         })
        .catch(function (response) {

        })
        .finally(function () {

        });
    }

    $scope.GetCustomersByStore = function (storeId) {
        $http.get(configurationService.basePath + "api/OrderApi/GetCustomersByStore?storeId=" + storeId + '&LoggedInUserId=' + $scope.LoggedInUserId)
        .then(function (response) {
            if (response.data.length > 0) {

                $scope.CustomerList = response.data;
                $scope.selectedCustomer = $scope.CustomerList[0].customer_id;
                if ($scope.OrderDetails.customer_id != 0 || $scope.OrderDetails.customer_id != null) {
                    $scope.selectedCustomer = $scope.OrderDetails.customer_id;
                }
                //$scope.CountryList = response.data;
                //$scope.selectedPaymentCountry = $scope.CountryList[0].country_id;
                //$scope.selectedShippingCountry = $scope.CountryList[0].country_id;
            }
        })
       .catch(function (response) {

       })
       .finally(function () {

       });
    }

    FillDropDown();
    function FillDropDown() {

        GetStoreList();

        GetCustomerGroupList();

        GetCurrencyList();

        GetCountryList();

        GetCustomerAddress();

        GetOrderStatus();

        if ($stateParams.orderId != undefined && $stateParams.orderId != null) {
            getOrderDetails();
        }
    }

    function getOrderDetails() {
        $http.get(configurationService.basePath + "api/OrderApi/GetOrderDetails?orderId=" + $stateParams.orderId + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.OrderDetails = response.data[0];
                  $scope.OrderDetails.date_added = $filter('date')(response.data[0].date_added, $rootScope.GlobalDateFormat);
                  //$scope.OrderHistoryList = $scope.OrderDetails.orderHistoryEntity;
                  $scope.Products = $scope.OrderDetails.orderProductEntity;
                  //$scope.MainTotal = $scope.Products[0].title;
                  if ($scope.Products.length > 0) {
                      $scope.total_title = $scope.Products[0].total_title;
                      $scope.total_value = $scope.Products[0].total_value;
                      $scope.subtotal_title = $scope.Products[0].subtotal_title;
                      $scope.subtotal_value = $scope.Products[0].subtotal_value;
                  }
                  $scope.selectedPaymentCountry = ($scope.OrderDetails.payment_country_id == null || $scope.OrderDetails.payment_country_id == '') ? 0 : $scope.OrderDetails.payment_country_id;
                  $scope.selectedShippingCountry = ($scope.OrderDetails.shipping_country_id == null || $scope.OrderDetails.shipping_country_id == '') ? 0 : $scope.OrderDetails.shipping_country_id;
                  $scope.selectedPaymentAddressId = parseInt($scope.OrderDetails.address_id);
                  $scope.selectedAddressShippingId = parseInt($scope.OrderDetails.address_id);
                  $scope.GetZoneListByCountryPayment($scope.selectedPaymentCountry);
                  //$scope.GetZoneListByCountryShipping($scope.selectedShippingCountry);
                  $scope.selectedPaymentZone = $scope.OrderDetails.payment_zone_id == 0 ? '' : $scope.OrderDetails.payment_zone_id;
                  //$scope.selectedShippingZone = 0;
                  $scope.selectedCustomerGroup = $scope.OrderDetails.customer_group_id;
                  $scope.selectedStore = $scope.OrderDetails.store_id;
                  $scope.selectedOrderStatus = $scope.OrderDetails.order_status_id;
                  $scope.selctedCurrency = $scope.OrderDetails.currency_id;
                  $scope.GetCustomersByStore($scope.selectedStore);

              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.UpdateOrder_CutomerDetails = function (form) {
        if (form.$valid) {
            $scope.OrderDetails.currency_id = $scope.selctedCurrency;
            $scope.OrderDetails.customer_id = $scope.selectedCustomer;
            $scope.OrderDetails.customer_group_id = $scope.selectedCustomerGroup;
            $scope.OrderDetails.store_id = $scope.selectedStore;
            $http.post(configurationService.basePath + "api/OrderApi/UpdateOrder_CutomerDetails?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, $scope.OrderDetails)
           .then(function (response) {
               if (response.data == 1) {
                   notificationFactory.customSuccess("Customer Section Information Saved Successfully.");
               }
           })
             .catch(function (response) {

             })
             .finally(function () {

             });
        }
    }

    $scope.UpdateOrder_PaymentDetails = function (form) {
        if (form.$valid) {
            $scope.OrderDetails.address_id = $scope.selectedPaymentAddressId;
            $scope.OrderDetails.payment_country_id = $scope.selectedPaymentCountry;
            $scope.OrderDetails.payment_zone_id = $scope.selectedPaymentZone;
            $http.post(configurationService.basePath + "api/OrderApi/UpdateOrder_PaymentDetails?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, $scope.OrderDetails)
            .then(function (response) {
                if (response.data == 1) {
                    notificationFactory.customSuccess("Payment Section Information Saved Successfully.");
                }
            })
             .catch(function (response) {

             })
             .finally(function () {

             });
        }
    }

    $scope.UpdateOrder_ShippingDetails = function (form) {
        if (form.$valid) {
            $scope.OrderDetails.address_id = $scope.selectedAddressShippingId;
            $scope.OrderDetails.shipping_country_id = $scope.selectedShippingCountry;

            $http.post(configurationService.basePath + "api/OrderApi/UpdateOrder_ShippingDetails?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, $scope.OrderDetails)
             .then(function (response) {
                 if (response.data == 1) {
                     notificationFactory.customSuccess("Shipping Section Information Saved Successfully.");
                 }
             })
            .catch(function (response) {

            })
             .finally(function () {

             });
        }
    }

    $scope.UpdateOrder_TotalDetails = function () {
        $http.get(configurationService.basePath + "api/OrderApi/UpdateOrder_TotalDetails?order_id=" + $scope.OrderDetails.order_id + "&order_status_id=" + $scope.selectedOrderStatus + "&comment=" + $scope.OrderDetails.comment + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
         .then(function (response) {
             if (response.data == 1) {
                 notificationFactory.customSuccess("Information Saved Successfully.");
             }
         })
        .catch(function (response) {

        })
         .finally(function () {

         });
    }


    $scope.UpdateOrderDate = function (OrderDetailForm) {
        if (OrderDetailForm.$valid) {
            $http.post(configurationService.basePath + "api/OrderApi/UpdateOrderDate?OrderId=" + $scope.OrderDetails.order_id + '&OrderDate=' + $scope.OrderDetails.date_added)
           .then(function (response) {
               if (response.data > 0) {
                   toastr.success("Order Date updated successfully.");
                   getOrderDetails();
               }
           })
            .catch(function (response) {

            })
             .finally(function () {

             });
        }
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

});