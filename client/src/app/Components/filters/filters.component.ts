import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { FilterService } from '../../Services/filter-service.service';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent implements OnInit {
  genres: string[] = [];
  languages: string[] = ['English', 'Spanish', 'French', 'German', 'Chinese'];
  priceRanges: string[] = ['Under 300', '$10 - $20', '$20 - $30', 'Above $30'];
  authors: string[] = ['Author A', 'Author B', 'Author C', 'Author D'];

  selectedGenres: Set<string> = new Set<string>();
  selectedLanguages: Set<string> = new Set<string>();
  selectedPriceRange: string = '';
  selectedAuthors: Set<string> = new Set<string>();

  @Output() filtersChanged = new EventEmitter<any>();

  constructor(private filterService: FilterService) {}

  ngOnInit(): void {
    this.filterService.getFilters().subscribe((tags: string[]) => {
      this.genres = tags; 
    });

    this.filterService.getAuthors().subscribe((authors: string[]) => {
      this.authors = authors;
    });
  }

  onCheckboxChange(type: string, value: string, event: any): void {
    let selection: Set<string> = this.selectedGenres;
    if (type === 'genre') selection = this.selectedGenres;
    else if (type === 'language') selection = this.selectedLanguages;
    else if (type === 'author') selection = this.selectedAuthors;

    if (event.target.checked) {
      selection.add(value);
    } else {
      selection.delete(value);
    }

    this.filtersChanged.emit({
      genres: Array.from(this.selectedGenres),
      languages: Array.from(this.selectedLanguages),
      priceRange: this.selectedPriceRange,
      authors: Array.from(this.selectedAuthors)
    });
  }

  onPriceRangeChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    this.selectedPriceRange = target.value;
    this.filtersChanged.emit({
      genres: Array.from(this.selectedGenres),
      languages: Array.from(this.selectedLanguages),
      priceRange: this.selectedPriceRange,
      authors: Array.from(this.selectedAuthors)
    });
  }
}
