import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, forkJoin, map } from 'rxjs';
import { User } from '../models/User';
import { Book } from '../models/Book';
import { Author } from '../models/Author';
import { Comments } from '../models/Comment';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:44384/api';

  constructor(private http: HttpClient) { }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwt');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/users`, { headers: this.getAuthHeaders() });
  }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/book/getallbook`, { headers: this.getAuthHeaders() });
  }

  getAuthors(): Observable<Author[]> {
    return this.http.get<Author[]>(`${this.apiUrl}/Author/GetAll`, { headers: this.getAuthHeaders() });
  }

  getCommentsByBook(bookId: string): Observable<Comments[]> {
    return this.http.get<Comments[]>(`${this.apiUrl}/Comment/${bookId}`, { headers: this.getAuthHeaders() });
  }

  addComment(comment: Comments): Observable<Comments> {
    return this.http.post<Comments>(`${this.apiUrl}/comment`, comment, { headers: this.getAuthHeaders() });
  }
  getAuthorNameByID(authorID:string):Observable<Author>{
    return this.http.get<Author>(`${this.apiUrl}/author/GetAuthorNameById/${authorID}`,{headers :this.getAuthHeaders() });
  }
  getAuthorByID(authorID:string):Observable<Author>{
    return this.http.get<Author>(`${this.apiUrl}/author/GetById/${authorID}`,{headers :this.getAuthHeaders() });
  }
  addBook(book:Book):Observable<Book>{
    return this.http.post<Book>(`${this.apiUrl}/book/create`,book,{headers:this.getAuthHeaders()});
  }
  getBookByID(bookID:string):Observable<Book>{
    return this.http.get<Book>(`${this.apiUrl}/book/getbyid/${bookID}`,{headers:this.getAuthHeaders()});
  }
  editBook(bookID:string,book:Book):Observable<Book>{
    return this.http.post<Book>(`${this.apiUrl}/book/update/${bookID}`,{headers:this.getAuthHeaders()});
  }
  deleteBook(bookId:string):Observable<Book>{
    return this.http.delete<Book>(`${this.apiUrl}/book/delete/${bookId}`,{headers:this.getAuthHeaders()});
  }
  getBooksAndAuthors(): Observable<Book[]> {
    return forkJoin([
      this.http.get<Book[]>(`${this.apiUrl}/book/getallbook`),
      this.http.get<Author[]>(`${this.apiUrl}/author/getall`)
    ]).pipe(
      map(([books, authors]) => {
        return books.map(book => {
          book.author = authors.find(author => author.id === book.authorID);
          return book;
        });
      })
    );
}
}