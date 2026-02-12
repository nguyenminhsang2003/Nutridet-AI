import { Component, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SafeHtml } from '@angular/platform-browser';
import { Subject } from 'rxjs';
import { switchMap, takeUntil, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { ScanService } from '../../core/services/scan.service';
import { MarkdownService } from '../../core/services/markdown.service';
import { FilePreviewService } from '../../core/services/file-preview.service';
import { validateFile, FILE_ERROR_MESSAGES } from '../../core/validators/file.validator';
import { APP_CONSTANTS } from '../../core/constants/app.constants';

@Component({
  selector: 'app-scan',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './scan.component.html'
})
export class ScanComponent implements OnDestroy {
  selectedFile: File | null = null;
  previewUrl: string | null = null;
  resultHtml: SafeHtml | null = null;
  isLoading = false;
  error: string | null = null;

  private readonly destroy$ = new Subject<void>();
  private readonly scanService = inject(ScanService);
  private readonly markdownService = inject(MarkdownService);
  private readonly filePreviewService = inject(FilePreviewService);
  private readonly userId = APP_CONSTANTS.DEFAULT_USER_ID;

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    
    if (!file) {
      this.resetState();
      return;
    }

    const validationResult = validateFile(file);
    if (!validationResult.isValid) {
      this.error = validationResult.error || FILE_ERROR_MESSAGES.NO_FILE;
      this.resetState();
      input.value = '';
      return;
    }

    this.selectedFile = file;
    this.clearErrorAndResult();

    this.filePreviewService.createPreview(file)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (previewUrl) => {
          this.previewUrl = previewUrl;
        },
        error: () => {
          this.error = FILE_ERROR_MESSAGES.READ_FILE_ERROR;
          this.resetState();
        }
      });
  }

  upload(): void {
    if (!this.selectedFile) {
      return;
    }

    const validationResult = validateFile(this.selectedFile);
    if (!validationResult.isValid) {
      this.error = validationResult.error || FILE_ERROR_MESSAGES.NO_FILE;
      return;
    }

    this.startLoading();

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.scanService.uploadImage(formData, this.userId)
      .pipe(
        switchMap((response) =>      
          this.markdownService.parseToSafeHtml(response.message).pipe(
            catchError(() => {
              // Fallback to plain text if markdown parsing fails
              const fallbackHtml = response.message.replace(/\n/g, '<br>');
              return of(this.markdownService.sanitizeHtml(fallbackHtml));
            })
          )
        ),
        takeUntil(this.destroy$)
      )
      .subscribe({
        next: (safeHtml) => {
          this.isLoading = false;
          this.resultHtml = safeHtml;
        },
        error: (error: string) => {
          this.startLoading();
        }
      });
  }

  private resetState(): void {
    this.selectedFile = null;
    this.previewUrl = null;
    this.resultHtml = null;
  }

  private clearErrorAndResult(): void {
    this.error = null;
    this.resultHtml = null;
  }

  private startLoading(): void {
    this.isLoading = true;
    this.clearErrorAndResult();
  }
}

