import { Routes } from '@angular/router';
import {FoodCart} from './food-cart/food-cart';
import { FoodDetail } from  './food-detail/food-detail';
import { FoodList } from './food-list/food-list';

export const routes: Routes = [
     { path: '', redirectTo: 'Food', pathMatch: 'full' },

  // list all foods
  { path: 'Food', component: FoodList },

  // single food details (dynamic id from API)
  { path: 'Food/:foodId', component: FoodDetail },

  // cart
  { path: 'Cart', component: FoodCart },

  // fallback for unknown routes
  { path: '**', redirectTo: 'Food' }

];
