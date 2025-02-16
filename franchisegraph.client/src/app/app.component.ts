import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LineChartComponent } from './line-chart/line-chart.component';
interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

export interface OMDBResponse {
  Title: string;
  Year: string;
  Poster: string;
  Metascore: string;
  ImdbRating: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  public movies: OMDBResponse[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getForecasts();
    this.getMovies();
  }

  getMovies() {
    this.http.get<OMDBResponse[]>('/getOMDBData').subscribe(
      (result) => {
        this.movies = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'franchisegraph.client';
}
