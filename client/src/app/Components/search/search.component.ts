import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookSearchService } from '../../Services/book-search.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  searchQuery: string = '';
  books: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private bookSearchService: BookSearchService
  ) {}

  ngOnInit(): void {
    // Capture query params when the route changes
    this.route.queryParams.subscribe((params) => {
      this.searchQuery = params['search'] || '';
      if (this.searchQuery) {
        this.loadBooks();
      }
    });
  }

  loadBooks(): void {
    // Call your service to search for books based on the searchQuery
    this.bookSearchService.searchBooks(this.searchQuery).subscribe({
      next: (response: any) => {
        // Ensure response has the $values property
        if (response && response.$values) {
          this.books = response.$values; // Assign the $values array to books
        } else {
          console.error('Invalid response structure:', response);
        }
      },
      error: (error) => {
        console.error('Error fetching books:', error);
      }
    });
  }
}
