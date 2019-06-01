(function (app) {
    'use strict';

    app.controller('fulltimeemployeesEditCtrl', fulltimeemployeesEditCtrl);

    fulltimeemployeesEditCtrl.$inject = ['$scope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function fulltimeemployeesEditCtrl($scope, $modalInstance, $timeout, apiService, notificationService) {

        //******************  Create temp object for handling model form values
       $scope.tempEditedFulltimeEmployee = $scope.EditedFulltimeEmployee;
    //   delete $scope.EditedFulltimeEmployee;


        $scope.cancelEdit = cancelEdit;
        $scope.updateFulltimeEmployee = updateFulltimeEmployee;

        function updateFulltimeEmployee() {
            console.log($scope.EditedFulltimeEmployee);
            apiService.post('/api/FullTimeEmployees/update/', $scope.EditedFulltimeEmployee,
            updateFulltimeEmployeeCompleted,
            updateFulltimeEmployeeLoadFailed);
        }

        function updateFulltimeEmployeeCompleted(response) {
            notificationService.displaySuccess('Fulltime Employee with ID ' + $scope.EditedFulltimeEmployee.ID + ' has been updated');
            $scope.EditedFulltimeEmployee = {};
            $modalInstance.dismiss();
        }

        function updateFulltimeEmployeeLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

    }

})(angular.module('Payroll'));