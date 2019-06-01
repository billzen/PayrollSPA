(function (app) { 
    'use strict';

    app.controller('logentriesCtrl', logentriesCtrl);

    logentriesCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];

    function logentriesCtrl($scope, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-customers'; // ??
        $scope.loadingLogEntries = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.LogEntries = [];
        

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function search(page) {
            page = page || 0;

            $scope.loadingLogEntries = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 6,
                    filter: $scope.filterLogEntries
                }
            };

            apiService.get('/api/logentries/search/', config,
                logentriesLoadCompleted,
                logentriesLoadFailed);
        }

        function openEditDialog(logentry) {
            $scope.EditedLogEntry = logentry;
            $modal.open({
                templateUrl: 'scripts/spa/logentries/editLogEntryModal.html',
                controller: 'logentriesEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {
            });
        }

        function logentriesLoadCompleted(result) {
            $scope.LogEntries = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.totalLogEntries = result.data.TotalCount;
            $scope.loadingLogEntries = false;

           //if ($scope.filterLogEntries && $scope.filterLogEntries.length) {
           // notificationService.displayInfo(result.data.Items.length + ' LogEntries found');
           //}


           if ($scope.LogEntries.length > 0)
           {
               if ($scope.page == 0)
               {
                   notificationService.displayInfo(result.data.TotalCount + ' logentries found')
               }
           }
           else
           {
               notificationService.displayError('No logentries found');
           }

        }

        function logentriesLoadFailed(response) {
               notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterLogEntries = 0;
            search();
        }

        $scope.search();
    }

})(angular.module('Payroll'));