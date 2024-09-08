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
  totalPages: number = 1;
  filters: any = {genres:[],authors:[]};
  pageSize: number = 6;

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.loadFiltersFromLocalStorage();
    this.fetchBooks();
  }

  onFiltersChanged(filters: any): void {
    this.filters = filters;
    this.fetchBooks();
  }
  
  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.fetchBooks();
    }
  }
  
  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.fetchBooks();
    }
  }
  
  fetchBooks(): void {
    this.bookService.getBooks(this.currentPage, this.pageSize, this.filters).subscribe({
      next: (response) => {
        if (response.books && response.totalCount !== undefined) {
          this.totalPages = Math.ceil(response.totalCount / this.pageSize);
          this.books = response.books.$values || []; // Handle case where $values might be undefined
        } else {
          console.error('Invalid response structure:', response);
        }
      },
      error: (error) => {
        console.error('Error fetching books:', error);
      }
    });
  }

  private loadFiltersFromLocalStorage(): void {
    const savedFilters = localStorage.getItem('bookFilters');
    if (savedFilters) {
      this.filters = JSON.parse(savedFilters);
    }
  }

}
