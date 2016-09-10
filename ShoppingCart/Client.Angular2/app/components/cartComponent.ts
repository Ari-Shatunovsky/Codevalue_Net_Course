import {Component, Inject} from "@angular/core";
import {Cart, Product} from "../models/models";
import {DataService} from "../services/dataService";

@Component({
    selector: "cart",
    templateUrl: "app/components/cartComponent.html",
    inputs: ['cart']
})

export class CartComponent {
    public boundAddItem: Function;

    public ngOnInit(){
        this.boundAddItem = this.addItem.bind(this);
    }
    public cart: Cart;
    public newProduct: Product;
    public total(){
        if(this.cart){
            var total = 0;
            this.cart.products.map(p => {total += p.price})
        }
        return total.toFixed(1);
    }

    public save() {
        this.dataService.saveCart(this.cart);
    }

     public addItem(a){
        this.cart.products.push(this.newProduct);
    }
    private stam = ["aaaa", "bbbbb", "ccccc"];

    private searchApi(): string  {
        return `http://localhost:16888/api/products/search/${this.cart.shop.id}?searchTerm=:keyword`
    }

    constructor(private dataService: DataService){
        this.dataService.initCarts();
    }
    // private searchApi = "https://maps.googleapis.com/maps/api/geocode/json?address=:keyword"
}

