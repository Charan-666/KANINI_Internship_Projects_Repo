import { Component, computed, inject } from '@angular/core';
import { foodService } from '../foodService';
import { Food } from '../../Models/Food';

import { ActivatedRoute, Router, RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-food-detail',
  imports: [RouterModule,RouterLink],
  templateUrl: './food-detail.html',
  styleUrl: './food-detail.css'
})
export class FoodDetail {

 food: Food={
  foodId: 0,
  name: " ",
  price: 0,
  category: " ",
  imageurl: " "

    };

  constructor(private route: ActivatedRoute,
              private foodService: foodService,private router:Router
            ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id')); 
    if (id) {
      this.foodService.getFood(id).subscribe({
        next: (data) => this.food = data,
        error: (err) => console.error("Error loading food detail:", err)
      });
    }
  
  }

  AddFood(food: Food) {
    this.foodService.addToCart(food.foodId).subscribe(() => {
      alert(`${food.name} added to cart`);
      this.router.navigate(['/Cart']);
    });
  }







// foods:Observable<Food>;
// constructor(private fservice:foodService){
// this.foods=this.fservice.getfoodbyid();
// }

// foodservice = inject(foodService);

// selectedfood = computed(()=>this.foodservice.selectfood());


//  Add(food:Food){
//   this.foodservice.addtocart(food);
//  }

//   constructor() {}
  
}
