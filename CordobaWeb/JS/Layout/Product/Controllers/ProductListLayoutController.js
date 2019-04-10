app.controller('ProductListLayoutController', function (StoreSessionDetail,UserDetail, $timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.StoreDetailInSession = StoreSessionDetail;
    //#endregion   
    if ($stateParams.CategoryId != undefined && $stateParams.CategoryId != null) {
        $scope.SelectedCategoryId = parseInt($stateParams.CategoryId);
    }
   

    $scope.GetCategoryListForDashboard = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetCategoryListByStoreId?StoreID="+$scope.StoreDetailInSession.store_id+"&NeedToGetAllSubcategory=true&customer_id=" + $rootScope.CustomerDetail.customer_id)
          .then(function (response) {     
              if (response.data.length > 0) {                  
                  $scope.CategoryList = response.data;               
                  var CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedCategoryId },true);
                  if(CategoryObj!=undefined && CategoryObj!=null )
                  {
                      $scope.SelectedCategory = CategoryObj[0];
                  }                 
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetProductListByCategoryAndStoreId= function () {
        $http.get(configurationService.basePath + "API/ProductApi/GetProductListByCategoryAndStoreId?StoreID=" + $scope.StoreDetailInSession.store_id + "&LoggedInUserId=" + UserDetail.customer_id + "&CategoryId=" + $scope.SelectedCategoryId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.ProductList = response.data;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetCategoryListForDashboard();
    $scope.GetProductListByCategoryAndStoreId();


});