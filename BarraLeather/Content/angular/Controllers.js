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
                //  window.location.replace("/Users/Validate");
                window.alert("Usuario Creado");
            } else {
                $scope.msg2 = data.errorMsg;
            }

        }, function () {
            $scope.msg2 = 'Error en conexion';
        })
    }
}]).controller('EditUserController', ['$scope', 'Irequest', function ($scope, Irequest) {

    $scope.user = ActualUser;
    $scope.user.fechanac =  $scope.user.fechanac!=null ? new Date(parseInt($scope.user.fechanac.substr(6))):null;
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
    $scope.cantidad = 1;
    $scope.newproducts1 = newProd.slice(0, 4);

    $scope.newproducts2 = newProd.slice(4, 8);
    Irequest.make("POST", "/Productos/GetRandom").then(function (data) {
        $scope.ramprod = data;
    })
    $scope.addItem = function () {
        Irequest.make("POST", "/Shop/AddItem", { producto: $scope.producto, cantidad: $scope.cantidad }).then(function (data) {
            if (!data.success) {
                if (data.errorMsg == "NotUser")
                {
                    window.location.replace("/Users/Register");
                }
                $scope.msg = data.errorMsg;
            }
            $scope.msg = "Se agrego al Carrito";

        })
    }
}]).controller('ShopController', ['$scope', 'Irequest', '$sce', function ($scope, Irequest, $sce) {
    $scope.shop = shopx;
    $scope.categorys = categorys;
    Irequest.make("POST", "/Productos/GetRandom").then(function (data) {
        $scope.ramprod = data;
    })
    $scope.category = category;

}]).controller('CartController', ['$scope', 'Irequest', '$sce', function ($scope, Irequest, $sce) {

    $scope.cart = [];
    var toDelete = [];
    Irequest.make("POST", "/Productos/GetRandom").then(function (data) {
        $scope.ramprod = data;
    })
    Irequest.make("GET", "/Shop/GetUserCart").then(function (data) {
        $scope.cart = data;
        $scope.total = function () {
            var a = 0
            $scope.cart.forEach(function (item) {
                a += item.cantidad * item.precio;
                
            })
            return a;
        }

    })

    $scope.remove = function (index,item) {
      
        Irequest.make("POST", "/Shop/DeleteItem", item)
            .then(function (data) {
                if (data.success)
                {
                    $scope.cart.splice(index, 1);
                } else {
                    window.alert(data.errorMsg);
                }

            }, function () {
                window.alert("Error de conexion");
            })

       
    }
    $scope.Modal = function (id, BoolShow) {
        var dis = "block";
        if (!BoolShow) {
            dis="none"
        }
        document.getElementById(id).style.display = dis;

    }
    $scope.Process = function () {
        Irequest.make("POST", "/Shop/Process").then(function (data) {
            if(!data.success)
            {
                window.alert("Error al procesar el pedido intente mas tarde");
            }
            window.location.replace("/Shop/Pedidos");
        })

    }

    
}]).controller('PedidosController', ['$scope', 'Irequest', '$sce', function ($scope, Irequest, $sce) {

    $scope.pedidos =pedidos;
   
    Irequest.make("POST", "/Productos/GetRandom").then(function (data) {
        $scope.ramprod = data;
    })
  
   


    $scope.Modal = function (id, BoolShow) {
        var dis = "block";
        if (!BoolShow) {
            dis = "none"
        }
        document.getElementById(id).style.display = dis;

    }

  

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