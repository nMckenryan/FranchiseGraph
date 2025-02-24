import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, startWith, map } from 'rxjs';

interface Franchise {
  BackdropPath: string;
  Id: number;
  Name: string;
  PosterPath: string;
  Type: string;
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
  options: string[] = this.franchise.map((franchise) => franchise.Name);
  filteredOptions!: Observable<string[]>;

  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }
  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
  constructor(private http: HttpClient) {
    this.getFranchises({ franchise: this.searchTerm });
  }

  getFranchises({ franchise }: { franchise: string; }) {
    this.http.get<any[]>(`/OMDB/getTMDBCollectionHead?collectionSearch=${franchise}`).subscribe(
      (result) => {
        this.franchise = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
