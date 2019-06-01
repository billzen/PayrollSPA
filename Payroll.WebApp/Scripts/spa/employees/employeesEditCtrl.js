(function (app) {
    'use strict';

    app.controller('employeesEditCtrl', employeesEditCtrl);

    employeesEditCtrl.$inject = ['$scope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function employeesEditCtrl($scope, $modalInstance, $timeout, apiService, notificationService) {
        $scope.EmployeeTypes = [];
        loadEmployeeTypes();

        $scope.cancelEdit = cancelEdit;
        $scope.updateEmployee = updateEmployee;



        function loadEmployeeTypes()
        {
            apiService.get('api/employeetypes/', null,
                employeeTypesCompleted,
                employeeTypesLoadFailed);
        }


        function employeeTypesCompleted(response) {
            $scope.EmployeeTypes = response.data;
        }

        function employeeTypesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        //$scope.openDatePicker = openDatePicker;
        //$scope.dateOptions = {
        //    formatYear: 'yy',
        //    startingDay: 1
        //};
        //$scope.datepicker = {};

        function updateEmployee() {
            console.log($scope.EditedEmployee);
            apiService.post('/api/employees/update/', $scope.EditedEmployee,
            updateEmployeeCompleted,
            updateEmployeeLoadFailed);
        }

        function updateEmployeeCompleted(response) {
            notificationService.displaySuccess($scope.EditedEmployee.FirstName + ' ' + $scope.EditedEmployee.LastName + ' has been updated');
            $scope.EditedEmployee = {};
            $modalInstance.dismiss();
        }

        function updateEmployeeLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

    }

})(angular.module('Payroll'));