app.controller('CategoryListController', function (StoreSessionDetail, UserDetail, $timeout, $state, $http, $location, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, $http, $log, $q) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    $scope.SelectedCategoryId = 0;
    $scope.SelectedSubCategory = 0;
    $scope.StoreDetailInSession = StoreSessionDetail;
    $scope.TitleHeader = '';
    $scope.WhatAreYouLookingFor = '';
    $scope.SearchByFilterId = '';
    $scope.SelectedPageIndex = 1;
    $scope.maxSize = 10;
    $scope.SortById = 1;

    $scope.SortByEnum = [
{ Id: 1, Name: 'High-Low' },
{ Id: 2, Name: 'Low-High' },
{ Id: 3, Name: 'A-Z' },
{ Id: 4, Name: 'Z-A' },
    ];

    $scope.SearchByPointsEnum = [
        { Id: 1, Name: '1 to 24', Checked: false },
        { Id: 2, Name: '25 to 49', Checked: false },
        { Id: 3, Name: '50 to 99', Checked: false },
        { Id: 4, Name: '100 to 199', Checked: false },
        { Id: 5, Name: '200 to 499', Checked: false },
        { Id: 6, Name: '500 to 999', Checked: false },
        { Id: 7, Name: '1000 to 1999', Checked: false },
        { Id: 8, Name: 'Above 2000', Checked: false },
    ];




    //#endregion   
    if ($stateParams.CategoryId != undefined && $stateParams.CategoryId != null) {
        $scope.SelectedCategoryId = parseInt($stateParams.CategoryId);
        if ($stateParams.SubCategoryId != undefined && $stateParams.SubCategoryId != null) {
            $scope.SelectedSubCategory = parseInt($stateParams.SubCategoryId);
        }
        if ($stateParams.Search != undefined && $stateParams.Search != null) {
            $scope.WhatAreYouLookingFor = $stateParams.Search;
        }
        if ($stateParams.PageIndex != undefined && $stateParams.PageIndex != null) {
            $scope.SelectedPageIndex = parseInt($stateParams.PageIndex);
            $scope.currentPage = $scope.SelectedPageIndex;
        }
        //else {
        //    $scope.currentPage = 1;
        //}

    }
    $scope.GetCategoryListForDashboard = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetCategoryListByStoreId?StoreID=" + $scope.StoreDetailInSession.store_id + "&NeedToGetAllSubcategory=true&customer_id=" + $rootScope.CustomerDetail.customer_id)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.CategoryList = response.data;
                  var CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedCategoryId }, true);
                  if (CategoryObj != undefined && CategoryObj != null) {
                      $scope.SelectedCategory = CategoryObj[0];
                      if ($scope.SelectedSubCategory != undefined && $scope.SelectedSubCategory != null && $scope.SelectedSubCategory != 0) {
                          var SubCategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedSubCategory }, true);
                          if (SubCategoryObj != undefined && SubCategoryObj != null) {
                              $scope.GetSubCategory($scope.SelectedSubCategory, 0);
                          }
                      }
                      else {
                          var SubCategoryObj = $filter('filter')($scope.CategoryList, { 'parent_Id': $scope.SelectedCategoryId }, true);
                          if ((CategoryObj != undefined && CategoryObj != null && CategoryObj.length > 0) && (SubCategoryObj != undefined && SubCategoryObj != null && SubCategoryObj.length > 0)) {
                              $scope.SelectedCategory = CategoryObj[0];
                              $scope.TitleHeader = $scope.SelectedCategory.name;
                          }
                          else {
                              $scope.SelectedSubCategory = $scope.SelectedCategoryId;
                              $scope.GetProductListByCategoryAndStoreId();
                          }

                      }

                  }
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.GetCategory = function (ParentCategoryId) {
        var EncodededParentCategoryValue = Encodestring(ParentCategoryId);
        $scope.SelectedCategoryId = ParentCategoryId;
        $state.go('.', { CategoryId: EncodededParentCategoryValue }, { notify: false, reload: false, location: 'replace', inherit: false });
        $scope.SelectedSubCategory = 0;
        var CategoryObj = [];
        var SubCategoryObj = [];
        CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedCategoryId }, true);
        SubCategoryObj = $filter('filter')($scope.CategoryList, { 'parent_Id': $scope.SelectedCategoryId }, true);
        if ((CategoryObj != undefined && CategoryObj != null && CategoryObj.length > 0) && (SubCategoryObj != undefined && SubCategoryObj != null && SubCategoryObj.length > 0)) {
            $scope.SelectedCategory = CategoryObj[0];
            $scope.TitleHeader = $scope.SelectedCategory.name;
        }
        else {
            $scope.SelectedCategory = CategoryObj[0];
            $scope.TitleHeader = $scope.SelectedCategory.name;
            $scope.SelectedSubCategory = ParentCategoryId;
            $scope.GetProductListByCategoryAndStoreId();
        }
    }

    $scope.GetSubCategory = function (SubCategoryId, PageIndex) {
        var EncodededParentCategoryValue = Encodestring($scope.SelectedCategoryId);
        var EncodededChildCategoryValue = Encodestring(SubCategoryId);

        if (PageIndex > 0) {
            var pageIndex = Encodestring(PageIndex);
            $scope.SelectedPageIndex = PageIndex;
        }
        else {
            var pageIndex = Encodestring($scope.SelectedPageIndex);
        }

        $scope.SelectedSubCategory = SubCategoryId;
        $state.go('.', { CategoryId: EncodededParentCategoryValue, SubCategoryId: EncodededChildCategoryValue, PageIndex: pageIndex }, { notify: false, reload: false, location: 'replace', inherit: true });
        var CategoryObj = $filter('filter')($scope.CategoryList, { 'Category_Id': $scope.SelectedSubCategory }, true);
        if (CategoryObj != undefined && CategoryObj != null && CategoryObj.length > 0) {
            $scope.SelectedCategory = CategoryObj[0];
            $scope.TitleHeader = $scope.SelectedCategory.name;
        }
        $scope.GetProductListByCategoryAndStoreId();
    }


    $scope.GetProductListByCategoryAndStoreId = function () {
        $http.get(configurationService.basePath + "API/ProductApi/GetProductListByCategoryAndStoreId?StoreID="
                            + $scope.StoreDetailInSession.store_id +
                            "&CategoryId=" + $scope.SelectedSubCategory +
                             "&PageIndex=" + $scope.SelectedPageIndex +
                             "&Customer_Id=" + UserDetail.customer_id +
                            "&WhatAreYouLookingFor=" + $scope.WhatAreYouLookingFor +
                            "&SearchByFilterId=" + $scope.SearchByFilterId +
                            "&OrderById=" + $scope.SortById
                            )
          .then(function (response) {
              $scope.ProductList = response.data;
              if ($scope.ProductList.length > 0) {
                  $scope.totalRecords = $scope.ProductList[0].TotalRecords;
              }
              else {
                  $scope.totalRecords = 0;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }


    $scope.GetOurProductListByByStoreId = function () {
        $scope.GetProductListByCategoryAndStoreId();
    }

    $scope.RemoveFromWishList = function (productObj) {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/RemoveFromWishList?StoreID=" + $scope.StoreDetailInSession.store_id + "&product_id=" + productObj.product_id + "&Customer_Id=" + UserDetail.customer_id)
         .then(function (response) {
             if (response.data > 0) {
                 toastr.success('Item removed from wishlist.');
             }
             else {
                 toastr.error('Something wrong! Please try again later.');
             }
             $scope.GetOurProductListByByStoreId();

         })
     .catch(function (response) {

     })
     .finally(function () {

     });
    }

    $scope.AddtoWishList = function (productObj) {
        if (UserDetail.customer_id > 0) {
            var WishObj = new Object();
            WishObj.customer_id = UserDetail.customer_id;
            WishObj.product_id = productObj.product_id;
            WishObj.store_id = $scope.StoreDetailInSession.store_id;


            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/AddtoWishList", WishObj)
                  .then(function (response) {
                      if (response.data == -1) {
                          toastr.warning('Item already present in wish list.');
                      }
                      else if (response.data > 0) {
                          toastr.success('Item successfully added in wish list.');

                      }
                      $scope.GetOurProductListByByStoreId();
                  })
              .catch(function (response) {

              })
              .finally(function () {

              });
        }
        else {
            $scope.OpenLoginPopUp();
        }

    }
    $scope.GoToPage = function (Index) {
        $scope.SelectedPageIndex = Index;
        $scope.currentPage = Index;
        var EncodededParentCategoryValue = Encodestring($scope.SelectedCategoryId);
        var EncodededChildCategoryValue = Encodestring($scope.SelectedSubCategory);
        var pageIndex = Encodestring(Index);
        $state.go('.', { CategoryId: EncodededParentCategoryValue, SubCategoryId: EncodededChildCategoryValue, PageIndex: pageIndex }, { notify: false, reload: false, location: 'replace', inherit: true });
        $scope.GetProductListByCategoryAndStoreId();
    }


    if ($scope.SelectedCategoryId == -1) {
        $scope.SelectedSubCategory = -1;
        $scope.TitleHeader = 'Our Products';
        $scope.GetOurProductListByByStoreId();
    }
    else if ($scope.SelectedCategoryId == -2) {
        $scope.SelectedSubCategory = -2;
        $scope.TitleHeader = 'My WishList';
        $scope.GetOurProductListByByStoreId();
    }
    else if ($scope.SelectedCategoryId == -3) {
        $scope.SelectedSubCategory = -3;
        $scope.TitleHeader = 'Search Result';
        $scope.GetOurProductListByByStoreId();
    }

    $scope.set = function () {
        $scope.totalRecords = 100000000;
    }

    $scope.SearchFilterByArray = function () {
        var SearchByFilterId = "";
        angular.forEach($scope.SearchByPointsEnum, function (dataItem) {
            if (dataItem.Checked) {
                SearchByFilterId += dataItem.Id + ",";
            }
        });
        $scope.SearchByFilterId = SearchByFilterId.slice(0, -1);
        $scope.SelectedPageIndex = 1;
        $scope.GetOurProductListByByStoreId();
    }

    $scope.SortBy = function (SortById) {
        $scope.SortById = SortById;
        $scope.GetOurProductListByByStoreId();
    }

    $scope.GetCategoryListForDashboard();
});