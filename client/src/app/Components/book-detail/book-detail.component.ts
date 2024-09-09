import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { BookService } from '../../Services/book-service.service';
import { FormsModule } from '@angular/forms'; // Import FormsModule for ngModel
import { AuthService } from '../../Services/auth.service';
import { CartService } from '../../Services/cart.service';

@Component({
  selector: 'app-book-detail',
  standalone: true,
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.css'
})
export class BookDetailComponent implements OnInit {
  book: any = {};
  similarBooks: any[] = [];
  reviews: any[] = [];
  bookId: number | null = null;
  quantity: number = 1;
  averageRating: number = 0;
  
  userRating: number = 5;
  userReview: string = '';
  isLoggedIn: boolean = false;

  constructor(private route: ActivatedRoute, private bookService: BookService,private authService:AuthService,private cartService:CartService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.bookId = +params.get('id')!;
      this.fetchBookDetails();
      this.fetchSimilarBooks();
      this.fetchReviews(); 
    });
    this.calculateAverageRating();
    this.authService.isLoggedIn.subscribe((status : boolean) => {
      this.isLoggedIn = status;
    });
  }

  fetchBookDetails(): void {
    if (this.bookId !== null) {
      this.bookService.getBookById(this.bookId).subscribe(data => {
        this.book = data;
      });
    }
  }

  fetchSimilarBooks(): void {
    if (this.bookId !== null) {
      this.bookService.getSimilarBooks(this.bookId).subscribe(data => {
        this.similarBooks = data.$values.filter((b: { bookId: number | null; }) => b.bookId !== this.bookId);
      });
    }
  }

  fetchReviews(): void {
    if (this.bookId !== null) {
      this.bookService.getReviews(this.bookId).subscribe(data => {
        this.reviews = data.$values;
        this.calculateAverageRating();
      });
    }
  }

  submitReview(): void {
    if (this.bookId !== null && this.userRating && this.userReview) {
      const rev = {
        bookId: this.bookId,
        rating: this.userRating,
        review: this.userReview
      };
      this.bookService.postReview(rev).subscribe((response) => {
        alert(response.statusMessage);
        this.fetchReviews();
        this.userRating = 5;
        this.userReview = '';
      });
    }else{
      alert("Rating / Review Cannot be empty !!");
    }
  }

  addToCart(): void {
    if(this.bookId != null){
      this.cartService.addToCart(this.bookId).subscribe((response) => {
        if(response){
          alert("Book added to cart !!");
        }
      });
    }
  }

  private calculateAverageRating(): void {
    if (this.reviews.length > 0) {
      const totalRating = this.reviews.reduce((sum, rating) => sum + rating.rating, 0);
      this.averageRating = totalRating / this.reviews.length;
    }
  }
}
