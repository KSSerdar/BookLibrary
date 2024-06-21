import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  isAdmin:boolean=false;
  constructor(private authService: AuthService){}
  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthenticated();
    this.isAdmin=this.authService.hasRole('Admin');
  }

  logout(): void {
    this.authService.logout();
    this.isLoggedIn = false;
  }
}
