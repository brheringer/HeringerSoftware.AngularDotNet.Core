
import {of as observableOf,  Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { EntityReference } from 'projects/client-library/src/lib/model/entity-reference';
import { EntitiesReferences } from 'projects/client-library/src/lib/model/entities-references';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public model: SandboxModel = new SandboxModel();
  public states: string[] = ['MG', 'RJ', 'SP'];
  public entities: EntityReference[] = [
    {id: 1, presentation: 'entity one', version: 1, response: null}, 
    {id: 2, presentation: 'entity two', version: 1, response: null}
  ];

  ngOnInit() {
  }

  changed(newValue: string): void {
    console.log(newValue);
  }

  mockedService(url:string, term:string): Observable<EntitiesReferences>
  {
    let x : EntitiesReferences = new EntitiesReferences();
    x.references.push({id: 1, presentation: 'entity one', version: 1, response: null});
    x.references.push({id: 2, presentation: 'entity two', version: 1, response: null});
    x.references.push({id: 3, presentation: 'entity three', version: 1, response: null});
    return observableOf(x)
  }
}

export class SandboxModel {
  booleanProperty: boolean;
  dateProperty: Date;
  entityProperty: EntityReference;
  anotherEntityProperty: EntityReference;
  numberProperty: number;
  textProperty: string;
  anotherTextProperty: string;
  state: string;
  globalDisabled: boolean = false;
}