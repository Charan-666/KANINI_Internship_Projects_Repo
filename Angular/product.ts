export interface Product{
    productId: number;
    productName: string;
    price: number;
    description: string;
    stock: number;
    onSale:boolean;
    rating:number;
   reviews:{user:string;comment:string}[]

}
