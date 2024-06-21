import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  email:string = '';
  password:string = '';

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit(event:Event): void {
    event.preventDefault();
    console.log('Email:', this.email);
  console.log('Password:', this.password);
    this.authService.login(this.email, this.password).subscribe(response => {
      this.router.navigate(['/']);
    });
  }
}
