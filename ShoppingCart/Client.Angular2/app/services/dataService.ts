import {Injectable, Inject} from "@angular/core";
import {Http} from "@angular/http";
import {Cart, Product, ShopInfo, ShopBrand} from "../models/models";
import {Observable} from "rxjs/Rx";
import {ApiService} from "./apiService";
@Injectable()

export class DataService {
    private currentCarts: Cart[];
    private savedCarts: Cart[];
    private shops: ShopInfo[];

    constructor(@Inject(ApiService) private apiService) {

    }

    public initCarts() {
        this.shops = [
            {id: 1, name: "Victory Lod", brand: ShopBrand.Victory, branchId: 10},
            {id: 2, name: "YBitan Ashkelon", brand: ShopBrand.YBitan, branchId: 1},
            {id: 3, name: "Coob Jerusaleem", brand: ShopBrand.Coob, branchId: 18},
        ]
        if(!this.currentCarts && !this.savedCarts){
            this.apiService.getSavedCarts().subscribe((carts) => {
                this.savedCarts = carts;
            });
            this.apiService.getEmptyCarts().subscribe((carts) => {
                this.currentCarts = carts;
            });
        }
    }

    public isMaxProduct(product: Product): boolean {
        var index = -1;
        var result = true;
        this.currentCarts.forEach((cart) => {
             if(cart.products.indexOf(product)  !== -1){
                 index = cart.products.indexOf(product);
             }
        });
        this.currentCarts.forEach((cart) => {
            if(cart.products[index] && cart.products[index].price > product.price){
                result = false;
            }
        });
        return result;
    }

    public isMinProduct(product: Product): boolean {
        var index = -1;
        var result = true;
        this.currentCarts.forEach((cart) => {
            if(cart.products.indexOf(product)  !== -1){
                index = cart.products.indexOf(product);
            }
        });
        this.currentCarts.forEach((cart) => {
            if(cart.products[index] && cart.products[index].price < product.price){
                result = false;
            }
        });
        return result;
    }

    public isMaxCart(cart): boolean {
        var result = true;
        var total = 0;
        cart.products.forEach((product) => {total += product.price});
        this.currentCarts.forEach((c) => {
            if(c !== cart){
                var t = 0;
                c.products.forEach((product) => {t += product.price});
                if(t > total){
                    result = false;
                }
            }
        });
        return result;
    }

    public isMinCart(cart): boolean {
        var result = true;
        var total = 0;
        cart.products.forEach((product) => {total += product.price});
        this.currentCarts.forEach((c) => {
            if(c !== cart){
                var t = 0;
                c.products.forEach((product) => {t += product.price});
                if(t < total){
                    result = false;
                }
            }
        });
        return result;
    }

    public generateRandomCarts(){
        this.apiService.getRandomCarts().subscribe((carts) => {
            this.currentCarts = carts;
        });
    }

    public generateEmptyCarts(){
        this.apiService.getEmptyCarts().subscribe((carts) => {
            this.currentCarts = carts;
        });
    }

    public deleteProduct(product: Product){
        this.currentCarts.forEach((cart) => {
            var index = cart.products.indexOf(product)
            if(index != -1){
                cart.products.splice(index , 1);
            }
        });
    }

    public setMainCart(cart: Cart){
        var index = this.currentCarts.indexOf(cart);
        var oldMain = this.currentCarts[0];
        this.currentCarts[index] = oldMain;
        this.currentCarts[0] = cart;
    }

    public setCurrentCart(cart: Cart){
        this.currentCarts = [cart];
        this.synchronizeCarts();
    }

    public getCurrentCarts(): Cart[]{
        return this.currentCarts;
    }

    public getSavedCarts(): Cart[]{
        return this.savedCarts;
    }

    public saveCart(cart: Cart) {
        this.apiService.saveCart(cart).subscribe((r) => {
            this.apiService.getSavedCarts().subscribe((carts) => {
                this.savedCarts = carts;
            });
        });
    }

    public reassignProduct(oldProduct: Product, newProduct: Product){
        var index = -1;
        var similarProduct;
        this.currentCarts.forEach((cart) => {
            if(cart.products.indexOf(oldProduct) >= 0){
                index = cart.products.indexOf(oldProduct);
                similarProduct = cart.products[index];
            }
        });
        var originalProduct = this.currentCarts[0].products[index];
        this.apiService.reassignProduct(originalProduct, newProduct, oldProduct).subscribe(() => {
            this.synchronizeCarts();
        });
    }

    public replaceProduct(oldProduct: Product, newProduct: Product){
        var index = -1;
        this.currentCarts.forEach((cart) => {
            if(cart.products.indexOf(oldProduct) >= 0){
                index = cart.products.indexOf(oldProduct);
                cart.products[index] = newProduct;
            }
        });
    }
    
    public synchronizeCarts() {
        var shops = [];
        this.shops.forEach((shop) => {
            if(shop.id !== this.currentCarts[0].shop.id){
                shops.push(shop);
            }
        });
        this.apiService.synchronizeCarts(this.currentCarts[0], shops).subscribe((carts) => {
            for(let i = 0; i < carts.length; i++){
                this.currentCarts[i + 1] = carts[i];
            }
            this.currentCarts[0].products.sort((p1, p2) => {return (p1.name > p2.name) ? 1 : ((p1.name > p2.name) ? -1 : 0);});
        });
    }
}