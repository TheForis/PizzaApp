import { Injectable, signal } from '@angular/core';
import { PizzaService } from './pizza.service';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { catchError, Observable, tap } from 'rxjs';
import { Login, LoginResponse, Register } from '../types/interfaces/auth.interface';
import { apiUrl, snackBarConfig } from '../constants/app.constants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  isLoggedIn = signal<boolean>(false);
  constructor(
    private pizzaService: PizzaService,
    private http: HttpClient,
    private snackBar: MatSnackBar,
    private router: Router
  ) { this.isLoggedIn.set(!!localStorage.getItem('token')) }

register(registerCredentials:Register) : Observable<any>{
    return this.http.post(`${apiUrl}/User/Register`,registerCredentials)
    .pipe(
      tap(()=>{
        this.snackBar.open('You have successfully registered','Close',snackBarConfig)
        this.router.navigate(['/login'])
        }),
      catchError((error, caught)=>{
        this.snackBar.open(error?.error?.errors?.[0]||`Error while loggin in!`,'Close',snackBarConfig)
        return caught;
      })
    )
  }
login(loginCredentials: Login):Observable<any> {
  return this.http.post<LoginResponse>(`${apiUrl}/User/Login`,loginCredentials)
  .pipe(
    tap((response:LoginResponse)=>{
      this.setToken(response.result.token, response.result.validTo);
      if(!this.isTokenValid()){
        throw new Error('Error while loggin in');
      }
      this.isLoggedIn.set(true);
      this.snackBar.open('You have successfully Logged In','Close',snackBarConfig)
      this.router.navigate(['/'])
      catchError((error, caught)=>{
         this.snackBar.open(error?.error?.errors?.[0]||`Error while loggin in!`,'Close',snackBarConfig)
        return caught;
        })
    })
  )
}
logout() {
  this.isLoggedIn.set(false);
  this.pizzaService.updateActiveOrder([]);
  this.pizzaService.currentUser.set(null);
  localStorage.removeItem('token');
  localStorage.removeItem('tokenExpirationDate');
  this.router.navigate(['/Login']);
}
 getUser (){
  return this.http.get(`${apiUrl}/User`).subscribe(result=>{
    console.log(result);
    
  })
 }

  private setToken(token:string,tokenExpirationDate:string):void {
    localStorage.setItem("token",token);
    localStorage.setItem('tokenExpirationDate',tokenExpirationDate);
  }
  private isTokenValid():boolean{
    const tokenExpirationDate: string | null = localStorage.getItem('tokenExpirationDate');
    if (!tokenExpirationDate){
      return false;
    }
    return new Date(tokenExpirationDate) > new Date();
  }

}
