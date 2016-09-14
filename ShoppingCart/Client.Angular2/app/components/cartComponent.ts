import {Component, Inject} from "@angular/core";
import {Cart, Product} from "../models/models";
import {DataService} from "../services/dataService";

@Component({
    selector: "cart",
    templateUrl: "app/components/cartComponent.html",
    inputs: ['cart']
})

export class CartComponent {
    private boundAddItem: Function;

    private ngOnInit(){
        this.boundAddItem = this.addItem.bind(this);
    }

    private cart: Cart;
    private newProduct: Product;

    private total(){
        if(this.cart){
            var total = 0;
            this.cart.products.map(p => {total += p.price})
        }
        return total.toFixed(1);
    }

    private synchronizeCarts(){
        this.dataService.synchronizeCarts();
    }

    private setMain(){
        this.dataService.setMainCart(this.cart);
    }

    private isMax(){
        return this.dataService.isMaxCart(this.cart);
    }

    private isMin(){
        return this.dataService.isMinCart(this.cart);
    }

    private isMainCart(){
        return this.dataService.getCurrentCarts().indexOf(this.cart) == 0;
    }

    private save() {
        this.dataService.saveCart(this.cart);
    }

    private addItem(a){
        this.cart.products.push(this.newProduct);
    }

    private searchApi(): string  {
        return `http://localhost:16888/api/products/search/${this.cart.shop.id}?searchTerm=:keyword`
    }

    constructor(private dataService: DataService){

    }
}

