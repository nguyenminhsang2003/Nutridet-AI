import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FILE_ERROR_MESSAGES } from '../validators/file.validator';

@Injectable({
  providedIn: 'root'
})
export class FilePreviewService {
  /**
   * Creates a data URL preview from a file
   * @param file - File to create preview for
   * @returns Observable that emits the data URL string
   */
  createPreview(file: File): Observable<string> {
    return new Observable<string>((observer) => {
      const reader = new FileReader();

      reader.onload = () => {
        const result = reader.result;
        if (typeof result === 'string') {
          observer.next(result);
          observer.complete();
        } else {
          observer.error(new Error(FILE_ERROR_MESSAGES.READ_FILE_ERROR));
        }
      };

      reader.onerror = () => {
        observer.error(new Error(FILE_ERROR_MESSAGES.READ_FILE_ERROR));
      };

      reader.readAsDataURL(file);
    });
  }
}

