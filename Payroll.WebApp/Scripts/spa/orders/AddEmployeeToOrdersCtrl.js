(function (app) {
    'use strict';

    app.controller('AddEmployeeToOrdersCtrl', AddEmployeeToOrdersCtrl);

    AddEmployeeToOrdersCtrl.$inject = ['$scope', '$modalInstance', 'apiService', 'notificationService'];

    function AddEmployeeToOrdersCtrl($scope, $modalInstance, apiService, notificationService) {
        $scope.pageClass = 'page-customers'; // ??
        $scope.loadingorders = true;
        $scope.Orders = [];
        $scope.totalorders = 0;
        $scope.AddOrdersToEmployee = AddOrdersToEmployee;
        $scope.loadData = loadData;

       /************* REGION FOR LOADING ORDERS  */
        function loadData() {
            apiService.get('/api/Orders/GetOrdersForEmployee/', null,
                ordersLoadCompleted,
                ordersLoadFailed)
        }

        function ordersLoadCompleted(result) {
            $scope.Orders = result.data;

            $scope.totalorders = $scope.Orders.length;
            if ($scope.Orders.length > 0) {
                notificationService.displayInfo($scope.Orders.length + ' orders found');
            }
            else {
                notificationService.displayError('No orders found');
            }

            //loadCurrentOrdersOfEmployee();
            //checkBoxController();

            $scope.loadingorders = false;

            loadCurrentOrdersOfEmployee();

        }

        function ordersLoadFailed(response) {
            notificationService.displayError(response.data);
        }
        /*************END OF REGION FOR LOADING ORDERS  */


        /************* REGION FOR ADDING ORDERS TO EMPLOYEE  */
        function AddOrdersToEmployee() {
            var employeeID = $scope.EmployeeToOrders.ID;
            var CheckedOrders = $scope.selection;

            if (CheckedOrders.length > 0) {
                var JsonOrders = new Array();

                for (var i = 0; i < CheckedOrders.length ; i++) {
                        JsonOrders.push('{"Id" : ' + CheckedOrders[i] + '}');                   
                }
                var Jsondata = '{jsonObj : {"employeeId" :' + employeeID + ', "orders" : [' + JsonOrders + ']}}';
                //console.log($scope.Order);                                             // JSON.stringify()        
                apiService.post('/api/Orders/AddOrdersToEmployee/', Jsondata,
                EmployeeOrderCompleted,
                EmployeeOrderLoadFailed);
            }
            else {
                notificationService.displayInfo('No orders added to employee ' + $scope.EmployeeToOrders.ID);

            }
                $modalInstance.dismiss();
        }

        function EmployeeOrderCompleted(response) {
            notificationService.displayInfo($scope.selection.length + ' orders added to employee ' + $scope.EmployeeToOrders.ID);
//            $scope.NewOrder = {};
            $modalInstance.dismiss();
        }

        function EmployeeOrderLoadFailed(response) {
            notificationService.displayError(response.data);
        }
        
        function checkBoxController() {

         //   $scope.selection = [];

            // toggle selection for a given employee by Id
            $scope.toggleSelection = function toggleSelection(orderID) {
                var idx = $scope.selection.indexOf(orderID);
                // is currently selected
                if (idx > -1) {
                    $scope.selection.splice(idx, 1);
                }
                    // is newly selected
                else {
                    $scope.selection.push(orderID);
                }
            }
        }
        /*************END REGION FOR ADDING ORDERS TO EMPLOYEE  */



        /*************REGION FOR LOADING CURRENT ORDERS OF EMPLOYEE  */
        function loadCurrentOrdersOfEmployee() {
            var employeeID = $scope.EmployeeToOrders.ID;

            var config = {
                params: {
                    employeeID: employeeID
                }
            };

            apiService.get('/api/CommissionedEmployees/GetEmployeeCurrentOrders/', config,
                currentordersLoadCompleted,
                currentordersLoadFailed)
        }

        function currentordersLoadCompleted(result) {
            var employeecurrentOrders = new Array();
            employeecurrentOrders = result.data;
            $scope.selection = result.data;


            checkBoxController();

            disableorders(employeecurrentOrders);

            changeselectedorderdivcolor(employeecurrentOrders);

            $scope.selection = [];            

            // notificationService.displayInfo($scope.selection.length + ' orders added to employee ');
        }

        function currentordersLoadFailed(response) {
            notificationService.displayError(response.data);
        }


        function disableorders(employeeOrders)
        {
            for (var i = 0; i < employeeOrders.length ; i++) {
                // disable current orders of employee 
                document.getElementById(employeeOrders[i]).disabled = true;
            }
        }


        function changeselectedorderdivcolor(employeeOrders)
        {
            $(".panel-body").each(function ()
            {
                for (var i = 0; i < employeeOrders.length ; i++)
                {
                    if ( "color_" + employeeOrders[i] == $(this).attr("id") )
                    {
                    //   alert("order " + employeeOrders[i]);
                        $(this).css("background", "#C0C0C0");
                    }
                }
            });
        }


       /*************END REGION FOR LOADING CURRENT ORDERS OF EMPLOYEE  */

        loadData();
    }

})(angular.module('Payroll'));