import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home.component';
import { LandingComponent } from './landing.component';

@NgModule({
  declarations: [HomeComponent, LandingComponent],
  imports: [CommonModule, RouterModule, FormsModule, SharedModule, HomeRoutingModule],
})
export class HomeModule {}
