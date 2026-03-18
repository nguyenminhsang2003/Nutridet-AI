import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  username: string = '';
  password: string = '';
  error: string = '';
  isLoading: boolean = false;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  login() {
    this.error = '';

    if (!this.username || !this.password) {
      this.error = 'Vui lòng nhập đầy đủ thông tin';
      return;
    }

    this.isLoading = true;

    this.http.post<any>('https://localhost:5001/api/auth/login', {
      username: this.username,
      password: this.password
    }).subscribe({
      next: (res) => {
        // lưu token
        localStorage.setItem('token', res.token);

        // redirect
        this.router.navigate(['/']);
      },
      error: (err) => {
        if (err.status === 401) {
          this.error = 'Sai tài khoản hoặc mật khẩu';
        } else {
          this.error = 'Có lỗi xảy ra, thử lại sau';
        }
        this.isLoading = false;
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}