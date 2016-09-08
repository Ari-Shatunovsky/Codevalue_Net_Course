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
var productService_1 = require("../services/productService");
var AppComponent = (function () {
    function AppComponent(productService) {
        var _this = this;
        this.productService = productService;
        this.productService.getRandomCarts().subscribe(function (r) { _this.carts = r; });
        var i = 0;
    }
    AppComponent.prototype.synchronizeCarts = function () {
        var _this = this;
        var shops = [];
        for (var i = 1; i < this.carts.length; i++) {
            shops.push(this.carts[i].shop);
        }
        this.productService.synchronizeCarts(this.carts[0], shops).subscribe(function (r) {
            for (var i = 0; i < r.length; i++) {
                _this.carts[i + 1] = r[i];
            }
        });
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: "app",
            templateUrl: "app/components/app.html",
            providers: [productService_1.ProductService]
        }), 
        __metadata('design:paramtypes', [productService_1.ProductService])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.js.map