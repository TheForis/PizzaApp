import { Component } from '@angular/core';
import { PizzaService } from '../../services/pizza.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
constructor(
  private pizzaService:PizzaService,
  private authService:AuthService
){}

  ngOnInit():void{
    // this.pizzaService.getOrderForUser(true).subscribe(result =>{
    //   console.log(result);
    // })

    this.authService.login({
      username: "boris123",
      password: "Boris123!"
    }).subscribe(result=>{
      console.log(result);
      
    })
  }
}
