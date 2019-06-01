(function (app) {
    'use strict';

    app.controller('logentriesEditCtrl', logentriesEditCtrl);

    logentriesEditCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService', 'fileUploadService'];

    function logentriesEditCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService, fileUploadService) {
        $scope.Employees = [];
        loadEmployees();

        $scope.changeWorkedHour = changeWorkedHour;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            //formatYear: 'yy',  //****** comment this option
            //startingDay: 1     //****** comment this option
        };
        $scope.format = 'dd/MM/yyyy'; //**** add datepicker format

        $scope.datepicker = {};

        $scope.cancelEdit = cancelEdit;

        $scope.UpdateLogEntry = UpdateLogEntry;
        $scope.prepareFiles = prepareFiles;
        var LogEntryImage = null;

        function loadEmployees()
        {
            apiService.get('api/logentries/LogEntryEmployees', null,
                employeesCompleted,
                employeesLoadFailed);
        }


        function employeesCompleted(response) {
            $scope.Employees = response.data;
            openTimepicker();
        }

        function employeesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        // for adding 0 when number < 10
        function pad2(number) {

            return (number < 10 ? '0' : '') + number

        }


        function changeWorkedHour() {
            var newtimeStart = new Date("01/01/2007 " + $scope.EditedLogEntry.EntryTime);
            var newtimeEnd = new Date("01/01/2007 " + $scope.EditedLogEntry.DepartTime);
            var newdifference = newtimeEnd - newtimeStart;
            var newdiff_result = new Date(newdifference);
            var newhourDiff = (newdiff_result.getHours() - 2) + ":" + pad2(newdiff_result.getMinutes()) + ":" + pad2(newdiff_result.getSeconds());
            $scope.EditedLogEntry.WorkerdHours = newhourDiff;
        }


        function openTimepicker()
        {
            $('#EntryTime').timepicker({
                minuteStep: 1,
                showSeconds: true,
                showMeridian: false
                //defaultTime: '{{EditedLogEntry.EntryTime}}'
            });

            $('#DepartTime').timepicker({
                minuteStep: 1,
                showSeconds: true,
                showMeridian: false
            });

        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);


        };


        function UpdateLogEntry()
        {
            if (LogEntryImage) {
                fileUploadService.uploadImage(LogEntryImage, $scope.EditedLogEntry.ID, UpdateLogEntryModel);
            }
            else
            {
                UpdateLogEntryModel();
            }

        }

        function UpdateLogEntryModel() {           
            console.log($scope.EditedLogEntry);
            apiService.post('/api/logentries/update/', $scope.EditedLogEntry,
            updateLogEntryCompleted,
            updateLogEntryLoadFailed);
        }


        function prepareFiles($files) {
            LogEntryImage = $files;
        }

        function updateLogEntryCompleted(response) {
            notificationService.displaySuccess('LogEntry : ' + $scope.EditedLogEntry.ID + ' has been updated');

        //    $scope.EditedLogEntry = {};

            LogEntryImage = null;
            $modalInstance.dismiss();
        }

        function updateLogEntryLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
             //document.getElementById("EditLogEntryForm").reset();

            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }



    }
})(angular.module('Payroll'));