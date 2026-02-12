import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScanService } from '../../core/services/scan.service';

@Component({
  selector: 'app-scan',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './scan.component.html'
})
export class ScanComponent {

  selectedFile!: File | null;
  previewUrl: string | ArrayBuffer | null = null;

  constructor(private scanService: ScanService) {}

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (!file) return;

    this.selectedFile = file;

    const reader = new FileReader();
    reader.onload = () => {
      this.previewUrl = reader.result;
    };
    reader.readAsDataURL(file);
  }

  upload() {
    if (!this.selectedFile) return;

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.scanService.uploadImage(formData)
      .subscribe({
        next: (res) => console.log(res),
        error: (err) => console.error(err)
      });
  }
}

