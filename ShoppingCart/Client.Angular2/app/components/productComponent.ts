import {Component, Inject} from "@angular/core";
import {Product, Units} from "../models/models";
import {DataService} from "../services/dataService";

@Component({
    selector: "product",
    templateUrl: "app/components/productComponent.html",
    inputs: ["product"],
})

export class ProductComponent {
    public product: Product;
    public newProduct: Product;
    private isReplaceMode: boolean = false;
    public boundSwapItem: Function;


    // public ngOnInit(){
    //     this.boundSwapItem = this.swapItem.bind(this);
    //
    // }

    public swapItem(a){
        // this.cart.products.push(this.newProduct);
    }

    private getUnitsAbbr(): string{
        if(this.product.units == Units.Kilogramm){
            return "kg";
        }
        if(this.product.units == Units.Liter){
            return "l";
        }
        return "un"
    }

    private getPricePerUnits(): string {
        if(this.product.pricePerUnit.toFixed){
            return this.product.pricePerUnit.toFixed(1);
        }
        return "";
    }

    private synchronizeCarts(){
        this.dataService.synchronizeCarts();
    }

    private isEmpty(): boolean {
        return this.product.id == 0;
    }

    private toggleReplace() {
        this.isReplaceMode = !this.isReplaceMode;
    }

    private replace() {
        this.dataService.replaceProduct(this.product, this.newProduct);
        this.toggleReplace();//this.product = this.newProduct;
    }

    private getNewProductName() {
        if(!this.newProduct){
            return "";
        } else {
            return this.newProduct.name;
        }
    }

    private cancel() {
        this.newProduct = null;
        this.isReplaceMode = false;
    }

    private searchApi(): string  {
        return `http://localhost:16888/api/products/search/${this.product.shop.id}?searchTerm=:keyword`
    }

    constructor(private dataService: DataService){    }

}

