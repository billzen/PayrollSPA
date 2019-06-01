(function (app) {
    'use strict';

    app.controller('ordersCtrl', ordersCtrl);

    ordersCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];

    function ordersCtrl($scope, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-customers'; // ??
        $scope.loadingorders = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Orders = [];


        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openAddDialog = openAddDialog;

        function search(page) {
            page = page || 0;

            $scope.loadingorders = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterorders
                }
            };

            apiService.get('/api/Orders/search/', config,
                ordersLoadCompleted,
                ordersLoadFailed);
        }

        function openEditDialog(order) {
            $scope.EditedOrder = order;
            $modal.open({
                templateUrl: 'scripts/spa/Orders/editOrderModal.html',
                controller: 'ordersEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }


        function openAddDialog() {
       //     $scope.EditedOrder = order;
            $modal.open({
                templateUrl: 'scripts/spa/Orders/AddOrderModal.html',
                controller: 'ordersAddCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function ordersLoadCompleted(result) {
            $scope.Orders = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.totalorders = result.data.TotalCount;
            $scope.loadingorders = false;


            if ($scope.Orders.length > 0) {
                if ($scope.page == 0)
                {
                    notificationService.displayInfo(result.data.TotalCount + ' orders found')
                }
            }
            else {
                notificationService.displayError('No orders found');
            }

        }

        function ordersLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterorders = '';
            search();
        }

        $scope.search();
    }

})(angular.module('Payroll'));