import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';  
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/authentication/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS }    from '@angular/common/http';
import { LocalStorageModule } from 'angular-2-local-storage';
import { ConfigService, ConfigServiceFactory, ConfigServiceProvider } from './services/shared/config.service';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import { MainlayoutComponent } from './components/layout/mainlayout.component';
import { SidebarComponent } from './components/layout/partial/sidebar.component';
import { TopnavComponent } from './components/layout/partial/topnav.component';
import { OrganizationListingComponent } from './components/organizations/organization-listing.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    MainlayoutComponent,
    SidebarComponent,
    TopnavComponent,
    OrganizationListingComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    LocalStorageModule.forRoot({
        prefix: 'brand-hub',
        storageType: 'localStorage'
    })
  ],
  providers: [
    ConfigService,
    ConfigServiceProvider,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
