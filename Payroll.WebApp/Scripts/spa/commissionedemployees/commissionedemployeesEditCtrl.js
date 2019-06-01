(function (app) {
    'use strict';

    app.controller('commissionedemployeesEditCtrl', commissionedemployeesEditCtrl);

    commissionedemployeesEditCtrl.$inject = ['$scope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function commissionedemployeesEditCtrl($scope, $modalInstance, $timeout, apiService, notificationService) {


        $scope.cancelEdit = cancelEdit;
        $scope.updateCommissionedEmployee = updateCommissionedEmployee;

        function updateCommissionedEmployee() {
            console.log($scope.EditedCommissionedEmployee);
            apiService.post('/api/CommissionedEmployees/update/', $scope.EditedCommissionedEmployee,
            updateCommissionedEmployeeCompleted,
            updateCommissionedEmployeeLoadFailed);
        }

        function updateCommissionedEmployeeCompleted(response) {
            notificationService.displaySuccess( 'Commisssioned Employee with ID '  + $scope.EditedCommissionedEmployee.ID + ' has been updated');
            $scope.EditedCommissionedEmployee = {};
            $modalInstance.dismiss();
        }

        function updateCommissionedEmployeeLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

    }

})(angular.module('Payroll'));