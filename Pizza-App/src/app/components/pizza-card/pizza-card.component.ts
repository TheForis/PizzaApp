import { CommonModule } from '@angular/common';
import { Component, computed, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { Pizza } from '../../types/interfaces/pizza.interface';
import { AuthService } from '../../services/auth.service';
import { PizzaService } from '../../services/pizza.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pizza-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatChipsModule,
    MatIconModule,
    MatButtonModule
],
  templateUrl: './pizza-card.component.html',
  styleUrl: './pizza-card.component.scss'
})
export class PizzaCardComponent {
  isLoggedIn = computed(()=> this.authService.isLoggedIn());
  @Input() pizza:Pizza | undefined;

  constructor(
    private authService: AuthService,
  private pizzaService: PizzaService,
    private router: Router
  ){}

  selectPizza(): void{
    this.pizzaService.updateSelectedIngredients(this.pizza?.ingredients ?? [])
    this.router.navigate(['/pizza-maker']);
  }
}
