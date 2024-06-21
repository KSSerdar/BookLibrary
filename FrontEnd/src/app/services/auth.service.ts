import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import jwt_decode,{jwtDecode} from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private token: string | null = null;
  private userId: string | null = null;
  private roles: string[] = [];
  private email: string | null = null;
  private username: string | null = null;
  private apiUrl = 'https://localhost:44384/api/Auth';

  constructor(private http: HttpClient) { 
    this.loadToken(); 
  }

  login(email: string, password: string) {
    return this.http.post<any>(`${this.apiUrl}/Login`, { email, password }).pipe(
      tap(response => {
        if (response && response.token && response.token.result) {
          this.saveToken(response.token.result); // Extract the JWT token from response.token.result
        } else {
          console.error('No token found in login response');
        }
      })
    );
  }


  register(user: { name: string; email: string; password: string; confirmPassword: string }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Register`, user);
  }

  
  logout(): void {
    this.clearToken();
    }
    saveToken(token: string): void {
      console.log('Saving token:', token);
      this.token = token;
      localStorage.setItem('token', token);
      this.decodeToken(); // Attempt to decode the token immediately after saving
    }
  
    loadToken(): void {
      this.token = localStorage.getItem('token');
      console.log('Loaded token:', this.token);
      if (this.token) {
        this.decodeToken();
      } else {
        console.error('No token found in localStorage');
      }
    }
  
    decodeToken(): void {
      if (this.token && typeof this.token === 'string') {
        const parts = this.token.split('.');
        if (parts.length === 3) {
          try {
            const decodedToken: any = jwtDecode(this.token);
            console.log('Decoded token:', decodedToken);
            this.userId = decodedToken.nameid;
            this.roles = decodedToken.role || [];
          } catch (error) {
            console.error('Error decoding token:', error);
            this.clearToken(); // Clear the invalid token
          }
        } else {
          console.error('Invalid token parts:', parts);
          this.clearToken(); // Clear the invalid token
        }
      } else {
        console.error('Invalid token:', this.token);
        this.clearToken(); // Clear the invalid token
      }
    }
  
    clearToken(): void {
      this.token = null;
      this.userId = null;
      this.roles = [];
      localStorage.removeItem('token');
    }
  
    isAuthenticated(): boolean {
      return !!this.token;
    }
  
    getToken(): string | null {
      return this.token;
    }
  
    getRoles(): string[] {
      return this.roles;
    }
  
    getUserId(): string | null {
      return this.userId;
    }
    hasRole(role: string): boolean {
      return this.roles.includes(role);
    }
  }