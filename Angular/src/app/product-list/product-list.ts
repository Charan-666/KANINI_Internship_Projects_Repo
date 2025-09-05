import { Component } from '@angular/core';
import { Product } from '../Product';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';



@Component({
  selector: 'app-product-list',
  imports: [CommonModule,FormsModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css'
})
export class ProductList {

 products:Product={
    productId:1,
    productName:'Shoes',
    price:2000,
    description:'This product comes under Fashion category',
    stock:10,
    onSale:true,
    rating:4.5,
    reviews:[
      {user:"Charan",comment:"Super cool product"},
      {user:"Suhas",comment:"durable product"},
      {user:"hadiya",comment:"Good Product"}
    ]
  }
  

   ShoppingCart:Product[]=[];

   addToCart(){
    this.ShoppingCart.push(this.products);
  alert('Product added to cart');
   }

   user={
    name:'Charan Shetty',
    email:'shetty@gmail.com'
   };


}
