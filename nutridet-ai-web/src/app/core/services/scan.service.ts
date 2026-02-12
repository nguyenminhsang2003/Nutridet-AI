import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { ScanResponse, ScanErrorResponse } from '../models/scan-response.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ScanService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  uploadImage(formData: FormData, userId: number): Observable<ScanResponse> {
    const url = `${this.baseUrl}/scan-image/upload?userId=${userId}`;
    return this.http.post<ScanResponse>(url, formData).pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(() => this.extractErrorMessage(error));
      })
    );
  }

  private extractErrorMessage(error: HttpErrorResponse): string {
    if (error.error) {
      const errorResponse = error.error as ScanErrorResponse;
      return errorResponse.message || errorResponse.error || error.message;
    }
    return error.message || 'Có lỗi xảy ra khi upload ảnh';
  }
}
