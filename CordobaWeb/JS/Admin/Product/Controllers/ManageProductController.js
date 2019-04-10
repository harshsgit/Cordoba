app.controller('ManageProductController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;
    $scope.ProductStoresList = [];
    $scope.selectedBestSellerStore = [];
    function Init() {
        $scope.ProductObjSubtract = [{ ID: 1, Name: 'Yes' }, { ID: 0, Name: 'No' }];
        $scope.ProductStatus = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];
        $scope.HotSpecialProductStatus = [{ ID: 1, Name: 'Enabled' }, { ID: 0, Name: 'Disabled' }];
        $scope.ProductObjStock_Status = [{ ID: 6, Name: '2-3 Days' }, { ID: 7, Name: 'In Stock' }, { ID: 5, Name: 'Out Of Stock' }, { ID: 8, Name: 'Pre-Order' }];
        $scope.IsEditMode = false;
        $scope.product_id = 0;
        $scope.ProductObj = new Object();
        $scope.CurrentDate = new Date();

        createDatePicker();
        $scope.EnumLanguageList = [
            { 'LangId': 1, 'LangName': 'English' }
          , { 'LangId': 2, 'LangName': 'Deutsch' }
           , { 'LangId': 3, 'LangName': 'Français' }
            , { 'LangId': 4, 'LangName': 'Italiano' }
             , { 'LangId': 5, 'LangName': 'Español' }
              , { 'LangId': 6, 'LangName': 'Português' }
               , { 'LangId': 7, 'LangName': 'Nederlands' }
        ];

        if ($stateParams.ProductId != undefined && $stateParams.ProductId != null) {
            $scope.PageTitle = "Update Product";
            $scope.product_id = $stateParams.ProductId;
            $scope.IsEditMode = true;
        }
        else {
            $scope.PageTitle = "Add Product";

        }
        GetLanguageList();
        GetManufacturersList();
        GetCategoryList();
        GetSupplierList();
        $scope.GetProductById();
        $scope.GetHotOrSpecialProductById();
        $scope.NeedToShowAddHotBtn = 0;
        $scope.NeedToShowSpecialAddBtn = 0;    
    }

    //#region Image Tab
    $scope.AddImage = function () {
        var data = new FormData();
        var files = $("#Image").get(0).files;
        if (files.length == 0) {
            notificationFactory.customError("Please select atleast one file.");
            return notificationFactory;
        }

        var filename = files[0].name;

        if (files.length > 0) {
            data.append("UploadedFile", files[0]);
        }

        var ajaxRequest = $.ajax({
            type: "POST",
            url: configurationService.basePath + 'api/ProductApi/UploadProductImage?product_id=' + $scope.ProductObj.product_id,
            contentType: false,
            processData: false,
            data: data,
            success: function (response) {
                notificationFactory.customSuccess("Product Image Upload Successfully.");
                $scope.GetProductImageById();
                $('#Image').val('');
                //$scope.GetBannerImageById();
            },
            error: function (response) {
                notificationFactory.error("Error occur during image upload.");
            }
        });


    }
    $scope.RemoveImage = function (event, item) {
        if (item.Id != undefined && item.Id != null && item.Id != 0) {
            RemoveBannerImage(item);
        }
        else {
            $scope.ProductObj.ImageList.pop(item);
        }
    }
    //#endregion


    $scope.DeleteProduct = function () {
        
        bootbox.dialog({
            message: "Do you want remove Product?",
            title: "Confirmation",
            className: "model",
            buttons: {
                success:
                    {
                        label: "Yes",
                        className: "btn btn-primary theme-btn",
                        callback: function (result) {
                            if (result) {
                                $http.post(configurationService.basePath + "api/ProductApi/DeleteProduct?product_id=" + $scope.product_id + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
                               .then(function (response) {
                                   if (response.data > 0)
                                       notificationFactory.successDelete();
                                   else {
                                       notificationFactory.FKReferenceDelete();
                                   }
                                   $state.go('Product');
                               })
                               .catch(function (response) {
                                   notificationFactory.errorDelete(response.data.ExceptionMessage);
                               })
                               .finally(function () {
                               });
                            }
                        }
                    },
                danger:
                    {
                        label: "No",
                        className: "btn btn-default",
                        callback: function () {
                            return true;
                        }
                    }
            }
        });
    };

    $scope.GetProductById = function () {
        $http.get(configurationService.basePath + "api/ProductApi/GetProductById?product_id=" + $scope.product_id + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {             
              $scope.ProductObj = response.data;
              $scope.ProductObj.date_available = $filter('date')(response.data.date_available, $rootScope.GlobalDateFormat);
              CreateDescriptionObject();
              if ($scope.ProductObj.product_id == 0) {
                  // Default Values
                  $scope.ProductObj.manufacturer_id = 0;
                  $scope.ProductObj.supplier_id = 0;
                  $scope.ProductObj.country_id = 222   // country_id  -United Kingdom
                  $scope.ProductObj.Quantity = 1;
                  $scope.ProductObj.minimum = 1;
                  $scope.ProductObj.minimum = 1;
                  $scope.ProductObj.subtract = 1;
                  $scope.ProductObj.stock_status_id = 6;
                  $scope.ProductObj.shipping = 1;
                  $scope.ProductObj.date_available = $filter('date')($scope.CurrentDate, $rootScope.GlobalDateFormat);
                  //$('#startdate').bootstrapDP('update', $scope.RewardObj.start_date);
                  $scope.ProductObj.shipping = 1;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    function CreateDescriptionObject() {
        var TempDescObject = [];
        angular.copy($scope.ProductObj.ProductDescriptionList, TempDescObject);
        $scope.ProductObj.ProductDescriptionList = [];
        angular.forEach($scope.LanguageList, function (col, i) {
            var ProductDescObj = $filter('filter')(TempDescObject, { language_id: col.language_id }, true);
            if (ProductDescObj == undefined || ProductDescObj == null || ProductDescObj.length == 0) {
                var DescObj = new Object();
                DescObj.language_id = col.language_id;
                DescObj.name = "";
                DescObj.description = "";
                DescObj.tag = "";
                $scope.ProductObj.ProductDescriptionList.push(DescObj);
            }
            else {
                $scope.ProductObj.ProductDescriptionList.push(ProductDescObj[0]);
            }
        });        
    }

    $scope.Cancel = function () {
        var hasAnyUnsavedData = false;
        hasAnyUnsavedData = (($scope.form != null && $("#form .ng-dirty").length > 0));
        if (hasAnyUnsavedData) {
            bootbox.confirm("You have unsaved data. Are you sure to leave page.", function (result) {
                if (result) {
                    $state.go('Product');
                }
            });
        }
        else {
            $state.go('Product');
        }
    }

    function GetLanguageList() {
        $http.get(configurationService.basePath + "api/LanguageApi/GetLanguageList?languageId=0" + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
        .then(function (response) {
            if (response.data.length > 0) {
                $scope.LanguageList = response.data;
            }
        })
        .catch(function (response) {

        })
        .finally(function () {

        });
    }

    function GetManufacturersList() {
        $http.get(configurationService.basePath + "api/ManufacturersApi/GetManufacturersList?ManufacturersID=0" + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.ManufacturersList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.manufacturer_id = 0;
                  DefaultOption.name = " --- None --- ";
                  $scope.ManufacturersList.push(DefaultOption);
              }
          })
      .catch(function (response) {
      })
      .finally(function () {

      });
    }

    function GetCategoryList() {
        $http.get(configurationService.basePath + "api/ProductApi/GetSubCategoryList?&StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
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

    function GetSupplierList() {
        $http.get(configurationService.basePath + "api/SupplierApi/GetSupplierList?SupplierID=0" + '&StoreId=' + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.SupplierList = response.data;
                  var DefaultOption = new Object()
                  DefaultOption.supplier_id = 0;
                  DefaultOption.name = " --- None --- ";
                  $scope.SupplierList.push(DefaultOption);
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    function GetSelectedCatalogueListCSV(CatalogueObj) {
        var CatalogueIdCSV = "";
        var SelectedCatalogueList = $filter('filter')(CatalogueObj, { IsSelected: true }, true);
        CatalogueIdCSV = GetCSVFromJsonArray(SelectedCatalogueList, "catalogue_Id");
        return CatalogueIdCSV;
    }

    $scope.InsertUpdateProduct = function (form) {
        if (form.$valid) {            
            $scope.ProductObj.CatalogueIdCSV = "";
            $scope.ProductObj.CatalogueIdCSV = GetSelectedCatalogueListCSV($scope.ProductObj.CatalogueList);
            if ($scope.selectedBestSellerStore && $scope.selectedBestSellerStore.length > 0) {
                $scope.ProductObj.StoreIds = $scope.selectedBestSellerStore.toString();
            }
            var productEntity = JSON.stringify($scope.ProductObj);
            $http.post(configurationService.basePath + "api/ProductApi/InsertUpdateProduct?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, productEntity)
              .then(function (response) {            
                  if (response.data > 0) {
                      if ($scope.product_id>0)
                      {
                          notificationFactory.customSuccess("Product Saved Successfully.");                          
                          $state.go('Product');
                      }
                      else {
                          notificationFactory.customSuccess("Product Saved Successfully.");
                          $state.go('ManageProduct', ({ 'ProductId': response.data }));
                      }
                   
                  }
                  else if (response.data == -1) {
                      notificationFactory.customError("Product name is already Exists!");
                  }
              })
          .catch(function (response) {
              notificationFactory.error("Error occur during save record.");
          })
          .finally(function () {

          });

        }
    }


    $scope.InsertAsHotOrSpecialProduct = function (form) {       
        $scope.form.HotOrSpecialStart.$setValidity("required", !($scope.HotOrSpecialProductObj.startDate == null || $scope.HotOrSpecialProductObj.startDate == undefined));
        $scope.form.HotOrSpecialEnd.$setValidity("required", !($scope.HotOrSpecialProductObj.endDate == null || $scope.HotOrSpecialProductObj.endDate == undefined));

        if (form.$valid) {

            if ($scope.HotOrSpecialProductObj.endDate < $scope.HotOrSpecialProductObj.startDate)
            {
                toastr.error("End Date should be greater or equal Start date.");
                return false;
            }
            $scope.HotOrSpecialProductObj.CatalogueIdCSV = "";
            $scope.HotOrSpecialProductObj.CatalogueIdCSV = GetSelectedCatalogueListCSV($scope.ProductObj.CatalogueList);    
            $scope.HotOrSpecialProductObj.product_id = $scope.product_id;
            $scope.HotOrSpecialProductObj.created_by = $scope.LoggedInUserId;
            var hotSpecialProductEntity = JSON.stringify($scope.HotOrSpecialProductObj);
            if($scope.HotOrSpecialProductObj.IsHotProduct==true)
            {            
                $http.post(configurationService.basePath + "api/ProductApi/InsertAsHotProduct?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, hotSpecialProductEntity)
                  .then(function (response) {
                      if (response.data > 0) {
                          notificationFactory.customSuccess("Product Saved Successfully.");
                          $scope.form.HotOrSpecialStart.$setValidity("required", true);
                          $scope.form.HotOrSpecialEnd.$setValidity("required", true);
                          $scope.HotOrSpecialProductObj = new Object();
                          $scope.NeedtoShowHot_SpeacialContainer = 0;
                          form.$valid = true;
                          $scope.GetHotOrSpecialProductById();                          
                      }
                      else if (response.data == -1) {
                          notificationFactory.customError("Product name is already Exists!");
                      }
                  })
              .catch(function (response) {
                  notificationFactory.error("Error occur during save record.");
              })
              .finally(function () {

              });
            }
            else {
                $http.post(configurationService.basePath + "api/ProductApi/InsertAsSpecialProduct?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId, hotSpecialProductEntity)
                            .then(function (response) {
                                if (response.data > 0) {
                                    notificationFactory.customSuccess("Product Saved Successfully.");
                                    $scope.HotOrSpecialProductObj = new Object();
                                    $scope.NeedtoShowHot_SpeacialContainer = 0;                                
                                    $scope.GetHotOrSpecialProductById();
                                }
                                else if (response.data == -1) {
                                    notificationFactory.customError("Product name is already Exists!");
                                }
                            })
                        .catch(function (response) {
                            notificationFactory.error("Error occur during save record.");
                        })
                        .finally(function () {

                        });
            }
        }
    }

    $scope.bindHotOrSpecial = function (data) {
      
        var dataList = data;
        if ($.fn.DataTable.isDataTable("#dataTableHotOrSpecial")) {
            $('#dataTableHotOrSpecial').DataTable().clear();
            $('#dataTableHotOrSpecial').DataTable().destroy();
        }

        $('#dataTableHotOrSpecial').DataTable({
            searching: false,
            dom: '<"table-responsive"rt><"bottom"lip<"clear">>',
            bProcessing: true,            
            retrieve: true,          
            stateSave: false,
            paging: false,
            bInfo : false,
            data: dataList,
            "order": [[0, "asc"]],
            columnDefs: [
            
                {
                    orderable: true,
                    mData: 'productName',
                    title: '<B><h5>Product Name</h5></B>',
                    targets: [0]
                },
                  {
                      orderable: true,
                      mData: 'store_name',
                      title: '<B><h5>Store</h5></B>',
                      targets: [1]
                  },
                {
                    orderable: true,                    
                    title: 'Type',
                    targets: [2],
                    "render": function (data, type, row) {
                        if (row.IsHotProduct ==true) {
                            return '<label>' + 'Hot Product' + '</label>';
                        }
                        else {
                            return '<label>' +'Special Product' + '</label>';;
                        }
                    }
                },
                {
                    orderable: true,
                    mData: 'startDate',
                    title: '<B><h5>Start Date</h5></B>',
                    "render": function (data, type, row) {
                        if (data != null) {
                            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                        }
                        else {
                            return "";
                        }
                    },
                    targets: [3]
                },
                {
                    orderable: true,
                    mData: 'endDate',
                    title: '<B><h5>End Date</h5></B>',
                    "render": function (data, type, row) {
                        if (data != null) {
                            return '<label>' + $filter("date")(data, $rootScope.GlobalDateFormat); '</label>';

                        }
                        else {
                            return "";
                        }
                    },
                    targets: [4]
                },
                {
                    orderable: true,
                    title: '<B><h5>Status</h5></B>',
                    targets: [5],
                    render: function (data, type, row) {
                        if (row.status == 1) {
                            return "Enabled";
                        }
                        else {
                            return "Disabled";
                        }

                    }
                },
               {
                   orderable: true,
                   mData: 'PrimaryKeyId',
                   title: '<B><h5>Id</h5></B>',
                   targets: [6],
                   visible:false
               },
                {
                    orderable: false,
                    width: '95px',
                    render: function (data, type, row) {
            
                        //return '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a title="Edit" href="" ng-click="EditHotOrSpecialProduct($event)"><span class="fa fa-pencil-square-o"></span></a>';             
                        if (row.status==1)
                        {                           
                            return '<button class="btn-danger" ng-click="EditHotOrSpecialProduct($event)">Disable</button>';
                        }
                        else {                        
                            return '<button class="btn-success" ng-click="EditHotOrSpecialProduct($event)">Enable</button>';
                        }
                      
                    },
                    title: 'Action',
                    targets: [7]
                }

            ],

            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            }
        });
    }

    $scope.EditHotOrSpecialProduct = function ($event)
    {
     
        var table = $('#dataTableHotOrSpecial').DataTable();
        var HorOrSpecialProductObj = table.row($($event.target).parents('tr')).data();

        if(HorOrSpecialProductObj.PrimaryKeyId>0)
        {
            $scope.HotOrSpecialProductObj = HorOrSpecialProductObj;
            if(HorOrSpecialProductObj.IsHotProduct==true)
            {
                $scope.HotOrSpecialProductObj.hot_productid = HorOrSpecialProductObj.PrimaryKeyId;
                $scope.HotOrSpecialProductObj.special_productid = 0;
            }
            else {
                $scope.HotOrSpecialProductObj.special_productid = HorOrSpecialProductObj.PrimaryKeyId;
                $scope.HotOrSpecialProductObj.hot_productid = 0;
            }           
            if ($scope.HotOrSpecialProductObj.status==1) {
                $scope.HotOrSpecialProductObj.status = 0;
            }
        else
        {
                $scope.HotOrSpecialProductObj.status = 1;
        }
            

            $scope.InsertAsHotOrSpecialProduct($scope.form);
           
        }      
    }
    $scope.GetHotOrSpecialProductById=function()
    {
        $http.get(configurationService.basePath + "API/ProductApi/GetHotOrSpecialProductById?store_id=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId + "&product_id=" + $scope.product_id)                                                                                                         
        .success(function (data) {               
            $scope.bindHotOrSpecial(data);

        }).error(function (err) {
          
        });
    };

    $scope.HotOrSpecialCancel = function () {
        $scope.NeedtoShowHot_SpeacialContainer = 0;
    }

    $scope.GetProductImageById = function()
    { 
        $http.get(configurationService.basePath + "API/ProductApi/GetProductImageById?product_id=" + $scope.ProductObj.product_id)
        .then(function (response) {
            $scope.ProductObj.Image = response.data[0]["Image"];
          
        })
    }

    $scope.NeedtoShowHot_SpeacialContainerDiv=function(IsHotProduct)
    { 
        $scope.form.HotOrSpecialStart.$setValidity("required", true);
        $scope.form.HotOrSpecialEnd.$setValidity("required", true);

        $scope.HotOrSpecialProductObj = new Object();
        $scope.HotOrSpecialProductObj.store_id = $scope.StoreId;
        $scope.HotOrSpecialProductObj.IsHotProduct = IsHotProduct;
        $scope.HotOrSpecialProductObj.status = 1;
        $scope.HotOrSpecialProductObj.created_by = $scope.LoggedInUserId;
        $scope.NeedtoShowHot_SpeacialContainer = 1;    

        if(IsHotProduct==1)
        {
            $scope.NeedtoShowHot_SpeacialContainerTitle = "Add as Hot Product";
        }
        else {
            $scope.NeedtoShowHot_SpeacialContainerTitle = "Add as Special Product";
        }
    }

    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {
                  $scope.StoreList = response.data;
                  $scope.ProductStoresList = angular.copy(response.data);
                  getBestSellerByProductId();
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.NgChangeShippingCountryDropDown = function (CountryId) {

        var SelectedShippingCountry = $filter('filter')($scope.shippingCostList, { country_id: CountryId }, true);
        if (SelectedShippingCountry.length>0) {
            $scope.ShippingObj = new Object();
            $scope.ShippingObj.country_id = SelectedShippingCountry[0].country_id;
            $scope.ShippingObj.shipping_cost = SelectedShippingCountry[0].shipping_cost;
        }
        else {
            $scope.ShippingObj.shipping_cost = 0;
        }

       
    }
    $scope.GetShippingCostDetail = function () {
        $http.get(configurationService.basePath + "api/ProductApi/GetShippingCostDetail?product_id=" + $scope.product_id)
          .then(function (response) {  
              if (response.data.length > 0) {
                  $scope.shippingCostList = response.data;
                  $scope.ShippingObj = new Object();
                  $scope.ShippingObj.country_id = $scope.shippingCostList[0].country_id;
                  $scope.ShippingObj.shipping_cost = $scope.shippingCostList[0].shipping_cost;
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    $scope.updateShippingCost = function()
    {
    
        if (!($scope.ShippingObj.country_id > 0) || ($scope.ShippingObj.shipping_cost == undefined || $scope.ShippingObj.shipping_cost == null)) {
            toastr.error("Shipping Country OR Shipping Cost should not be empty.");
            return false;
        }        
        var shipping_cost = $scope.ShippingObj.shipping_cost;
        var country_id = $scope.ShippingObj.country_id;
        $http.post(configurationService.basePath + "api/ProductApi/updateShippingCost?product_id="+$scope.product_id+"&country_id="+country_id+"&shipping_cost="+shipping_cost)
                            .then(function (response) {
                                if (response.data > 0) {
                                    $scope.ShippingObj = new Object();
                                    notificationFactory.customSuccess("Product Saved Successfully.");
                                    $scope.GetShippingCostDetail();
                                }
                                
                            })
                        .catch(function (response) {
                            notificationFactory.error("Error occur during save record.");
                        })
                        .finally(function () {

                        });

        
    }

    //#region init

    $scope.bestSellerChecked = function (storeId, status) {
        if (storeId) {
            if (status === true) {
                $scope.selectedBestSellerStore.push(storeId);
            } else {
                var index = $scope.selectedBestSellerStore.indexOf(storeId);
                if (index > -1) {
                    $scope.selectedBestSellerStore.splice(index, 1);
                }
            }
        }

    }

    function getBestSellerByProductId() {
        $http.get(configurationService.basePath + "api/ProductApi/GetBestSellerByProductId?productId=" + $scope.product_id)
            .then(function (response) {
                if (response && response.data && response.data.length > 0) {
                    $scope.bestSellerStoreList = response.data;
                    angular.forEach($scope.bestSellerStoreList, function (item) {
                        $scope.ProductStoresList.find(x => x.store_id === item.store_id).IsSelected = true;
                    });
                    var selectedStoreList = $filter('filter')($scope.ProductStoresList, { IsSelected: true }, true);
                    if (selectedStoreList && selectedStoreList.length > 0) {
                        angular.forEach(selectedStoreList, function (item) {
                            $scope.selectedBestSellerStore.push(item.store_id);
                        }); 
                    }
                }
            })
            .catch(function (response) {

            })
            .finally(function () {

            });

    }

    GetStoreList();
    Init();
    $scope.GetShippingCostDetail();
    //#endregion Image Tab
});