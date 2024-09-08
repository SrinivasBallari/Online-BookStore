import { Component, NgModule, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { BookService } from '../../Services/book-service.service'; 
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-books',
  standalone: true,
  imports: [FormsModule,CommonModule,ReactiveFormsModule],
  templateUrl: './admin-books.component.html',
  styleUrl: './admin-books.component.css'
})
export class AdminBooksComponent implements OnInit {
  books: any[] = [];
  bookForm: FormGroup;
  editingBook: any = null;

  constructor(private bookService: BookService, private fb: FormBuilder) {
    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      authorId: [null],
      authorName: [''],
      authorBio: [''],
      pagesCount: [null, Validators.required],
      language: ['', Validators.required],
      publisherId: [null],
      publisherName: [''],
      publisherAddress: [''],
      publishedDate: ['', Validators.required],
      publishedVersion: [null],
      price: [null, Validators.required],
      description: [''],
      imageUrl: [''],
      tagIds: [[]],
      tagNames: [[]]
    });
  }

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.bookService.getAllBooks().subscribe((response: any) => {
      this.books = response.$values;
    });
  }

  addOrUpdateBook(): void {
    if (this.bookForm.invalid) return;

    const bookData = this.bookForm.value;
    if (this.editingBook) {
      this.bookService.updateBook(this.editingBook.id, bookData).subscribe(() => {
        this.loadBooks();
        this.resetForm();
      });
    } else {
      this.bookService.addBook(bookData).subscribe(() => {
        this.loadBooks();
        this.resetForm();
      });
    }
  }

  editBook(book: any): void {
    this.editingBook = book;
    this.bookForm.patchValue(book);
  }

  deleteBook(id: number): void {
    this.bookService.deleteBook(id).subscribe(() => {
      this.loadBooks();
    });
  }

  resetForm(): void {
    this.editingBook = null;
    this.bookForm.reset();
  }
}
