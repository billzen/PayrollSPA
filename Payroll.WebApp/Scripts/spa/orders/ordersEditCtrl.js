(function (app) {
    'use strict';

    app.controller('ordersEditCtrl', ordersEditCtrl);
    // '$rootScope' fro datepicker
    ordersEditCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService'];

    function ordersEditCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService) {


        $scope.cancelEdit = cancelEdit;
        $scope.updateOrder = updateOrder;


        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            //formatYear: 'yy',  //****** comment this option
            //startingDay: 1     //****** comment this option
        };
        $scope.format = 'dd/MM/yyyy'; //**** add datepicker format

        $scope.datepicker = {};

        $scope.OrderEmployeesList = [];

        loadOrderEmployees();

        function updateOrder() {
            console.log($scope.EditedOrder);
            apiService.post('/api/Orders/update/', $scope.EditedOrder,
            updateOrderCompleted,
            updateOrderLoadFailed);
        }

        function updateOrderCompleted(response) {
            notificationService.displaySuccess( 'Order : '  + $scope.EditedOrder.Orderdescription + ' has been updated');
            $scope.EditedOrder = {};
            $modalInstance.dismiss();
        }

        function updateOrderLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
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


        function loadOrderEmployees() {

            var orderID = $scope.EditedOrder.ID;

            var config = {
                params: {
                    orderid: orderID
                }
            };

            apiService.get('api/orders/OrderEmployees', config,
                OrderEmployeesCompleted,
                OrderEmployeesFailed);
        }


        function OrderEmployeesCompleted(response) {

            $scope.OrderEmployeesList = response.data;

            if ($scope.OrderEmployeesList.length > 0)
            {
                $scope.employeesmessage = $scope.OrderEmployeesList.length  + " Employees for this order";
            }
            else
            {
                $scope.employeesmessage = "No Employees for this order";
            }
            
        }

        function OrderEmployeesFailed(response) {
            notificationService.displayError(response.data);
        }

    }

})(angular.module('Payroll'));