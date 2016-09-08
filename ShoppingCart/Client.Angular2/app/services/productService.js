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
var ProductService = (function () {
    function ProductService(http) {
        this.http = http;
        this.baseUrl = "http://localhost:16888/api/products";
    }
    ProductService.prototype.getRandomCarts = function () {
        var carts = this.http.get(this.baseUrl + "/carts")
            .map(function (responseData) { return responseData.json(); })
            .map(this.toCarts);
        return carts;
    };
    ProductService.prototype.synchronizeCarts = function (cart, shops) {
        var headers = new http_1.Headers();
        headers.append("Content-Type", "application/json");
        //
        // headers.append("Accept", 'application/json');
        // headers.append("Access-Control-Allow-Origin", "*");
        // headers.append("Access-Control-Allow-Headers", 'x-requested-with');
        var requestOptions = new http_1.RequestOptions({
            method: http_1.RequestMethod.Post,
            url: this.baseUrl + "/similar",
            headers: headers,
            body: { cart: cart, shops: shops }
        });
        var carts = this.http.post(this.baseUrl + "/similar", JSON.stringify({ cart: cart, shops: shops }), { headers: headers })
            .map(function (responseData) { return responseData.json(); })
            .map(this.toCarts);
        return carts;
    };
    ProductService.prototype.toCarts = function (carts) {
        return carts.map(function (c) {
            var cart = {
                products: c.products.map(function (p) { return toProduct(p); }),
                shop: toShopInfo(c.shop)
            };
            return cart;
        });
        function toProduct(p) {
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
                    quantity: 0
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
    ProductService = __decorate([
        core_1.Injectable(),
        __param(0, core_1.Inject(http_1.Http)), 
        __metadata('design:paramtypes', [Object])
    ], ProductService);
    return ProductService;
}());
exports.ProductService = ProductService;
//# sourceMappingURL=productService.js.map