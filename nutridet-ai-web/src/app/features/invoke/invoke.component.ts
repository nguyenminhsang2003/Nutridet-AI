import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-invoke',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invoke.component.html',
  styleUrl: './invoke.component.css'
})
export class InvokeComponent implements OnInit {

  private route = inject(ActivatedRoute);
  private http = inject(HttpClient);
  private router = inject(Router);

  data: any = null;
  loading = false;

  ngOnInit(): void {
    const scanImageId = this.route.snapshot.paramMap.get('id');

    if (scanImageId) {
      this.getInvoke(+scanImageId);
    }
  }

  getInvoke(scanImageId: number) {

    this.loading = true;

    this.http.post(
      `${environment.apiUrl}/invoke/get-invoke?scanImageId=${scanImageId}`,
      {}
    ).subscribe({
      next: (res: any) => {
        this.data = res;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }

  ChangeIsDone(id: number) {

    // this.loading = true;

    this.http.patch(
      `${environment.apiUrl}/excercise/update-isDone?outputNutritionExcerciseId=${id}`,
      {}
    ).subscribe({
      next: (res: any) => {
        // this.loading = false;
      },
      error: (err) => {
        console.error(err);
        // this.loading = false;
      }
    });
  }

  goBack() {
    this.router.navigate(['/invoke-list']);
  }
}