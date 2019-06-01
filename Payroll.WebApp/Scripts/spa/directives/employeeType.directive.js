(function (app) {
    'use strict';

    app.directive('employeeType', employeeType);

    function employeeType() {
        return {
            restrict: 'E',
            templateUrl: "/Scripts/spa/directives/employeeType.html",
            link: function ($scope, $element, $attrs) {
                $scope.getTypeClass = function () {
                    if ($attrs.isType == 1)
                        return 'label label-success'
                    else if ($attrs.isType == 2)
                        return 'label label-danger'
                    else if($attrs.isType = 3)
                    return 'label label-warning'
                };
                $scope.getType = function () {
                    if ($attrs.isType == 1)
                        return 'Fulltime'
                    else if ($attrs.isType == 2)
                        return 'PartTime'
                    else if ($attrs.isType == 3)
                        return 'Commissioned'
                };
            }
        }
    }

})(angular.module('common.ui'));