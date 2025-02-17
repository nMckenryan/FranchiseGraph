import { Component, Input } from "@angular/core";
import { AgCharts } from "ag-charts-angular";
import { AgChartThemeName, AgLineSeriesOptions } from "ag-charts-types";


export interface OMDBResponse {
  Title: string;
  Year: string;
  Poster: string;
  Metascore: string;
  ImdbRating: number;
}


@Component({
  selector: "chart-component",
  standalone: true,
  imports: [AgCharts],
  template: `<ag-charts [options]="options"></ag-charts>
`,
})


export class ChartComponent {
  public options;


  @Input() movieDataSample: OMDBResponse[] = [
    {
      Title: "Inception",
      Year: "2010",
      Poster: "https://example.com/inception.jpg",
      Metascore: "8.8/10",
      ImdbRating: 8.8
    },
    {
      Title: "Inception 3",
      Year: "2011",
      Poster: "https://example.com/inception3.jpg",
      Metascore: "9.0/10",
      ImdbRating: 9.1
    },
    {
      Title: "Inception 4",
      Year: "2012",
      Poster: "https://example.com/inception4.jpg",
      Metascore: "9.1/10",
      ImdbRating: 9.2
    },
    {
      Title: "Inception 5",
      Year: "2013",
      Poster: "https://example.com/inception5.jpg",
      Metascore: "9.2/10",
      ImdbRating: 9.3
    },
    {
      Title: "Inception 6",
      Year: "2014",
      Poster: "https://example.com/inception6.jpg",
      Metascore: "9.3/10",
      ImdbRating: 9.4
    },
    {
      Title: "Inception 7",
      Year: "2015",
      Poster: "https://example.com/inception7.jpg",
      Metascore: "9.4/10",
      ImdbRating: 9.5
    }
  ];

  constructor() {
    this.options = {
      // Data: Data to be displayed in the chart
      data: this.movieDataSample,
      theme: "ag-vivid-dark" as AgChartThemeName,
      // Series: Defines which chart type and data to use
      series: [
        {
          type: "line",
          xKey: "Title",
          xName: "Title",
          yKey: "ImdbRating",
          yName: "IMDB Rating",
          interpolation: { type: "smooth" },
        } as AgLineSeriesOptions,
      ],
    };
  }
}
