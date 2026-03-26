import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
    selector: 'app-invoke-list',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './invoke-list.component.html',
    styleUrls: ['./invoke-list.component.css']
})
export class InvokeListComponent implements OnInit {

    invokes: any[] = [];
    loading = false;
    total: any;

    filter = {
        startDate: '',
        endDate: '',
        page: 1,
        pageSize: 10
    };

    constructor(
        private http: HttpClient,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.loadData();
    }

    loadData() {
        this.loading = true;

        let params = new HttpParams()
            .set('page', this.filter.page)
            .set('pageSize', this.filter.pageSize);

        if (this.filter.startDate) {
            params = params.set('startDate', this.filter.startDate);
        }

        if (this.filter.endDate) {
            params = params.set('endDate', this.filter.endDate);
        }
        this.http.post(
            `${environment.apiUrl}/invoke/get-all-invoke`,
            {},
            { params }
        ).subscribe({
            next: (res: any) => {
                this.invokes = res.listInvoke;
                this.total = res.total;
                this.loading = false;
            },
            error: () => {
                this.invokes = [];
                this.total = 0;
                this.loading = false;
            }
        });
    }

    goDetail(id: number) {
        this.router.navigate(['/invoke', id]);
    }

    search() {
        this.filter.page = 1;
        this.loadData();
    }

    nextPage() {
        if (this.total == this.filter.pageSize) {
            this.filter.page++;
            this.loadData();
        }
    }

    prevPage() {
        if (this.filter.page > 1) {
            this.filter.page--;
            this.loadData();
        }
    }

}