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
var apiService_1 = require("../services/apiService");
var dataService_1 = require("../services/dataService");
var AppComponent = (function () {
    function AppComponent(dataService) {
        this.dataService = dataService;
        this.dataService.initCarts();
    }
    AppComponent.prototype.currentCarts = function () {
        return this.dataService.getCurrentCarts();
    };
    ;
    AppComponent.prototype.savedCarts = function () {
        return this.dataService.getSavedCarts();
    };
    ;
    AppComponent.prototype.synchronizeCarts = function () {
        this.dataService.synchronizeCarts();
    };
    AppComponent.prototype.selectCart = function (cart) {
        this.dataService.setCurrentCart(cart);
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: "app",
            templateUrl: "app/components/appComponent.html",
            providers: [dataService_1.DataService, apiService_1.ApiService]
        }), 
        __metadata('design:paramtypes', [dataService_1.DataService])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=appComponent.js.map