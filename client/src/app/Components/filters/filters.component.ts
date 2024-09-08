import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { FilterService } from '../../Services/filter-service.service';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css'],
})
export class FiltersComponent implements OnInit {
  genres: string[] = [];
  languages: string[] = ['English', 'Spanish', 'French', 'German', 'Chinese'];
  authors: string[] = [];

  selectedGenres: Set<string> = new Set<string>();
  selectedLanguages: Set<string> = new Set<string>();
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

    this.loadFiltersFromLocalStorage();
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

    const filters = {
      genres: Array.from(this.selectedGenres),
      languages: Array.from(this.selectedLanguages),
      authors: Array.from(this.selectedAuthors),
    };

    this.filtersChanged.emit(filters);
    this.saveFiltersToLocalStorage();
  }

  private loadFiltersFromLocalStorage(): void {
    const savedFilters = localStorage.getItem('bookFilters');
    if (savedFilters) {
      const filters = JSON.parse(savedFilters);
      this.selectedGenres = new Set(filters.genres);
      this.selectedLanguages = new Set(filters.languages);
      this.selectedAuthors = new Set(filters.authors);
    }
  }

  private saveFiltersToLocalStorage(): void {
    const filters = {
      genres: Array.from(this.selectedGenres),
      languages: Array.from(this.selectedLanguages),
      authors: Array.from(this.selectedAuthors),
    };
    localStorage.setItem('bookFilters', JSON.stringify(filters));
  }
}
