app.controller('Dashboard_BannerController', function ($timeout, StoreSessionDetail, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreDetailInSession = StoreSessionDetail;
    //#endregion    

    $scope.GetBanner_Layout = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetBanner_Layout?StoreID=" + $scope.StoreDetailInSession.store_id)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.BannerList = response.data;
                }
            })
            .catch(function (response) {

            })
            .finally(function () {

            });
    }

    $scope.BannerCategoryLinkClick = function (objBanner) {
        var CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': objBanner.categoryId }, true);
        if (CategoryObj && CategoryObj.length > 0) {
            if (objBanner.parentCategoryId > 0) {
                var EncodededParentCategoryValue = Encodestring(objBanner.parentCategoryId);
                var EncodededChildCategoryValue = Encodestring(objBanner.categoryId);
                var pageIndex = Encodestring(1);
                $state.go('LayoutCategoryORProductList',
                    {
                        'CategoryId': EncodededParentCategoryValue,
                        'SubCategoryId': EncodededChildCategoryValue,
                        'PageIndex': pageIndex
                    });
            } else {
                var EncodededChildCategoryValue = Encodestring(objBanner.categoryId);
                $state.go('LayoutCategoryORProductList',
                    {
                        'CategoryId': EncodededChildCategoryValue,
                    });
            }
        }
    }
    $scope.GetCategoryListForDashboardBanner = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetCategoryListByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id + "&NeedToGetAllSubcategory=true&customer_id=" + $rootScope.CustomerDetail.customer_id)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.CategoryList = response.data;

                }
            })
            .catch(function (response) {

            })
            .finally(function () {

            });
    }

    $scope.GetCategoryListForDashboardBanner();
    $scope.GetBanner_Layout();

});