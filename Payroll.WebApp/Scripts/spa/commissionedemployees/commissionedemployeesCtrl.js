(function (app) {
    'use strict';

    app.controller('commissionedemployeesCtrl', commissionedemployeesCtrl);

    commissionedemployeesCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];

    function commissionedemployeesCtrl($scope, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-customers'; // ??
        $scope.loadingcommissionedemployees = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.CommissionedEmployees = [];
 

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;
        $scope.openOrdersDialog = openOrdersDialog;

        function search(page) {
            page = page || 0;

            $scope.loadingcommissionedemployees = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filtercommissionedEmployees
                }
            };

            apiService.get('/api/CommissionedEmployees/search/', config,
                commissionedemployeesLoadCompleted,
                commissionedemployeesLoadFailed);
        }

        function openEditDialog(commissionedemployee) {
            $scope.EditedCommissionedEmployee = commissionedemployee;
            $modal.open({
                templateUrl: 'scripts/spa/commissionedemployees/editCommissionedEmployeeModal.html',
                controller: 'commissionedemployeesEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }


        function openOrdersDialog(commissionedemployee) {
            $scope.EmployeeToOrders = commissionedemployee;
            $modal.open({
                templateUrl: 'scripts/spa/orders/AddEmployeeToOrdersModal.html',
                controller: 'AddEmployeeToOrdersCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function() {
            });
        }



        function commissionedemployeesLoadCompleted(result) {
            $scope.CommissionedEmployees = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.totalcommissionedemployees = result.data.TotalCount;
            $scope.loadingcommissionedemployees = false;


            if ($scope.CommissionedEmployees.length > 0) {

                if ($scope.page == 0)
                {
                  notificationService.displayInfo(result.data.TotalCount + ' Commissioned employees found')
                }
                
            }
            else {
                notificationService.displayError('No Employees found');
            }

        }

        function commissionedemployeesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filtercommissionedemployees = '';
            search();
        }

        $scope.search();
    }

})(angular.module('Payroll'));