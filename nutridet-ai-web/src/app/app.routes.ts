import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/login.component';
import { ScanComponent } from './features/scan/scan.component';
import { InvokeComponent } from './features/invoke/invoke.component';
import { MainLayout } from './features/layouts/main-layout/main-layout.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: MainLayout,
    children:[
      { path: 'scan', component: ScanComponent },
      { path: 'invoke/:id', component: InvokeComponent },
    ]
  }
];
