import { inject, Injectable, signal } from '@angular/core';
import { CartItem, Food } from '../Models/Food';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class foodService {

private http = inject(HttpClient);
private baseurl = "https://localhost:7295/api";

getfoods():Observable<Food[]>{
  return this.http.get<Food[]>(`${this.baseurl}/Food`);
}


getFood(id: number): Observable<Food> {
    return this.http.get<Food>(`${this.baseurl}/Food/${id}`);
  }

getCart(): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.baseurl}/Cart`);
  }

  addToCart(foodId: number, quantity: number = 1): Observable<CartItem> {
    return this.http.post<CartItem>(`${this.baseurl}/Cart`, { foodId, quantity });
  }

  removeFromCart(itemId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseurl}/Cart/${itemId}`);
  }

  clearCart(): Observable<void> {
    return this.http.delete<void>(`${this.baseurl}/Cart/clear`);
  }


  //actually you get data by connecting to db but here we are hardcoding it
  
//   foods=signal<Food[]>([
//     {Id:1,Name:'Biriyani',price:200,category:'Non-Veg',imageurl:'biriyani.jpeg'},
//     {Id:2,Name:'Gobi Manjurian',price:100,category:'Fast-Food',imageurl:'gobi.jpeg'},
//     {Id:3,Name:'Idli-Vada',price:150,category:'South-Indian',imageurl:'idlivada.jpeg'},
//   ])

// cart=signal<Food[]>([])

// selectfood = signal<Food | null>(null);

//   Onselectedfood(food:Food){
//     this.selectfood.set(food);
//   }


// addtocart(food:Food){
//   this.cart.update(current=>[...current,food]);
   
// }

// clearcart(){
//   this.cart.set([]);
// }

// removefromcart(Id:number){
//   this.cart.update(current=>current.filter(f=>f.Id !== Id));
 
// }

//   constructor(){

//   }
}
