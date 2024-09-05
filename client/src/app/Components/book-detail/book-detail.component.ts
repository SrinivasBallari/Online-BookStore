import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-book-detail',
  standalone: true,
  imports: [RouterModule,CommonModule],
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.css'
})
export class BookDetailComponent implements OnInit {
  book: any = {
    imageUrl: 'https://via.placeholder.com/500x750?text=Book+Cover',
    title: 'Sample Book Title',
    author: {
      name: 'John Doe',
      bio: 'John Doe is a fictional author used for demonstration purposes.'
    },
    publisher: 'Sample Publisher',
    publishedDate: new Date('2023-01-01'),
    pagesCount: 320,
    price: 29.99,
    tags: ['Fiction', 'Adventure', 'Mystery'],
    description: 'This is a sample description of the book. It provides an overview of the book’s plot and themes.',
  };

  similarBooks: any[] = [
    {
      id: 1,
      imageUrl: 'https://via.placeholder.com/300x450?text=Similar+Book+1',
      title: 'Similar Book 1',
      price: 19.99
    },
    {
      id: 2,
      imageUrl: 'https://via.placeholder.com/300x450?text=Similar+Book+2',
      title: 'Similar Book 2',
      price: 25.99
    },
    {
      id: 3,
      imageUrl: 'https://via.placeholder.com/300x450?text=Similar+Book+3',
      title: 'Similar Book 3',
      price: 22.99
    },
    {
      id: 2,
      imageUrl: 'https://via.placeholder.com/300x450?text=Similar+Book+2',
      title: 'Similar Book 2',
      price: 25.99
    },
    {
      id: 3,
      imageUrl: 'https://via.placeholder.com/300x450?text=Similar+Book+3',
      title: 'Similar Book 3',
      price: 22.99
    },
    {
      id: 2,
      imageUrl: 'https://via.placeholder.com/300x450?text=Similar+Book+2',
      title: 'Similar Book 2',
      price: 25.99
    },

  ];

  ratings: any[] = [
    {
      username: 'Alice',
      userImage: 'https://via.placeholder.com/50x50?text=A',
      rating: 4,
      comment: 'Great book! Really enjoyed it.A must-read for everyoneA must-read for everyoneA must-read for everyoneA must-read for everyoneA must-read for everyoneA must-read for everyoneA must-read for everyone'
    },
    {
      username: 'Bob',
      userImage: 'https://via.placeholder.com/50x50?text=B',
      rating: 5,
      comment: 'A must-read for everyone!It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.'
    },
    {
      username: 'Charlie',
      userImage: 'https://via.placeholder.com/50x50?text=C',
      rating: 3,
      comment: 'It was okay, but not as engaging as I expected.Great book! Really enjoyeGreat book! Really enjoyeGreat book! Really enjoyeGreat book! Really enjoye'
    },
    {
      username: 'Bob',
      userImage: 'https://via.placeholder.com/50x50?text=B',
      rating: 5,
      comment: 'A must-read for everyone!It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.'
    },
    {
      username: 'Charlie',
      userImage: 'https://via.placeholder.com/50x50?text=C',
      rating: 3,
      comment: 'It was okay, but not as engaging as I expected.Great book! Really enjoyeGreat book! Really enjoyeGreat book! Really enjoyeGreat book! Really enjoye'
    },
    {
      username: 'Bob',
      userImage: 'https://via.placeholder.com/50x50?text=B',
      rating: 5,
      comment: 'A must-read for everyone!It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.It was okay, but not as engaging as I expected.'
    },
    {
      username: 'Charlie',
      userImage: 'https://via.placeholder.com/50x50?text=C',
      rating: 3,
      comment: 'It was okay, but not as engaging as I expected.Great book! Really enjoyeGreat book! Really enjoyeGreat book! Really enjoyeGreat book! Really enjoye'
    }
  ];

  quantity: number = 1;
  averageRating: number = 0;

  ngOnInit(): void {
    this.calculateAverageRating();
  }

  increaseQuantity(): void {
    this.quantity++;
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  addToCart(): void {
    // Implement add to cart logic here
  }

  private calculateAverageRating(): void {
    if (this.ratings.length > 0) {
      const totalRating = this.ratings.reduce((sum, rating) => sum + rating.rating, 0);
      this.averageRating = totalRating / this.ratings.length;
    }
  }
}
