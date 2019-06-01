(function (app) {
    'use strict';

    app.controller('employeesRegCtrl', employeesRegCtrl);

    employeesRegCtrl.$inject = ['$scope', '$location', '$routeParams',  '$rootScope', 'apiService', 'notificationService'];

    function employeesRegCtrl($scope, $location, $routeParams,  $rootScope, apiService, notificationService) {
        $scope.pageClass = 'page-movies'; //???
        $scope.newEmployee = {TypeId:1};

        $scope.EmployeeTypes = [];


        $scope.Register = Register;

        //$scope.openDatePicker = openDatePicker;
        //$scope.dateOptions = {
        //    formatYear: 'yy',
        //    startingDay: 1
        //};
        //$scope.datepicker = {};

        $scope.submission = {
            successMessages: ['Successfull submission will appear here.'],
            errorMessages: ['Submition errors will appear here.']
        };


        function loadEmployeeTypes() {
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


        function Register() {
            apiService.post('/api/employees/register', $scope.newEmployee,
           registerEmployeeSucceded,
           registerEmployeeFailed);
        }

        function registerEmployeeSucceded(response) {
            $scope.submission.errorMessages = ['Submition errors will appear here.'];
            console.log(response);
            var employeeRegistered = response.data;
            $scope.submission.successMessages = [];
            $scope.submission.successMessages.push($scope.newEmployee.LastName + ' has been successfully registed');
            //$scope.submission.successMessages.push('Check ' + customerRegistered.UniqueKey + ' for reference number');
            $scope.newEmployee = {};
        }

        function registerEmployeeFailed(response) {
            console.log(response);
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }

        //function openDatePicker($event) {
        //    $event.preventDefault();
        //    $event.stopPropagation();

        //    $scope.datepicker.opened = true;
        //};

        loadEmployeeTypes();
    }

})(angular.module('Payroll'));