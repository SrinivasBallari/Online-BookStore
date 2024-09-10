import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router'; // Import router for navigation
import { AuthService } from '../../Services/auth.service';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule,RouterModule, FormsModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  
  isLoggedIn: boolean = false;
  isAdmin : boolean = false;
  searchQuery: string = ''; 

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe((status : boolean) => {
      this.isLoggedIn = status;
    });

    this.authService.isAdmin().subscribe((response) => {
      if(response.message == "true"){
        this.authService.isAdminTemp.next(true);
        this.isAdmin = true;
      }
    });

    this.authService.isUserAdmin.subscribe((status : boolean) => {
      this.isAdmin = status;
    });
    
  }
  
  onSearch(): void {
    // Only navigate if the search query is not empty
    if (this.searchQuery.trim()) {
      this.router.navigate(['/search'], { queryParams: { search: this.searchQuery } });
    }
  }

  logout(): void {
    this.authService.logout(); 
    this.router.navigate(['/login']);
  }
}
