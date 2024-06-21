import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooklistComponent } from './book/booklist/booklist.component';
import { BookdetailComponent } from './book/bookdetail/bookdetail.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthorComponent } from './author/author.component';
import { CreatebookComponent } from './book/createbook/createbook.component';
import { EditbookComponent } from './book/editbook/editbook.component';
import { DeletebookComponent } from './book/deletebook/deletebook.component';
import { accountGuard } from './account.guard';
import { adminGuard } from './admin.guard';

const routes: Routes = [
  { path: 'books', component: BooklistComponent },
  { path: 'book/:id', component: BookdetailComponent },
  { path: 'login', component: LoginComponent ,canActivate:[accountGuard]},
  { path: 'register', component: RegisterComponent ,canActivate:[accountGuard]},
  { path: 'authors', component: AuthorComponent },
  { path: 'create-book', component: CreatebookComponent, canActivate: [accountGuard, adminGuard] },
  { path: 'edit-book/:id', component: EditbookComponent, canActivate: [accountGuard, adminGuard] },
  { path: 'delete-book/:id', component: DeletebookComponent, canActivate: [accountGuard, adminGuard] }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
