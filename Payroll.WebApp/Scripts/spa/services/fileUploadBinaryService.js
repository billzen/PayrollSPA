(function (app) {
    'use strict';

    app.factory('fileUploadBinaryService', fileUploadBinaryService);

    fileUploadBinaryService.$inject = ['$rootScope', '$http', '$timeout', '$upload', 'notificationService'];

    function fileUploadBinaryService($rootScope, $http, $timeout, $upload, notificationService) {

        $rootScope.upload = [];

        var service = {
            uploadBinaryImage: uploadBinaryImage
        }
        //************** MUST Change static URL for warking globally
        function uploadBinaryImage($files, Id, callback) {
            //$files: an array of files selected
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $rootScope.upload[index] = $upload.upload({
                        url: "api/Products/images/upload?ProductId=" + Id, // webapi url
                        method: "POST",
                        file: $file
                    }).progress(function (evt) {
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        notificationService.displaySuccess(data.LogFilename + ' uploaded successfully');
                        callback();
                    }).error(function (data, status, headers, config) {
                        notificationService.displayError(data.Message + " not uploaded successfully");
                    });
                })(i);
            }
        }

        return service;
    }

})(angular.module('common.core'));