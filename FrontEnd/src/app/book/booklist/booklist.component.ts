import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Book } from '../../models/Book';
import { Author } from '../../models/Author';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-booklist',
  templateUrl: './booklist.component.html',
  styleUrl: './booklist.component.css'
})
export class BooklistComponent implements OnInit {
  books: Book[] = [];
 author!:Author;
 isAdmin:boolean=false;
constructor(private apiService:ApiService,private authService:AuthService){}

  ngOnInit(): void {
    this.apiService.getBooksAndAuthors().subscribe((data:Book[]) => {
      this.books = data;
      
    });
    this.isAdmin=this.authService.hasRole('Admin');
}
}
     

