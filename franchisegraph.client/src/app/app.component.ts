import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, startWith, map } from 'rxjs';

interface Movie {
  Title: string;
  Year: string;
  Poster: string;
  Metascore: string;
  ImdbRating: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false
})


export class AppComponent implements OnInit {
  public movies: any[] = [];
  public searchTerm: string = 'captain america';
  myControl = new FormControl('');
  options: string[] = ['One', 'Two', 'Three'];
  filteredOptions!: Observable<string[]>;

  constructor(private http: HttpClient) {
    this.getMovies(this.searchTerm);
  }

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

  getMovies(searchTerm: string) {
    this.http.get<any[]>(`/OMDB/getOMDBData?franchiseName=${searchTerm}`).subscribe(
      (result) => {
        this.movies = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'franchisegraph.client';
}
