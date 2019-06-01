(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function indexCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-home';
      //  $scope.Employees = true;
        $scope.loadingEmployees = true;
        $scope.isReadOnly = true;

        $scope.Employees = [];
        $scope.loadData = loadData;

        function loadData() {
            apiService.get('/api/employees/latest', null,
                        employeesLoadCompleted,
                        employeesLoadFailed);

            //apiService.get("/api/genres/", null,
            //    genresLoadCompleted,
            //    genresLoadFailed);
        }

        function employeesLoadCompleted(result) {
            $scope.Employees = result.data;
            $scope.totalCount = result.totalCount;
            $scope.loadingEmployees = false;
        }

        function employeesLoadFailed(response) {
            notificationService.displayError(response.data);
        }



        loadData();
    }


})(angular.module('Payroll'));