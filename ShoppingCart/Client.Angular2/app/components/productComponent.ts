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


    private getUnitsAbbr(): string{
        if(this.product.units == Units.Kilogramm){
            return "kg";
        }
        if(this.product.units == Units.Liter){
            return "l";
        }
        return "un"
    }

    private delete(){
        this.dataService.deleteProduct(this.product);
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

    private isMin(): boolean {
        return this.dataService.isMinProduct(this.product);
    }

    private isMax(): boolean {
        return this.dataService.isMaxProduct(this.product);
    }

    private toggleReplace() {
        this.isReplaceMode = !this.isReplaceMode;
    }

    private reassign() {
        this.dataService.reassignProduct(this.product, this.newProduct);
        this.toggleReplace();//this.product = this.newProduct;
    }

    private replace() {
        this.dataService.replaceProduct(this.product, this.newProduct);
        this.toggleReplace();
    }

    private getNewProductName() {
        if(!this.newProduct){
            return "";
        } else {
            return this.newProduct.name;
        }
    }

    private isMainCart(): boolean{
        return this.dataService.getCurrentCarts()[0].shop.id == this.product.shop.id;
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

