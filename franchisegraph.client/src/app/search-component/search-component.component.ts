import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent, MatOption } from '@angular/material/autocomplete';
import { Observable, startWith, map, debounceTime, of, switchMap } from 'rxjs';
import { Franchise } from '../app.component';

@Component({
  selector: 'search-component',
  standalone: false,
  templateUrl: './search-component.component.html',
})
export class SearchComponentComponent {
  public franchise: Franchise[] = [];
  public searchTerm: string = '';

  @Output() franchiseSelected = new EventEmitter<Franchise>();

  sendFranchiseToParent(franchise: Franchise) {
    this.franchiseSelected.emit(franchise);
  }

  myControl = new FormControl('');
  options: Franchise[] = this.franchise.map((franchise) => franchise);
  searchResults!: Observable<Franchise[]>;

  ngOnInit() {
    this.searchResults = this.myControl.valueChanges.pipe(
      debounceTime(300),
      switchMap(value => {
        if (value === null) {
          return of([]);
        }
        return this.getFranchises({ franchise: value });
      })
    );
  }

  constructor(private http: HttpClient) {
    this.getFranchises({ franchise: this.searchTerm });
  }

  getFranchises({ franchise }: { franchise: string; }): Observable<Franchise[]> {
    return this.http.get<any[]>(`/TMDBRequest/getTMDBCollectionData?collectionSearch=${franchise}`).pipe(
      map(result => {
        return result;
      })
    );
  }

  onFranchiseSelected(option: MatOption) {
    this.sendFranchiseToParent(option.value as Franchise);
  }
}
