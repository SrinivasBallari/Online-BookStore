import { Component, OnInit } from '@angular/core';
import { BookService } from '../../Services/book-service.service';
import { FiltersComponent } from '../filters/filters.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [FiltersComponent,CommonModule,RouterModule],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent implements OnInit {
  books: any[] = [];
  currentPage: number = 1;
  totalPages: number = 2;
  filters: any = {};

  ngOnInit(): void {
    // Static data for demonstration
    this.books = [
      { id: 1, name: 'Book 1', rating: 4.5, imageUrl: 'https://via.placeholder.com/150' },
      { id: 2, name: 'Book 2', rating: 4.0, imageUrl: 'https://via.placeholder.com/150' },
      { id: 3, name: 'Book 3', rating: 3.5, imageUrl: 'https://via.placeholder.com/150' },
      { id: 4, name: 'Book 4', rating: 5.0, imageUrl: 'https://via.placeholder.com/150' },
      { id: 5, name: 'Book 5', rating: 3.0, imageUrl: 'https://via.placeholder.com/150' },
      { id: 6, name: 'Book 6', rating: 4.2, imageUrl: 'https://via.placeholder.com/150' },
      // Add more books as needed
    ];
  }

  onFiltersChanged(filters: any): void {
    this.filters = filters;
    // Logic to filter books based on filters
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      // Fetch next page of books
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      // Fetch previous page of books
    }
  }
}
