import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'normalizeEnum',
  standalone: true
})
export class NormalizeEnumPipe implements PipeTransform {

  transform(value: string | string[]): string {
    const result = [];
    for (let i = 0; i < value.length; i++) {
      const element = value[i];
      let capitalizedElement = this.capitalizeIngredient(element);
      result.push(capitalizedElement);
    }
    return result.join(', ');
  }
  capitalizeIngredient(value:string): string{
    const firstLetter = value.charAt(0).toUpperCase();
    const lowercaseValue = value.toLowerCase().slice(1);
    return value;
  }

}
