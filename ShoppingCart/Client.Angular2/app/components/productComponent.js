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
var core_1 = require("@angular/core");
var models_1 = require("../models/models");
var dataService_1 = require("../services/dataService");
var ProductComponent = (function () {
    function ProductComponent(dataService) {
        this.dataService = dataService;
        this.isReplaceMode = false;
    }
    ProductComponent.prototype.getUnitsAbbr = function () {
        if (this.product.units == models_1.Units.Kilogramm) {
            return "kg";
        }
        if (this.product.units == models_1.Units.Liter) {
            return "l";
        }
        return "un";
    };
    ProductComponent.prototype.delete = function () {
        this.dataService.deleteProduct(this.product);
    };
    ProductComponent.prototype.getPricePerUnits = function () {
        if (this.product.pricePerUnit.toFixed) {
            return this.product.pricePerUnit.toFixed(1);
        }
        return "";
    };
    ProductComponent.prototype.synchronizeCarts = function () {
        this.dataService.synchronizeCarts();
    };
    ProductComponent.prototype.isEmpty = function () {
        return this.product.id == 0;
    };
    ProductComponent.prototype.toggleReplace = function () {
        this.isReplaceMode = !this.isReplaceMode;
    };
    ProductComponent.prototype.reassign = function () {
        this.dataService.reassignProduct(this.product, this.newProduct);
        this.toggleReplace(); //this.product = this.newProduct;
    };
    ProductComponent.prototype.replace = function () {
        this.dataService.replaceProduct(this.product, this.newProduct);
        this.toggleReplace();
    };
    ProductComponent.prototype.getNewProductName = function () {
        if (!this.newProduct) {
            return "";
        }
        else {
            return this.newProduct.name;
        }
    };
    ProductComponent.prototype.isMainCart = function () {
        return this.dataService.getCurrentCarts()[0].shop.id == this.product.shop.id;
    };
    ProductComponent.prototype.cancel = function () {
        this.newProduct = null;
        this.isReplaceMode = false;
    };
    ProductComponent.prototype.searchApi = function () {
        return "http://localhost:16888/api/products/search/" + this.product.shop.id + "?searchTerm=:keyword";
    };
    ProductComponent = __decorate([
        core_1.Component({
            selector: "product",
            templateUrl: "app/components/productComponent.html",
            inputs: ["product"],
        }), 
        __metadata('design:paramtypes', [dataService_1.DataService])
    ], ProductComponent);
    return ProductComponent;
}());
exports.ProductComponent = ProductComponent;
//# sourceMappingURL=productComponent.js.map