import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list';
import { RegisterComponent } from './components/register/register.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { ApiurlInterceptor } from './interceptors/apiurl.interceptor';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NoopAnimationsModule,
    MatStepperModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule,
    MatListModule
  ],
  providers: [
    { 
      provide: "API_ENDPOINT", 
      useValue: environment.apiUrl 
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ApiurlInterceptor,
        multi: true
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ErrorInterceptor,
        multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
