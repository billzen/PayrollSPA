(function () {
    'use strict';

    angular.module('Payroll', ['common.core', 'common.ui'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                controller: "indexCtrl"
            })
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            })

            .when("/employees/register", {
                templateUrl: "scripts/spa/employees/register.html",
                controller: "employeesRegCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
      
            .when("/FullTimeEmployees", {
                templateUrl: "scripts/spa/fulltimeemployees/FullTimeEmployees.html",
                controller: "fulltimeemployeesCtrl"
            })

            .when("/PartTimeEmployees", {
                templateUrl: "scripts/spa/parttimeemployees/PartTimeEmployees.html",
                controller: "parttimeemployeesCtrl"
            })

            .when("/CommissionedEmployees", {
                templateUrl: "scripts/spa/commissionedemployees/CommissionedEmployees.html",
                controller: "commissionedemployeesCtrl"
            })

            .when("/Orders", {
                templateUrl: "scripts/spa/orders/Orders.html",
                controller: "ordersCtrl"
            })

            .when("/Products", {
                templateUrl: "scripts/spa/products/Products.html",
                controller: "productsCtrl"
            })

            .when("/LogEntries", {
                templateUrl: "scripts/spa/logentries/LogEntries.html",
                controller: "logentriesCtrl"
            })

            .when("/employees", {
                templateUrl: "scripts/spa/employees/Employees.html",
                controller: "employeesCtrl"
            }).otherwise({ redirectTo: "/" });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        // handle page refreshes
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }

        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    }

})();