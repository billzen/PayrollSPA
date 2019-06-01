(function (app) {
    'use strict';

    app.controller('fulltimeemployeesCtrl', fulltimeemployeesCtrl);

    fulltimeemployeesCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];  

    function fulltimeemployeesCtrl($scope, $modal, apiService, notificationService) { 
        $scope.pageClass = 'page-customers'; // ??
        $scope.loadingfulltimeemployees = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.FullTimeEmployees = [];


        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;



        function search(page) {
            page = page || 0;

            $scope.loadingfulltimeemployees = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterFullTimeEmployees
                }
            };

            apiService.get('/api/FullTimeEmployees/search/', config,
                fulltimeemployeesLoadCompleted,
                fulltimeemployeesLoadFailed);
        }


        function openEditDialog(FullTimeEmployee) {
            $scope.EditedFulltimeEmployee = FullTimeEmployee;
            $modal.open({
                templateUrl: 'scripts/spa/fulltimeemployees/editFullTimeEmployeeModal.html',
                controller: 'fulltimeemployeesEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function fulltimeemployeesLoadCompleted(result) {
            $scope.FullTimeEmployees = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.totalfulltimeemployees = result.data.TotalCount;
            $scope.loadingfulltimeemployees = false;            

            //if ($scope.filterEmployees && $scope.filterEmployees.length) {
            // notificationService.displayInfo(result.data.Items.length + ' employees found');
            //}


            if ($scope.FullTimeEmployees.length > 0) {

                if ($scope.page == 0)
                {
                    notificationService.displayInfo(result.data.TotalCount + ' Fulltime employees found')
                }                
            }
            else {
                notificationService.displayError('No Employees found');
            }



        }

        function fulltimeemployeesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterfulltimeemployees = '';
            search();
        }

        $scope.search();
    }

})(angular.module('Payroll'));