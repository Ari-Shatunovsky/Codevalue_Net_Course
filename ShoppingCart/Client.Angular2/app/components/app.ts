import {Component, Inject} from "@angular/core";
import {ProductService} from "../services/productService";
import {Cart} from "../models/models";
import forEach = require("core-js/fn/array/for-each");

@Component({
    selector: "app",
    templateUrl: "app/components/app.html",
    providers: [ProductService]
})

export class AppComponent {
    private carts: Cart[];
    private synchronizeCarts(){
        var shops = [];
        for(let i = 1; i < this.carts.length; i++){
            shops.push(this.carts[i].shop);
        }
        this.productService.synchronizeCarts(this.carts[0], shops).subscribe((r) => {

            for(let i = 0; i < r.length; i++){
                this.carts[i + 1] = r[i];
            }
        });
    }

    constructor(private productService: ProductService){
         this.productService.getRandomCarts().subscribe((r) => {this.carts = r});
        var i = 0;
    }
}