import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms'; 
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
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
      const formValue = { ...this.taskForm.value };

      if (formValue.dueDate) {
        formValue.dueDate = new Date(formValue.dueDate).toISOString();
      }

      console.log('Wysyłam dane do API:', formValue); 

      this.taskService.addTask(formValue).subscribe({
        next: (savedTask) => {
          console.log('Sukces! Zadanie dodane do centralnej bazy:', savedTask);
          this.taskForm.reset({ category: 'Praca', priority: 'Średni', status: 'Nowe' });

          window.location.reload();
        },
        error: (err) => {
          console.error('Błąd zapisu do API:', err);
          alert(`Nie udało się dodać zadania. Status: ${err.status} - ${err.statusText}`);
        }
      });
    }
  }
}
