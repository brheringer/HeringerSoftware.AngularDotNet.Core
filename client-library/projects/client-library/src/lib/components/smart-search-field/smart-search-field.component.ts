
import {debounceTime, mergeMap, distinctUntilChanged, catchError, merge, switchMap, tap} from 'rxjs/operators';
import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { Observable, of } from 'rxjs';
import { EmptyObservable } from 'rxjs/Observable/EmptyObservable';
import 'rxjs/Rx';

import { GenericService } from '../../services/generic.service';
import { EntityReference } from '../../model/entity-reference';
import { EntitiesReferences } from '../../model/entities-references';

@Component({
  selector: 'metalsoft-smart-search-field',
  templateUrl: './smart-search-field.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './smart-search-field.component.css']
})
export class SmartSearchFieldComponent {
  @Input() model: EntityReference;
  @Output() modelChange = new EventEmitter<EntityReference>();
  @Input() label: string;
  @Input() tip: string;
  @Input() targetService: string;
  @Input() fnTargetService: any;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;
  @Input() ignitionMilisseconds: number = 300;
  @Input() ignitionMinChars: number = 2;

  public searching = false;
  private searchFailed = false;
  private hideSearchingWhenUnsubscribed = new Observable(() => () => this.searching = false);

  constructor(private service: GenericService) { }

  format(dto: EntityReference): string {
    return dto == null
      ? ''
      : dto.presentation;
  }

  changeModel(event: any): void {
    if (!event)
      event = null;

    if (event === null || event.id) { //verifica se é um objeto válido (pq antes de selecionar é apenas uma string)
      this.model = event;
      this.modelChange.emit(this.model);
    }
  }

  searchAsYouType = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(this.ignitionMilisseconds),
      distinctUntilChanged(),
      tap(() => this.searching = true),
      switchMap((term:string) => {
        if (!term || term.length < this.ignitionMinChars)
          return new EmptyObservable<EntityReference[]>();
        else
          return this.realSearch(term).pipe(
            mergeMap(dto => of(dto.references)),
            tap(() => {
              this.searchFailed = false
            }),
            catchError(() => {
              this.searchFailed = true;
              return of([]);
            }),)
      }),
      tap(() => this.searching = false),
      merge(this.hideSearchingWhenUnsubscribed),);

  realSearch(term: string): Observable<EntitiesReferences> {
    return this.fnTargetService == null
      ? this.service.smartSearch(this.targetService, term)
      : this.fnTargetService(this.targetService, term);
  }
}

//ref: //https://ng-bootstrap.github.io/#/components/typeahead/examples
//considerações:
//model:EntityReference e targetService:string deixam o componente bem acoplado ao restante do design (o que é ruim)
//tavez uma solução seria não usar o tipo EntityReference e tentar abstrair o uso da propriedade Presentation
//outra parte da solução seria criar um @Output para a pesquisa ser feita de fora
