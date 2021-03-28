import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {catchError, map} from 'rxjs/operators';
import {of} from 'rxjs';
import {SolutionRequest} from '../../../models/SolutionRequest';
import {Task} from '../../../models/Task';

const cSharpId = 1;

@Component({
  selector: 'app-solve',
  templateUrl: './solve.component.html',
  styleUrls: ['./solve.component.scss']
})
export class SolveComponent implements OnInit {
  solutionForm: FormGroup;
  tasks: Task[] | undefined = undefined;
  selectedTask: Task | undefined = undefined;
  isLoading = true;

  constructor(private httpClient: HttpClient) {
    this.solutionForm = new FormGroup({
      name: new FormControl('', Validators.required),
      task: new FormControl(null, Validators.required),
      solution: new FormControl('', Validators.required)
    });

    this.taskControl?.valueChanges.subscribe((taskId: number) => {
      taskId = Number(taskId);
      this.selectedTask = this.tasks?.find(t => t.id === taskId);
      const sampleCode = this.selectedTask?.sampleCodes.find(c => c.codingLanguage.id === cSharpId)?.code;
      this.solutionControl?.setValue(sampleCode);
    });

    this.httpClient.get<Task[]>('api/tasks').subscribe(t => {
      this.tasks = t;
      this.isLoading = false;
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
    this.nameControl?.markAsDirty();
    this.taskControl?.markAsDirty();
    this.solutionControl?.markAsDirty();

    if (!this.solutionForm.valid) {
      return;
    }

    this.isLoading = true;

    const solutionRequest: SolutionRequest = {
      codingLanguageId: cSharpId,
      code: this.solutionControl?.value,
      taskId: this.selectedTask?.id || 0,
      user: this.nameControl?.value
    };

    this.httpClient.post('api/solution', solutionRequest).pipe(
      map(_ => {
        return { isSuccessful: true };
      }),
      catchError(err => {
        return of({ isSuccessful: false, error: err});
      })
    ).subscribe(result => {
      alert(JSON.stringify(result));
      this.isLoading = false;
    });
  }
}
