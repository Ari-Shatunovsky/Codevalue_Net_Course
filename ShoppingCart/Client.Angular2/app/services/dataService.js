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
var models_1 = require("../models/models");
var apiService_1 = require("./apiService");
var DataService = (function () {
    function DataService(apiService) {
        this.apiService = apiService;
    }
    DataService.prototype.initCarts = function () {
        var _this = this;
        this.shops = [
            { id: 1, name: "Victory Lod", brand: models_1.ShopBrand.Victory, branchId: 10 },
            { id: 2, name: "YBitan Ashkelon", brand: models_1.ShopBrand.YBitan, branchId: 1 },
            { id: 3, name: "Coob Jerusaleem", brand: models_1.ShopBrand.Coob, branchId: 18 },
        ];
        if (!this.currentCarts && !this.savedCarts) {
            this.apiService.getSavedCarts().subscribe(function (carts) {
                _this.savedCarts = carts;
            });
            this.apiService.getEmptyCarts().subscribe(function (carts) {
                _this.currentCarts = carts;
            });
        }
    };
    DataService.prototype.isMaxProduct = function (product) {
        var index = -1;
        var result = true;
        this.currentCarts.forEach(function (cart) {
            if (cart.products.indexOf(product) !== -1) {
                index = cart.products.indexOf(product);
            }
        });
        this.currentCarts.forEach(function (cart) {
            if (cart.products[index] && cart.products[index].price > product.price) {
                result = false;
            }
        });
        return result;
    };
    DataService.prototype.isMinProduct = function (product) {
        var index = -1;
        var result = true;
        this.currentCarts.forEach(function (cart) {
            if (cart.products.indexOf(product) !== -1) {
                index = cart.products.indexOf(product);
            }
        });
        this.currentCarts.forEach(function (cart) {
            if (cart.products[index] && cart.products[index].price < product.price) {
                result = false;
            }
        });
        return result;
    };
    DataService.prototype.isMaxCart = function (cart) {
        var result = true;
        var total = 0;
        cart.products.forEach(function (product) { total += product.price; });
        this.currentCarts.forEach(function (c) {
            if (c !== cart) {
                var t = 0;
                c.products.forEach(function (product) { t += product.price; });
                if (t > total) {
                    result = false;
                }
            }
        });
        return result;
    };
    DataService.prototype.isMinCart = function (cart) {
        var result = true;
        var total = 0;
        cart.products.forEach(function (product) { total += product.price; });
        this.currentCarts.forEach(function (c) {
            if (c !== cart) {
                var t = 0;
                c.products.forEach(function (product) { t += product.price; });
                if (t < total) {
                    result = false;
                }
            }
        });
        return result;
    };
    DataService.prototype.generateRandomCarts = function () {
        var _this = this;
        this.apiService.getRandomCarts().subscribe(function (carts) {
            _this.currentCarts = carts;
        });
    };
    DataService.prototype.generateEmptyCarts = function () {
        var _this = this;
        this.apiService.getEmptyCarts().subscribe(function (carts) {
            _this.currentCarts = carts;
        });
    };
    DataService.prototype.deleteProduct = function (product) {
        this.currentCarts.forEach(function (cart) {
            var index = cart.products.indexOf(product);
            if (index != -1) {
                cart.products.splice(index, 1);
            }
        });
    };
    DataService.prototype.setMainCart = function (cart) {
        var index = this.currentCarts.indexOf(cart);
        var oldMain = this.currentCarts[0];
        this.currentCarts[index] = oldMain;
        this.currentCarts[0] = cart;
    };
    DataService.prototype.setCurrentCart = function (cart) {
        this.currentCarts = [cart];
        this.synchronizeCarts();
    };
    DataService.prototype.getCurrentCarts = function () {
        return this.currentCarts;
    };
    DataService.prototype.getSavedCarts = function () {
        return this.savedCarts;
    };
    DataService.prototype.saveCart = function (cart) {
        var _this = this;
        this.apiService.saveCart(cart).subscribe(function (r) {
            _this.apiService.getSavedCarts().subscribe(function (carts) {
                _this.savedCarts = carts;
            });
        });
    };
    DataService.prototype.reassignProduct = function (oldProduct, newProduct) {
        var _this = this;
        var index = -1;
        var similarProduct;
        this.currentCarts.forEach(function (cart) {
            if (cart.products.indexOf(oldProduct) >= 0) {
                index = cart.products.indexOf(oldProduct);
                similarProduct = cart.products[index];
            }
        });
        var originalProduct = this.currentCarts[0].products[index];
        this.apiService.reassignProduct(originalProduct, newProduct, oldProduct).subscribe(function () {
            _this.synchronizeCarts();
        });
    };
    DataService.prototype.replaceProduct = function (oldProduct, newProduct) {
        var index = -1;
        this.currentCarts.forEach(function (cart) {
            if (cart.products.indexOf(oldProduct) >= 0) {
                index = cart.products.indexOf(oldProduct);
                cart.products[index] = newProduct;
            }
        });
    };
    DataService.prototype.synchronizeCarts = function () {
        var _this = this;
        var shops = [];
        this.shops.forEach(function (shop) {
            if (shop.id !== _this.currentCarts[0].shop.id) {
                shops.push(shop);
            }
        });
        this.apiService.synchronizeCarts(this.currentCarts[0], shops).subscribe(function (carts) {
            for (var i = 0; i < carts.length; i++) {
                _this.currentCarts[i + 1] = carts[i];
            }
            _this.currentCarts[0].products.sort(function (p1, p2) { return (p1.name > p2.name) ? 1 : ((p1.name > p2.name) ? -1 : 0); });
        });
    };
    DataService = __decorate([
        core_1.Injectable(),
        __param(0, core_1.Inject(apiService_1.ApiService)), 
        __metadata('design:paramtypes', [Object])
    ], DataService);
    return DataService;
}());
exports.DataService = DataService;
//# sourceMappingURL=dataService.js.map