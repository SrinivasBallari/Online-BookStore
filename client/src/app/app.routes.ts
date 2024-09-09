import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { HomeComponent } from './Components/home/home.component';
import { BookDetailComponent } from './Components/book-detail/book-detail.component';
import { CartComponent } from './Components/cart/cart.component';
import { UserProfileComponent } from './Components/user-profile/user-profile.component';
import { AdminBooksComponent } from './Components/admin-books/admin-books.component';

export const routes: Routes = [
    {
        path: 'login',
        component : LoginComponent
    },
    {
        path: 'register',
        component : RegisterComponent
    },
    {
        path: '',
        component: HomeComponent
    },
    {
        path:'book-details/:id',
        component: BookDetailComponent
    },
    {
        path:'cart',
        component: CartComponent
    },
    { 
        path: 'profile', 
        component: UserProfileComponent 
    },
    {
        path: 'admin/books',
        component : AdminBooksComponent
    },
];
