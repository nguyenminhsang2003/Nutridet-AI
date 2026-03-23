import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  email: string = '';
  password: string = '';
  error: string = '';
  isLoading: boolean = false;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  login() {
    this.error = '';

    if (!this.email || !this.password) {
      this.error = 'Vui lòng nhập đầy đủ thông tin';
      return;
    }

    this.isLoading = true;

    this.http.post<{ token: string }>(
      `${environment.apiUrl}/login`,
      {
        email: this.email,
        password: this.password
      }
    ).subscribe({
      next: (res) => {
        localStorage.setItem('token', res.token);
        this.email = '';
        this.password = '';
        this.router.navigate(['/']);
      },
      error: (err) => {
        if (err.status === 401) {
          this.error = 'Sai tài khoản hoặc mật khẩu';
        } else {
          this.error = 'Có lỗi xảy ra, thử lại sau';
        }

        this.password = '';
        this.isLoading = false;
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}