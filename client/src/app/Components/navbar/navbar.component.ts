import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router'; // Import router for navigation
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  
  isLoggedIn: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.isLoggedIn.subscribe((status : boolean) => {
      this.isLoggedIn = status;
    });
  }

  logout(): void {
    this.authService.logout(); 
    this.router.navigate(['/login']); // Navigate to login page after logout
  }
}
