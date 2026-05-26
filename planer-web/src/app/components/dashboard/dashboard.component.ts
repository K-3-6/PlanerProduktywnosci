import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskFormComponent } from '../task-form/task-form.component';
import { TaskListComponent } from '../task-list/task-list.component';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, TaskFormComponent, TaskListComponent],
  template: `
    <nav class="navbar">
      <h2>📌 Planer Produktywności</h2>
      <button (click)="logout()">Wyloguj się</button>
    </nav>
    <div class="dashboard-content">
      <app-task-form></app-task-form>
      <app-task-list></app-task-list>
    </div>
  `,
  styles: [`
    .navbar { display: flex; justify-content: space-between; align-items: center; padding: 1rem 2rem; background: #2c3e50; color: white; }
    .navbar button { background: #e74c3c; border: none; padding: 0.5rem 1rem; color: white; cursor: pointer; border-radius: 4px; }
    .dashboard-content { display: grid; grid-template-columns: 1fr 2fr; gap: 2rem; padding: 2rem; }
    @media (max-width: 768px) { .dashboard-content { grid-template-columns: 1fr; } }
  `]
})
export class DashboardComponent {
  constructor(private authService: AuthService) { }
  logout() { this.authService.logout(); }
}
