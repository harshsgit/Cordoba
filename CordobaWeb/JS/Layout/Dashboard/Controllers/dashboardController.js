app.controller('DashboardController', function (StoreSessionDetail, $timeout, UserDetail, $state, $http, $rootScope, $stateParams, $filter, $scope, $window, $state, notificationFactory, configurationService, $compile, $interval, DTOptionsBuilder, $http, $log, $q, localStorageService, $sce) {
    //#region CallGlobalFunctions
    decodeParams($stateParams);
    BindToolTip();
    Tab();
    //#endregion
    $scope.customerpoint = 0;
    $scope.StoreDetailInSession = StoreSessionDetail;
    $rootScope.no_image_path = StoreSessionDetail.no_image_path;
    $scope.selectedlanguage = localStorageService.get("selectedlanguage");
    $scope.trustAsHtml = function (string) {
        return $sce.trustAsHtml(string);
    };

    $scope.LanguageList = [{
        language_id: '1',
        name: 'English'
    }, {
        language_id: '2',
        name: 'Deutsch'
    }, {
        language_id: '3',
        name: 'Français'
    }, {
        language_id: '4',
        name: 'Italiano'
    }, {
        language_id: '5',
        name: 'Español'
    }, {
        language_id: '6',
        name: 'Português'
    }];

    $scope.GetCustomerDetails = function () {
        if (UserDetail.customer_id > 0) {
            $http.get(configurationService.basePath + "API/LayoutDashboardAPI/CustomerDetailLayout?CustomerId=" + UserDetail.customer_id + "&StoreId=" + $scope.StoreDetailInSession.store_id)
                .then(function (response) {
                     
                    $rootScope.CustomerDetail.points = response.data.points;
                    UserDetail.points = $rootScope.CustomerDetail.points;
                    $scope.customerpoint = $rootScope.CustomerDetail.points;
                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }
    $scope.GetCustomerDetails();
    //$scope.GetLanguageList = function () {
    //    $http.get(configurationService.basePath + "API/CategoryAPI/GetLanguageList?StoreId=" + $scope.StoreDetailInSession.store_id + "&LoggedInUserId=" + UserDetail.customer_id)
    //    .then(function (response) {
    //        $scope.LanguageList = response.data;
    //    })
    //    .catch(function (response) {

    //    })
    //    .finally(function () {
    //    });
    //}
    //$scope.GetLanguageList();

    $scope.UpdateLanguageForCustomer = function (selectedlanguage) {
        $http.post(configurationService.basePath + "API/LayoutDashboardAPI/UpdateLanguageForCustomer?customerid=" + UserDetail.customer_id + "&languageid=" + selectedlanguage)
            .then(function (response) {
                localStorageService.set("selectedlanguage", selectedlanguage);
                //if (selectedlanguage != "" )
                //{
                //    //window.location.reload();
                //    $state.reload();
                //}
                //else 
                if (selectedlanguage == "") {
                    $scope.selectedlanguage = "";
                }
                $state.reload();
            })
            .catch(function () {

            })
            .finally(function () {

            });
    }

    //angular hack to html decode
    $scope.WelcomeMsg = $scope.StoreDetailInSession.description.split('##ReadMore##');
    //var encoded = $scope.WelcomeMsg[0];
    //var old = $scope.WelcomeMsg[0].toString();
    ////"&lt;h1 style=&quot;font-size:40px;color:#3E53A4;font-weight:bold;line-height:50px;margin-bottom:20px&quot;&gt;\r\n	Welcome to the Make a Difference &amp;lsquo;thank you&amp;rsquo; store&lt;/h1&gt;\r\n&lt;p&gt;\r\n	The Pitney Bowes &amp;lsquo;thank you&amp;rsquo; site is the official mechanism for rewarding employees for excellence in what they do, and living the behaviours that support our Client, Team, Win philosophy.&lt;/p&gt;\r\n&lt;p&gt;\r\n	Many team programmes are rewarded using this gift catalogue including the &lt;strong&gt;Make a Difference employee reward programme&lt;/strong&gt; outlined below:&lt;/p&gt;\r\n&lt;p&gt;\r\n	&lt;img alt=&quot;Make a Difference Thank You&quot; height=&quot;140&quot; src=&quot;http://static.cordobarewards.co.uk/image/data/pitney-bowes/pitney-bowes-power.png&quot; width=&quot;140&quot; /&gt;&lt;/p&gt;\r\n&lt;div style=&quot;color:#4E4E4E;line-height:16px&quot;&gt;\r\n	&lt;p&gt;\r\n		Putting customers first is a company value that Make a Difference award winners bring to life. We all have the ability to deliver on this value. Whether you support internal customers or external customers, striving for excellence in everything you do will impact our customers and support our businesses growth.&lt;/p&gt;\r\n	&lt;p&gt;\r\n		Winners of this award are given points which can be accumulated and exchanged for a range of gifts.&lt;/p&gt;\r\n	&lt;p&gt;\r\n		So, if you&amp;rsquo;ve received points, Pitney Bowes thanks you again for all your efforts and trusts you enjoy your shopping experience.&lt;/p&gt;\r\n&lt;/div&gt;"
    //$scope.WelcomeMsg[0] = $scope.trustAsHtml(old);

    //$scope.WelcomeMsg[1] = $scope.WelcomeMsg[1].replace('\\r\\n', '<br/>');
    $scope.TermsConditionMsg = "";

    function OpenLoginForm() {
        $rootScope.OpenLoginPopUpUsingRootScope();
    }

    $rootScope.OpenLoginPopUpUsingRootScope = function () {
        $scope.Logout();
        $scope.OpenLoginPopUp();
    }
    $scope.OpenLoginPopUp = function () {
        angular.element("#DivLoginModel").modal('show');
        $scope.IsVisibleloginForm = false;
        $scope.IsVisibleChangePassswordForm = true;
        $scope.IsVisibleforgotPasswordForm = true;
        $scope.IsVisibleResetPassswordForm = true;
        $scope.IsVisibleFirstTimeVisiteForm = true;
        $scope.IsVisibleOTPForm = true;
        $scope.CustomerObj = new Object();
        $scope.loginForm.$submitted = false;
        $scope.otpObj = new Object();
    }

    $scope.Login = function (form) {
        if (form.$valid) {

            $scope.CustomerObj.cartgroup_id = UserDetail.cartgroup_id;
            $scope.CustomerObj.store_id = $scope.StoreDetailInSession.store_id;
            $scope.CustomerObj.IsFromAdmin = $("#IsFromAdmin").val();

            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/CustomerLogin", $scope.CustomerObj)
                .then(function (response) {

                    if (response.data != null) {
                        switch (response.data.ErrorTypeId) {
                            case 0:

                                UserDetail.customer_id = response.data.customer_id;
                                UserDetail.firstname = response.data.firstname;
                                UserDetail.lastname = response.data.lastname;
                                UserDetail.points = response.data.points;
                                UserDetail.address_id = response.data.address_id;
                                UserDetail.cartgroup_id = response.data.cartgroup_id;
                                UserDetail.TotalItemAdded = response.data.TotalItemAdded;
                                UserDetail.store_id = response.data.store_id;

                                localStorageService.set("loggedInUser", response.data);
                                $rootScope.CustomerDetail = response.data;
                                $scope.customerpoint = $rootScope.CustomerDetail.points;

                                angular.element("#DivLoginModel").modal('hide');

                                break;
                            case 1:
                                toastr.error('Please enter correct email!');
                                $scope.CustomerObj.email = null;
                                break;
                            case 2:
                                toastr.error('Please enter correct password!');
                                $scope.CustomerObj.password = null;
                                break;
                            case 3:
                                toastr.error('Please enter correct email and password!');
                                $scope.CustomerObj = null;
                                break;
                        }
                    } else {
                        toastr.error('Please enter correct email and password!');
                        //$scope.CustomerObj = null;
                    }

                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }

    //for first time login 
    $scope.validateEmailaddress = function () {
        if ($scope.CustomerObj.email != null) {

            //$scope.validateObj = new Object();
            //$scope.validateObj.email = $scope.CustomerObj.email;
            //$scope.validateObj.store_id = $scope.StoreDetailInSession.store_id;

            //$http.post(configurationService.basePath + "API/LayoutDashboardAPI/VisitedCustomerInfo", $scope.validateObj)
            //   .then(function (response) {


            //       if (response.data === 0) {
            //           $scope.IsVisibleloginForm = true;
            //           $scope.IsVisibleFirstTimeVisiteForm = false;
            //       }
            //    })
            //    .catch(function (response) {

            //    })
            //    .finally(function () {

            //    });
        }
    }

    $scope.OpenResetPasswordForm = function () {
        if ($scope.CustomerObj.email != null) {

            $scope.resetObj = new Object();
            $scope.resetObj.store_id = $scope.StoreDetailInSession.store_id;
            $scope.resetObj.store_name = $scope.StoreDetailInSession.name;
            $scope.resetObj.logo = $scope.StoreDetailInSession.logo;
            $scope.resetObj.email = $scope.CustomerObj.email;

            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/SendResetPassEmail", $scope.resetObj)
                .then(function (response) {


                    if (response.data.errorcode > 0) {

                        $scope.IsVisibleloginForm = true;
                        $scope.IsVisibleFirstTimeVisiteForm = true;
                        $scope.IsVisibleResetPassswordForm = false;
                    }
                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }

    $scope.ResetPassword = function (form) {
        if (form.$valid) {
            $scope.resetObj.email = $scope.CustomerObj.email;
            $scope.resetObj.store_id = $scope.StoreDetailInSession.store_id;

            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/ResetPasswordAndOtpVerify", $scope.resetObj)
                .then(function (response) {


                    if (response.data.errorcode > 0) {

                        notificationFactory.customSuccess("Password Reseted Successfully.");

                        $scope.IsVisibleforgotPasswordForm = true;
                        $scope.IsVisibleOTPForm = true;
                        $scope.IsVisibleChangePassswordForm = true;
                        $scope.IsVisibleResetPassswordForm = true;
                        $scope.IsVisibleFirstTimeVisiteForm = true;
                        $scope.IsVisibleloginForm = false;
                        $scope.otpObj.otp = '';
                        $scope.otpObj.password = '';
                        $scope.otpObj.confirmPassword = '';
                        $scope.otpObj.email = '';
                        form.$valid = true;
                        $scope.forgotPasswordForm.$valid = true;
                    } else {
                        toastr.error("OTP verification failed.Please Check");
                    }
                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }

    $scope.Logout = function () {
        $scope.UpdateLanguageForCustomer("");
        UserDetail.customer_id = 0;
        UserDetail.firstname = "";
        UserDetail.lastname = "";
        UserDetail.points = "";
        UserDetail.address_id = 0;
        UserDetail.cartgroup_id = 0;
        UserDetail.TotalItemAdded = 0;
        localStorageService.set("loggedInUser", UserDetail);
        $rootScope.CustomerDetail = UserDetail;
        $state.go('Home');
        //$state.reload();
    }

    $scope.ForgotPassword = function (form) {
        $scope.IsVisibleloginForm = true;
        $scope.IsVisibleforgotPasswordForm = false;

        if (form.$valid) {

            $scope.otpObj.store_id = $scope.StoreDetailInSession.store_id;
            $scope.otpObj.store_name = $scope.StoreDetailInSession.name;
            $scope.otpObj.logo = $scope.StoreDetailInSession.logo;
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/ForgotPassword", $scope.otpObj)
                .then(function (response) {
                    if (response.data.errorcode > 0) {
                        $scope.IsVisibleloginForm = true;
                        $scope.IsVisibleforgotPasswordForm = true;
                        $scope.IsVisibleOTPForm = false;
                        $scope.IsVisibleChangePassswordForm = true;
                        $scope.otpObj = response.data;

                    }
                    else {
                        //notificationFactory.customError("Email does not exist");
                        toastr.error("Email does not exist");
                    }
                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }

    $scope.VerifyOTP = function (form) {
        $scope.IsVisibleloginForm = true;
        $scope.IsVisibleforgotPasswordForm = true;

        if (form.$valid) {
            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/VerifyOTP", $scope.otpObj)
                .then(function (response) {
                    if (response.data && response.data.errorcode > 0) {
                        //$scope.IsVisibleloginForm = true;
                        //$scope.IsVisibleforgotPasswordForm = true;
                        $scope.IsVisibleOTPForm = true;
                        $scope.IsVisibleChangePassswordForm = false;
                        $scope.otpObj.password = '';
                        $scope.otpObj.email = '';
                    }
                    else {
                        toastr.error("Please Enter valid OTP");
                    }

                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }

    $scope.GotoMyWishlist = function () {

        if (UserDetail.customer_id > 0) {
            $state.go('LayoutCategoryORProductList', { 'CategoryId': -2 });
        }
        else {
            $scope.OpenLoginPopUp();
        }
    }

    $scope.GotoProductList = function (Whatyouarelookingfor) {
        if (Whatyouarelookingfor != undefined && Whatyouarelookingfor != null && Whatyouarelookingfor != "") {
            $state.go('LayoutCategoryORProductList', { 'CategoryId': -3, 'Search': Whatyouarelookingfor });
        }
    }

    $scope.OpenTermsCondition = function () {
        $scope.GetTermsCondition();
        angular.element("#DivTermsConditionModel").modal('show');
    }

    $scope.GetTermsCondition = function () {
        $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetStoreTermsDetail?Store_Id=" + $scope.StoreDetailInSession.store_id)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.TermsConditionMsg = response.data[0].Terms;
                }
            })
            .catch(function (response) {

            })
            .finally(function () {

            });
    }

    $scope.ChangePassword = function (form) {
        if (form.$valid) {

            $http.post(configurationService.basePath + "API/LayoutDashboardAPI/SaveChangedPassword_Layout?StoreId=" + $scope.StoreDetailInSession.store_id, $scope.otpObj)
                .then(function (response) {
                    if (response.data > 0) {
                        notificationFactory.customSuccess("Password changed Successfully.");

                        $scope.IsVisibleforgotPasswordForm = true;
                        $scope.IsVisibleOTPForm = true;
                        $scope.IsVisibleChangePassswordForm = true;
                        $scope.IsVisibleloginForm = false;
                        $scope.otpObj.otp = '';
                        $scope.otpObj.password = '';
                        $scope.otpObj.confirmPassword = '';
                        $scope.otpObj.email = '';
                        form.$valid = true;
                        $scope.forgotPasswordForm.$valid = true;
                    }
                    else {
                        //notificationFactory.customError("Something went wrong");
                        toastr.error("Something went wrong");
                    }


                })
                .catch(function (response) {

                })
                .finally(function () {

                });
        }
    }

    $scope.SetNoImageSrc = function (Image) {
        Image.src = $rootScope.no_image_path;
    }

    //function decodeHtml(html) {
    //    var txt = document.createElement("textarea");
    //    txt.innerHTML = html;
    //    return txt.value;
    //}

    //$scope.GetStoreDetailForDashboard = function () {

    //    $http.get(configurationService.basePath + "API/LayoutDashboardAPI/GetStoreDetailByStoreId?StoreID=0")
    //      .then(function (response) {
    //          if (response.data.length > 0) {
    //              $scope.StoreDetail = response.data;
    //          }
    //      })
    //  .catch(function (response) {

    //  })
    //  .finally(function () {

    //  });
    //}


    //$scope.GetStoreDetailForDashboard();

    function inIframe() {
        try {
            return window.self !== window.top;
        } catch (e) {
            return true;
        }
    }

    angular.element(document).ready(function () {
        function param(name) {
            return (location.href.split(name + '=')[1] || '').split('&')[0];
        }

        function DecodeString(element) {
            if (element.indexOf(specialCharacter) > -1) {
                element = decodeURIComponent(element);
                var decodedstring = element.substring(keylength + 1, element.length - (keylength + 1));
                if (decodedstring.length >= 3) {

                    decodedstring = (String.fromCharCode(parseInt(decodedstring.substring(0, 3))) + decodedstring.substring(3, decodedstring.length)).toString();
                }
                return decodedstring;
            }
        }

        if (window.location.href.indexOf("IsFromAdmin") > 0 && window.location.href.indexOf("Email") > 0) {
            var IsFromAdmin = DecodeString(param('IsFromAdmin'));
            var Email = DecodeString(param('Email'));
            if (IsFromAdmin == "true") {
                window.document.getElementById("IsFromAdmin").value = IsFromAdmin;
                window.document.getElementById("email").value = Email;
                var scope = angular.element('#loginForm').scope();
                scope.CustomerObj = new Object();
                scope.CustomerObj.email = Email;

                if (inIframe()) {
                    OpenLoginForm();
                }
            }
        }
    });

    $interval(function () { CheckVersionNumber(); }, 3000);
    function CheckVersionNumber() {
        $.ajax({
            method: "GET",
            url: "Home/CheckVersionNumber",
            async: false,
            success: function (data) {
                if (data != ApplicationVersion) {
                    angular.element("#Reloadlookup").modal('show');
                }
            }

        })
    };
});


