app.controller('ShowProductController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();

    $scope.StoreId = $rootScope.storeId;
    $scope.LoggedInUserId = $rootScope.loggedInUserId;

    $scope.CatalogueList = [];
    //#endregion  

    //$scope.dtOptions = DTOptionsBuilder.newOptions()
    //                 .withOption('bDestroy', true)
    //                 .withOption("deferRender", true)
    //.withOption('bFilter', false);

    $scope.PageTitle = "Products";

    $scope.ProductFilter = new Object();
    $scope.ProductFilter.name = "";
    $scope.ProductFilter.Model = "";
    $scope.ProductFilter.Price = "";
    $scope.ProductFilter.Quantity = "";
    $scope.ProductFilter.status = "";
    
    //Remove local storage of ther pages
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Customer');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowOrders');
    //$rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_Product');
    $rootScope.RemoveAllFromLocalStorage_StartWith($scope.LoggedInUserId + '_ShowReward');

    if ($stateParams.Quantity != undefined && $stateParams.Quantity != null) {
        $scope.ProductFilter.Quantity = parseInt($stateParams.Quantity);
    }
   
    if ($stateParams.Status != undefined && $stateParams.Status != null) {
        $scope.ProductFilter.status =$stateParams.Status;
    }
    
    //$scope.GetProductList = function () {
    //    $http.get(configurationService.basePath + "api/ProductApi/GetProductList")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.ProductList = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
    //}



    function BindSearchCriteria(aoData) {

        aoData.push({ 'name': 'searchKey', 'value': '' });
        aoData.push({ 'name': 'Operation', 'value': '' });
        aoData.push({ 'name': 'searchValue', 'value': '' });

        return aoData;
    }

    function BindSorting(aoData, oSettings) {
        angular.forEach(oSettings.aaSorting, function (row, i) {
            var sortObj = new Object();
            sortObj.Column = oSettings.aoColumns[row[0]].mData;
            sortObj.Desc = row[1] == 'desc';
            aoData.push({ 'name': 'SortColumns', 'value': JSON.stringify(sortObj) });
            return;
        });
        return aoData;
    }

    var userid = $scope.LoggedInUserId;
    function MaintainLocalStorage() {

        //Store
        if ((localStorageService.get(userid + "_Product_Store") == "" || localStorageService.get(userid + "_Product_Store") == null)) {
            localStorageService.set(userid + "_Product_Store", $scope.ProductFilter.storeId);
        }
        else if ($scope.ProductFilter.storeId != "" ? (localStorageService.get(userid + "_Product_Store") != $scope.ProductFilter.storeId) : false) {
            localStorageService.set(userid + "_Product_Store", $scope.ProductFilter.storeId);
        }
        else if ((localStorageService.get(userid + "_Product_Store") != null || localStorageService.get(userid + "_Product_Store") != "")) {
            $scope.ProductFilter.storeId = localStorageService.get(userid + "_Product_Store");
        }

        //Price
        if ((localStorageService.get(userid + "_Product_Price") == "" || localStorageService.get(userid + "_Product_Price") == null)) {
            localStorageService.set(userid + "_Product_Price", $scope.ProductFilter.Price);
        }
        else if ($scope.ProductFilter.Price != "" ? (localStorageService.get(userid + "_Product_Price") != $scope.ProductFilter.Price) : false) {
            localStorageService.set(userid + "_Product_Price", $scope.ProductFilter.Price);
        }
        else if ((localStorageService.get(userid + "_Product_Price") != null || localStorageService.get(userid + "_Product_Price") != "")) {
            $scope.ProductFilter.Price = localStorageService.get(userid + "_Product_Price");
        }

        //Status
        if ((localStorageService.get(userid + "_Product_status") == "" || localStorageService.get(userid + "_Product_status") == null)) {
            localStorageService.set(userid + "_Product_status", $scope.ProductFilter.status);
        }
        else if ($scope.ProductFilter.status != "" ? (localStorageService.get(userid + "_Product_status") != $scope.ProductFilter.status) : false) {
            localStorageService.set(userid + "_Product_status", $scope.ProductFilter.status);
        }
        else if ((localStorageService.get(userid + "_Product_status") != null || localStorageService.get(userid + "_Product_status") != "")) {
            $scope.ProductFilter.status = localStorageService.get(userid + "_Product_status");
        }

        //name
        if ((localStorageService.get(userid + "_Product_name") == "" || localStorageService.get(userid + "_Product_name") == null)) {
            localStorageService.set(userid + "_Product_name", $scope.ProductFilter.name);
        }
        else if ($scope.ProductFilter.name != "" ? (localStorageService.get(userid + "_Product_name") != $scope.ProductFilter.name) : false) {
            localStorageService.set(userid + "_Product_name", $scope.ProductFilter.name);
        }
        else if ((localStorageService.get(userid + "_Product_name") != null || localStorageService.get(userid + "_Product_name") != "")) {
            $scope.ProductFilter.name = localStorageService.get(userid + "_Product_name");
        }

        //Quantity
        if ((localStorageService.get(userid + "_Product_Quantity") == "" || localStorageService.get(userid + "_Product_Quantity") == null)) {
            localStorageService.set(userid + "_Product_Quantity", $scope.ProductFilter.Quantity);
        }
        else if ($scope.ProductFilter.Quantity != "" ? (localStorageService.get(userid + "_Product_Quantity") != $scope.ProductFilter.Quantity) : false) {
            localStorageService.set(userid + "_Product_Quantity", $scope.ProductFilter.Quantity);
        }
        else if ((localStorageService.get(userid + "_Product_Quantity") != null || localStorageService.get(userid + "_Product_Quantity") != "")) {
            $scope.ProductFilter.Quantity = localStorageService.get(userid + "_Product_Quantity");
        }

        //Model
        if ((localStorageService.get(userid + "_Product_Model") == "" || localStorageService.get(userid + "_Product_Model") == null)) {
            localStorageService.set(userid + "_Product_Model", $scope.ProductFilter.Model);
        }
        else if ($scope.ProductFilter.Model != "" ? (localStorageService.get(userid + "_Product_Model") != $scope.ProductFilter.Model) : false) {
            localStorageService.set(userid + "_Product_Model", $scope.ProductFilter.Model);
        }
        else if ((localStorageService.get(userid + "_Product_Model") != null || localStorageService.get(userid + "_Product_Model") != "")) {
            $scope.ProductFilter.Model = localStorageService.get(userid + "_Product_Model");
        }
    }

    $scope.checkIfEnterKeyWasPressed = function ($event) {
        var keyCode = $event.which || $event.keyCode;
        if (keyCode === 13) {
            $scope.ContainValueOrNot();
            $scope.GetProductList();
        }
    };

    $scope.ContainStoreOrNot = function () {
        //Store
        if ($scope.ProductFilter.storeId == "" || $scope.ProductFilter.storeId == null) {
            localStorageService.set(userid + "_Product_Store", "");
        }
        //MaintainLocalStorage();
    }

    $scope.ContainStatusOrNot = function () {
        //Store
        if ($scope.ProductFilter.status == "" || $scope.ProductFilter.status == null) {
            localStorageService.set(userid + "_Product_status", "");
        }
        //MaintainLocalStorage();
    }

    $scope.ContainValueOrNot = function () {
        //Price
        if ($scope.ProductFilter.Price == "" || $scope.ProductFilter.Price == null) {
            localStorageService.set(userid + "_Product_Price", "");
        }

        //name
        if ($scope.ProductFilter.name == "" || $scope.ProductFilter.name == null) {
            localStorageService.set(userid + "_Product_name", "");
        }
        //Quantity
        if ($scope.ProductFilter.Quantity == "" || $scope.ProductFilter.Quantity == null) {
            //localStorageService.removeItem(userid + "_Customer_Email");
            localStorageService.set(userid + "_Product_Quantity", "");
        }

        //Model
        if ($scope.ProductFilter.Model == "" || $scope.ProductFilter.Model == null) {
            localStorageService.set(userid + "_Product_Model", "");
        }
    }

    $scope.GetProductList = function () {
        MaintainLocalStorage();

        var filter = $.param({
            name: $scope.ProductFilter.name,
            Price: $scope.ProductFilter.Price,
            status: $scope.ProductFilter.status,
            Model: $scope.ProductFilter.Model,
            Quantity: $scope.ProductFilter.Quantity,
        });
        if ($.fn.DataTable.isDataTable("#tblProduct")) {
            $('#tblProduct').DataTable().destroy();
        }
        var table = $('#tblProduct').DataTable({
            stateSave: false,
            "oLanguage": {
                "sProcessing": "",
                "sZeroRecords": "<span class='pull-left'>No records found</span>",
            },
            "autoWidth": false,
            "searching": false,
            "dom": '<"table-responsive"><"top"lrt><"bottom"ip<"clear">>',
            "bProcessing": true,
            "bServerSide": true,
            "iDisplayStart": 0,
            "iDisplayLength": configurationService.pageSize,
            "lengthMenu": configurationService.lengthMenu,
            "sAjaxDataProp": "aaData",
            "aaSorting": [[1, 'desc']],
            "sAjaxSource": configurationService.basePath + 'api/ProductApi/GetProductList',
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                //aoData = BindSearchCriteria(aoData);
                aoData = BindSorting(aoData, oSettings);
                var PageIndex = parseInt($('#tblProduct').DataTable().page.info().page) + 1;
                oSettings.jqXHR = $.ajax({
                    'dataSrc': 'aaData',
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource + "?StoreId=" + ($scope.ProductFilter.storeId == null ? 0 : $scope.ProductFilter.storeId) + "&LoggedInUserId=" + $scope.LoggedInUserId + "&PageIndex=" + PageIndex + "&name=" + $scope.ProductFilter.name + "&Price=" + $scope.ProductFilter.Price + "&status=" + $scope.ProductFilter.status + "&Model=" + $scope.ProductFilter.Model + "&Quantity=" + $scope.ProductFilter.Quantity,
                    "data": aoData,
                    "success": fnCallback,
                    "error": function (data, statusCode) {
                        exceptionService.ShowException(data.responseJSON, data.status);
                    }
                });
            },

            "aoColumns": [
                {
                    "mData": "ImagePath", "bSortable": false, "width": "10%"
                    , "render": function (data, type, row) {
                        return '<img ng-src=' + row.Image+ ' class="img-thumbnail" />'
                    }
                },
                { "mData": "name", "bSortable": true },
                { "mData": "Model", "bSortable": true },
                { "mData": "Price", "bSortable": true },
                {
                    "mData": "Quantity", "bSortable": true
                    ,"sClass": "action text-center"
                      , "render": function (data, type, row) {
                          if (row.Quantity>0)
                              return ' <span class="label label-success">' + row.Quantity + '</span>'
                          else
                              return ' <span class="label label-warning">' + row.Quantity + '</span>'
                      }
                },
                { "mData": "StatusName", "bSortable": true },
                {
                    "mData": null, "bSortable": false,
                    "sClass": "action text-center",
                    "render": function (data, type, row) {
                        return '<a ui-sref="ManageProduct({ProductId:' + row.product_id + '})"><i class="glyphicon glyphicon-edit cursor-pointer" title="edit"></i></a>'
                    }
                },
            ],
            "initComplete": function () {
                $compile(angular.element("#tblProduct").contents())($scope);
            },
            "fnCreatedRow": function (nRow, aData, iDataIndex) {
                $compile(angular.element(nRow).contents())($scope);
            },
            "fnDrawCallback": function () {
                BindToolTip();
            }
        });
    }

    function GetStoreList() {
        $http.get(configurationService.basePath + "api/StoreApi/GetStoreList?StoreId=" + $scope.StoreId + '&LoggedInUserId=' + $scope.LoggedInUserId)
          .then(function (response) {
              if (response.data.length > 0) {              
                  $scope.StoreList = response.data;
                  $scope.ProductFilter.storeId = $scope.StoreId;             
              }
          })
      .catch(function (response) {

      })
      .finally(function () {

      });
    }

    GetStoreList();
    $scope.GetProductList();
});