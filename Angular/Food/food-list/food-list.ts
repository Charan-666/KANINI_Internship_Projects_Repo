import { Component, inject } from '@angular/core';
import { foodService } from '../foodService';
import { Food } from '../../Models/Food';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

import { Router, RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-food-list',
  imports: [CommonModule,RouterModule,RouterLink],
  templateUrl: './food-list.html',
  styleUrl: './food-list.css'
})
export class FoodList {
// private foodservice =inject(foodService)
// foods:Food[]=[];

// ngOnInit(){
//   this.foodservice.getfoods().subscribe(data=>this.foods=data);
// }

foods:Observable<Food[]>;
constructor(private fservice:foodService,private router:Router){
this.foods=this.fservice.getfoods();
}



// private api = inject(foodService);
// private router=inject(Router);
//   foods: Food[] = [];

//   ngOnInit() {
//     this.api.getfoods().subscribe(data => this.foods = data);
//   }

 
  viewDetails(food: Food) {
  this.router.navigate(['/Food', food.foodId]);

  }

  AddFood(food: Food) {
    this.fservice.addToCart(food.foodId).subscribe(() => {
      alert(`${food.name} added to cart`);
      this.router.navigate(['/Cart']);
    });
  }





// viewdetail(food:Food){
//   this.foodservice.Onselectedfood(food);
// }


}
