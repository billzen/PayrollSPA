(function (app) {
    'use strict';

    app.controller('ordersAddCtrl', ordersAddCtrl);
    // '$rootScope' fro datepicker
    ordersAddCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function ordersAddCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {


        $scope.cancelAdd = cancelAdd;
        $scope.AddOrder = AddOrder;


        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            //formatYear: 'yy',  //****** comment this option
            //startingDay: 1     //****** comment this option
        };
        $scope.format = 'dd/MM/yyyy'; //**** add datepicker format

        $scope.datepicker = {};

        function AddOrder() {
            console.log($scope.NewOrder);
            apiService.post('/api/Orders/Add/', $scope.NewOrder,
            NewOrderCompleted,
            NewOrderLoadFailed);
        }

        function NewOrderCompleted(response) {
            notificationService.displaySuccess( 'Order : '  + $scope.NewOrder.Orderdescription + ' has been added');
            $scope.NewOrder = {};
            $modalInstance.dismiss();
        }

        function NewOrderLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelAdd() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
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


        //};

    }

})(angular.module('Payroll'));