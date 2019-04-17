
'use strict';
var app = angular.module("CordobaApp", ["ui.router", "LocalStorageModule", "datatables", "ngFileUpload", "ngSanitize", 'ngAnimate', 'ngDragDrop', "textAngular", "uiSwitch", "ngCkeditor", "angular-star-rating", "ui.bootstrap", "textAngular"]);
GetAdminUserDetail();
GetLayoutName();



function GetAdminUserDetail() {
    var adminUserId = $("#hdAdminUserId").val();
    if (adminUserId != undefined && adminUserId != null) {
        $.ajax({
            url: window.location.origin + "/Home/GetAdminUserDetail",
            async: false,
            success: function (data) {
                app.value('AdminUserDetail', data);
            }
        });
    }
}

function GetLayoutName() {

    $.ajax({
        url: window.location.origin + "/Home/GetStoreDetail?URL=" + window.location.href,
        async: false,
        success: function (data) {
            app.value('StoreSessionDetail', data);
            var User = new Object();
            User.customer_id = 0;
            User.address_id = 0;
            User.cartgroup_id = 0;
            User.TotalItemAdded = 0;
            app.value('UserDetail', User);
            var LayoutName = data.template;

            app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
                var
                    Home = {
                        name: 'Home',
                        url: '/Home?Email:email',
                        templateUrl: 'Templates/' + LayoutName + '/Home/index.cshtml'
                    }
                    , ShowCountry = {
                        name: 'ShowCountry',
                        url: '/Catalog/ShowCountry',
                        templateUrl: 'Templates/' + LayoutName + '/Country/Index.html'
                    }
                    , ManageCountry = {
                        name: 'ManageCountry',
                        url: '/Catalog/ManageCountry?CountryId:countryId',
                        templateUrl: 'Templates/' + LayoutName + '/Country/AddOrUpdateCountry.html'
                    }
                    //, TestPage = {
                    //    name: 'TestPage',
                    //    url: '/TestPage',
                    //    templateUrl: 'Templates/TestPage/Index.html'
                    //}
                    , ShowCategory = {
                        name: 'ShowCategory',
                        url: '/Catalog/ShowCategory',
                        templateUrl: 'Templates/' + LayoutName + '/Category/Index.html'
                    }
                    , ManageCategory = {
                        name: 'ManageCategory',
                        url: '/Catalog/ManageCategory?CategoryId:categoryId',
                        templateUrl: 'Templates/' + LayoutName + '/Category/ManageCategory.html'
                    }
                    , PopularCategory = {
                        name: 'PopularCategory',
                        url: '/Category/PopularCategory',
                        templateUrl: 'Templates/' + LayoutName + '/Category/PopularCategory.html'
                    }
                    , ShowManufacturer = {
                        name: 'ShowManufacturer',
                        url: '/Catalog/ShowManufacturer',
                        templateUrl: 'Templates/' + LayoutName + '/Manufacturers/Index.html'
                    }
                    , ManageManufacturer = {
                        name: 'ManageManufacturer',
                        url: '/Catalog/ManageManufacturer?ManufacturerID:manufacturerID',
                        templateUrl: 'Templates/' + LayoutName + '/Manufacturers/ManageManufacturer.html'
                    }
                    , ShowProductCatalogue = {
                        name: 'ShowProductCatalogue',
                        url: '/Catalog/ShowProductCatalogue',
                        templateUrl: 'Templates/' + LayoutName + '/ProductCatalogue/Index.html'
                    }
                    , ManageProductCatalogue = {
                        name: 'ManageProductCatalogue',
                        url: '/Catalog/ManageProductCatalogue?CatalogueId:catalogueId',
                        templateUrl: 'Templates/' + LayoutName + '/ProductCatalogue/ManageProductCatalogue.html'
                    }
                    , ShowSupplier = {
                        name: 'ShowSupplier',
                        url: '/Catalog/ShowSupplier',
                        templateUrl: 'Templates/' + LayoutName + '/Supplier/Index.html'
                    }
                    , ManageSupplier = {
                        name: 'ManageSupplier',
                        url: '/Catalog/ManageSupplier?SupplierID:supplierID',
                        templateUrl: 'Templates/' + LayoutName + '/Supplier/ManageSupplier.html'
                    }
                    , ShowStore = {
                        name: 'ShowStore',
                        url: '/System/ShowStore',
                        templateUrl: 'Templates/' + LayoutName + '/Store/Index.html'
                    }
                    , ManageStore = {
                        name: 'ManageStore',
                        url: '/System/ManageStore?StoreID:storeID',
                        templateUrl: 'Templates/' + LayoutName + '/Store/ManageStore.html'
                    }
                    , Users = {
                        name: 'Users',
                        url: '/System/Users',
                        templateUrl: 'Templates/' + LayoutName + '/Users/Index.html'
                    }
                    , ManageUser = {
                        name: 'ManageUser',
                        url: '/System/ManageUser?UserID:userID',
                        templateUrl: 'Templates/' + LayoutName + '/Users/ManageUser.html'
                    }
                    , Banner = {
                        name: 'Banner',
                        url: '/System/Design/Banner',
                        templateUrl: 'Templates/' + LayoutName + '/Banner/Index.html'
                    }
                    , ManageBanner = {
                        name: 'ManageBanner',
                        url: '/System/Design/ManageBanner?BannerId:bannerId',
                        templateUrl: 'Templates/' + LayoutName + '/Banner/ManageBanner.html'
                    }
                    , OrderReport = {
                        name: 'OrderReport',
                        url: '/Reports/Sales/Order',
                        templateUrl: 'Templates/' + LayoutName + '/Report/OrderReport.html'
                    }
                    , ShippingReport = {
                        name: 'ShippingReport',
                        url: '/Reports/Shipping',
                        templateUrl: 'Templates/' + LayoutName + '/Report/ShippingReport.html'
                    }
                    , TransactionReport = {
                        name: 'TransactionReport',
                        url: '/Reports/Sales/TransactionReport',
                        templateUrl: 'Templates/' + LayoutName + '/Report/TransactionReport.html'
                    }
                    , TransactionItemReport = {
                        name: 'TransactionItemReport',
                        url: '/Reports/Sales/TransactionItemReport',
                        templateUrl: 'Templates/' + LayoutName + '/Report/TransactionItemReport.html'
                    }
                    , TransactionItemCategoryReport = {
                        name: 'TransactionItemCategoryReport',
                        url: '/Reports/Sales/TransactionItemCategoryReport',
                        templateUrl: 'Templates/' + LayoutName + '/Report/TransactionItemCategoryReport.html'
                    }
                    , Product = {
                        name: 'Product',
                        url: '/Catalog/Product?Quantity:quantity&Status:status',
                        templateUrl: 'Templates/' + LayoutName + '/Product/Index.html'
                    }
                    , ManageProduct = {
                        name: 'ManageProduct',
                        url: '/Catalog/ManageProduct?ProductId:productId',
                        templateUrl: 'Templates/' + LayoutName + '/Product/ManageProduct.html'
                    }

                    , ProductViewedReport = {
                        name: 'ProductViewedReport',
                        url: '/Reports/ProductViewedReport',
                        templateUrl: 'Templates/' + LayoutName + '/Report/ProductViewedReport.html'
                    }
                    , ProductPurchasedReport = {
                        name: 'ProductPurchasedReport',
                        url: '/Reports/ProductPurchasedReport',
                        templateUrl: 'Templates/' + LayoutName + '/Report/ProductPurchasedReport.html'
                    }

                    , ReturnReport = {
                        name: 'ReturnReport',
                        url: '/Reports/Return',
                        templateUrl: 'Templates/' + LayoutName + '/Report/ReturnReport.html'
                    }
                    , CouponsReport = {
                        name: 'CouponsReport',
                        url: '/Reports/Coupons',
                        templateUrl: 'Templates/' + LayoutName + '/Report/CouponsReport.html'
                    }
                    , CustomerOnlineReport = {
                        name: 'CustomerOnlineReport',
                        url: '/Reports/Customer/OnlineCustomer',
                        templateUrl: 'Templates/' + LayoutName + '/Report/OnlineCustomerReport.html'
                    }
                    , CustomerActivityReport = {
                        name: 'CustomerActivityReport',
                        url: '/Reports/Customer/CustomerActivity',
                        templateUrl: 'Templates/' + LayoutName + '/Report/CustomerActivityReport.html'
                    }
                    , CustomerOrderReport = {
                        name: 'CustomerOrderReport',
                        url: '/Reports/Customer/Order',
                        templateUrl: 'Templates/' + LayoutName + '/Report/ShowCustomerOrderReport.html'
                    }
                    , ManageCustomerOrder = {
                        name: 'ManageCustomerOrder',
                        url: '/Reports/Customer/ManageCustomerOrder?CustomerId:customerId',
                        templateUrl: 'Templates/' + LayoutName + '/Customer/ManageCustomer.html'
                    }

                    , ImportCatalogue = {
                        name: 'ImportCatalogue',
                        url: '/Tools/ImportCatalogue',
                        templateUrl: 'Templates/' + LayoutName + '/ImportCatalogue/Index.html'
                    }

                    , PointsImporter = {
                        name: 'PointsImporter',
                        url: '/Sales/PointsImporter',
                        templateUrl: 'Templates/' + LayoutName + '/PointsImporter/PointsImporter.html'
                    }

                    , ErrorLog = {
                        name: 'ErrorLog',
                        url: '/Tools/ErrorLog',
                        templateUrl: 'Templates/' + LayoutName + '/ErrorLog/Index.html'
                    }
                    , RewardPointsReport = {
                        name: 'RewardPointsReport',
                        url: '/Reports/RewardPoints',
                        templateUrl: 'Templates/' + LayoutName + '/Report/RewardPointsReport.html'
                    }
                    , Language = {
                        name: 'Language',
                        url: '/System/Language',
                        templateUrl: 'Templates/' + LayoutName + '/Language/Index.html'
                    }
                    , ManageLanguage = {
                        name: 'ManageLanguage',
                        url: '/System/ManageLanguage?LanguageId:languageId',
                        templateUrl: 'Templates/' + LayoutName + '/Language/ManageLanguage.html'
                    }

                    , Login = {
                        name: 'Login',
                        url: 'Login',
                        templateUrl: 'Views/' + LayoutName + '/Login.chtml'
                    }

                    , StoreDashboard = {
                        name: 'StoreDashboard',
                        url: '/StoreDashboard',
                        templateUrl: 'Templates/' + LayoutName + '/Home/StoreDashboard.html'
                    }
                    , Currency = {
                        name: 'Currency',
                        url: '/System/Currency',
                        templateUrl: 'Templates/' + LayoutName + '/Currency/Index.html'
                    }
                    , ManageCurrency = {
                        name: 'ManageCurrency',
                        url: '/System/ManageCurrency?CurrencyId:currencyId',
                        templateUrl: 'Templates/' + LayoutName + '/Currency/ManageCurrency.html'
                    }
                    , Customer = {
                        name: 'Customer',
                        url: '/System/Customer?CustomerApproved:customerApproved',
                        templateUrl: 'Templates/' + LayoutName + '/Customer/Index.html'

                    }
                    , ManageCustomer = {
                        name: 'ManageCustomer',
                        url: '/System/ManageCustomer?CustomerId=customerId',
                        templateUrl: 'Templates/' + LayoutName + '/Customer/ManageCustomer.html'
                    }

                    , CustomerGroup = {
                        name: 'CustomerGroup',
                        url: '/System/CustomerGroup',
                        templateUrl: 'Templates/' + LayoutName + '/CustomerGroup/Index.html'
                    }

                    , ManageCustomerGroup = {
                        name: 'ManageCustomerGroup',
                        url: '/System/ManageCustomerGroup?CustomerGroupId:customerGroupId',
                        templateUrl: 'Templates/' + LayoutName + '/CustomerGroup/ManageCustomerGroup.html'
                    }
                    , Orders = {
                        name: 'Orders',
                        url: '/Orders?OrderId:order_id',
                        templateUrl: 'Templates/' + LayoutName + '/Orders/Orders.html'
                    }
                    , ShowOrders = {
                        name: 'ShowOrders',
                        url: '/ShowOrders?OrderStatusId',
                        templateUrl: 'Templates/' + LayoutName + '/Orders/Index.html'
                    }
                    , ManageOrder = {
                        name: 'ManageOrder',
                        url: '/ManageOrders?orderId:order_id',
                        templateUrl: 'Templates/' + LayoutName + '/Orders/ManageOrder.html'
                    }
                    , LayoutCategoryORProductList = {
                        name: 'LayoutCategoryORProductList',
                        url: '/Category?CategoryId:categoryId&SubCategoryId:subcategory_id&Search:search&PageIndex:pageindex',
                        templateUrl: 'Templates/' + LayoutName + '/Category/Index.html'
                    }
                    //, LayoutProducts = {
                    //    name: 'LayoutProducts',
                    //    url: '/Products?CategoryId:categoryId',
                    //    templateUrl: 'Templates/' + LayoutName + '/Product/Index.html'
                    //}
                    , ShowReward = {
                        name: 'ShowReward',
                        url: '/Reward/ShowReward',
                        templateUrl: 'Templates/' + LayoutName + '/Reward/Index.html'
                    }
                    , ManageReward = {
                        name: 'ManageReward',
                        url: '/Reward/ManageReward?rewardId:reward_id',
                        templateUrl: 'Templates/' + LayoutName + '/Reward/AddOrUpdateReward.html'
                    }
                    , ViewReward = {
                        name: 'ViewReward',
                        url: '/Reward/ViewReward?rewardId:reward_id',
                        templateUrl: 'Templates/' + LayoutName + '/Reward/ViewCustomerRewards.html'
                    }
                    , ViewRewardCustomerDetails = {
                        name: 'ViewRewardCustomerDetails',
                        url: '/Reward/ViewReward/CustomerReward?rewarduserid:reward_user_id&name:customername',
                        templateUrl: 'Templates/' + LayoutName + '/Reward/CustomerRewardDetails.html'
                    }
                    , MyReward = {
                        name: 'MyReward',
                        url: '/MyReward',
                        templateUrl: 'Templates/' + LayoutName + '/Reward/MyRewards.html'
                    }
                    , CustomerRewardDetail = {
                        name: 'CustomerRewardDetail',
                        url: '/CustomerRewards?rewardId:reward_id&rewardname:reward_name',
                        templateUrl: 'Templates/' + LayoutName + '/Reward/ViewCustomerRewardDetails.html'
                    }
                    , AddReward = {
                        name: 'AddReward',
                        url: '/AddReward?rewardId:reward_id&type:reward_type_id',
                        templateUrl: 'Templates/' + LayoutName + '/Reward/AddReward.html'
                    }
                    , LayoutProductDetail = {
                        name: 'LayoutProductDetail',
                        url: '/ProductDetails?ProductId:product_id',
                        templateUrl: 'Templates/' + LayoutName + '/Product/ProductDetail.html'
                    }
                    , CartDetail = {
                        name: 'CartDetail',
                        url: '/CartDetail?cartgroup_id:CartGroupId',
                        templateUrl: 'Templates/' + LayoutName + '/Cart/CartDetail.html'
                    }
                    , CustomerImport = {
                        name: 'CustomerImport',
                        url: '/Customer/CustomerImport',
                        templateUrl: 'Templates/' + LayoutName + '/Customer/CustomerImport.html'
                    }
                    , Checkout = {
                        name: 'Checkout',
                        url: '/Checkout?cartgroup_id:CartGroupId',
                        templateUrl: 'Templates/' + LayoutName + '/Cart/Checkout.html'
                    }
                    , EditProfile = {
                        name: 'EditProfile',
                        url: '/EditProfile',
                        templateUrl: 'Templates/' + LayoutName + '/MyAccount/EditProfile.html'
                    }
                    , ChangePassword = {
                        name: 'ChangePassword',
                        url: '/ChangePassword',
                        templateUrl: 'Templates/' + LayoutName + '/MyAccount/ChangePassword.html'
                    }
                    , AddressBook = {
                        name: 'AddressBook',
                        url: '/AddressBook',
                        templateUrl: 'Templates/' + LayoutName + '/MyAccount/AddressBook.html'
                    }
                    , OrderHistory = {
                        name: 'OrderHistory',
                        url: '/OrderHistory',
                        templateUrl: 'Templates/' + LayoutName + '/MyAccount/OrderHistory.html'
                    }
                    , PointsAudit = {
                        name: 'PointsAudit',
                        url: '/PointsAudit',
                        templateUrl: 'Templates/' + LayoutName + '/MyAccount/PointsAudit.html'
                    }
                    , OrderSuccessful = {
                        name: 'OrderSuccessful',
                        url: '/OrderSuccessful?OrderId:orderid',
                        templateUrl: 'Templates/' + LayoutName + '/Cart/OrderSuccessful.html'
                    }
                    , OrderDetail = {
                        name: 'OrderDetail',
                        url: '/OrderDetail?OrderId:orderid',
                        templateUrl: 'Templates/' + LayoutName + '/Order/OrderDetail.html'
                    }
                    ,
                    ShowCustomerDepartment = {
                        name: 'ShowCustomerDepartment',
                        url: '/ShowCustomerDepartment',
                        templateUrl: 'Templates/' + LayoutName + '/CustomerDepartment/Index.html'
                    },
                    ManageCustomerDepartment = {
                        name: 'ManageCustomerDepartment',
                        url: '/ManageCustomerDepartment?CustomerDepartmentId',
                        templateUrl: 'Templates/' + LayoutName + '/CustomerDepartment/ManageCustomerDepartment.html'
                    },
                    ContactUs = {
                        name: 'ContactUs',
                        url: '/ContactUs',
                        templateUrl: 'Templates/' + LayoutName + '/ContactUs/index.html'
                    },
                    StoreReport = {
                        name: 'StoreReport',
                        url: '/StoreReport',
                        templateUrl: 'Templates/' + LayoutName + '/Report/StoreReport.html'
                    }
                    , StoreHTML = {
                        name: 'StoreHTML',
                        url: '/System/StoreHTML?StoreID:storeID',
                        templateUrl: 'Templates/' + LayoutName + '/Store/StoreHTML.html'
                    },
                    CustomerBalanceReport = {
                        name: 'CustomerBalanceReport',
                        url: '/Reports/CustomerBalanceReport',
                        templateUrl: 'Templates/' + LayoutName + '/Report/CustomerBalanceReport.html'
                    },
                    TermsOfUse = {
                        name: 'TermsOfUse',
                        url: '/TermsOfUse',
                        templateUrl: 'Templates/' + LayoutName + '/Footer/TermsOfUse.html'
                    },
                    WebsitePrivacyStatement = {
                        name: 'WebsitePrivacyStatement',
                        url: '/WebsitePrivacyStatement',
                        templateUrl: 'Templates/' + LayoutName + '/Footer/WebsitePrivacyStatement.html'
                    },
                    CookiePolicy = {
                        name: 'CookiePolicy',
                        url: '/CookiePolicy',
                        templateUrl: 'Templates/' + LayoutName + '/Footer/CookiePolicy.html'
                    };

                $stateProvider.state(StoreDashboard);
                $stateProvider.state(Home);
                $stateProvider.state(ShowCountry);
                $stateProvider.state(ManageCountry);
                $stateProvider.state(ShowCategory);
                $stateProvider.state(ManageCategory);
                $stateProvider.state(PopularCategory);
                $stateProvider.state(ShowManufacturer);
                $stateProvider.state(ManageManufacturer);
                $stateProvider.state(ShowSupplier);
                $stateProvider.state(ManageSupplier);
                $stateProvider.state(ShowStore);
                $stateProvider.state(ManageStore);
                $stateProvider.state(Users);
                $stateProvider.state(ManageUser);
                //$stateProvider.state(TestPage);

                $stateProvider.state(ShowProductCatalogue);
                $stateProvider.state(ManageProductCatalogue);

                $stateProvider.state(Banner);
                $stateProvider.state(ManageBanner);

                $stateProvider.state(OrderReport);
                $stateProvider.state(TransactionReport);
                $stateProvider.state(TransactionItemReport);
                $stateProvider.state(TransactionItemCategoryReport);
                $stateProvider.state(ShippingReport);
                $stateProvider.state(Product);
                $stateProvider.state(ManageProduct);

                $stateProvider.state(ReturnReport);
                $stateProvider.state(CouponsReport);
                $stateProvider.state(CustomerOnlineReport);
                $stateProvider.state(CustomerActivityReport);
                $stateProvider.state(CustomerOrderReport);
                $stateProvider.state(ManageCustomerOrder);
                $stateProvider.state(ProductViewedReport);
                $stateProvider.state(ProductPurchasedReport);
                $stateProvider.state(ImportCatalogue);
                $stateProvider.state(CustomerImport);

                $stateProvider.state(PointsImporter);

                $stateProvider.state(ErrorLog);
                $stateProvider.state(RewardPointsReport);

                $stateProvider.state(Language);
                $stateProvider.state(ManageLanguage);

                $stateProvider.state(Orders);
                $stateProvider.state(ShowOrders);
                $stateProvider.state(ManageOrder);

                $stateProvider.state(Login);

                $stateProvider.state(Currency);
                $stateProvider.state(ManageCurrency);
                $stateProvider.state(Customer);
                $stateProvider.state(ManageCustomer);
                $stateProvider.state(CustomerGroup);
                $stateProvider.state(ManageCustomerGroup);
                $stateProvider.state(LayoutCategoryORProductList);
                //$stateProvider.state(LayoutProducts);
                $stateProvider.state(LayoutProductDetail);

                $stateProvider.state(ShowReward);
                $stateProvider.state(ManageReward);
                $stateProvider.state(ViewReward);
                $stateProvider.state(ViewRewardCustomerDetails);


                //Customer Rewards
                $stateProvider.state(MyReward);
                $stateProvider.state(CustomerRewardDetail);
                $stateProvider.state(AddReward);

                $stateProvider.state(CartDetail);
                $stateProvider.state(Checkout);
                $stateProvider.state(EditProfile);
                $stateProvider.state(ChangePassword);
                $stateProvider.state(AddressBook);
                $stateProvider.state(OrderHistory);
                $stateProvider.state(PointsAudit);
                $stateProvider.state(OrderSuccessful);
                $stateProvider.state(OrderDetail);
                $stateProvider.state(ShowCustomerDepartment);
                $stateProvider.state(ManageCustomerDepartment);
                $stateProvider.state(ContactUs);
                $stateProvider.state(StoreReport);
                $stateProvider.state(StoreHTML);
                $stateProvider.state(CustomerBalanceReport);

                $stateProvider.state(TermsOfUse);
                $stateProvider.state(WebsitePrivacyStatement);
                $stateProvider.state(CookiePolicy);


                //any url that doesn't exist in routes redirect to '/'
                $urlRouterProvider.otherwise('/Home');
                //$locationProvider.html5Mode({
                //    enabled: true,
                //    requireBase: true
                //});

            })
                .run(function ($http, $rootScope, StoreSessionDetail, $location, UserDetail, $filter, $state, localStorageService, $templateCache) {
                    var user = localStorageService.get("loggedInUser");
                    if (user == null || user == undefined) {
                        user = new Object();
                        user.customer_id = 0;
                    }
                    if (user.customer_id > 0) {

                        UserDetail.customer_id = user.customer_id;
                        UserDetail.firstname = user.firstname;
                        UserDetail.lastname = user.lastname;
                        UserDetail.points = user.points;
                        UserDetail.address_id = user.address_id;
                        UserDetail.cartgroup_id = user.cartgroup_id;
                        UserDetail.TotalItemAdded = user.TotalItemAdded;
                        $rootScope.CustomerDetail = UserDetail;
                    }
                    else {
                        localStorageService.set("loggedInUser", user);
                        $rootScope.CustomerDetail = user;
                    }

                    $rootScope.storeName = StoreSessionDetail.name;
                    $rootScope.GlobalDateFormat = 'dd/MM/yyyy';

                    //var now1 = new Date();
                    //var GETLocalStorageDateTime = localStorageService.get("CurrentDateTime");
                    //var diff = (now1.getTime() - new Date(GETLocalStorageDateTime).getTime());

                    //if (diff > 10000) {

                    //    localStorageService.clearAll();
                    //    localStorageService.set("CurrentDateTime", now1);
                    //    if (document.location.origin.toLowerCase() != document.location.href.toString().toLowerCase()) {

                    //        location.href = document.location.origin;
                    //    }
                    //    //localStorageService.remove("loggedInUser");
                    //    //localStorageService.remove("ImpersonatedUserDetail");
                    //    //localStorageService.remove("NotificationMesseges");
                    //    //localStorageService.remove("userPermissions");
                    //    //localStorageService.remove("CurrentDateTime");

                    //    //if (localStorageService != undefined) {
                    //    //    for (var i = 0; i < localStorageService.keys(0).length; i++) {

                    //    //        if (localStorageService.keys(0)[i].split('-')[0] == 'pageName') {

                    //    //            localStorageService.remove(localStorageService.keys(0)[i]);
                    //    //        }
                    //    //    }
                    //    //    var now = new Date();
                    //    //    localStorageService.set("CurrentDateTime", now);

                    //    //}


                    //    //if (!document.location.origin &&  document.location.href.toString().toLowerCase().indexOf('home') == -1) {

                    //    //    location.href = document.location.origin;
                    //    //}



                    //}


                    //localStorageService.remove("NotificationMesseges");
                    //localStorageService.remove("userPermissions");
                    $rootScope.$state = $state;

                    //function InitGlobalVariables() {

                    //    $rootScope.CurrentUser = function () {
                    //        var user = localStorageService.get("loggedInUser");
                    //        if (user != null) {
                    //            return user;
                    //        }
                    //        return null;
                    //    };

                    //    $rootScope.CurrentUserInternalId = function () {
                    //        var user = $rootScope.CurrentUser();
                    //        if (user != null) {
                    //            return user.InternalId;
                    //        }
                    //        return 0;
                    //    };

                    //    $rootScope.CurrentUserIsAdmin = function () {
                    //        var user = $rootScope.CurrentUser();
                    //        if (user != null) { return user.IsAdmin; }
                    //        return false;
                    //    };

                    //}


                    //InitGlobalVariables();
                    //InitGloablEnums();

                    //$rootScope.isSubModuleAccessibleToUser = function (subModule, func) {
                    //    var permissions = [];
                    //     permissions = localStorageService.get("userPermissions");
                    //    if (subModule == 'default') {
                    //        return true;
                    //    }
                    //    if (permissions == null) {
                    //        var userPermissions = permissionService.GetUserPermissions(user.InternalId)

                    //        userPermissions.success(function (permissions) {
                    //            localStorageService.set("userPermissions", permissions);
                    //            permissions = localStorageService.get("userPermissions");                
                    //            return false;
                    //        });
                    //    }   
                    //    if (permissions != undefined && permissions != null)
                    //            {
                    //           if (permissions.length) {
                    //           return $filter('filter')(permissions, { SubModule: subModule, Function: func }, true).length >= 1;
                    //         }
                    //     }

                    //    else {
                    //        return true;
                    //    }

                    //}
                    $rootScope.RemoveAllFromLocalStorage_StartWith = function (startWith) {

                        var CmpStrLength = 0;

                        if (startWith == null || startWith === undefined || startWith == "") {
                            CmpStrLength = 0;
                        } else {
                            CmpStrLength = startWith.length;
                        }
                        if (CmpStrLength > 0) {
                            if (localStorage.length > 0) {
                                var arr = []; // Array to hold the keys
                                // Iterate over localStorage and insert the keys that meet the condition into arr
                                for (var i = 0; i < localStorage.length; i++) {
                                    //if (localStorage.key(i).substring(0, 3) == startWith) {
                                    if (localStorage.key(i).substring(3, localStorage.key(i).length).split('_')[0] + '_' + localStorage.key(i).split('_')[1] == startWith) {
                                        arr.push(localStorage.key(i));
                                    }
                                }

                                // Iterate over arr and remove the items by key
                                for (var i = 0; i < arr.length; i++) {
                                    localStorage.removeItem(arr[i]);
                                }
                            }
                        }

                    }

                    $rootScope.$on('$locationChangeStart', function (event, next, current) {

                        if (StoreSessionDetail.Is_AccessStore != null && StoreSessionDetail.Is_AccessStore == true && !$rootScope.CustomerDetail.customer_id > 0) {

                            $rootScope.OpenLoginPopUpUsingRootScope();
                        }
                        //if (!$rootScope.CustomerDetail.customer_id > 0) {
                        //    $rootScope.OpenLoginPopUpUsingRootScope();
                        //}
                        var geturlparameters = next.toString().split('?')[1];
                        var isAlreadyDecoded = false;
                        if (geturlparameters != undefined) {
                            if (geturlparameters.indexOf('~') == -1)
                                isAlreadyDecoded = true;
                        }
                        if (isAlreadyDecoded) {
                            //window.location.href = 'home/accessdenied';
                        }

                    });
                    // Redirect to login if route requires auth and you're not logged in
                    $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {

                        $state.previous = fromState;
                        $state.previousParams = fromParams;
                        if (fromState.name != "") {
                            localStorageService.remove("previous");
                            localStorageService.set("previous", $state.previous);
                            localStorageService.remove("previousParams");
                            localStorageService.set("previousParams", $state.previousParams);
                        }
                        if (!(fromParams == toParams)) {
                            encodeParams(toParams);

                        }
                        // to track previous url
                        $state.previousHref = $state.href(fromState, fromParams)

                        //check for user authentication         


                        // check for authorization
                        //if (localStorageService.get("userPermissions") != null) {

                        //    var hasAccess = $filter('filter')(localStorageService.get("userPermissions"),
                        //        { SubModule: toState.submodule, Function: toState.submoduleFunction }, true).length >= 1;
                        //    if (toState.submodule == 'default') {
                        //        hasAccess = true;
                        //    }
                        //    if (!hasAccess) {
                        //        window.location.href = 'home/accessdenied';
                        //    }
                        //}
                        //else {
                        //    var user = $rootScope.CurrentUser();

                        //    if (user != null) {

                        //        var userPermissions = permissionService.GetUserPermissions(user.InternalId)

                        //        userPermissions.success(function (permissions) {
                        //            localStorageService.set("userPermissions", permissions);

                        //            var hasAccess = $filter('filter')(localStorageService.get("userPermissions"),
                        //        { SubModule: toState.submodule, Function: toState.submoduleFunction }, true).length >= 1;

                        //            if (toState.submodule == 'default') {
                        //                hasAccess = true;
                        //            }
                        //            if (!hasAccess) {
                        //                window.location.href = 'home/accessdenied';
                        //            }

                        //        });
                        //    }
                        //}

                        // check for user authentication
                        //if (localStorageService.get('AppConfigurationStoreId', data.store_id) != data.store_Id) {
                        //    window.location.href = 'home/accessdenied';
                        //}

                    });
                });

        }
    });
}


app.factory("ShareData", function () {
    return { value: 0 };
});

app.config(function (localStorageServiceProvider) {
    localStorageServiceProvider
        .setStorageType('localStorage');

});

app.constant('OrderStatusEnum',
    {
        Processing: 1,
        Shipped: 2,
        Completed: 2,
        PartiallyShipped: 3,
        Returned: 4,
        Cancelled: 5,
        Delivered: 6
    });

//var allowCrossDomain = function (req, res, next) {
//    res.header('Access-Control-Allow-Origin', '*');
//    res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
//    res.header('Access-Control-Allow-Headers', 'X-Requested-With,content-type');

//    // Pass to next layer of middleware
//    next();

//}
//app.use(allowCrossDomain);
