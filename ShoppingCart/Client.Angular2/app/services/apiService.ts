
import {Injectable, Inject} from "@angular/core";
import {Http, Headers, Response, RequestOptions, RequestMethod, Request} from "@angular/http";
import {Cart, Product, ShopInfo, Units} from "../models/models";
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Rx";

@Injectable()

export class ApiService {
    private baseUrl = "http://localhost:16888/api/products";
    constructor(@Inject(Http) private http){

    }
    getRandomCarts(): Observable<Cart[]>{
        let carts = this.http.get(`${this.baseUrl}/randomcarts`)
            .map(responseData => responseData.json())
            .map(this.toCarts);
        return carts;
    }

    replaceProduct(originalProduct: Product, similarProduct: Product): Observable<boolean>{
        var headers = new Headers();
        headers.append("Content-Type", "application/json");

        return this.http.post(`${this.baseUrl}/connectproducts`,JSON.stringify([originalProduct, similarProduct]), {headers: headers});
    }

    saveCart(cart: Cart){
        var headers = new Headers();
        headers.append("Content-Type", "application/json");

        return this.http.post(`${this.baseUrl}/cart`,JSON.stringify(cart), {headers: headers});
    }

    getSavedCarts(): Observable<Cart[]>{
        let carts = this.http.get(`${this.baseUrl}/carts`)
            .map(responseData => responseData.json())
            .map(this.toCarts);
        return carts;
    }

    synchronizeCarts(cart: Cart, shops: ShopInfo[]): Observable<Cart[]>{
        var headers = new Headers();
        headers.append("Content-Type", "application/json");

        // headers.append('Access-Control-Allow-Headers', 'Content-Type');
        // headers.append('Access-Control-Allow-Methods', 'POST');
        // headers.append('Access-Control-Allow-Origin', '*');
        // headers.append("Allow-Orgin", "http://localhost:3000");

        // var requestOptions = new RequestOptions({
        //     method: RequestMethod.Post,
        //     url: `${this.baseUrl}/similar`,
        //     headers: headers,
        //     body: {cart: cart, shops: shops }
        // });

        let carts = this.http.post(`${this.baseUrl}/similar`,JSON.stringify({cart: cart, shops: shops }), {headers: headers})
            .map(responseData => responseData.json())
            .map(this.toCarts);
        return carts;
    }

    toCarts(carts: any[]): Cart[]{
        return carts.map(c => {
            let cart = <Cart>{
                id: c.id,
                name: c.name,
                products: c.products.map(p => toProduct(p, c.shop)),
                shop: toShopInfo(c.shop)
            }
            return cart;
        });

        function toProduct(p: any, s?: any): Product{
            if(p){
                let product = <Product>{
                    id: p.id,
                    name: p.name,
                    price: p.price,
                    pricePerUnit: p.pricePerUnit,
                    productId: p.productId,
                    manufactureCountry: p.manufactureCountry,
                    manufactureName: p.manufactureName,
                    units: p.units,
                    quantity: p.quantity,
                    shop: toShopInfo(p.shop)
                }
                return product;
            } else {
                let product = <Product>{
                    id: 0,
                    name: "Not Found",
                    price: 0,
                    pricePerUnit: 0,
                    productId: "",
                    manufactureCountry: "",
                    manufactureName: "",
                    units: Units.Kilogramm,
                    quantity: 0,
                    shop: s? toShopInfo(s) : null
                }
                return product;
            }
        }

        function toShopInfo(s: any): ShopInfo{
            let shopInfo = <ShopInfo>{
                branchId : s.branchId,
                brand: s.brand,
                id: s.id,
                name: s.name
            }

            return shopInfo;
        }
    }
}