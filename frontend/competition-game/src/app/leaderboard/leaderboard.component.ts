import { Component, OnInit } from '@angular/core';
import {LeaderboardEntry} from '../../models/LeaderboardEntry';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent implements OnInit {
  leaderboardEntries: LeaderboardEntry[] | undefined;

  constructor(httpClient: HttpClient) {
    httpClient.get<LeaderboardEntry[]>('/api/leaderboard').subscribe(e => {
      this.leaderboardEntries = e;
    });
  }

  ngOnInit(): void {
  }

}
