import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

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
  standalone: false,
})


export class AppComponent implements OnInit {
  public movies: any[] = [];
  public searchTerm: string = 'captain america';

  constructor(private http: HttpClient) {
    this.getMovies(this.searchTerm);
  }

  ngOnInit(): void { }

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
