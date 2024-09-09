import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CartService } from '../../Services/cart.service';
import { Cart } from '../../Models/cart.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})
export class CartComponent implements OnInit {
  
  productsInCart: Cart[] = [];
  subTotal: number = 0;
  totalItems: number = 0;
  selectedPaymentMethod: string = '';

  constructor(private router: Router, private cartService: CartService) {}

  ngOnInit(): void {
    this.fetchCartData();
  }

  navigateToHome() {
    this.router.navigate(['']);
  }

  fetchCartData(): void {
    this.subTotal = 0; 
    this.cartService.fetchCartData().subscribe((response) => {
      this.productsInCart = response.$values.map((item: any) => {
        this.subTotal += item.book.price * item.quantity; 
        return {
          bookId: item.book.bookId,
          imageUrl: item.book.imageUrl,
          title: item.book.title,
          price: item.book.price,
          quantity: item.quantity,
        };
      });
      this.totalItems = this.productsInCart.length;
    });
  }

  removeItemFromCart(bookId: number): void {
    this.cartService.removeItemFromCart(bookId).subscribe((response) => {
      alert('Book removed from cart!');
      this.fetchCartData();
    });
  }

  reduceItemQuantity(bookId: number): void {
    if (bookId != null) {
      this.cartService.reduceItemQuantity(bookId).subscribe(() => {
        this.fetchCartData();
      });
    }
  }

  increaseItemQuantity(bookId: number): void {
    if (bookId != null) {
      this.cartService.addToCart(bookId).subscribe(() => {
        this.fetchCartData();
      });
    }
  }

  pay(): void {
    if (this.selectedPaymentMethod) {
      this.cartService.placeOrder(this.selectedPaymentMethod).subscribe((response) => {
        console.log(response);
        alert('Payment successful via ' + this.selectedPaymentMethod);
        this.fetchCartData();
      });
      
    }
  }
}
