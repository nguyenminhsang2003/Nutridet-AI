import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-scan',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './scan.component.html',
  styleUrl: './scan.component.css'
})
export class ScanComponent {

  private http = inject(HttpClient);

  imagePreview: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;

  resultText: any;

  onFileSelected(event: Event): void {

    const input = event.target as HTMLInputElement;

    if (input.files && input.files[0]) {

      const file = input.files[0];

      if (!file.type.startsWith('image/')) {
        alert('Vui lòng chọn một file hình ảnh!');
        return;
      }

      this.selectedFile = file;

      const reader = new FileReader();

      reader.onload = () => {
        this.imagePreview = reader.result;
      };

      reader.readAsDataURL(file);
    }
  }

  save(): void {

    if (!this.selectedFile) {
      alert('Chưa chọn ảnh!');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.http.post(
      `${environment.apiUrl}/scan-image/upload?userId=1`,
      formData
    ).subscribe({
      next: (res) => {
        console.log('Upload thành công', res);
        this.resultText = res;
      },
      error: (err) => {
        console.error(err);
        alert('Upload thất bại');
      }
    });
  }
}