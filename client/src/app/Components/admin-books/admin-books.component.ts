import { Component, OnInit } from '@angular/core';
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
  successMessage: string | null = null; 

  constructor(private bookService: BookService, private fb: FormBuilder) {
    this.bookForm = this.fb.group({
      title: '',
      authorName: '',
      authorBio: '',
      pagesCount: 0,
      language: '',
      publisherName: '',
      publisherAddress: '',
      publishedDate: '',
      publishedVersion: '',
      price: 0,
      description: '',
      imageUrl: '',
      tagNames: [''],
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

    const bookData = {
      title: this.bookForm.value.title,
      authorName: this.bookForm.value.authorName,
      authorBio: this.bookForm.value.authorBio,
      pagesCount: this.bookForm.value.pagesCount,
      language: this.bookForm.value.language,
      publisherName: this.bookForm.value.publisherName,
      publisherAddress: this.bookForm.value.publisherAddress,
      publishedDate: this.bookForm.value.publishedDate,
      publishedVersion: this.bookForm.value.publishedVersion,
      price: this.bookForm.value.price,
      description: this.bookForm.value.description,
      imageUrl: this.bookForm.value.imageUrl,
      tagNames: this.bookForm.value.tagNames.split(',')
    };
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
      this.successMessage = `Book deleted successfully.`;
      setTimeout(() => {
        this.successMessage = null;
      }, 3000);
    });
  }

  resetForm(): void {
    this.editingBook = null;
    this.bookForm.reset();
  }
}
