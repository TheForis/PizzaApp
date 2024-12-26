import { Component, computed } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { PizzaService } from '../../services/pizza.service';
import { CommonModule } from '@angular/common';import { PizzaCardsComponent } from '../pizza-cards/pizza-cards.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    PizzaCardsComponent,
    MatCardModule,
    MatButtonModule,
    RouterLink

  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  
    isLoggedIn = computed(()=> this.authService.isLoggedIn());
    constructor(
      private authService: AuthService,
      private pizzaService: PizzaService){}
}
