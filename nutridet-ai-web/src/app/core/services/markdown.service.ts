import { Injectable, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { marked } from 'marked';
import { Observable, from, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MarkdownService {
  private readonly sanitizer = inject(DomSanitizer);

  /**
   * Parses markdown to HTML and returns it as a sanitized SafeHtml
   * @param markdown - Markdown string to parse
   * @returns Observable that emits sanitized HTML as SafeHtml
   */
  parseToSafeHtml(markdown: string): Observable<SafeHtml> {
    return this.parseToHtml(markdown).pipe(
      map((html: string) => this.sanitizeHtml(html))
    );
  }

  /**
   * Parses markdown to HTML string
   * @param markdown - Markdown string to parse
   * @returns Observable that emits HTML string
   */
  parseToHtml(markdown: string): Observable<string> {
    try {
      const parseResult = marked.parse(markdown);
      
      if (parseResult instanceof Promise) {
        return from(parseResult).pipe(
          map((html: string) => this.processHtml(html, markdown)),
          catchError(() => of(this.createFallbackHtml(markdown)))
        );
      }
      
      const html = parseResult as string;
      return of(this.processHtml(html, markdown));
    } catch {
      return of(this.createFallbackHtml(markdown));
    }
  }

  /**
   * Sanitizes HTML string to SafeHtml
   * @param html - HTML string to sanitize
   * @returns Sanitized SafeHtml
   */
  sanitizeHtml(html: string): SafeHtml {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }

  private processHtml(html: string, fallback: string): string {
    return html && html.trim().length > 0 
      ? html 
      : this.createFallbackHtml(fallback);
  }

  private createFallbackHtml(text: string): string {
    return text.replace(/\n/g, '<br>');
  }
}

