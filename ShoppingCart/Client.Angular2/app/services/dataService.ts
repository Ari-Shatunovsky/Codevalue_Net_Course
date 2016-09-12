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
        this.apiService.reassignProduct(originalProduct, newProduct).subscribe(() => {
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
        // for(let i = 1; i < this.currentCarts.length; i++){
        //     shops.push(this.currentCarts[i].shop);
        // }
        this.apiService.synchronizeCarts(this.currentCarts[0], shops).subscribe((carts) => {
            for(let i = 0; i < carts.length; i++){
                this.currentCarts[i + 1] = carts[i];
            }
        });
    }
}