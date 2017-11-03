angular.module("IlogicApp", ["Services"])
.controller('homeController', ['$scope', 'Irequest', 'Categorys', 'Productos', function ($scope, Irequest, Categorys, Productos) {

    $scope.newproducts1 = newProd.slice(0, 4);

    $scope.newproducts2 = newProd.slice(4, 8);

    $scope.offerproducts1 = offerProd.slice(0, 4);

    $scope.offerproducts2 = offerProd.slice(4, 8);

}]).controller('loginController', ['$scope', 'Irequest', function ($scope, Irequest) {

    $scope.msg = '';
    var usuario = { nombre: '', clave: '' };
    $scope.user = usuario;

    $scope.login = function (event) {
        if (!$scope.myform1.$valid) {
            return;
        }


        event.preventDefault();
        Irequest.make('post', '/Users/Login', usuario).then(function (data) {
            if (!data.success) {
                $scope.msg = data.errorMsg;
                $scope.user.clave = '';
            } else {
                $scope.msg = 'Ingresando...';
                window.location.replace("/");
            }



        }, function () {
            $scope.msg = 'Error en conexion';
        }

            );

    }


}
]).controller('registerController', ['$scope', 'Irequest', function ($scope, Irequest) {
    var usuario = { nombre: '', clave: '', email: '' };
    $scope.user = usuario;
    $scope.rclave = '';

    $scope.register = function (event) {
        if (!$scope.myform2.$valid) {
            return;
        }
        event.preventDefault();
        Irequest.make('post', '/Users/Register', usuario).then(function (data) {

            if (data.success) {
                window.location.replace("/Users/Validate");
            } else {
                $scope.msg2 = data.errorMsg;
            }

        }, function () {
            $scope.msg2 = 'Error en conexion';
        })
    }

}
]).controller('menuController', ['$scope', 'Irequest', '$sce', 'Categorys', function ($scope, Irequest, $sce, Categorys) {






    $scope.categorias = categorys;







}]).controller('contactController', ['$scope', 'Irequest', '$sce', 'Categorys', function ($scope, Irequest, $sce, Categorys) {;

    $scope.contact = contactx;
    $scope.Contactar = function (event) {
        if (!$scope.myform2.$valid) {
            return;
        }
        event.preventDefault();
        Irequest.make('POST', '/Home/Contact', $scope.contact).then(function (data) {

            if (data.success) {
                Irequest.make('POST', '/Home/Msg', "Mensaje enviado")
            } else {
                $scope.msg2 = data.errorMsg;
            }

        }, function () {
            $scope.msg2 = 'Error en conexion';
        })


    }


}]);
$(function () {
    $('.noSpaces').on('keypress', function (e) {
        if (e.which == 32)
            return false;
    });
});