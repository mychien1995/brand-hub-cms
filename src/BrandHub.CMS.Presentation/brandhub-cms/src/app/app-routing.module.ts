import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/authentication/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { Guard } from './interceptors/guard.canactivate';
import { MainlayoutComponent } from './components/layout/mainlayout.component';
import { OrganizationListingComponent } from './components/organizations/organization-listing.component';
import { OrganizationEditComponent } from './components/organizations/organization-edit.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: MainlayoutComponent,
    canActivate: [Guard],
    children: [
      { path: '', component: DashboardComponent },
      { path: 'organizations', component: OrganizationListingComponent },
      { path :'organizations/:id', component : OrganizationEditComponent }
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
