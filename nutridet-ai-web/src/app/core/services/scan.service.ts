import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ScanService {

  private baseUrl = 'https://localhost:7200/api';

  constructor(private http: HttpClient) {}

  uploadImage(formData: FormData) {
    return this.http.post(`${this.baseUrl}/upload`, formData);
  }
}
