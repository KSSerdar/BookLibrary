import { Component } from '@angular/core';
import { Book } from '../../models/Book';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-createbook',
  templateUrl: './createbook.component.html',
  styleUrl: './createbook.component.css'
})
export class CreatebookComponent {
  book: Book = { name: '', authorID: '', description: '', pictureURL: '',publishDate:'' };
  constructor(private apiService:ApiService,private router:Router){}

  onSubmit(): void {
    this.apiService.addBook(this.book).subscribe(() => {
      this.router.navigate(['/books']);
    });
  }
}
