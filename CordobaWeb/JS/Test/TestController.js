app.controller('TestController', function ($timeout, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval) {


    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion

    $scope.CompanyLayoutObj = new Object();
    $scope.CompanyLayoutObj.CreatedBy = 1;

    $scope.Save=function(form)
    {
        if (form.$valid) {

            $.ajax({
                url: configurationService.basePath + "api/TestApi/InsertOrUpdateCompanyLayout",
                dataType: 'json',
                type:'POST',
                async: true,
                data:$scope.CompanyLayoutObj,
                success: function (data) {

             
                }
            });

        }
    }



});