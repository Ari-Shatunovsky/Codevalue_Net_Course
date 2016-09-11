"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var models_1 = require("../models/models");
require('rxjs/add/operator/map');
var ApiService = (function () {
    function ApiService(http) {
        this.http = http;
        this.baseUrl = "http://localhost:16888/api/products";
    }
    ApiService.prototype.getRandomCarts = function () {
        var carts = this.http.get(this.baseUrl + "/randomcarts")
            .map(function (responseData) { return responseData.json(); })
            .map(this.toCarts);
        return carts;
    };
    ApiService.prototype.replaceProduct = function (originalProduct, similarProduct) {
        var headers = new http_1.Headers();
        headers.append("Content-Type", "application/json");
        return this.http.post(this.baseUrl + "/connectproducts", JSON.stringify([originalProduct, similarProduct]), { headers: headers });
    };
    ApiService.prototype.saveCart = function (cart) {
        var headers = new http_1.Headers();
        headers.append("Content-Type", "application/json");
        return this.http.post(this.baseUrl + "/cart", JSON.stringify(cart), { headers: headers });
    };
    ApiService.prototype.getSavedCarts = function () {
        var carts = this.http.get(this.baseUrl + "/carts")
            .map(function (responseData) { return responseData.json(); })
            .map(this.toCarts);
        return carts;
    };
    ApiService.prototype.synchronizeCarts = function (cart, shops) {
        var headers = new http_1.Headers();
        headers.append("Content-Type", "application/json");
        var carts = this.http.post(this.baseUrl + "/similar", JSON.stringify({ cart: cart, shops: shops }), { headers: headers })
            .map(function (responseData) { return responseData.json(); })
            .map(this.toCarts);
        return carts;
    };
    ApiService.prototype.toCarts = function (carts) {
        return carts.map(function (c) {
            var cart = {
                id: c.id,
                name: c.name,
                products: c.products.map(function (p) { return toProduct(p, c.shop); }),
                shop: toShopInfo(c.shop)
            };
            return cart;
        });
        function toProduct(p, s) {
            if (p) {
                var product = {
                    id: p.id,
                    name: p.name,
                    price: p.price,
                    pricePerUnit: p.pricePerUnit,
                    productId: p.productId,
                    manufactureCountry: p.manufactureCountry,
                    manufactureName: p.manufactureName,
                    units: p.units,
                    quantity: p.quantity,
                    shop: toShopInfo(p.shop)
                };
                return product;
            }
            else {
                var product = {
                    id: 0,
                    name: "Not Found",
                    price: 0,
                    pricePerUnit: 0,
                    productId: "",
                    manufactureCountry: "",
                    manufactureName: "",
                    units: models_1.Units.Kilogramm,
                    quantity: 0,
                    shop: s ? toShopInfo(s) : null
                };
                return product;
            }
        }
        function toShopInfo(s) {
            var shopInfo = {
                branchId: s.branchId,
                brand: s.brand,
                id: s.id,
                name: s.name
            };
            return shopInfo;
        }
    };
    ApiService = __decorate([
        core_1.Injectable(),
        __param(0, core_1.Inject(http_1.Http)), 
        __metadata('design:paramtypes', [Object])
    ], ApiService);
    return ApiService;
}());
exports.ApiService = ApiService;
//# sourceMappingURL=apiService.js.map