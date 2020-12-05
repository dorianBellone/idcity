import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ListeComponent } from './liste/liste.component';
import { LoginComponent } from './login/login.component';
import { MaterialModule } from './material/material.module';
import { NavComponent } from './nav/nav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthGuard } from './services/auth.guard';
import { NgxFileSaverModule } from '@clemox/ngx-file-saver';
import { ClasseComponent } from './classe/classe.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ListeComponent,
    LoginComponent,
    NavComponent,
    ClasseComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxFileSaverModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'login' },
      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent /*,canActivate: [AuthGuard] */},
      { path: 'liste', component: ListeComponent /*,canActivate: [AuthGuard]*/ },
      { path: 'classe/:classe', component: ClasseComponent }
    ]),
    MaterialModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
