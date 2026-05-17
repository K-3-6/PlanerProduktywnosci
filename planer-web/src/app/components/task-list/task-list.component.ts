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
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  filteredTasks: Task[] = [];

  // Zmienne powiązane z filtrami w HTML za pomocą [(ngModel)]
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

  deleteTask(id: number): void {
    if (confirm('Czy na pewno chcesz usunąć to zadanie?')) {
      this.taskService.deleteTask(id);
    }
  }
}
