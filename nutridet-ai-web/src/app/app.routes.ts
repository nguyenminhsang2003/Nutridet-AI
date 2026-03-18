import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/login.component';
import { ScanComponent } from './features/scan/scan.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'scan', component: ScanComponent }
];
