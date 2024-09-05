import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  productsInCart = [
    {
      image: 'https://via.placeholder.com/150',
      name: 'Book 1',
      price: 25,
      quantity: 2
    },
    {
      image: 'https://via.placeholder.com/150',
      name: 'Book 2',
      price: 15,
      quantity: 1
    },
    {
      image: 'https://via.placeholder.com/150',
      name: 'Book 3',
      price: 20,
      quantity: 3
    },{
      image: 'https://via.placeholder.com/150',
      name: 'Book 1',
      price: 25,
      quantity: 2
    },
    {
      image: 'https://via.placeholder.com/150',
      name: 'Book 2',
      price: 15,
      quantity: 1
    },
    {
      image: 'https://via.placeholder.com/150',
      name: 'Book 3',
      price: 20,
      quantity: 3
    },{
      image: 'https://via.placeholder.com/150',
      name: 'Book 1',
      price: 25,
      quantity: 2
    },
    {
      image: 'https://via.placeholder.com/150',
      name: 'Book 2',
      price: 15,
      quantity: 1
    },
    {
      image: 'https://via.placeholder.com/150',
      name: 'Book 3',
      price: 20,
      quantity: 3
    }
  ];

  removeProduct(index: number) {
    this.productsInCart.splice(index, 1);
  }

  updateQuantity(index: number, change: number) {
    this.productsInCart[index].quantity += change;
    if (this.productsInCart[index].quantity < 1) {
      this.productsInCart[index].quantity = 1;
    }
  }
}
