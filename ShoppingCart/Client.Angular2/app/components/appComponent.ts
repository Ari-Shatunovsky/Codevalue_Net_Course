import {Component, Inject} from "@angular/core";
import {ApiService} from "../services/apiService";
import {Cart, Product} from "../models/models";
import forEach = require("core-js/fn/array/for-each");
import {DataService} from "../services/dataService";

@Component({
    selector: "app",
    templateUrl: "app/components/appComponent.html",
    providers: [DataService, ApiService]
})

export class AppComponent {
    private currentCarts(): Cart[]{
        return this.dataService.getCurrentCarts();
    };

    private savedCarts(): Cart[]{
        return this.dataService.getSavedCarts();
    };

    private selectCart(cart: Cart){
        this.dataService.setCurrentCart(cart);
    }

    constructor(private dataService: DataService){
         this.dataService.initCarts();
    }
}