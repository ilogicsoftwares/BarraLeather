angular.module('Services', [])
.factory("Irequest", ['$window', '$http', '$q', function ($window, $http, $q) {

    var make = function (method, controllerAction, datax) {
        var def = $q.defer();

        $http({
            url: controllerAction,
            method: method,
            data: JSON.stringify(datax)
        })
.then(function (response) {
    def.resolve(response.data);
},
function (response) { // optional
    def.reject(response);
});



    return def.promise;


    }
    return {
        make: make
    }
}]).factory("Categorys", ['$window', '$http', '$q', 'Irequest', function ($window, $http, $q, Irequest) {


    var getAll = function () {

        return Irequest.make('get', '/Productos/Categorys', {});
    }

    return {
        getAll:getAll
    }

}]).factory("Productos", ['$window', '$http', '$q', 'Irequest', function ($window, $http, $q, Irequest) {

    var getAll = function () {

        return Irequest.make('post', '/Productos/GetAll', {});
    }

    var getProduct = function (id) {
        var codigo = { id: id };
        return Irequest.make('post', '/Productos/Get', codigo);
    }
    var getByCategory = function (categoryId) {
        var category = { id: categoryId };
        return Irequest.make('post', '/Productos/GetCategory', category);
    }
    var getByStatus = function (statusId) {
        var status = { id: statusId };
        return Irequest.make('post', '/Productos/GetStatus', status);
    }

    return {
        getAll: getAll,
        getProduct: getProduct,
        getByCategory: getByCategory,
        getByStatus: getByStatus
    }

}]);
