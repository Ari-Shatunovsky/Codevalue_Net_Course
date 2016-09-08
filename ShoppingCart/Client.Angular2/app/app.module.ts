import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppComponent } from "./components/app";
import {ProductComponent} from "./components/productComponent";
import {CartComponent} from "./components/cartComponent";
import {ProductService} from "./services/productService";
import {Http, HttpModule, HTTP_PROVIDERS} from "@angular/http";
import {Ng2AutoCompleteModule} from "ng2-ui/dist/ng2-auto-complete/ng2AutoComplete.module";
import {FormsModule} from "@angular/forms";

@NgModule({
    imports: [ BrowserModule, FormsModule, HttpModule, Ng2AutoCompleteModule ],
    declarations: [ AppComponent, CartComponent, ProductComponent],
    bootstrap: [AppComponent],
})

export class AppModule {}