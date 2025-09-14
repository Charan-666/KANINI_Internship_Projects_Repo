import { Component, computed, inject } from '@angular/core';
import { foodService } from '../foodService';
import { CartItem } from '../../Models/Food';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-food-cart',
  imports: [RouterModule,RouterLink],
  templateUrl: './food-cart.html',
  styleUrl: './food-cart.css'
})
export class FoodCart {

 private api = inject(foodService);
  cartItems: CartItem[] = [];
  total = 0;

  ngOnInit() {
    this.loadCart();
  }

  loadCart() {
    this.api.getCart().subscribe(data => {
      this.cartItems = data;
      this.total = data.reduce((sum, item) => sum + (item.food?.price ?? 0) * item.quantity, 0);
    });
  }

  removeItem(id: number) {
    this.api.removeFromCart(id).subscribe(() => this.loadCart());
  }

  clearCart() {
    this.api.clearCart().subscribe(() => this.loadCart());
  }



// foodservice = inject(foodService);

// cartItems=computed(()=> this.foodservice.cart());


// total = computed(()=>this.foodservice.cart().reduce((sum,food)=>sum+food.price,0) );

}
