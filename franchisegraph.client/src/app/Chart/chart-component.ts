import { Component, Input } from "@angular/core";
import { AgCharts } from "ag-charts-angular";
import { AgChartThemeName, AgLineSeriesOptions } from "ag-charts-types";

export interface Movie {
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

  @Input() movies: Movie[] = [];

  constructor() {
    this.options = {
      // Data: Data to be displayed in the chart
      data: this.movies,
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
