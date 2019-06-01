// *********** Not used in FullTime. performance errors. Keep for future use eith only one reponse
(function (app) {
    'use strict';

    app.directive('employeeDetails', employeeDetails);

    var firstname = "";

    employeeDetails.$inject = ['$rootScope', '$http', 'apiService', 'notificationService'];

    function employeeDetails($rootScope, $http, apiService, notificationService) {  

         return {
            restrict: 'E',
            templateUrl: "/Scripts/spa/directives/EmployeeDetails.html",
            link: function ($scope, $element, $attrs) {
                $scope.getFirstName = function () {

                    var config = {
                        params: {
                            Id: $attrs.empId
                        }
                    };

                    apiService.get('api/employees/GetDetails/', config,
                        detailsuccess,
                        detailserror
                        );
                    return firstname;

                };
            }
        }
    }

    function detailsuccess(response) {
    	//$scope.EmployeeDetails = response.data;
        firstname = response.data.FirstName;

    }

    function detailserror(response) {
        firstname = response.data;
    	//notificationService.displayError(response.data);
    }


})(angular.module('common.ui'));