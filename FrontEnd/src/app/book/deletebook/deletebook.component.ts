import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { Book } from '../../models/Book';

@Component({
  selector: 'app-deletebook',
  templateUrl: './deletebook.component.html',
  styleUrl: './deletebook.component.css'
})
export class DeletebookComponent implements OnInit {
  book: Book | null = null;
  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const bookId = String(this.route.snapshot.paramMap.get('id'));
    this.apiService.getBookByID(bookId).subscribe(book => this.book = book);
  }

  deleteBook(): void {
    if (this.book && this.book.id !== null && this.book.id !== undefined) {
      this.apiService.deleteBook(this.book.id).subscribe(() => {
        this.router.navigate(['/books']);
      });
    } else {
      console.error('Book or book id is null or undefined');
    }
  }

  cancel(): void {
    this.router.navigate(['/books']);
  }
}
