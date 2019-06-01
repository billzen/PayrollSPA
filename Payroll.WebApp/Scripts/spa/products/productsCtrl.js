(function (app) {
    'use strict';

    app.controller('productsCtrl', productsCtrl);

    productsCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];

    function productsCtrl($scope, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-customers'; // ??
        $scope.loadingproducts = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Products = [];


        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openAddDialog = openAddDialog;

        function search(page) {
            page = page || 0;

            $scope.loadingproducts = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterproducts
                }
            };

            apiService.get('/api/Products/search/', config,
                productsLoadCompleted,
                productsLoadFailed);
        }

        function openEditDialog(product) {
            $scope.EditedProduct = product;
            $modal.open({
                templateUrl: 'scripts/spa/products/editProductModal.html',
                controller: 'productsEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }


        function openAddDialog() {
       //     $scope.Editedproduct = product;
            $modal.open({
                templateUrl: 'scripts/spa/Products/AddProductModal.html',
                controller: 'productsAddCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function productsLoadCompleted(result) {
            $scope.Products = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.totalproducts = result.data.TotalCount;
            $scope.loadingproducts = false;


            if ($scope.Products.length > 0) {
                if ($scope.page == 0)
                {
                    notificationService.displayInfo(result.data.TotalCount + ' products found')
                }
            }
            else {
                notificationService.displayError('No products found');
            }

        }

        function productsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterproducts = '';
            search();
        }

        $scope.search();
    }

})(angular.module('Payroll'));