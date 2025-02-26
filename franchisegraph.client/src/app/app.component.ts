import { HttpClient } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { map } from 'rxjs';

export interface Movie {
  Title: string;
  Year: string;
  Poster: string;
  Metascore: string;
  ImdbRating: number;
}

export interface Franchise {
  backdropPath: string;
  id: number;
  name: string;
  posterPath: string;
  type: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false
})

export class AppComponent {
  movies = signal(<Movie[]>[]);
  franchise = signal(<Franchise | null>null);

  franchiseSelected(selectedFranchise: Franchise) {
    this.franchise.set(selectedFranchise);
    this.getMovies(selectedFranchise.id).subscribe(movies => {
      this.movies.set(movies as Movie[]);
    });
  }

  constructor(private http: HttpClient) {
  }

  getMovies(franchiseId: number) {
    return this.http.get<any[]>(`/TMDBRequest/getMoviesFromCollection?collectionId=${franchiseId}`).pipe(
      map(result => {
        return result;
      })
    );
  }

  title = 'franchisegraph.client';
}
