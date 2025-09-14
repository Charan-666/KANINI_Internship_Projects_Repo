export interface Food{
    foodId:number;
    name:string;
    price:number;
    category:string;
    imageurl:string;
}

export interface CartItem {
  id?: number;
  foodId: number;
  quantity: number;
  food?: Food;
}

export interface Order {
  id: number;
  orderDate: string
  cartItems: CartItem[];
}