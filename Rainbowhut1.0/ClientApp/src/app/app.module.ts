import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent, SafePipe } from './app.component';
import { MatIconModule } from '@angular/material/icon';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { RouterModule, Routes } from '@angular/router';



import { HttpClientModule } from '@angular/common/http';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '', component: AppComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    SafePipe
   
  ],
  imports: [
    BrowserModule,
    MatIconModule,
    NgbModule,
    BrowserAnimationsModule,
    CommonModule,
    FlexLayoutModule,
    CarouselModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    [RouterModule.forRoot(routes)]


  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
