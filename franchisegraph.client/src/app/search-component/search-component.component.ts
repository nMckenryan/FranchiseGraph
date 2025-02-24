import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, startWith, map, debounceTime, of, switchMap } from 'rxjs';

interface Franchise {
  backdropPath: string;
  id: number;
  name: string;
  posterPath: string;
  type: string;
}

@Component({
  selector: 'search-component',
  standalone: false,
  templateUrl: './search-component.component.html',
})
export class SearchComponentComponent {
  public franchise: Franchise[] = [];
  public searchTerm: string = '';

  myControl = new FormControl('');
  options: Franchise[] = this.franchise.map((franchise) => franchise);
  filteredOptions!: Observable<Franchise[]>;

  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges.pipe(
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
    return this.http.get<any[]>(`/OMDB/getOMDBData?collectionSearch=${franchise}`).pipe(
      map(result => {
        return result;
      })
    );
  }
}
