import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LeaderboardEntry} from '../../../models/LeaderboardEntry';
import {BehaviorSubject, Observable} from 'rxjs';
import {retry, tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {
  entries$: Observable<LeaderboardEntry[]>;
  get isLoading$(): Observable<boolean> {
    return this.isLoadingField$.asObservable();
  }

  private isLoadingField$ = new BehaviorSubject(true);

  constructor(httpClient: HttpClient) {
    this.entries$ = httpClient.get<LeaderboardEntry[]>('/api/leaderboard')
      .pipe(
        retry(3),
        tap(_ => {
          this.isLoadingField$.next(false);
    }));
  }
}
