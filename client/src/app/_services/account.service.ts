import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentuserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentuserSource.asObservable();
  constructor(private http: HttpClient) { }
  

  login(model: User)
  {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response as User;
        if (user) {
          this.currentuserSource.next(user);
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
          
        }
        return user;
      })
    );
  }

  // login(model: any): Observable<void> 
  // {     
  //   return this.http.post(this.baseUrl + 'account/login', model).pipe(
  //            map((response) => { 
  //                     const user = response as User;
  //                              if (user) {           
  //                               localStorage.setItem('user', JSON.stringify(user));           
  //                               this.currentUserSource.next(user);         
  //                             }      
  //                            })     
  //                            );   }   setCurrentUser(user: User): void{     this.currentUserSource.next(user);   }   logout(): void{     localStorage.removeItem('user');     this.currentUserSource.next(undefined);   }



  
  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentuserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecoedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentuserSource.next(user);
  }



  logout() {
    localStorage.removeItem('user');
    this.currentuserSource.next(null);
  }

  getDecoedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
