(function (app) {
    'use strict';
    app.controller('parttimeemployeesEditCtrl', parttimeemployeesEditCtrl);

    parttimeemployeesEditCtrl.$inject = ['$scope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function parttimeemployeesEditCtrl($scope, $modalInstance, $timeout, apiService, notificationService)
    {
        $scope.cancelEdit = cancelEdit;
        $scope.updatePartTimeEmployee = updatePartTimeEmployee; 

        function updatePartTimeEmployee()
        {
            console.log($scope.EditedPartTimeEmployee);
            apiService.post('/api/PartTimeEmployees/update/', $scope.EditedPartTimeEmployee,
                updatePartTimeEmployeeCompleted,
                updatePartTimeEmployeeFailed
            );
        }

        function updatePartTimeEmployeeCompleted(response)
        {
            notificationService.displaySuccess('PartTime Employee with ID ' + $scope.EditedPartTimeEmployee.ID + ' has been updated');
            $scope.EditedPartTimeEmployee = {};
            $modalInstance.dismiss();
        }

        function updatePartTimeEmployeeFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }
    } 

})(angular.module('Payroll'));