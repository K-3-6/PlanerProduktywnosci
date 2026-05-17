import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // <-- Dodane
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms'; // <-- Dodane
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule], // <-- To naprawi formGroup
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent {
  taskForm: FormGroup;

  constructor(private fb: FormBuilder, private taskService: TaskService) {
    this.taskForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', Validators.maxLength(200)],
      category: ['Praca', Validators.required],
      priority: ['Średni', Validators.required],
      dueDate: ['', Validators.required],
      status: ['Nowe', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.taskForm.valid) {
      this.taskService.addTask(this.taskForm.value);
      this.taskForm.reset({ category: 'Praca', priority: 'Średni', status: 'Nowe' });
    }
  }
}
