(function (app) {
 //   'use strict';

    app.controller('productsEditCtrl', productsEditCtrl);
    // '$rootScope' fro datepicker
    productsEditCtrl.$inject = ['$scope', '$rootScope', '$modalInstance', '$timeout', 'apiService', 'notificationService', 'fileUploadBinaryService']; 

    function productsEditCtrl($scope, $rootScope, $modalInstance, $timeout, apiService, notificationService, fileUploadBinaryService) {
        var  upmessage = 0;  // var for pruduct updated message when last new image uploaded from all new images array

        $scope.cancelEdit = cancelEdit;
    //  $scope.updateProduct = updateProduct; //******************???????

        $scope.prepareFiles = prepareFiles;
        $scope.UpdateProductImages = UpdateProductImages;

        //******  Existing  Product Images
        $scope.ProductImagesList = [];     


        loadProductImages();


       

       //**********************************
        var NewProductImages = new Array();

        function prepareFiles($files)
        {
             
            NewProductImages.push($files);
        }


        function UpdateProductImages()
        {
            if (NewProductImages.length > 0)
            {               
                fileUploadBinaryService.uploadBinaryImage(NewProductImages, $scope.EditedProduct.ID, updateProduct)
            }
            else
            {
              //  $scope.imagesmessage = "No images";
                  updateProduct();
            }
        }


        function updateProduct() {
            upmessage++;   // increment var upmessage for pruduct updated message when last new image uploaded from all new images array
            console.log($scope.EditedProduct);
            apiService.post('/api/Products/update/', $scope.EditedProduct,
            updateProductCompleted,
            updateProductLoadFailed);
        }

        function updateProductCompleted(response) {

            //  **** Show this message when last new image uploaded or not images uploaded
            if (upmessage == NewProductImages.length || NewProductImages.length == 0)
            {
                notificationService.displaySuccess('Product : ' + $scope.EditedProduct.ProductDescription + ' has been updated');
            }
            NewProductImages = {};
            $modalInstance.dismiss();
        }

        function updateProductLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }


        function loadProductImages() {

            var productID = $scope.EditedProduct.ID;

            var config = {
                params: {
                    productid: productID
                }
            };

            apiService.get('api/Products/GetProductImages', config,
                ProductImagesCompleted,
                ProductImagesFailed);
        }


        function ProductImagesCompleted(response) {

            $scope.ProductImagesList = response.data;

            if ($scope.ProductImagesList.length > 0)
            {
              $scope.imagesmessage = $scope.ProductImagesList.length + " Images for this product";

  //              GetproductImageDetails($scope.ProductImagesList);
            }
            else
            {
                $scope.imagesmessage = "No Images for this product";
            }            
        }

        function ProductImagesFailed(response) {
            notificationService.displayError(response.data);
        }

    }

})(angular.module('Payroll'));