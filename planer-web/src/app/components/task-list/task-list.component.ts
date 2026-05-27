import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms'; 
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  filteredTasks: Task[] = [];

  filterStatus: string = 'Wszystkie';
  filterCategory: string = 'Wszystkie';

  constructor(private taskService: TaskService) { }

  ngOnInit(): void {
    this.taskService.getTasks().subscribe(data => {
      this.tasks = data;
      this.applyFilters();
    });
  }

  applyFilters(): void {
    this.filteredTasks = this.tasks.filter(task => {
      const matchStatus = this.filterStatus === 'Wszystkie' || task.status === this.filterStatus;
      const matchCategory = this.filterCategory === 'Wszystkie' || task.category === this.filterCategory;
      return matchStatus && matchCategory;
    });
  }

  updateStatus(task: Task, event: any): void {
    task.status = event.target.value;
    this.taskService.updateTask(task);
  }

  deleteTask(task: any): void {
    console.log('Pełny obiekt zadania odebrany z HTML:', task);

    const taskId = task?.id !== undefined ? task.id : task?.Id;

    if (taskId === undefined || taskId === null) {
      console.error('Błąd: Nie udało się odnaleźć pola id ani Id w obiekcie!', task);
      alert('Nie można usunąć zadania: brak identyfikatora.');
      return;
    }

    console.log('Wysyłam żądanie DELETE do API dla ID:', taskId);

    this.taskService.deleteTask(taskId).subscribe({
      next: () => {
        console.log('Sukces! Zadanie usunięte z bazy danych.');
        window.location.reload();
      },
      error: (err) => {
        console.error('Błąd serwera podczas usuwania:', err);
        alert(`Serwer odrzucił żądanie. Status: ${err.status}`);
      }
    });
  }
}
