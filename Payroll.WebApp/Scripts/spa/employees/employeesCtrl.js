(function (app) { 
    'use strict';

    app.controller('employeesCtrl', employeesCtrl);

    employeesCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];

    function employeesCtrl($scope, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-customers'; // ??
        $scope.loadingEmployees = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Employees = [];
        

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function search(page) {
            page = page || 0;

            $scope.loadingEmployees = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 12,
                    filter: $scope.filterEmployees
                }
            };

            apiService.get('/api/employees/search/', config,
                employeesLoadCompleted,
                employeesLoadFailed);
        }

        function openEditDialog(employee) {
            $scope.EditedEmployee = employee;
            $modal.open({
                templateUrl: 'scripts/spa/employees/editEmployeeModal.html',
                controller: 'employeesEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function employeesLoadCompleted(result) {
            $scope.Employees = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.totalEmployees = result.data.TotalCount;
            $scope.loadingEmployees = false;

           //if ($scope.filterEmployees && $scope.filterEmployees.length) {
           // notificationService.displayInfo(result.data.Items.length + ' employees found');
           //}


            if ($scope.Employees.length > 0 )
            {
                if ($scope.page === 0)
                {
                    notificationService.displayInfo(result.data.TotalCount + ' employees found');
                }
               
           }
           else
           {
               notificationService.displayError('No Employees found');
           }

        }

        function employeesLoadFailed(response) {
               notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterEmployees = '';
            search();
        }

        $scope.search();
    }

})(angular.module('Payroll'));