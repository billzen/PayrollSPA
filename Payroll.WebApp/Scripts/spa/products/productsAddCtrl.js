(function (app) {
 //   'use strict';

    app.controller('productsAddCtrl', productsAddCtrl);
    // '$rootScope' fro datepicker
    productsAddCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService', 'fileUploadBinaryService']; 

    function productsAddCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService, fileUploadBinaryService) {


        $scope.cancelEdit = cancelEdit;
        $scope.addProduct = addProduct;

        $scope.prepareFiles = prepareFiles;

       //**********************************
        var NewProductImages = new Array();

        function prepareFiles($files)
        {
             
            NewProductImages.push($files);
        }


        function addProductImages()
        {
            if (NewProductImages.length > 0)
            {               
                fileUploadBinaryService.uploadBinaryImage(NewProductImages,$scope.NewProduct.ID, redirectToEdit())
            }
            else
            {
                  redirectToEdit()();
            }
        }


        function addProduct() {

            console.log($scope.NewProduct);
            apiService.post('/api/Products/add/', $scope.NewProduct,
            addProductCompleted,
            addProductLoadFailed);
        }

        function addProductCompleted(response) {

            notificationService.displaySuccess('Product : ' + $scope.NewProduct.ProductDescription + ' has been added');

            $scope.NewProduct = response.data;

            addProductImages();

              NewProductImages = {};

          //$modalInstance.dismiss(); 
        }



        function redirectToEdit() {
           //   $location.url('products/editProduct/' + $scope.NewProduct);
                $modalInstance.dismiss();
        }



        function addProductLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }


     }

})(angular.module('Payroll'));