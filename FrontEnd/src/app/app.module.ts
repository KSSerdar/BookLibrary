import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { BookdetailComponent } from './book/bookdetail/bookdetail.component';
import { BooklistComponent } from './book/booklist/booklist.component';
import { LoginComponent } from './login/login.component';

import { ApiService } from './services/api.service';
import { AuthService } from './services/auth.service';
import { RegisterComponent } from './register/register.component';
import { AuthorComponent } from './author/author.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CreatebookComponent } from './book/createbook/createbook.component';
import { EditbookComponent } from './book/editbook/editbook.component';
import { DeletebookComponent } from './book/deletebook/deletebook.component';
import { AuthInterceptorService } from './services/auth-interceptor.service';



@NgModule({
  declarations: [
    AppComponent,
    BookdetailComponent,
    BooklistComponent,
    LoginComponent,
    RegisterComponent,
    AuthorComponent,
    NavbarComponent,
    CreatebookComponent,
    EditbookComponent,
    DeletebookComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [ApiService,AuthService,
    { provide: HTTP_INTERCEPTORS, useClass:AuthInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
