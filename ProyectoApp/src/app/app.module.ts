import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterComponent } from './Shared/footer/footer.component';
import { HomeComponent } from './Shared/home/home.component';
import { NavBarComponent } from './Shared/nav-bar/nav-bar.component';
import { WorkSpaceComponent } from './Shared/work-space/work-space.component';
import { LoginComponent } from './Pages/Security/login/login.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsersComponent } from './Pages/Security/users/users.component';
import { InterceptorService } from './Services/Interceptors/interceptor.service';
import { BooksComponent } from './Pages/Administrations/books/books.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorsComponent } from './Pages/Administrations/authors/authors.component';
import { SincronizationsComponent } from './Pages/Administrations/sincronizations/sincronizations.component';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HomeComponent,
    NavBarComponent,
    WorkSpaceComponent,
    LoginComponent, UsersComponent, BooksComponent, AuthorsComponent, SincronizationsComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    CommonModule
  ],

  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true
    }
  ],

  bootstrap: [AppComponent]

})
export class AppModule { }
