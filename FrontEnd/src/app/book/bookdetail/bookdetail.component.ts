import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../../models/Book';
import { Comments } from '../../models/Comment';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-bookdetail',
  templateUrl: './bookdetail.component.html',
  styleUrl: './bookdetail.component.css'
})
export class BookdetailComponent implements OnInit {
  isLoggedIn: boolean=false;
  book: Book | null = null;
  comments: Comments[] = [];
  newCommentContent = '';
constructor(private apiService:ApiService,private route:ActivatedRoute,private authService:AuthService){}
ngOnInit(): void {
  const bookId = String(this.route.snapshot.paramMap.get('id'));
  this.apiService.getBooksAndAuthors().subscribe(books => {
    this.book = books.find(book => book.id === bookId)!;
    this.loadComments();
    this.isLoggedIn = this.authService.isAuthenticated();
  });
}

loadComments(): void {
  if (this.book && this.book.id !== null && this.book.id !== undefined) {
    this.apiService.getCommentsByBook(this.book.id).subscribe(comments => {
      this.comments = comments;
    });
  }
  else {
    // Handle the case where the book id is null
    console.error('Book id is null or undefined');
  }

}

addComment(): void {
  const auserid=this.authService.getUserId();
  if(this.book&&this.book.id!==null&&this.book.id!==undefined){
    if(auserid){
      const comment: Comments = {
        id: '', // The server will assign an ID
        comment: this.newCommentContent,
       // createdAt: new Date(),
        //userId: auserid, // The server will get the user ID from the JWT token
        bookId: this.book.id
      };
      this.apiService.addComment(comment).subscribe(newComment => {
        this.comments.push(newComment);
        this.newCommentContent = '';
      });
    }
    }
   
  
  else {
    // Handle the case where the book id is null
    console.error('Book id is null or undefined');
  }
  }


}
