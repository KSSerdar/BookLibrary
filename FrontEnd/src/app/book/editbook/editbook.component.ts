import { Component, OnInit } from '@angular/core';
import { Book } from '../../models/Book';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-editbook',
  templateUrl: './editbook.component.html',
  styleUrl: './editbook.component.css'
})
export class EditbookComponent implements OnInit {
  book: Book = { name: '', authorID:'', description: '', pictureURL: '', publishDate: '' };
  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private router: Router
  ) { }
  ngOnInit(): void {
    const bookId = String(this.route.snapshot.paramMap.get('id'));
    this.apiService.getBookByID(bookId).subscribe(book => this.book = book);
  }

  onSubmit(): void {
    if (this.book.id !== null && this.book.id !== undefined) {
      this.apiService.editBook(this.book.id, this.book).subscribe(() => {
        this.router.navigate(['/books']);
      });
    } else {
      // Handle the case where the book id is null
      console.error('Book id is null or undefined');
    }
  
  }
}
