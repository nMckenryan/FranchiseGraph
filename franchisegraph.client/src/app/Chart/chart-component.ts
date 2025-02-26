import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import { AgCharts } from "ag-charts-angular";
import { AgChartThemeName, AgLineSeriesOptions } from "ag-charts-types";
import { Movie } from "../app.component";

@Component({
  selector: "chart-component",
  standalone: true,
  imports: [AgCharts],
  template: `<ag-charts [options]="options"></ag-charts>
`,
})

export class ChartComponent implements OnChanges {
  public options;

  @Input() movies: Movie[] = [];

  constructor() {
    this.options = {
      data: this.movies,
      theme: "ag-vivid-dark" as AgChartThemeName,
      series: [
        {
          type: "line",
          xKey: "title",
          xName: "Title",
          yKey: "vote_average",
          yName: "Rating",
          interpolation: { type: "smooth" },
        } as AgLineSeriesOptions,
      ],
    };
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["movies"]) {
      this.updateChartOptions();
    }
  }

  updateChartOptions() {
    this.options = {
      ...this.options,
      data: this.movies,
    };
  }
}
