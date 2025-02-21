import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, startWith, map } from 'rxjs';

interface Franchise {
  BackdropPath: string;
  Id: number;
  Name: string;
  PosterPath: string;
  Type: string;
}

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
  public movies: Movie[] = [];
  public franchise: Franchise[] = [];
  public searchTerm: string = '';

  myControl = new FormControl('');
  options: string[] = this.movies.map((movie) => movie.Title);
  filteredOptions!: Observable<string[]>;

  constructor(private http: HttpClient) {
    this.getMovies({ searchTerm: this.searchTerm });
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

  getFranchises({ franchise }: { franchise: string; }) {
    this.http.get<any[]>(`/OMDB/getTMDBCollectionHead?collectionSearch=${franchise}`).subscribe(
      (result) => {
        this.movies = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getMovies({ searchTerm }: { searchTerm: string; }) {
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
