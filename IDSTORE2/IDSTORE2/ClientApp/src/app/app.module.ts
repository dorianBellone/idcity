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
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from './classe/dialog/dialog.component';
import { DialogUpdateCoursComponent } from './classe/dialog-update-cours/dialog-update-cours.component';
import { FileUploadService } from './services/file-upload-service';
import { DialogCoursComponent } from './classe/dialog-cours/dialog-cours.component';
import { AdminPanelComponent } from './admin/admin-panel.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ListeComponent,
    LoginComponent,
    NavComponent,
    ClasseComponent,
    DialogComponent,
    DialogUpdateCoursComponent,
    DialogCoursComponent,
    AdminPanelComponent,
  ],
  entryComponents: [DialogComponent, DialogUpdateCoursComponent, DialogCoursComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxFileSaverModule,
    Ng2SearchPipeModule,
    BrowserModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'login' },
      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent /*,canActivate: [AuthGuard] */},
      { path: 'liste', component: ClasseComponent /*,canActivate: [AuthGuard]*/ },
      { path: 'classe/:classe', component: ClasseComponent },
      { path: 'admin-panel', component: AdminPanelComponent }
    //  { path: 'classe/:classe/:matiere', ListeComponent }
    ]),
    MaterialModule,
    BrowserAnimationsModule,
    MatDialogModule

  ],
  providers: [FileUploadService],
  bootstrap: [AppComponent]
})
export class AppModule { }
