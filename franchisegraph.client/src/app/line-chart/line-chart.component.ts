import { Component, Input } from '@angular/core';
import Chart from 'chart.js/auto';
import { OMDBResponse } from '../app.component';

@Component({
  selector: 'line-chart',
  standalone: false,
  templateUrl: './line-chart.component.html',
  styles: []
})

export class LineChartComponent {
  public chart: any;

  @Input() movieDataSample: OMDBResponse[] = [
    {
      Title: "Inception",
      Year: "2010",
      Poster: "https://example.com/inception.jpg",
      Metascore: "8.8/10",
      ImdbRating: "8.8"
    },
    {
      Title: "Inception 3",
      Year: "2011",
      Poster: "https://example.com/inception3.jpg",
      Metascore: "9.0/10",
      ImdbRating: "9.1"
    },
    {
      Title: "Inception 4",
      Year: "2012",
      Poster: "https://example.com/inception4.jpg",
      Metascore: "9.1/10",
      ImdbRating: "9.2"
    },
    {
      Title: "Inception 5",
      Year: "2013",
      Poster: "https://example.com/inception5.jpg",
      Metascore: "9.2/10",
      ImdbRating: "9.3"
    },
    {
      Title: "Inception 6",
      Year: "2014",
      Poster: "https://example.com/inception6.jpg",
      Metascore: "9.3/10",
      ImdbRating: "9.4"
    },
    {
      Title: "Inception 7",
      Year: "2015",
      Poster: "https://example.com/inception7.jpg",
      Metascore: "9.4/10",
      ImdbRating: "9.5"
    }
  ];


  ngOnInit(): void {
    this.createChart();
  }

  createChart() {
    const movieTitles: string[] = this.movieDataSample.map(movie => movie.Title);
    const movieRatings: number[] = this.movieDataSample.map(movie => Number(movie.ImdbRating));

    this.chart = new Chart("MyChart", {
      type: 'line', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: movieTitles,
        datasets: [
          {
            label: "Reviews",
            data: movieRatings,
            backgroundColor: 'blue'
          },
        ]
      },
      options: {
        aspectRatio: 2.5
      }

    });
  }
}
