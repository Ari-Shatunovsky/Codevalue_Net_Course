export enum Units {
    Unit = 1,
    Kilogramm,
    Liter
}

export enum ShopBrand {
    Victory = 1,
    YBitan,
    Coob,
}

export interface Product {
    id: number;
    productId: string;
    name: string;
    manufactureName: string;
    manufactureCountry: string;
    price: number;
    pricePerUnit: number;
    units: Units;
    quantity: number;
}

export interface ShopInfo {
    id: number;
    brand: ShopBrand;
    name: string;
    branchId: number;
}

export interface Cart {
    products: Product[];
    shop: ShopInfo;
}