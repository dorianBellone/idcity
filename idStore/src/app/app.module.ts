import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { MaterialModule } from './material/material.module';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { DeveloppementComponent } from './aspects/developpement/developpement.component';
import { ReflexionComponent } from './aspects/reflexion/reflexion.component';
import { FormsModule } from '@angular/forms';
import { ListeComponent } from './liste/liste.component';
import { LoginComponent } from './login/login.component';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    DeveloppementComponent,
    ReflexionComponent,
    ListeComponent,
    LoginComponent
  ],
  imports: [
    Ng2SearchPipeModule,
    MaterialModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
