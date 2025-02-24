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

export class AppComponent {
  public movies: Movie[] = [];

  title = 'franchisegraph.client';
}
