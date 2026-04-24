import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home.component';
import { LandingComponent } from './landing.component';
import { CvAnalyseComponent } from './cv-analyse.component';

@NgModule({
  declarations: [HomeComponent, LandingComponent, CvAnalyseComponent],
  imports: [CommonModule, RouterModule, FormsModule, SharedModule, HomeRoutingModule],
})
export class HomeModule {}
