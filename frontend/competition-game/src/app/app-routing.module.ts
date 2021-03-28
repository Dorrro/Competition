import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SolveComponent} from './solve/solve.component';
import {LeaderboardComponent} from './leaderboard/leaderboard.component';

const routes: Routes = [
  {
    component: SolveComponent,
    path: 'solve'
  },
  {
    component: LeaderboardComponent,
    path: 'leaderboard'
  },
  {
    component: SolveComponent,
    path: '**'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
