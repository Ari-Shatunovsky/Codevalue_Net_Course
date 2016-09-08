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
var ProductComponent = (function () {
    function ProductComponent() {
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
    ProductComponent.prototype.getPricePerUnits = function () {
        if (this.product.pricePerUnit.toFixed) {
            return this.product.pricePerUnit.toFixed(1);
        }
        return "";
    };
    ProductComponent = __decorate([
        core_1.Component({
            selector: "product",
            templateUrl: "app/components/productComponent.html",
            inputs: ["product"]
        }), 
        __metadata('design:paramtypes', [])
    ], ProductComponent);
    return ProductComponent;
}());
exports.ProductComponent = ProductComponent;
//# sourceMappingURL=productComponent.js.map