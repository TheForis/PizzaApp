import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { PizzaCardComponent } from '../pizza-card/pizza-card.component';
import { Pizza } from '../../types/interfaces/pizza.interface';
import { PizzaService } from '../../services/pizza.service';

@Component({
  selector: 'app-pizza-cards',
  standalone: true,
  imports: [
    CommonModule,
    MatGridListModule,
    PizzaCardComponent,
  ],
  templateUrl: './pizza-cards.component.html',
  styleUrl: './pizza-cards.component.scss'
})
export class PizzaCardsComponent {
  pizzas: Pizza[] = [];
  breakPoint: number = 3;

  constructor(
    private pizzaService:PizzaService
  ){}

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.pizzas = this.pizzaService.defaultPizzas;
  }
  onResize(event:any):void{
    this.breakPoint = Math.floor(event.target.innerWidth / 320);
  }

}
