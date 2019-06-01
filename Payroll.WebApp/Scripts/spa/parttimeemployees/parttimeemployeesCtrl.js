(function (app) {
    'use strict';

    app.controller('parttimeemployeesCtrl', parttimeemployeesCtrl);

    parttimeemployeesCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];

    function parttimeemployeesCtrl($scope, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-customers'; // ??
        $scope.loadingEmployees = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.PartTimeEmployees = [];


        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function search(page) {
            page = page || 0;

        //  $scope.loadingEmployees = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 12,
                    filter: $scope.filterPartTimeEmployees
                }
            };

            apiService.get('/api/parttimeemployees/search/', config,
                parttimeemployeesLoadCompleted,
                parttimeemployeesLoadFailed);
        }

        function openEditDialog(parttimeemployee) {
            $scope.EditedPartTimeEmployee = parttimeemployee;
            $modal.open({
                templateUrl: 'scripts/spa/parttimeemployees/editPartTimeEmployeeModal.html',
                controller: 'parttimeemployeesEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function parttimeemployeesLoadCompleted(result) {
            $scope.PartTimeEmployees = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.totalPartTimeEmployees = result.data.TotalCount;
            $scope.loadingEmployees = false;

            //if ($scope.filterEmployees && $scope.filterEmployees.length) {
            // notificationService.displayInfo(result.data.Items.length + ' employees found');
            //}


            if ($scope.PartTimeEmployees.length > 0) {
                if ($scope.page == 0) {
                    notificationService.displayInfo(result.data.TotalCount + ' PartTime employees found');
                }

            }
            else {
                notificationService.displayError('No PartTime Employees found');
            }

        }

        function parttimeemployeesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterPartTimeEmployees = '';
            search();
        }

        $scope.search();
    }

})(angular.module('Payroll'));