import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(this.checkLoginStatus());

  constructor(private router: Router) { }

  private checkLoginStatus(): boolean {
    return sessionStorage.getItem('isLoggedIn') === 'true';
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  login(username: string, password: string): boolean {
    // Prosta walidacja "na sztywno" na potrzeby braku backendu
    if (username === 'admin' && password === 'admin123') {
      sessionStorage.setItem('isLoggedIn', 'true');
      this.loggedIn.next(true);
      this.router.navigate(['/dashboard']);
      return true;
    }
    return false;
  }

  logout(): void {
    sessionStorage.removeItem('isLoggedIn');
    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }
}
