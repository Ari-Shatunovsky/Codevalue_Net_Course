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
var dataService_1 = require("../services/dataService");
var CartComponent = (function () {
    function CartComponent(dataService) {
        this.dataService = dataService;
    }
    CartComponent.prototype.ngOnInit = function () {
        this.boundAddItem = this.addItem.bind(this);
    };
    CartComponent.prototype.total = function () {
        if (this.cart) {
            var total = 0;
            this.cart.products.map(function (p) { total += p.price; });
        }
        return total.toFixed(1);
    };
    CartComponent.prototype.synchronizeCarts = function () {
        this.dataService.synchronizeCarts();
    };
    CartComponent.prototype.setMain = function () {
        this.dataService.setMainCart(this.cart);
    };
    CartComponent.prototype.isMax = function () {
        return this.dataService.isMaxCart(this.cart);
    };
    CartComponent.prototype.isMin = function () {
        return this.dataService.isMinCart(this.cart);
    };
    CartComponent.prototype.isMainCart = function () {
        return this.dataService.getCurrentCarts().indexOf(this.cart) == 0;
    };
    CartComponent.prototype.save = function () {
        this.dataService.saveCart(this.cart);
    };
    CartComponent.prototype.addItem = function (a) {
        this.cart.products.push(this.newProduct);
    };
    CartComponent.prototype.searchApi = function () {
        return "http://localhost:16888/api/products/search/" + this.cart.shop.id + "?searchTerm=:keyword";
    };
    CartComponent = __decorate([
        core_1.Component({
            selector: "cart",
            templateUrl: "app/components/cartComponent.html",
            inputs: ['cart']
        }), 
        __metadata('design:paramtypes', [dataService_1.DataService])
    ], CartComponent);
    return CartComponent;
}());
exports.CartComponent = CartComponent;
//# sourceMappingURL=cartComponent.js.map