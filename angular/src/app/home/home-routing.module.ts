import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { LandingComponent } from './landing.component';
import { CvAnalyseComponent } from './cv-analyse.component';

const routes: Routes = [
  { path: '', component: LandingComponent },
  { path: 'dashboard', component: HomeComponent },
  { path: 'cv-analyse', component: CvAnalyseComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
