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


}]).controller('registerController', ['$scope', 'Irequest', function ($scope, Irequest) {
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
}]).controller('EditUserController', ['$scope', 'Irequest', function ($scope, Irequest) {

    $scope.user = ActualUser;
    $scope.user.fechanac = new Date(parseInt($scope.user.fechanac.substr(6)));
    $scope.rclave = $scope.user.clave;
    $scope.EditUser = function (event) {
        if ($scope.user.clave != $scope.rclave) {
            $scope.msg = "Las claves no coinciden";
            return
        }
        if (!$scope.myform1.$valid) {
            return;
        }
        event.preventDefault();

        Irequest.make('POST', '/Users/Edit', $scope.user).then(function (data) {
            if (data.success) {
                $scope.msg = "Usuario Actualizado";
            } else {
                $scope.msg = "Error al Actualizar el usuario en la Base de Datos";
            }
         
        }, function () {
            $scope.msg = "Erros en la conexion con el servidor";
        })
    }


}]).controller('menuController', ['$scope', 'Irequest', '$sce', 'Categorys', function ($scope, Irequest, $sce, Categorys) {






    $scope.categorias = categorys;




}]).controller('DetailsController', ['$scope', 'Irequest', '$sce', function ($scope, Irequest, $sce) {

    $scope.producto = producto;
    $scope.newproducts1 = newProd.slice(0, 4);

    $scope.newproducts2 = newProd.slice(4, 8);
    Irequest.make("POST", "/Productos/GetRandom").then(function (data) {
        $scope.ramprod = data;
    })


}]).controller('contactController', ['$scope', 'Irequest', '$sce', 'Categorys', '$http', function ($scope, Irequest, $sce, Categorys, $http) {;

    $scope.contact = contactx;
    $scope.Contactar = function (event) {
        if (!$scope.myform2.$valid) {
            return;
        }
        event.preventDefault();
        Irequest.make('POST', '/Home/Contact', $scope.contact).then(function (data) {

            if (data.success) {
                $http({
                    url: "/Home/Msg",
                    method: "POST",
                    params: { msg: "Mensaje Enviado"}
                });
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