import { Component } from '@angular/core';
import {LeaderboardEntry} from '../../../models/LeaderboardEntry';
import {LeaderboardService} from '../../services/leaderboard/leaderboard.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent {
  leaderboardEntries$: Observable<LeaderboardEntry[]>;
  isLoading$: Observable<boolean>;

  constructor(leaderboardService: LeaderboardService) {
    this.leaderboardEntries$ = leaderboardService.entries$;
    this.isLoading$ = leaderboardService.isLoading$;
  }
}
