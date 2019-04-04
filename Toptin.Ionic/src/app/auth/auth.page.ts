import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.page.html',
  styleUrls: ['./auth.page.scss'],
})
export class AuthPage implements OnInit {

  model: any = {};

  constructor(private authService: AuthService , private router: Router) { }

  ngOnInit() {
  }

  login() {
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      console.log('Login successful');
    }, async error => {
      console.log(error);
    }, () => {
      this.router.navigate(['/home']);
    });
  }

  loggedIn() {
    this.authService.loggedIn();
  }

  logout () {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    console.log('Logged out');
    this.router.navigate(['/home']);
  }
}
