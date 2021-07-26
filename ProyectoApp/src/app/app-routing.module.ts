import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'; 
import { HomeComponent } from './Shared/home/home.component'; 
import { UsersComponent } from './Pages/Security/users/users.component';
import { AuthenticationGuard } from './Guards/authentication.guard'; 
import { BooksComponent } from './Pages/Administrations/books/books.component';
import { AuthorsComponent } from './Pages/Administrations/authors/authors.component';
import { SincronizationsComponent } from './Pages/Administrations/sincronizations/sincronizations.component';

 
const routes: Routes = [

  { path: 'home', component: HomeComponent, canActivate: [AuthenticationGuard] },
   
  { path: 'users', component: UsersComponent, canActivate: [AuthenticationGuard] },
  { path: 'books', component: BooksComponent, canActivate: [AuthenticationGuard] },
  { path: 'authors', component: AuthorsComponent, canActivate: [AuthenticationGuard] },
  { path: 'sincronizations', component: SincronizationsComponent, canActivate: [AuthenticationGuard] },

   
  //  
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
