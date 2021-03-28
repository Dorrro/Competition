import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import {of} from 'rxjs';

@Component({
  selector: 'app-solve',
  templateUrl: './solve.component.html',
  styleUrls: ['./solve.component.scss']
})
export class SolveComponent implements OnInit {
  solutionForm: FormGroup;

  constructor(private httpClient: HttpClient) {
    this.solutionForm = new FormGroup({
      name: new FormControl('', Validators.required),
      task: new FormControl(null, Validators.required),
      solution: new FormControl('', Validators.required)
    });
  }

  get nameControl(): AbstractControl | null {
    return this.solutionForm.get('name');
  }

  get taskControl(): AbstractControl | null {
    return this.solutionForm.get('task');
  }

  get solutionControl(): AbstractControl | null {
    return this.solutionForm.get('solution');
  }

  ngOnInit(): void {
  }

  onFormSubmit(): void {
    this.solutionForm.markAsDirty();
    if (!this.solutionControl?.valid) {
      return;
    }

    const compileOptions = {
      LanguageChoice: 1,
      Program: this.solutionControl.value,
      Input: '1, 2',
      CompilerArgs: ''
    };

    this.httpClient.post('compiler', compileOptions).pipe(
      catchError(err => {
        return of(null);
      })
    ).subscribe(result => {
      alert(JSON.stringify(result));
    });
  }
}
