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
  isAdmin : boolean = false;

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

  logout(): void {
    this.authService.logout(); 
    this.router.navigate(['/login']);
  }
}
