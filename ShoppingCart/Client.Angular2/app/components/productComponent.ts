import {Component, Inject} from "@angular/core";
import {Product, Units} from "../models/models";

@Component({
    selector: "product",
    templateUrl: "app/components/productComponent.html",
    inputs: ["product"]
})

export class ProductComponent {
    public product: Product;

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

}

